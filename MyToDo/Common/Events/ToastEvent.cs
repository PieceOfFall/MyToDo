using Prism.Events;

namespace MyToDo.Common.Events
{
    public class ToastEvent : PubSubEvent<ToastModel>
    {

    }

    public class ToastModel
    {

        public string Message { get; set; } = "";

        public string Title { get; set; } = "";
    }
}
