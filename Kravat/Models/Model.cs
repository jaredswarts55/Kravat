using System;
using System.Collections.Generic;
using System.Text;

namespace Kravat.Models
{
    public class Model
    {
        public string Name { get; set; }
        public string NamePlural { get; set; }
        public ModelProperty[] Properties { get; set; }
        public string Type { get; set; }
        public string TypeNamespace { get; set; }
        public Dictionary<string, object> Options { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, object> MetaData { get; set; } = new Dictionary<string, object>();
    }
}
