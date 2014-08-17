using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.TaskList
{
    internal class AutoMapperConfig
    {
        internal static void CreateMaps()
        {
            //ViewModels to Entities
            Mapper.CreateMap<ViewModels.Task, Entities.Task>();

            //Entities to ViewmModels
            Mapper.CreateMap<Entities.Task, ViewModels.Task>();
        }
    }
}