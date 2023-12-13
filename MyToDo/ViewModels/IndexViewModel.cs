using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Service;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;

namespace MyToDo.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {
        private readonly IToDoService toDoService;

        private readonly IMemoService memoService;

        private ObservableCollection<TaskBar> taskBars;
        private ObservableCollection<ToDoDto> toDoDtos;
        private ObservableCollection<MemoDto> memoDtos;

        public IndexViewModel(IContainerProvider provider,
            IDialogHostService dialog) : base(provider)
        {
            this.dialog = dialog;
            taskBars = new ObservableCollection<TaskBar>();
            CreateTaskBars();
            toDoDtos = new ObservableCollection<ToDoDto>();
            memoDtos = new ObservableCollection<MemoDto>();
            toDoService = provider.Resolve<IToDoService>();
            memoService = provider.Resolve<IMemoService>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            EditToDoCommand = new DelegateCommand<ToDoDto>(AddToDo);
            ToDoCompleteComand = new DelegateCommand<ToDoDto>(ToDoComplete);
            
        }

        private IDialogHostService dialog { get; set; }

        public DelegateCommand<string> ExecuteCommand { get; private set; }

        public DelegateCommand<ToDoDto> EditToDoCommand { get; private set; }

        public DelegateCommand<ToDoDto> ToDoCompleteComand { get; private set; }

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
                case "新增待办": AddToDo(null); break;
                case "新增备忘录": AddMemo(); break;
            }
        }

        async void AddToDo(ToDoDto? model)
        {
            var param =  new DialogParameters();
            if (model != null)
                param.Add("Value", model);

            var ret = await dialog.ShowDialog("AddToDoView", param);
            if(ret.Result == ButtonResult.OK)
            {
                var todo = ret.Parameters.GetValue<ToDoDto>("Value");

                if(todo.Id == 0)
                {
                    var addRet = await toDoService.AddAsync(todo);
                    if(addRet.data > 0 )
                        GetTodos();
                }
                else
                {
                    var updateRet = await toDoService.UpdateAsync(todo);
                    if (updateRet.data > 0)
                        GetTodos();
                }
            }
        }

        private async void ToDoComplete(ToDoDto dto)
        {
            var ret = await toDoService.UpdateAsync(dto);
            if(ret.data >0)
            {
                GetTodos();
            }
        }

        void AddMemo()
        {
            dialog.ShowDialog("AddMemoView", null);
        }

        async void GetTodos()
        {
            ToDoDtos.Clear();
            var todoRet = await toDoService.QueryAsync(new QueryToDo() { pageNum = 1, pageSize = 15,Status = 0 });
            if (todoRet.data != null)
                foreach (var item in todoRet.data.list)
                {
                    ToDoDtos.Add(item);
                }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            GetTodos();
        }

    }
}
