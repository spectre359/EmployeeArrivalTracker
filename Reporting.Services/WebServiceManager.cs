using Refit;
using Reporting.Contracts.Token;
using Reporting.Services.Interfaces;
using Serilog;
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
        private readonly ILogger _logger;
        public WebServiceManager(IWebService webService, IWebServiceSettings settings, ILogger logger)
        {
            _webService = webService;
            _settings = settings;
            _logger = logger;
        }

        public async Task<TokenResponse> Subscribe(string date, string callback)
        {
            try
            {                
                return await _webService.Subscribe(_settings.AcceptClientHeader,date,callback);
            }
            catch (ApiException ex)
            {
                _logger.Error(ex, $"Subscribing to WebService failed. DateOfEvent: {DateTime.UtcNow}");
                throw ex;
            }
        }
    }
}
