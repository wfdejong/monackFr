using System.Collections.Generic;
using System.Web.Http;
using MonackFr.Library.Module;

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
            List<Tile> tiles = new List<Tile>();

            foreach (IModule module in ModuleKeeper.Instance.Modules)
            {
                tiles.Add(module.GetTile());
            }

            var mapper = AutoMapperConfig.Mapper;

            IEnumerable<ViewModels.Tile> viewModelTiles = mapper.Map<IEnumerable<ViewModels.Tile>>(tiles);

            return Ok(viewModelTiles);
        }

        public IHttpActionResult Get(string moduleName)
        {
            IModule module = ModuleKeeper.Instance.GetModule(moduleName);
            return Ok(module.GetTile());
        }
    }
}
