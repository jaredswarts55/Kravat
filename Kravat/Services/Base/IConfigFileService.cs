using Kravat.Models;

namespace Kravat.Services.Base
{
    public interface IConfigFileService
    {
        KravatConfiguration ReadConfigurationJson(string configurationJson);
    }
}