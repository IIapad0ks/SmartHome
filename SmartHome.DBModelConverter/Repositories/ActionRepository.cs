using SmartHome.Core.Repositories;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;
using SmartHome.Core;
using System.Linq;

namespace SmartHome.DBModelConverter.Repositories
{
    public class ActionRepository : DBModelNameRepository<EventActionModel, EventAction>, IActionRepository
    {
        public ActionRepository(ISHRepository<EventAction> repository) : base(repository) { }

        public override bool Remove(int id)
        {
            IEventLogRepository eventLogRepository = SIManager.Container.GetInstance<IEventLogRepository>();
            foreach (var eventLog in eventLogRepository.GetAll().Where(e => e.Action.ID == id))
            {
                eventLogRepository.Remove(eventLog.ID);
            }

            return base.Remove(id);
        }
    }
}