using AutoMapper;

namespace MonackFr.Mvc.Areas.UserManagement
{
    public static class AutoMapperConfig
    {
        private static IMapper _mapper;

        public static void CreateMaps()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.User, ViewModels.User>();
                cfg.CreateMap<Entities.Group, ViewModels.Group>();
                cfg.CreateMap<Entities.Role, ViewModels.Role>();
                cfg.CreateMap<ViewModels.User, Entities.User>();
                cfg.CreateMap<ViewModels.Group, Entities.Group>();
            });

            _mapper = config.CreateMapper();
        }

        public static IMapper Mapper
        {
            get { return _mapper; }
        }
    }
}