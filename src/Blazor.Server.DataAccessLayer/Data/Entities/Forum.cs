using System;
using System.Collections.Generic;
using System.Text;
using Blazor.Server.DataAccessLayer.Data.Entities;

namespace Blazor.Server.DataAccessLayer.Data.Entities
{
    public class Forum : BaseEntity
    {
        public string Value { get; set; }
    }
}
