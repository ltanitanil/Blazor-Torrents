using AutoMapper;
using Blazor.Server.DataAccessLayer.Entities;
using Blazor.Shared.Models.ViewModels.TorrentModel;

namespace Blazor.Server.WebApi.AutoMapper
{
    public class TorrentDescriptionViewProfile : Profile
    {
        public TorrentDescriptionViewProfile() => 
            CreateMap<Torrent, TorrentDescriptionView>();
    }
}
