using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.App_Start
{
    public class AutoMapperConfig
    {
        internal static void CreateMaps()
        {
            Mapper.CreateMap<Entities.Package, Module.Package>();
        }
    }
}