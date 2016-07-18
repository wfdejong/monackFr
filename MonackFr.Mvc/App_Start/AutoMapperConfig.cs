using System;
using AutoMapper;

namespace MonackFr.Mvc
{
    public class AutoMapperConfig
    {
        public static void CreateMaps()
        {
            //Module to ViewModels
            //Mapper.Initialize(cfg => cfg.CreateMap<Repository.Tile, ViewModels.Tile>());
            //Mapper.Initialize(cfg => cfg.CreateMap<Repository.IModule, ViewModels.Module>());
        }
    }
}