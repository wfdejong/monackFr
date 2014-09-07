using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.UserManagement
{
    public class AutoMapperConfig
    {
        internal static void CreateMaps()
        {
            //from entities
            Mapper.CreateMap<Entities.User, Security.MfrUser>();

            //to entities
            Mapper.CreateMap<Security.IMfrRole, Entities.Role>();
        }
    }
}