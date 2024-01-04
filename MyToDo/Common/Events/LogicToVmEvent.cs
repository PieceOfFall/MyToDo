using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Events
{
    public class LogicToVmEvent : PubSubEvent<LogicToVmModel>
    {

    }

    public class LogicToVmModel
    {

        public string VmName { get; set; } = string.Empty;

        public object Data { get; set; }
    }
}
