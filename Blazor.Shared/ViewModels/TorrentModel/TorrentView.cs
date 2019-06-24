using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Shared.ViewModels.TorrentModel
{
    public class TorrentView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public long Size { get; set; }
        public DateTimeOffset RegistredAt { get; set; }
    }
}
