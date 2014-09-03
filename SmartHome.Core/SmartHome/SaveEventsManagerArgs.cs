using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.SmartHome
{
    public class SaveEventsManagerArgs : EventArgs
    {
        public string ActionName { get; set; }

        public SaveEventsManagerArgs(string actionName)
        {
            this.ActionName = actionName;
        }
    }
}
