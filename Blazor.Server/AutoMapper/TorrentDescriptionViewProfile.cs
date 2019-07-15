using ApplicationCore.Entities;
using AutoMapper;
using Blazor.Server.Helpers;
using Blazor.Shared.ViewModels.TorrentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Server.AutoMapper
{
    public class TorrentDescriptionViewProfile : Profile
    {
        public TorrentDescriptionViewProfile()
        {
            CreateMap<Torrent, TorrentDescriptionView>().ForMember("Content", opt => opt.MapFrom(x => BBCodeToHTMLConverter.Format(x.Content)));
        }
    }
}
