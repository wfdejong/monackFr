using AutoMapper;
using MonackFr.Repository;
using MonackFr.Security;
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
            Mapper.CreateMap<PackageManagement.Package, ViewModels.Package>();
            Mapper.CreateMap<PackageManagement.Module, ViewModels.Module>();
                
            //To Entities
            Mapper.CreateMap<Package, Mvc.Entities.Package>();
            Mapper.CreateMap<Module, Mvc.Entities.Module>();
            Mapper.CreateMap<IMfrRole, Mvc.Entities.Role>();
            
            //From packages
            Mapper.CreateMap<IPackage, Package>();
            Mapper.CreateMap<IModule, Module>();
        }
    }
}