using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kravat.Models
{
    public class Service
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public string VariableName { get; set; }

        public Dictionary<string, ServiceMethod> Methods { get; set; }

        public string Namespace { get; set; }
    }
}