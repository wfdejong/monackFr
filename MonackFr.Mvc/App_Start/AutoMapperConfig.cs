using System;
using AutoMapper;

namespace MonackFr.Mvc
{
    public class AutoMapperConfig
    {
        public static void CreateMaps()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Repository.Tile, ViewModels.Tile>());
        }
    }
}