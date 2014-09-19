using SmartHome.Core.Repositories;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;
using SmartHome.Core;
using System.Linq;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.DBModelConverter.Repositories
{
    public class ActionConverter : DBModelNameConverter<EventActionModel, EventAction>, IActionConverter
    {
        public ActionConverter(ISHRepository repository) : base(repository) { }

        public override bool Remove(int id)
        {
            IEventLogConverter eventLogRepository = SIManager.Container.GetInstance<IEventLogConverter>();
            foreach (var eventLog in eventLogRepository.GetAll().Where(e => e.Action.ID == id))
            {
                eventLogRepository.Remove(eventLog.ID);
            }

            return base.Remove(id);
        }
    }
}