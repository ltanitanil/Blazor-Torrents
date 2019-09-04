using System.Collections.Generic;
using Blazor.Server.DataAccessLayer.Entities;

namespace Blazor.Server.BusinessLayer.Services.JwtTokenService
{
    public interface IJwtTokenService
    {
        string BuildToken(ApplicationUser user, IList<string> roles);
    }
}
