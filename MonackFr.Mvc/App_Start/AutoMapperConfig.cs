using AutoMapper;
using MonackFr.Mvc.Areas.PackageManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.App_Start
{
    internal class AutoMapperConfig
    {
        internal static void CreateMaps()
        {
            //From entities to modules
            //Should not be necesary entities go to viewmodels or module goes to viewmodels Mapper.CreateMap<Entities.Module, Module.IModule>();

			//Module to ViewModels
			Mapper.CreateMap<Repository.Tile, ViewModels.Tile>();
			Mapper.CreateMap<Repository.IModule, ViewModels.Module>();

			//From Entities to ViewModules
			Mapper.CreateMap<Entities.Module, ViewModels.Module>();
        }
    }
}