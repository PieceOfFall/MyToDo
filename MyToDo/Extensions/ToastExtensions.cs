using MyToDo.Common.Events;
using Prism.Events;

namespace MyToDo.Extensions
{
    public static class ToastExtensions
    {
        /* 注册提示消息事件 */
        public static void RegisterToast(this IEventAggregator aggregator, Action<ToastModel> action)
        {
            aggregator.GetEvent<ToastEvent>().Subscribe(action);
        }

        /* 发送提示消息 */
        public static void SendToast(this IEventAggregator aggregator, ToastModel toast)
        {
            aggregator.GetEvent<ToastEvent>().Publish(toast);
        }
    }
}
