using System;
using System.Collections.Generic;
using System.Text;

namespace Reporting.Services.Interfaces
{
    public interface IWebServiceSettings
    {
        string WebServiceBaseUrl { get; }
        string AcceptClientHeader { get; }
    }
}
