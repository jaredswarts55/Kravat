using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Kravat.Models;

namespace Kravat.EntityFramework6
{
    public class DbContextReader : IDbContextReader
    {
        private readonly string _path;
        private readonly string _dllName;
        private readonly Assembly _assembly;
        private readonly List<Assembly> _loadedAssemblies = new List<Assembly>();
        private SqlProviderServices _instance;

        public DbContextReader(string path, string dllName)
        {
            _path = path;
            _dllName = dllName;
            foreach (var dll in Directory.GetFiles(path, "*.dll"))
            {
                try
                {
                    var assembly = Assembly.LoadFile(dll);
                    if (Path.GetFileName(dll) == dllName)
                        _assembly = assembly;
                    _loadedAssemblies.Add(assembly);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[Error] Could not read assembly: {0}", ex);
                }
            }
        }

        public List<Model> ReadEntities()
        {
            var dbContextTypes = this.GetDbContexts();
            var entities = new List<Model>();
            foreach (var contextType in dbContextTypes)
            {
                List<Model> contextEntities = this.ReadEntitiesFromContext(contextType);
                entities.AddRange(contextEntities);
            }
            return entities;
        }

        public List<Model> ReadEntitiesFromContext(Type contextType)
        {
            var entities = new List<Model>();
            foreach (var property in contextType.GetProperties())
            {
                if (!property.PropertyType.IsGenericType || typeof(DbSet<>) != property.PropertyType.GetGenericTypeDefinition())
                    continue;
                var entityType = property.PropertyType.GetGenericArguments().First();
                var entity = new Model
                {
                    MetaData = new Dictionary<string, object>
                    {
                        {"ContextTypeNamespace", contextType.Namespace },
                        {"ContextTypeFullName", contextType.FullName},
                        {"ContextTypeName", contextType.Name},
                    },
                    Name = entityType?.Name,
                    NamePlural = property.Name,
                    TypeNamespace = entityType?.Namespace,
                    Type = entityType?.FullName,
                    Properties = entityType?.GetProperties().Select(pi => new ModelProperty
                    {
                        Name = pi.Name,
                        Type = pi.PropertyType.Name,
                        TypeNamespace = pi.PropertyType.Namespace,
                    }).ToArray()
                };
                entities.Add(entity);
            }
            return entities;
        }

        public Type[] GetDbContexts()
        {
            try
            {
                var types = _assembly.GetTypes();
                return FilterToBase(typeof(DbContext), types);
            }
            catch (ReflectionTypeLoadException ex)
            {
                try
                {
                    return FilterToBase(typeof(DbContext), ex.Types.Where(x => x != null).ToArray());
                }
                catch (Exception x)
                {
                    Console.WriteLine(x);
                }
            }
            return new Type[0];
        }

        private Type[] FilterToBase(Type baseType, Type[] types)
        {
            return types.Where(x => baseType.IsAssignableFrom(x) && x != baseType).ToArray();
        }
    }
}
