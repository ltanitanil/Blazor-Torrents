using AutoMapper;
using Blazor.Shared.ViewModels.TorrentModel;
using Blazor.Server.BusinessLayer.Entities;
using Blazor.Server.WebApi.Helpers;

namespace Blazor.Server.WebApi.AutoMapper
{
    public class TorrentDescriptionViewProfile : Profile
    {
        public TorrentDescriptionViewProfile()
        {
            CreateMap<Torrent, TorrentDescriptionView>().ForMember(dist => dist.Content, 
                opt => opt.MapFrom(x => BBCodeToHTMLConverter.Format(x.Content)));
        }
    }
}
