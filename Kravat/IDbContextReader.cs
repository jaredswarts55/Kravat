using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kravat.Models;

namespace Kravat
{
    public interface IDbContextReader
    {
        List<Model> ReadEntities();
        List<Model> ReadEntitiesFromContext(Type contextType);
        Type[] GetDbContexts();
    }
}
