using AutoMapper;
using MonackFr.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.PackageManagement
{
    internal class AutoMapperConfig
    {
        internal static void CreateMaps()
        {
            //To ViewModels
            Mapper.CreateMap<IPackage, ViewModels.Package>();
            Mapper.CreateMap<IModule, ViewModels.Module>()
                .ForMember(module => module.Name, m => m.MapFrom(mi => mi.MetaData["Name"]))
                .ForMember(module => module.Author, m => m.MapFrom(mi => mi.MetaData["Author"]));
        }
    }
}