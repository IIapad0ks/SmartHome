using SmartHome.Core.Repositories;
using Models = SmartHome.Core.Models;
using Entities = SmartHome.Core.Entities;
using SmartHome.Core;
using System.Linq;

namespace SmartHome.DBModelConverter.Repositories
{
    public class ActionRepository : DBModelNameRepository<Models.ActionModel, Entities.EventAction>, IActionRepository
    {
        public ActionRepository(ISHRepository<Entities.EventAction> repository)
        {
            this.repository = repository;
        }

        public override bool Remove(int id)
        {
            IEventLogRepository eventLogRepository = SIManager.Container.GetInstance<IEventLogRepository>();
            foreach (var eventLog in eventLogRepository.GetAll().Where(e => e.Action.ID == id))
            {
                eventLogRepository.Remove(eventLog.ID);
            }

            return base.Remove(id);
        }

        public override Models.ActionModel DBItemToItem(Entities.EventAction dbAction)
        {
            if (dbAction == null)
            {
                return null;
            }

            return new Models.ActionModel { ID = dbAction.ID, Name = dbAction.Name };
        }

        public override Entities.EventAction ItemToDBItem(Models.ActionModel action)
        {
            if (action == null)
            {
                return null;
            }

            return new Entities.EventAction { ID = action.ID, Name = action.Name };
        }
    }
}