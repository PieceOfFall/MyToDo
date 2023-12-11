using MyToDo.Common.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class IndexViewModel : BindableBase
    {
        private ObservableCollection<TaskBar> taskBars;
        private ObservableCollection<ToDoDto> toDoDtos;
        private ObservableCollection<MemoDto> memoDtos;

        public IndexViewModel()
        {
            taskBars = new ObservableCollection<TaskBar>();
            CreateTaskBars();
            toDoDtos = new ObservableCollection<ToDoDto>();
            memoDtos = new ObservableCollection<MemoDto>();
            CreateMemoAndToDo();
        }

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

        void CreateMemoAndToDo()
        {
            for (int i = 0; i < 10; i++)
            {
                ToDoDtos.Add(new ToDoDto() { Title = "待办"+i, Content = "正在处理中..." });
                MemoDtos.Add(new MemoDto() { Title = "备忘"+i, Content = "我的密码" });
            }
        }

    }
}
