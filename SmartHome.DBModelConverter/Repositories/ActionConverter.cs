using SmartHome.Core.Repositories;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;
using SmartHome.Core;
using System.Linq;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.DBModelConverter.Repositories
{
    public class ActionConverter : DBModelNameConverter<ActionModel, EventAction>, IActionConverter
    {
        public ActionConverter(ISHRepository repository) : base(repository) { }

        public override ActionModel DBItemToItem(EventAction dbItem)
        {
            ActionModel item = base.DBItemToItem(dbItem);
            item.CanSetValue = dbItem.CanSetValue;
            return item;
        }

        public override EventAction ItemToDBItem(ActionModel item)
        {
            EventAction dbItem = base.ItemToDBItem(item);
            dbItem.CanSetValue = item.CanSetValue;
            return dbItem;
        }
    }
}