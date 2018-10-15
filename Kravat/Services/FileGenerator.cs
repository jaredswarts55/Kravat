using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using Kravat.Models;
using Kravat.Services.Base;
using Mustache;

namespace Kravat.Services
{
    public class FileGeneratorService : IFileGeneratorService
    {
        public void GenerateFiles(string templatePath, KravatConfiguration config)
        {
            var (fileNameTemplate, filePathTemplate, templateBody) = ProcessTemplate(File.ReadAllText(templatePath));
            for (var i = 0; i < config.Models.Length; i++)
            {
                var model = config.Models[i];
                if (model.Options.TryGetValue("GenerateController", out var generateController))
                    if (!((bool)generateController))
                        continue;
                var endpoints = config.Endpoints.Select(x => x.Value).Where(x => x.ControllerName.Substring(1) == $"{model.NamePlural}Controller").ToArray();
                var dependencies = endpoints.SelectMany(x => x.Workflow.ServiceMethods.Select(y => y.ParentService)).GroupBy(x => x.Name).Select(x => x.First()).ToArray();
                var templateModel = new ContextModel
                {
                    Model = model,
                    Endpoints = endpoints,
                    Options = config.Options,
                    Dependencies = dependencies,
                    ServiceNamespaces = dependencies.Select(x => x.Namespace).GroupBy(x => x).Select(x => x.First()).ToArray()
                };
                GenerateFile(fileNameTemplate, filePathTemplate, templateBody, templateModel);
            }
        }

        private void GenerateFile(string fileNameTemplate, string filePathTemplate, string templateBody, object model)
        {
            var fileNameCompiled = CompileTemplate(fileNameTemplate, model);
            var filePathCompiled = CompileTemplate(filePathTemplate, model);
            var bodyCompiled = CompileTemplate(templateBody, model);
            if (!Directory.Exists(filePathCompiled) && !string.IsNullOrWhiteSpace(filePathCompiled))
                Directory.CreateDirectory(filePathCompiled);

            File.WriteAllText(Path.Combine(filePathCompiled, fileNameCompiled), bodyCompiled);
        }

        public string CompileTemplate(string templateText, object model)
        {
            FormatCompiler compiler = new FormatCompiler();
            var template = compiler.Compile(templateText);
            return template.Render(model);
        }

        public (string fileName, string filePath, string body) ProcessTemplate(string templateText)
        {
            var allLines = templateText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var body = string.Join("\n", allLines.Skip(2));
            var properties = allLines.Take(2).Select(x =>
            {
                var y = x.Split(':');
                return (PropertyName: y[0].Trim(), Value: y[1].Trim());
            }).ToArray();
            return (properties.FirstOrDefault(x => x.PropertyName?.ToLower() == "filename").Value,
                properties.FirstOrDefault(x => x.PropertyName?.ToLower() == "filepath").Value,
                body);
        }
    }
}
