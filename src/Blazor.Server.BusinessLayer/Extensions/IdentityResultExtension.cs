using Blazor.Server.BusinessLayer.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blazor.Server.BusinessLayer.Extensions
{
    public static class IdentityResultExtension
    {
        public static void ErrorChecking(this IdentityResult identityResult)
        {
            if(!identityResult.Succeeded)
                throw new AppException(ExceptionEvent.RegistrationFailed, string.Join("\n", identityResult.Errors.Select(x => x.Description)));
        }
    }
}
