using AutoMapper;
using Blazor.Shared.ViewModels.TorrentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Data.Entities;

namespace Blazor.Server.WebApi.AutoMapper
{
    public class ForumViewProfile : Profile
    {
        public ForumViewProfile() => CreateMap<Forum, ForumView>();
    }
}
