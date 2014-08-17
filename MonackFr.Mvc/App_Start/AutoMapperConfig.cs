using AutoMapper;
using MonackFr.Module;
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
            //From entities
            Mapper.CreateMap<Entities.Module, IModule>();
        }
    }
}