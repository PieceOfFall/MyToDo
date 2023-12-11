using Prism.Events;

namespace MyToDo.Common.Events
{
    public class UpdateModel
    {
        public bool IsWindowOpen { get; set; }
    }

    public class UpdateLoadingEvent : PubSubEvent<UpdateModel>
    {

    }
}
