using Reporting.Contracts.Token;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Services.Interfaces
{
    public interface IWebServiceManager
    {
        Task<TokenResponse> Subscribe(string date, string callback);
    }
}
