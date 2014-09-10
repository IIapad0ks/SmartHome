using SmartHome.Core.Repositories;
using Models = SmartHome.Core.Models;
using Entities = SmartHome.Core.Entities;

namespace SmartHome.DBModelConverter.Repositories
{
    public class SHServiceRepository : DBModelNameRepository<Models.SHServiceModel, Entities.SmartHomeService>, ISHServiceRepository
    {
        public SHServiceRepository(ISHRepository<Entities.SmartHomeService> repository) {
            this.repository = repository;
        }

        public override Models.SHServiceModel DBItemToItem(Entities.SmartHomeService dbItem)
        {
            if (dbItem == null)
            {
                return null;
            }

            return new Models.SHServiceModel { ID = dbItem.ID, Name = dbItem.Name, ConfigFilename = dbItem.ConfigFilename, LibDirname = dbItem.LibsDirname, IsOn = dbItem.IsOn };
        }

        public override Entities.SmartHomeService ItemToDBItem(Models.SHServiceModel item)
        {
            if (item == null)
            {
                return null;
            }

            return new Entities.SmartHomeService { ID = item.ID, Name = item.Name, ConfigFilename = item.ConfigFilename, LibsDirname = item.LibDirname, IsOn = item.IsOn };
        }
    }
}
