using SmartHome.Core;
using SmartHome.Core.DBModelConverters;
using SmartHome.Core.Entities;
using SmartHome.Core.Models;
using SmartHome.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.DBModelConverter.Repositories
{
    public class RoomConverter : DBModelNameConverter<RoomModel, Room>, IRoomConverter
    {
        public RoomConverter(ISHRepository repository) : base(repository) { }

        public override RoomModel DBItemToItem(Room dbItem)
        {
            RoomModel item = base.DBItemToItem(dbItem);
            item.Home = SIManager.Container.GetInstance<ISHServiceConverter>().DBItemToItem(dbItem.SHService);
            return item;
        }

        public override Room ItemToDBItem(RoomModel item)
        {
            Room dbItem = base.ItemToDBItem(item);
            dbItem.SHServiceId = item.Home.Id;
            return dbItem;
        }
    }
}
