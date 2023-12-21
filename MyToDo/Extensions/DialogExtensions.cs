using MyToDo.Common;
using MyToDo.Common.Events;
using Prism.Events;
using Prism.Services.Dialogs;

namespace MyToDo.Extensions
{
    public static class DialogExtensions
    {
        /* 询问窗口 */
        public static async Task<IDialogResult> Question(this IDialogHostService dialogHost,
            string title, string content, string dialogHostName="Root")
        {
            var param = new DialogParameters
            {
                { "Title", title },
                { "Content", content },
                { "dialogHostName", dialogHostName }
            };

            var ret = await dialogHost.ShowDialog("MsgView",param,dialogHostName);
            return ret;
        }


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

        /* 注册提示消息事件 */
        public static void RegisterMessage(this IEventAggregator aggregator,Action<MessageModel> action,
            string filterName = "Main")
        {
            aggregator.GetEvent<MessageEvent>().Subscribe(action,
                ThreadOption.PublisherThread, true, (m) =>
                {
                    return m.Filter.Equals(filterName);
                });
        }

        /* 发送提示消息 */
        public static void SendMessage(this IEventAggregator aggregator, string message,
            string filterName = "Main")
        {
            aggregator.GetEvent<MessageEvent>().Publish(new MessageModel()
            {
                Filter = filterName,
                Message = message
            });
        }

    }
}
