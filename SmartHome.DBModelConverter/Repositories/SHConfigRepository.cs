using SmartHome.Core.Repositories;
using Models = SmartHome.Core.Models;
using Entities = SmartHome.Core.Entities;

namespace SmartHome.DBModelConverter.Repositories
{
    public class SHConfigRepository : DBModelRepository<Models.SHConfig, Entities.SHConfig>, ISHConfigRepository
    {
        public SHConfigRepository(ISHRepository<Entities.SHConfig> repository) {
            this.repository = repository;
        }

        public override Models.SHConfig DBItemToItem(Entities.SHConfig dbItem)
        {
            if (dbItem == null)
            {
                return null;
            }

            return new Models.SHConfig { ID = dbItem.ID, ConfigFilename = dbItem.ConfigFilename, LibDirname = dbItem.LibDirectory, IsOn = dbItem.IsOn };
        }

        public override Entities.SHConfig ItemToDBItem(Models.SHConfig item)
        {
            if (item == null)
            {
                return null;
            }

            return new Entities.SHConfig { ID = item.ID, ConfigFilename = item.ConfigFilename, LibDirectory = item.LibDirname, IsOn = item.IsOn };
        }
    }
}
