using System;
using System.Collections.Generic;
using System.Text;

namespace Kravat.Models
{
    public class KravatConfiguration
    {
        public Model[] Models { get; set; }
        public Dictionary<string, Endpoint> Endpoints { get; set; }
        public Dictionary<string, Service> Services { get; set; }
        public Dictionary<string, Workflow> Workflows { get; set; }
        public Dictionary<string, string> Options { get; set; }
    }
}
