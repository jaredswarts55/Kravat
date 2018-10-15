using System;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Reflection;
using Kravat.EntityFramework6;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Kravat.Tests
{
    [TestClass]
    public class DbContextReaderTests
    {
        private SqlProviderServices _instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

        [TestMethod]
        public void ReadEntities_ReturnsReadEntityList()
        {
            var reader = new DbContextReader(@"path", "dllPath");
            var entities = reader.ReadEntities();

            var jsonData = JsonConvert.SerializeObject(entities, Formatting.Indented);
            File.WriteAllText("types.json", jsonData);


        }
    }
}
