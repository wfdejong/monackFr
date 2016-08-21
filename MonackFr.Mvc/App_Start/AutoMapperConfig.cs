using AutoMapper;
using MonackFr.Library.Module;

namespace MonackFr.Mvc
{
    public static class AutoMapperConfig
    {
        private static IMapper _mapper;

        public static void CreateMaps()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tile, ViewModels.Tile>();
                cfg.CreateMap<IPackage, PackageManagement.Package>();

                //Packagemanagement to entities
                cfg.CreateMap<PackageManagement.Package, Entities.Package>();
                cfg.CreateMap<PackageManagement.Module, Entities.Module>();
                cfg.CreateMap<Security.IMfrRole, Entities.Role>();

                cfg.CreateMap<IModule, PackageManagement.Module>();
            });

            _mapper = config.CreateMapper();
        }

        public static IMapper Mapper { get { return _mapper; } }
    }
}