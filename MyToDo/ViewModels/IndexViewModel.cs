using MyToDo.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;

namespace MyToDo.ViewModels
{
    public class IndexViewModel : BindableBase
    {
        private ObservableCollection<TaskBar> taskBars;
        private ObservableCollection<ToDoDto> toDoDtos;
        private ObservableCollection<MemoDto> memoDtos;

        public IndexViewModel(IDialogService dialog)
        {
            
            taskBars = new ObservableCollection<TaskBar>();
            CreateTaskBars();
            toDoDtos = new ObservableCollection<ToDoDto>();
            memoDtos = new ObservableCollection<MemoDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            Dialog = dialog;
        }

        public IDialogService Dialog { get; set; }

        public DelegateCommand<string> ExecuteCommand { get; set; }

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value;RaisePropertyChanged(); }
        }

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }

  

        void CreateTaskBars()
        {
            TaskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "汇总", Color = "#FF0CA0FF", Content = "9", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", Title = "已完成", Color = "#FF1ECA3A", Content = "9", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "ChartLineVariant", Title = "完成比例", Color = "#FF02C6DC", Content = "100%", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "PlaylistStar", Title = "备忘录", Color = "#FFFFA000", Content = "19", Target = "" });
        }

        private void Execute(string targetCommand)
        {
            switch (targetCommand)
            {
                case "新增待办": AddToDo(); break;
                case "新增备忘录": AddMemo(); break;
            }
        }

        void AddToDo()
        {
            Dialog.ShowDialog("AddToDoView");
        }

        void AddMemo()
        {
            Dialog.ShowDialog("AddMemoView");
        }

    }
}
