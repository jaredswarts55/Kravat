using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kravat.Models
{
    public class ServiceMethod
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public Model InputModel { get; set; }

        [JsonIgnore]
        public Model OutputModel { get; set; }

        [JsonIgnore]
        public Service ParentService { get; set; }

        public string Input { get; set; }
        public string Output { get; set; }
    }
}