using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Base.Configuration
{
    public class CoreAuthenticationOptionsConfig
    {
        public byte[] SecretKey { get; set; }
        public bool AdditionalRandomKey { get; set; }
        public int ExprireInMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string HrAuthentication { get; set; }
    }
}
