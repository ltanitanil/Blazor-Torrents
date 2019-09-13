using AutoMapper;
using Blazor.Server.DataAccessLayer.Entities;
using Blazor.Shared.Models.ViewModels.TorrentModel;

namespace Blazor.Server.WebApi.AutoMapper
{
    public class ForumViewProfile : Profile
    {
        public ForumViewProfile() => CreateMap<Forum, ForumView>();
    }
}
