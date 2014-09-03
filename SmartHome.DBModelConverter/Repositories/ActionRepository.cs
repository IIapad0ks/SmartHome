using SmartHome.Core.Repositories;
using Models = SmartHome.Core.Models;
using Entities = SmartHome.Core.Entities;

namespace SmartHome.DBModelConverter.Repositories
{
    public class ActionRepository : DBModelNameRepository<Models.Action, Entities.Action>, IActionRepository
    {
        public ActionRepository(ISHRepository<Entities.Action> repository)
        {
            this.repository = repository;
        }

        public override Models.Action DBItemToItem(Entities.Action dbAction)
        {
            if (dbAction == null)
            {
                return new Models.Action();
            }

            return new Models.Action { ID = dbAction.ID, Name = dbAction.Name };
        }

        public override Entities.Action ItemToDBItem(Models.Action action)
        {
            if (action == null)
            {
                return new Entities.Action();
            }

            Entities.Action dbAction = new Entities.Action { Name = action.Name };

            if (action.ID != 0)
            {
                dbAction.ID = action.ID;
            }

            return dbAction;
        }
    }
}