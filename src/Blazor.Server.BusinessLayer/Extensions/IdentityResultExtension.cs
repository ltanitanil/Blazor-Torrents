using Blazor.Shared.Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Linq;

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
