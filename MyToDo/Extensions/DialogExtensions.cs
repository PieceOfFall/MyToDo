using MyToDo.Common.Events;
using Prism.Events;

namespace MyToDo.Extensions
{
    public static class DialogExtensions
    {
        /* 推送等待消息 */
        public static void UpdateLoading(this IEventAggregator aggregator,UpdateModel model)
        {
            aggregator.GetEvent<UpdateLoadingEvent>().Publish(model);
        }

        /* 注册等待消息 */
        public static void Register(this IEventAggregator aggregator,Action<UpdateModel> action)
        {
            aggregator.GetEvent<UpdateLoadingEvent>().Subscribe(action);
        }
    }
}
