using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBModels = SmartHome.WebAPI.Models;

namespace SmartHome.WebAPI.Models.WebAPIModels
{
    public interface IActionRepository
    {
        Action Get(string name);
    }

    public class ActionRepository : SmartHomeRepository<Action, DBModels.Action>, IActionRepository
    {
        public Action Get(string name)
        {
            DBModels.Action dbAction = db.Actions.FirstOrDefault(d => d.Name == name);

            if (dbAction == null)
            {
                return null;
            }

            return this.DBItemToItem(dbAction);
        }

        public override Action DBItemToItem(DBModels.Action dbAction)
        {
            return new Action { ID = dbAction.ID, Name = dbAction.Name };
        }

        public override DBModels.Action ItemToDBItem(Action action)
        {
            DBModels.Action dbAction = new DBModels.Action { Name = action.Name };

            if (action.ID != null)
            {
                dbAction.ID = action.ID;
            }

            return dbAction;
        }
    }
}