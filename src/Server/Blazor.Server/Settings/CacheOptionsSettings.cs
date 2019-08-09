using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Server.Settings
{
    public class CacheOptionsSettings
    {
        public TimeSpan? SlidingExpiration { get; set; }
    }
}
