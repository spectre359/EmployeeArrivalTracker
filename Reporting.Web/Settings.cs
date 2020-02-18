using Reporting.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.Web
{
    public class Settings : IWebServiceSettings
    {
        public string WebServiceBaseUrl { get; set; }
        public string AcceptClientHeader { get; set; }
       
    }
}
