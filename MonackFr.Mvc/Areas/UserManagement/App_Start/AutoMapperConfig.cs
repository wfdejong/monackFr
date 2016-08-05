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
                cfg.CreateMap<Entities.User, ViewModels.User>()
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
                cfg.CreateMap<Entities.Group, ViewModels.Group>();
            });

            _mapper = config.CreateMapper();
        }

        public static IMapper Mapper
        {
            get { return _mapper; }
        }
    }
}