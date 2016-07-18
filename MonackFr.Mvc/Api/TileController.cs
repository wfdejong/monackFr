using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;

namespace MonackFr.Mvc.Api
{
    public class TileController : ApiController
    {

        /// <summary>
        /// returns all tiles of installed modules
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Configure AutoMapper
                cfg.CreateMap<Repository.Tile, ViewModels.Tile>();
            });


            List<Repository.Tile> tiles = new List<Repository.Tile>();

            foreach (Repository.IModule module in ModuleKeeper.Instance.Modules)
            {
                tiles.Add(module.GetTile());
            }

            IEnumerable<ViewModels.Tile> viewModelTiles = config.CreateMapper().Map<IEnumerable<ViewModels.Tile>>(tiles);

            return Ok(viewModelTiles);
        }

        public IHttpActionResult Get(string moduleName)
        {
            Repository.IModule module = ModuleKeeper.Instance.GetModule(moduleName);
            return Ok(module.GetTile());
        }
    }
}
