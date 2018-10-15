using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kravat.EntityFramework6;
using Kravat.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kravat.Tests
{
    [TestClass]
    public class FileGeneratorTests
    {

        #region configuration
        const string configuration  = @"
{
    ""Models"": [
        {
            ""Name"": ""Company"",
            ""NamePlural"": ""Companies"",
            ""Properties"": [
                {
                    ""Name"": ""Id"",
                    ""Type"": ""Guid"",
                    ""TypeNamespace"": ""System""
                },
                {
                    ""Name"": ""Name"",
                    ""Type"": ""String"",
                    ""TypeNamespace"": ""System""
                },
                {
                    ""Name"": ""DateCreated"",
                    ""Type"": ""DateTime"",
                    ""TypeNamespace"": ""System""
                },
                {
                    ""Name"": ""DateLastModified"",
                    ""Type"": ""Nullable`1"",
                    ""TypeNamespace"": ""System""
                }
            ],
            ""Type"": ""MyCompany.Models.Company"",
            ""TypeNamespace"": ""MyCompany.Models"",
            ""Options"": {},
            ""MetaData"": {}
        },
        {
            ""Name"": ""User"",
            ""NamePlural"": ""Users"",
            ""Properties"": [
                {
                    ""Name"": ""Id"",
                    ""Type"": ""Guid"",
                    ""TypeNamespace"": ""System""
                },
                {
                    ""Name"": ""DateCreated"",
                    ""Type"": ""DateTime"",
                    ""TypeNamespace"": ""System""
                },
                {
                    ""Name"": ""DateLastModified"",
                    ""Type"": ""Nullable`1"",
                    ""TypeNamespace"": ""System""
                },
                {
                    ""Name"": ""Name"",
                    ""Type"": ""String"",
                    ""TypeNamespace"": ""System""
                },
                {
                    ""Name"": ""Username"",
                    ""Type"": ""String"",
                    ""TypeNamespace"": ""System""
                },
                {
                    ""Name"": ""Email"",
                    ""Type"": ""String"",
                    ""TypeNamespace"": ""System""
                },
                {
                    ""Name"": ""CompanyId"",
                    ""Type"": ""Guid"",
                    ""TypeNamespace"": ""System""
                },
                {
                    ""Name"": ""Company"",
                    ""Type"": ""Company"",
                    ""TypeNamespace"": ""MyCompany.Models""
                }
            ],
            ""Type"": ""MyCompany.Models.User"",
            ""TypeNamespace"": ""MyCompany.Models"",
            ""Options"": {},
            ""MetaData"": {}
        },
        {
            ""Name"": ""RequestCompany"",
            ""NamePlural"": ""RequestCompanies"",
            ""Type"": ""SteamChain.Core.Models.Requests.RequestCompany"",
            ""TypeNamespace"": ""SteamChain.Core.Models.Requests"",
            ""Options"": {
                ""GenerateController"": false
            },
            ""MetaData"": {}
        }
    ],
    ""Endpoints"": {
        ""GET:/api/Companies"": {
            ""controllerName"": ""@CompaniesController"",
            ""workflowName"": ""@getCompanies""
        }
    },
    ""Services"": {
        ""CompaniesService"": {
            ""Namespace"": ""MyCompany.Services"",
            ""Methods"": {
                ""Get"": {
                    ""Name"": ""Get"",
                    ""Input"": ""@RequestCompany"",
                    ""Output"": ""@Company""
                }
            }
        }
    },
    ""Workflows"": {
        ""getCompanies"": {
            ""Actions"": [""@CompaniesService.Get""]
        }
    },
    ""Options"": {
        ""Path"": ""Output\\Controllers"",
        ""Namespace"": ""Test.Controllers"",
    }
}
";
        #endregion

        [TestMethod]
        public void GenerateFromTemplate_CreatesFiles()
        {
            //var reader = new DbContextReader(@"pathToDll", "dll");
            //var entities = reader.ReadEntities();
            var generator = new FileGeneratorService();
            var config = new ConfigFileService().ReadConfigurationJson(configuration);


            generator.GenerateFiles("Templates/ApiController.mhbs", config);
        }
    }
}
