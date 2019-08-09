using Blazor.Core.Entities;
using AutoMapper;
using Blazor.Shared.ViewModels.TorrentModel;

namespace Blazor.Server.AutoMapper
{
    public class FileViewProfile : Profile
    {
        public FileViewProfile() => CreateMap<File, FileView>();
    }
}
