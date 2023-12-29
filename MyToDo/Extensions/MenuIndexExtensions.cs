using MyToDo.Common.Events;
using Prism.Events;

namespace MyToDo.Extensions
{
    public static class MenuIndexExtensions
    {
        /* 注册修改菜单索引事件 */
        public static void RegisterMenuIndex(this IEventAggregator aggregator, Action<MenuIndexModel> action)
        {
            aggregator.GetEvent<MenuIndexEvent>().Subscribe(action);
        }

        /* 发送修改菜单索引消息 */
        public static void SendMenuIndex(this IEventAggregator aggregator, MenuIndexModel toast)
        {
            aggregator.GetEvent<MenuIndexEvent>().Publish(toast);
        }
    }
}


