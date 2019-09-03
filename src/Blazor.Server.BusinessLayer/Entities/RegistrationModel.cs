using System;
using System.Collections.Generic;
using System.Text;
using Blazor.Server.DataAccessLayer.Entities;

namespace Blazor.Server.BusinessLayer.Entities
{
    public class RegistrationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string AboutUser { get; set; }
    }
}
