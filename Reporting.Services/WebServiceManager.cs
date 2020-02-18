using Refit;
using Reporting.Contracts.Token;
using Reporting.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Services
{
    public class WebServiceManager : IWebServiceManager
    {
        private readonly IWebService _webService;
        private readonly IWebServiceSettings _settings;

        public WebServiceManager(IWebService webService, IWebServiceSettings settings)
        {
            _webService = webService;
            _settings = settings;
        }

        public async Task<TokenResponse> Subscribe(string date, string callback)
        {
            try
            {                
                return await _webService.Subscribe(_settings.AcceptClientHeader,date,callback);
            }
            catch (ApiException ex)
            {                
                throw ex;
            }
        }
    }
}
