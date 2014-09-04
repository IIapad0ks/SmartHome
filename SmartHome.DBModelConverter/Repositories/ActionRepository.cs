using SmartHome.Core.Repositories;
using Models = SmartHome.Core.Models;
using Entities = SmartHome.Core.Entities;
using SmartHome.Core;
using System.Linq;

namespace SmartHome.DBModelConverter.Repositories
{
    public class ActionRepository : DBModelNameRepository<Models.Action, Entities.Action>, IActionRepository
    {
        public ActionRepository(ISHRepository<Entities.Action> repository)
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

        public override Models.Action DBItemToItem(Entities.Action dbAction)
        {
            if (dbAction == null)
            {
                return null;
            }

            return new Models.Action { ID = dbAction.ID, Name = dbAction.Name };
        }

        public override Entities.Action ItemToDBItem(Models.Action action)
        {
            if (action == null)
            {
                return null;
            }

            return new Entities.Action { ID = action.ID, Name = action.Name };
        }
    }
}