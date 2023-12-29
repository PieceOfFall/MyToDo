using HandyControl.Controls;
using HandyControl.Data;
using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Common.Models.db;
using MyToDo.Extensions;
using MyToDo.Service;
using MyToDo.Views.Dialogs;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace MyToDo.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {
        private readonly IToDoService toDoService;

        private readonly IMemoService memoService;

        private readonly IRegionManager regionManager;

        // 标志位，检查是否正在进行完成待办Command
        private bool isToDoCompleting = false;

        // 待办完成计时器 
        private Timer? todoTimer;

        // 汇总
        private Summary? summary;

        public Summary Summary
        {
            get => summary!;
            set { summary = value; RaisePropertyChanged(); }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; RaisePropertyChanged(); }
        }

        private string year;

        public string Year
        {
            get { return year; }
            set { year = value; RaisePropertyChanged(); }
        }

        private string month;

        public string Month
        {
            get { return month; }
            set { month = value; RaisePropertyChanged(); }
        }
        private string day;

        public string Day
        {
            get { return day; }
            set { day = value; RaisePropertyChanged(); }
        }
        private string dayOfWeek;

        public string DayOfWeek
        {
            get { return dayOfWeek; }
            set { dayOfWeek = value; RaisePropertyChanged(); }
        }

        private MainViewModel mainViewModel;

        private ObservableCollection<TaskBar> taskBars;
        private ObservableCollection<ToDoDto> toDoDtos;
        private ObservableCollection<MemoDto> memoDtos;

        public IndexViewModel(IContainerProvider provider,
            IDialogHostService dialog) : base(provider)
        {
            var currentDate= DateTime.Now;
            Year = currentDate.Year.ToString();
            Month = currentDate.Month.ToString();
            Day = currentDate.Day.ToString();
            DayOfWeek = currentDate.DayOfWeek.ToString();
            this.dialog = dialog;
            regionManager = provider.Resolve<IRegionManager>();
            toDoService = provider.Resolve<IToDoService>();
            memoService = provider.Resolve<IMemoService>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            EditToDoCommand = new DelegateCommand<ToDoDto>(AddToDo);
            ToDoCompleteComand = new DelegateCommand<ToDoDto>(ToDoComplete);
            NavigateCommand = new DelegateCommand<TaskBar>(Navigate);
            summary = new Summary();
            taskBars = [];
            toDoDtos = [];
            memoDtos = [];
            
        }

        private IDialogHostService dialog { get; set; }

        public DelegateCommand<string> ExecuteCommand { get; private set; }

        public DelegateCommand<ToDoDto> EditToDoCommand { get; private set; }

        public DelegateCommand<ToDoDto> ToDoCompleteComand { get; private set; }

        public DelegateCommand<TaskBar> NavigateCommand { get; private set; }

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
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

  

        void CreateTaskBars(Summary summary)
        {
            TaskBars.Clear();
            TaskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "汇总", Color = "#ef5777", Content = $"{summary.Sum}", Target="ToDoView" });
            TaskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", Title = "已完成", Color = "#4bcffa", Content = $"{summary.CompletedCount}", Target = "ToDoView" });
            TaskBars.Add(new TaskBar() { Icon = "ChartLineVariant", Title = "完成比例", Color = "#05c46b", Content = summary.CompletedRatio });
            TaskBars.Add(new TaskBar() { Icon = "PlaylistStar", Title = "备忘录 (敬请期待...)", Color = "#d2dae2", Content = string.Empty });
        }

        private async void  RefreshTaskBars()
        {
            var ret = await toDoService.SummaryAsync();
            if (ret.status == 200 && ret.data != null)
            {
                var currentSummary = ret.data;
                TaskBars[0].Content = currentSummary.Sum.ToString();
                TaskBars[1].Content = currentSummary.CompletedCount.ToString();
                TaskBars[2].Content = currentSummary.CompletedRatio;
            }
        }

        private void Execute(string targetCommand)
        {
            switch (targetCommand)
            {
                case "新增待办": AddToDo(null); break;
                case "新增备忘录": AddMemo(); break;
            }
        }

        async void AddToDo(ToDoDto? model)
        {
            // 如果正在执行完成待办，则退出
            if(isToDoCompleting)
                return;

            var param =  new DialogParameters();
            if (model != null)
                param.Add("Value", model);

            var ret = await dialog.ShowDialog("AddToDoView", param);
            if(ret.Result == ButtonResult.OK)
            {
                var todo = ret.Parameters.GetValue<ToDoDto>("Value");

                if(todo.Id == 0)
                {
                    var addRet = await toDoService.AddAsyncByName(todo);
                    if(addRet.data > 0 )
                        GetTodos();
                }
                else
                {
                    var updateRet = await toDoService.UpdateAsync(todo);
                    if (updateRet.data > 0)
                        GetTodos();
                }
                RefreshTaskBars();
            }
        }

        private async void ToDoComplete(ToDoDto dto)
        {
            isToDoCompleting = true;
            // 设置防抖标志位阻止Command冒泡
            if (todoTimer == null)
                todoTimer = new Timer(e =>
                {
                    isToDoCompleting = false;
                    todoTimer?.Dispose();
                    todoTimer = null;
                },null, 500,Timeout.Infinite);
            else
            {
                todoTimer.Change(500,Timeout.Infinite);
            }
            dto.Status = 1;
            var ret = await toDoService.UpdateAsync(dto);
            if(ret.data >0)
            {
                GetTodos();
                RefreshTaskBars();
            }
        }

        private void Navigate(TaskBar bar)
        {
            if(string.IsNullOrWhiteSpace(bar.Target)) 
                return;
            var param = new NavigationParameters();

            if(bar.Title == "已完成")
            {
                param.Add("Value", 2);
            } 
            else if(bar.Title == "汇总")
            {
                param.Add("Value", 0);
            }

            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(bar.Target, param);
        }

        void AddMemo()
        {
            dialog.ShowDialog("AddMemoView", null);
        }

        async void GetTodos()
        {
            ToDoDtos.Clear();
            var todoRet = await toDoService.QueryAsync(new { pageNum = 1, pageSize = 500,Status = 0,position = 2 });
            if (todoRet.data != null)
                foreach (var item in todoRet.data.list)
                {
                    ToDoDtos.Add(item);
                }
        }

        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            Username = AppSession.Username;
            base.OnNavigatedTo(navigationContext);
            var ret = await toDoService.SummaryAsync();
            if (ret.status == 200)
            {
                summary = ret.data;
                if (summary != null)
                    CreateTaskBars(summary);
            }

            GetTodos();
        }

    }
}
