using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

using MyCompany.Models;

using MyCompany.Services;


namespace Test.Controllers
{
    public partial class CompaniesController : Controller
    {
        public readonly CompaniesService companiesService;
        

        public CompaniesController(
            CompaniesService companiesService,
        )
        {
            this._companiesService = companiesService;
        }

        
        [Route("/api/Companies"), HttpGET]
        public Company getCompanies(RequestCompany request)
        { 
            return _companiesService.Get(request);
        }
        
    }
}
