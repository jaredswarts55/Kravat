using Newtonsoft.Json;

namespace Kravat.Models
{
    public class Endpoint
    {
        [JsonIgnore]
        public string Method { get; set; }
        [JsonIgnore]
        public string Url { get; set; }
        [JsonIgnore]
        public Workflow Workflow {get;set; }

        public string ControllerName { get; set; }
        public string WorkflowName { get; set; }
    }
}