using System;
using Newtonsoft.Json;

namespace Kravat.Models
{
    public class Workflow
    {
        [JsonIgnore]
        public string Name { get; set; }
        [JsonIgnore]

        public Model InputType { get; set; }
        [JsonIgnore]
        public Model  OutputType{ get; set; }
        [JsonIgnore]
        public ServiceMethod[] ServiceMethods { get; set; }
        public string[] Actions { get; set; }

    }
}