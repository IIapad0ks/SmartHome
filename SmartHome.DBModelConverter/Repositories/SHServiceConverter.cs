using SmartHome.Core.Repositories;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.DBModelConverter.Repositories
{
    public class SHServiceConverter : DBModelNameConverter<SHServiceModel, SHService>, ISHServiceConverter
    {
        public SHServiceConverter(ISHRepository repository) : base(repository) { }

        public override SHServiceModel DBItemToItem(SHService dbItem)
        {
            SHServiceModel item = base.DBItemToItem(dbItem);
            item.IsOn = dbItem.IsOn;
            return item;
        }

        public override SHService ItemToDBItem(SHServiceModel item)
        {
            SHService dbItem = base.ItemToDBItem(item);
            dbItem.IsOn = item.IsOn;
            return dbItem;
        }
    }
}
