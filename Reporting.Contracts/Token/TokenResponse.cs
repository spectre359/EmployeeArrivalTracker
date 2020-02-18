using System;
using System.Collections.Generic;
using System.Text;

namespace Reporting.Contracts.Token
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
