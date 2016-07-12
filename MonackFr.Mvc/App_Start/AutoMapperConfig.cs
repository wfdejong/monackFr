using System;
using AutoMapper;

namespace MonackFr.Mvc
{
    public class AutoMapperConfig
    {
        public static void CreateMaps()
        {
            //Module to ViewModels
            Mapper.CreateMap<Repository.Tile, ViewModels.Tile>();
            Mapper.CreateMap<Repository.IModule, ViewModels.Module>();
        }
    }
}