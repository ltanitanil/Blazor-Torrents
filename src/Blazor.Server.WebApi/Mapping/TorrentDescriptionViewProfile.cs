using AutoMapper;
using Blazor.Shared.ViewModels.TorrentModel;
using Blazor.Server.DataAccessLayer.Data.Entities;

namespace Blazor.Server.WebApi.AutoMapper
{
    public class TorrentDescriptionViewProfile : Profile
    {
        public TorrentDescriptionViewProfile() => CreateMap<Torrent, TorrentDescriptionView>();
    }
}
