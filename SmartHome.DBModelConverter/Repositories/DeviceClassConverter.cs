using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHome.Core;
using SmartHome.Core.Repositories;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.DBModelConverter.Repositories
{
    public class DeviceClassConverter : DBModelNameConverter<DeviceClassModel, DeviceClass>, IDeviceClassConverter
    {
        public DeviceClassConverter(ISHRepository repository) : base(repository) { }
    }
}