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
            var param = new DialogParameters();
            param.Add("Title", title);
            param.Add("Content", content);
            param.Add("dialogHostName", dialogHostName);

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


    }
}
