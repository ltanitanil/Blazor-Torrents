﻿using ApplicationCore.Entities;
using AutoMapper;
using Blazor.Shared.ViewModels.TorrentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Server.AutoMapper
{
    public class FileViewProfile : Profile
    {
        public FileViewProfile() => CreateMap<File, FileView>();
    }
}