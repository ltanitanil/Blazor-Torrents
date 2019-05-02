using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace Web.ViewModels
{
    public class TorrentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Size { get; set; } 
        public DateTimeOffset RegistredAt { get; set; }
    }
}
