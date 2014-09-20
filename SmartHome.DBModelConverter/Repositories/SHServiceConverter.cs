﻿using SmartHome.Core.Repositories;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.DBModelConverter.Repositories
{
    public class SHServiceConverter : DBModelNameConverter<SHServiceModel, SmartHomeService>, ISHServiceConverter
    {
        public SHServiceConverter(ISHRepository repository) : base(repository) { }

        public override SHServiceModel DBItemToItem(SmartHomeService dbItem)
        {
            SHServiceModel item = base.DBItemToItem(dbItem);
            item.ConfigFilename = dbItem.ConfigFilename; 
            item.LibDirname = dbItem.LibsDirname;
            item.IsOn = dbItem.IsOn;
            return item;
        }

        public override SmartHomeService ItemToDBItem(SHServiceModel item)
        {
            SmartHomeService dbItem = base.ItemToDBItem(item);
            dbItem.ConfigFilename = item.ConfigFilename;
            dbItem.LibsDirname = item.LibDirname;
            dbItem.IsOn = item.IsOn;
            return dbItem;
        }
    }
}