using MyToDo.Common.Events;
using Prism.Events;

namespace MyToDo.Extensions
{
    public static class LogicToVmExtensions
    {
        public static void RegisterLogicToVm(this IEventAggregator aggregator, Action<LogicToVmModel> action)
        {
            aggregator.GetEvent<LogicToVmEvent>().Subscribe(action);
        }
        public static void SendLogicToVm(this IEventAggregator aggregator, LogicToVmModel data)
        {
            aggregator.GetEvent<LogicToVmEvent>().Publish(data);
        }
    }
}
