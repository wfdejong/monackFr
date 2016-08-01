using System;
using AutoMapper;

namespace MonackFr.Mvc
{
    public static class AutoMapperConfig
    {
        private static IMapper _mapper;

        public static void CreateMaps()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Repository.Tile, ViewModels.Tile>();
            });

            _mapper = config.CreateMapper();
        }

        public static IMapper Mapper { get { return _mapper; } }
    }
}