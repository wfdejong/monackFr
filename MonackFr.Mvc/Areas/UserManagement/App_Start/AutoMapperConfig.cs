using AutoMapper;

namespace MonackFr.Mvc.Areas.UserManagement
{
    public class AutoMapperConfig
    {
        internal static void CreateMaps()
        {
            //from entities
            Mapper.Initialize(cfg => cfg.CreateMap<Entities.User, Security.MfrUser>());
            Mapper.Initialize(cfg => cfg.CreateMap<Entities.User, ViewModels.User>());

            //to entities
            Mapper.Initialize(cfg => cfg.CreateMap<Security.IMfrRole, Entities.Role>());
        }
    }
}