using Prism.Events;

namespace MyToDo.Common.Events
{
    public class MessageEvent : PubSubEvent<MessageModel>
    {

    }

    public class MessageModel
    {
        public string Message { get; set; } = "";

        public string Filter { get; set; } = "";
    }
}
