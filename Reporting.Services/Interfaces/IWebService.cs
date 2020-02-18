using Refit;
using Reporting.Contracts.Token;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Services.Interfaces
{
    public interface IWebService
    {
        [Get("/api/clients/subscribe?date={date}&callback={callback}")]
        Task<TokenResponse> Subscribe([Header("Accept-Client")] string acceptClientHeader, string date, string callback);

    }
}
