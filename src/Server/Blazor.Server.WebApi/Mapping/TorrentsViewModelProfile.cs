using AutoMapper;
using Blazor.Shared.ViewModels.TorrentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.BusinessLayer.Entities;

namespace Blazor.Server.WebApi.AutoMapper
{
    public class TorrentsViewModelProfile : Profile
    {
        public TorrentsViewModelProfile() => CreateMap<Torrent, TorrentView>();
    }
}
