using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Kravat.Models;
using Kravat.Services.Base;
using Newtonsoft.Json;

namespace Kravat.Services
{
    public class ConfigFileService : IConfigFileService
    {
        public Stream CreateConfigFile(IEnumerable<Model> models)
        {
            throw new NotImplementedException();
        }

        public KravatConfiguration ReadConfigurationJson(string configurationJson)
        {
            var configuration = JsonConvert.DeserializeObject<KravatConfiguration>(configurationJson);
            configuration = FillInNames(configuration);
            return FillInReferences(configuration);
        }

        private KravatConfiguration FillInNames(KravatConfiguration config)
        {
            foreach (var pair in config.Endpoints)
            {
                var methodUrl = pair.Key.Split(':').Select(x => x.Trim()).ToArray();
                pair.Value.Method = methodUrl[0];
                pair.Value.Url = methodUrl[1];
            }
            foreach (var pair in config.Services)
            {
                pair.Value.Name = pair.Key;
                pair.Value.VariableName = pair.Key.Substring(0,1).ToLower() + pair.Key.Substring(1);
                foreach(var methodPair in pair.Value.Methods)
                    methodPair.Value.Name = methodPair.Key;
            }

            foreach (var pair in config.Workflows)
            {
                pair.Value.Name = pair.Key;
            }

            return config;
        }

        private KravatConfiguration FillInReferences(KravatConfiguration config)
        {
            foreach (var endpoint in config.Endpoints.Select(x => x.Value))
            {
                endpoint.Workflow = config.Workflows.Select(x => x.Value).FirstOrDefault(x => x.Name == endpoint.WorkflowName.Substring(1));
            }

            foreach (var service in config.Services.Select(x => x.Value))
            {
                var s = service;
                foreach (var method in service.Methods.Select(x => x.Value))
                {
                    method.InputModel = config.Models.FirstOrDefault(x => x.Name == method.Input.Substring(1));
                    method.OutputModel = config.Models.FirstOrDefault(x => x.Name == method.Output.Substring(1));
                    method.ParentService = s;
                }
            }

            foreach (var workflow in config.Workflows.Select(x => x.Value))
            {
                var actions = workflow.Actions.Select(x => x.Substring(1).Split('.').Select(y => y.Trim()).ToArray()).ToArray();
                var methods = config.Services.SelectMany((kv) => kv.Value.Methods.Values).ToArray();
                var serviceMethods = new List<ServiceMethod>();
                foreach (var action in actions)
                {
                    var method = methods.FirstOrDefault(y => y.ParentService.Name == action[0] && y.Name == action[1]);
                    serviceMethods.Add(method);
                }
                workflow.ServiceMethods = serviceMethods.ToArray();
                workflow.InputType = workflow.ServiceMethods.FirstOrDefault()?.InputModel;
                if(workflow.InputType == null)
                    throw new Exception($"Could not retrieve input type of workflow '{workflow.Name}'");
                workflow.OutputType = workflow.ServiceMethods.LastOrDefault()?.OutputModel;
                if(workflow.OutputType == null)
                    throw new Exception($"Could not retrieve output type of workflow '{workflow.Name}'");
            }

            return config;
        }
    }
}
