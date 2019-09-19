using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blazor.Server.DataAccessLayer.Entities;
using Blazor.Shared.Models.ViewModels.TorrentModel;

namespace Blazor.Server.WebApi.Mapping
{
    public class SubcategoryViewProfile : Profile
    {
        public SubcategoryViewProfile() => 
            CreateMap<Subcategory, SubcategoryView>();
    }
}
