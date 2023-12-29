using Prism.Events;
namespace MyToDo.Common.Events
{
    public class MenuIndexEvent : PubSubEvent<MenuIndexModel>
    {

    }

    public class MenuIndexModel
    {
        public int Index { get; set; }

    }
}
