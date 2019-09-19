using System;
using Blazor.Server.DataAccessLayer.Entities;
using AutoMapper;
using Blazor.Shared.Models.ViewModels.TorrentModel;

namespace Blazor.Server.WebApi.Mapping
{
    public class TorrentProfile:Profile
    {
        public TorrentProfile() =>
            CreateMap<TorrentUploadViewModel, Torrent>()
                .ForMember(x=>x.RegisteredAt,
                    opt=>opt.MapFrom(x=>DateTimeOffset.Now));
    }
}
