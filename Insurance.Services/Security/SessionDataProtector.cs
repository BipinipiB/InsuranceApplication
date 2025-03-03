using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Services.Security
{
    public class SessionDataProtector
    {

        private readonly IDataProtector _protector;

        public SessionDataProtector(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("SessionDataProtector");
        }

        public string Protect(string data)
        {
            return _protector.Protect(data);
        }

        public string Unprotect(string protectedData)
        {
            return _protector.Unprotect(protectedData);
        }
    }
}
