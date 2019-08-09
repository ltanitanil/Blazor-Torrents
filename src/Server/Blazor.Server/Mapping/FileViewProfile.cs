using AutoMapper;
using Blazor.Shared.ViewModels.TorrentModel;
using Blazor.BusinessLayer.Entities;

namespace Blazor.Server.WebApi.AutoMapper
{
    public class FileViewProfile : Profile
    {
        public FileViewProfile() => CreateMap<File, FileView>();
    }
}
