using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Blazor.Server.DataAccessLayer.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AboutUser { get; set; }
    }
}
