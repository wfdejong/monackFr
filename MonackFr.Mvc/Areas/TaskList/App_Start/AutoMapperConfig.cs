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
            Mapper.CreateMap<ViewModels.Task, Entities.Task>()
				.ForMember(t => t.Created, opt => opt.Ignore())
				.ForMember(t=>t.LastUpdate, opt=>opt.Ignore());

            //Entities to ViewmModels
            Mapper.CreateMap<Entities.Task, ViewModels.Task>();
        }
    }
}