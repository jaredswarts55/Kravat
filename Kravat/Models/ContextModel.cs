using System;
using System.Collections.Generic;
using System.Text;

namespace Kravat.Models
{
    public class ContextModel
    {
        public Model Model { get; set; }
        public Endpoint[] Endpoints { get; set; }
        public Dictionary<string, string> Options { get; set; }
        public Service[] Dependencies { get; set; }
        public string[] ServiceNamespaces { get; set; }
    }
}
