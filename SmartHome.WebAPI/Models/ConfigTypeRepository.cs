using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBModels = SmartHome.WebAPI.Models;

namespace SmartHome.WebAPI.Models.WebAPIModels
{
    public interface IConfigTypeRepository
    {
        ConfigType Get(string name);
    }

    public class ConfigTypeRepository : SmartHomeRepository<ConfigType, DBModels.ConfigType>, IConfigTypeRepository
    {
        public ConfigType Get(string name)
        {
            DBModels.ConfigType dbConfigType = db.ConfigTypes.FirstOrDefault(ct => ct.Name == name);

            if (dbConfigType == null)
            {
                return null;
            }

            return this.DBItemToItem(dbConfigType);
        }

        public override ConfigType DBItemToItem(DBModels.ConfigType dbConfigType)
        {
            return new ConfigType { ID = dbConfigType.ID, Name = dbConfigType.Name };
        }

        public override DBModels.ConfigType ItemToDBItem(ConfigType configType)
        {
            DBModels.ConfigType dbConfigType = new DBModels.ConfigType { Name = configType.Name };
            
            if(configType.ID != null) {
                dbConfigType.ID = configType.ID;
            }

            return dbConfigType;
        }
    }
}