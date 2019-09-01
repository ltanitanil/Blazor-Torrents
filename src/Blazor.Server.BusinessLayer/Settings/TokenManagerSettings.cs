using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Server.BusinessLayer.Settings
{
    public class TokenManagerSettings
    {
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiryInDays { get; set; }
    }
}
