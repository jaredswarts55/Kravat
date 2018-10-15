using System.Collections.Generic;
using Kravat.Models;

namespace Kravat.Services.Base
{
    public interface IFileGeneratorService
    {
        void GenerateFiles(string templatePath, KravatConfiguration config);
        string CompileTemplate(string templateText, object model);
        (string fileName, string filePath, string body) ProcessTemplate(string templateText);
    }
}