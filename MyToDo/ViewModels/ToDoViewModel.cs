﻿using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using MyToDo.Service;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows;

namespace MyToDo.ViewModels
{
    public class ToDoViewModel : NavigationViewModel
    {
        private readonly IDialogHostService dialogHost;

        //private readonly IEventAggregator aggregator;

        public ToDoViewModel(IToDoService service,IContainerProvider provider):base(provider)
        {
            receiverName = string.Empty;
            search = string.Empty;
            selectedIndex = 1;
            AddMethodSelectedIndex = -1;
            loginService = provider.Resolve<ILoginService>();
            this.service = service;
            toDoDtos = [];
            currentTodo = new ToDoDto();
            currentUrgencyColor = "Black";
            maxPageCount = 10;
            pageIndex = 1;
            sender = string.Empty;
            isSender = false;
            isReceiver = true;
            isEnableDeleteButton = false;
            isEnableDeleteButton = false;
            GetSimpleDeptTree();
            aggregator.RegisterLogicToVm(e =>
            {
                if (e.VmName == "ToDoViewModel")
                {
                    DeptEmpName = (string)e.Data;
                }
            });

            dialogHost = provider.Resolve<IDialogHostService>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectCommand = new DelegateCommand<ToDoDto>(Selected);
            SelectRadioCommand = new DelegateCommand<string>(SelectRadio);
            DeleteCommand = new DelegateCommand<ToDoDto>(Delete);
        }

        private readonly IToDoService service;

        private ObservableCollection<ToDoDto> toDoDtos;

        private ILoginService loginService;

        public ILoginService LoginService
        {
            get { return loginService; }
            set { loginService = value; }
        }

        private bool isEdit;

        public bool IsEdit
        {
            get { return isEdit; }
            set 
            {
                isEdit = value;
                IsAddByName = (!isEdit & AddByName);
                RaisePropertyChanged(); 
            }
        }

        private string receiverName;

        public string ReceiverName
        {
            get { return receiverName; }
            set { receiverName = value; RaisePropertyChanged(); }
        }

        private string sender;

        private ObservableCollection<TreeItem> selectDept;

        public ObservableCollection<TreeItem> SelectDept
        {
            get { return selectDept; }
            set { selectDept = value; RaisePropertyChanged(); }
        }

        public string Sender
        {
            get { return sender; }
            set { sender = value; RaisePropertyChanged(); }
        }

        private int addMethodSelectedIndex;

        public int AddMethodSelectedIndex
        {
            get { return addMethodSelectedIndex; }
            set { addMethodSelectedIndex = value; RaisePropertyChanged(); }
        }

        private int urgencySelectedIndex;

        public int UrgencySelectedIndex
        {
            get { return urgencySelectedIndex; }
            set 
            {
                CurrentUrgencyColor = MapUrgencyToColor(3 - value);

                urgencySelectedIndex = value; RaisePropertyChanged(); 
            }
        }

        private int maxPageCount;

        public int MaxPageCount
        {
            get { return maxPageCount; }
            set { maxPageCount = value; RaisePropertyChanged(); }
        }

        private int pageIndex;

        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; RaisePropertyChanged(); }
        }

        private bool isSender;

        public bool IsSender
        {
            get { return isSender; }
            set { isSender = value; }
        }

        private bool isReceiver;

        public bool IsReceiver
        {
            get { return isReceiver; }
            set 
            { 
                isReceiver = value;
                if (value && isSender == false)
                    IsEnableDeleteButton = false;
                else
                    IsEnableDeleteButton = true;
            }
        }

        private bool isEnableDeleteButton;

        public bool IsEnableDeleteButton
        {
            get { return isEnableDeleteButton; }
            set { isEnableDeleteButton = value; RaisePropertyChanged(); }
        }

        private bool isAddByName;

        public bool IsAddByName
        {
            get { return isAddByName; }
            set { isAddByName = value; RaisePropertyChanged(); }
        }



        private static string MapUrgencyToColor(int? urgency)
        {
            return urgency switch
            {
                3 => "Red",
                2 => "#d4675f",
                1 => "#d4b8b6",
                0 => "Black",
                _ => "Black",
            };
        }

        private string currentUrgencyColor;

        public string CurrentUrgencyColor
        {
            get { return currentUrgencyColor; }
            set { currentUrgencyColor = value; RaisePropertyChanged(); }
        }

        private bool addById = false;

        public bool AddById
        {
            get { return addById; }
            set { addById = value; RaisePropertyChanged(); }
        }

        private bool addByName = false;

        public bool AddByName
        {
            get { return addByName; }
            set 
            { 
                addByName = value;
                IsAddByName = (!isEdit & AddByName);
                RaisePropertyChanged(); 
            }
        }

        private bool addByDept = false;

        public bool AddByDept
        {
            get => addByDept;
            set
            {
                addByDept = value;
                RaisePropertyChanged();
            }
        }

        private TreeItem selectedTreeItem;

        public TreeItem SelectedTreeItem
        {
            get { return selectedTreeItem; }
            set { selectedTreeItem = value; RaisePropertyChanged(); }
        }

        private string deptEmpName;

        public string DeptEmpName
        {
            get { return deptEmpName; }
            set { deptEmpName = value; RaisePropertyChanged(); }
        }


        private void SelectRadio(string radioIndex)
        {
            switch(radioIndex)
            {
                case "0": IsSender = true; IsReceiver = false; break;
                case "1": IsSender = false; IsReceiver = true; break;
                case "2": IsSender = true; IsReceiver = true; break;
            }
            Query();
        }

        public DelegateCommand<string> ExecuteCommand { get; private set; }

        public DelegateCommand<ToDoDto> SelectCommand { get; private set; }

        public DelegateCommand<ToDoDto> DeleteCommand { get; private set; }

        public DelegateCommand<string> SelectRadioCommand { get; private set; }

        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get => isRightDrawerOpen;
            set { isRightDrawerOpen = value;RaisePropertyChanged(); }
        }

        private ToDoDto currentTodo;

        public ToDoDto CurrentTodo
        {
            get { return currentTodo; }
            set { currentTodo = value; RaisePropertyChanged(); }
        }

        /* 编辑 选中、新增时对象 */
        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get => toDoDtos;
            set { toDoDtos = value;RaisePropertyChanged(); }
        }

        private string search;

        /* 搜索条件 */
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }

        private int selectedIndex;

        /* 下拉列表选中状态值 */
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged();Query(); }
        }

        private void Execute(string operation)
        {
            switch(operation)
            {
                case "新增":  Add()   ;break;
                case "查询": SearchInputQuery(); break;
                case "保存":  Save()  ;break;
                case "页码更新": Query(); break;
                case "选择添加方式": ChooseAddMethod();break;
                case "选择紧急程度": ChooseUrgency();break;
            }
        }

        private Timer? searchTimer = null;

        private void SearchInputQuery()
        {
            if (searchTimer == null)
            {
                
                searchTimer = new Timer((e) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Query();
                        searchTimer?.Dispose();
                        searchTimer = null;
                    });
                    
                },null,500,Timeout.Infinite);
            }
            else
            {
                searchTimer.Change(500, Timeout.Infinite);
            }
        }

        private async void Query()
        {
            try
            {
                int? Status = SelectedIndex switch
                {
                    0 => null,
                    1 => 0,
                    2 => 1,
                    _ => throw new NotImplementedException()
                };
                var position = 0;
                if(isSender)
                    position |= 1;
                if(isReceiver)
                    position |= 2;
                position = position == 0 ? 3: position;

                var todoRet = await service.QueryAsync(new { pageNum = PageIndex, pageSize = 15, Title = Search, Status, position });

                if (todoRet.data != null)
                {
                    ToDoDtos.Clear();
                    // 更新分页
                    MaxPageCount = todoRet.data.pages;
                    PageIndex = todoRet.data.pageNum;
                    // 更新数据
                    foreach (var item in todoRet.data.list)
                    {
                        ToDoDtos.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void Save()
        {
                if (string.IsNullOrWhiteSpace(CurrentTodo.Title) ||
                string.IsNullOrWhiteSpace(CurrentTodo.Content))
                    return;

                UpdateLoading(true);
            try
            {
                // 更新
                if (CurrentTodo.Id != 0)
                {
                    var ret = await service.UpdateAsync(CurrentTodo);
                    if (ret.data != 0)
                    {
                        Query();
                        IsRightDrawerOpen = false;
                    }
                }
                else
                {
                    //添加
                    CurrentTodo.ReceiverName = ReceiverName;
                    ApiResponse<int>? ret = null;
                    if (AddById)
                    {
                       ret = await service.AddAsync(CurrentTodo);
                    } 
                    else if(AddByName)
                    {
                        ret = await service.AddAsyncByName(CurrentTodo);
                    }
                    else if(AddByDept)
                    {
                        CurrentTodo.ReceiverName = DeptEmpName;
                        ret = await service.AddAsyncByName(CurrentTodo);
                    }
                     
                    if (ret!=null && ret.data != 0)
                    {
                        Query();
                        IsRightDrawerOpen = false;
                    }
                }

            }
            catch(Exception ex)
            {

            }
            finally
            {
                UpdateLoading(false);
            }
            
        }

        private void ChooseAddMethod()
        {
            switch(AddMethodSelectedIndex)
            {
                case 0: AddByName = true; AddById = false; AddByDept = false; break;
                case 1: AddById = false; AddByName = false; AddByDept = true; break;
            }
        }

        private void ChooseUrgency()
        {
            CurrentTodo.Urgency = 3 - UrgencySelectedIndex;
        }


        private void Add()
        {
            ReceiverName = "";
            Sender = "";
            ReceiverName = "";
            IsEdit = false;
            AddMethodSelectedIndex = 0;
            IsRightDrawerOpen = true;
            CurrentTodo = new ToDoDto
            {
                Urgency = 3,
                ReceiverId = 0,
                DueDate = DateTime.Now,
            };
            UrgencySelectedIndex = 0;
            CurrentUrgencyColor = MapUrgencyToColor(3);
        }

        private async void Selected(ToDoDto obj)
        {
            AddByDept = false;
            IsEdit = true;
            UpdateLoading(true);
            try
            {
                var todoRet = await service.SelectToDo(obj.Id);
                if (todoRet.status == 200 && todoRet.data != null)
                {
                    ReceiverName = todoRet.data.SelectedReceiver;
                    Sender = todoRet.data.SelectedSender;
                    if (todoRet.data.Urgency != null)
                        UrgencySelectedIndex = 3 - (int)todoRet.data.Urgency;
                    CurrentTodo = todoRet.data;
                    IsRightDrawerOpen = true;
                }
            } catch (Exception ex)
            {

            } finally { UpdateLoading(false); }
            
        }

        private async void Delete(ToDoDto dto)
        {
            var dialogRet = await dialogHost.Question(title: "提示", content: $"确认删除待办: '{dto.Title}' 吗？");

            if (dialogRet.Result != Prism.Services.Dialogs.ButtonResult.OK)
                return;

            var ret = await service.DeleteAsync(dto.Id);
            if (ret.data != 0)
            {
                Query();
            }
        }

        private async void GetSimpleDeptTree()
        {
            SelectDept =
            [
                (await loginService.GetDeptTreeAsync()).data! 
            ];
        }

        private bool isChanging = false;
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            aggregator.SendMenuIndex(new Common.Events.MenuIndexModel
            {
                Index = 1
            });
            if (navigationContext.Parameters.ContainsKey("Value"))
            {
                SelectedIndex = navigationContext.Parameters.GetValue<int>("Value");
                var timer = new System.Timers.Timer(500);
                isChanging = true;
                timer.Elapsed += (s, q) =>
                {
                    isChanging = false;
                    ((System.Timers.Timer)s!).Stop();
                };
                timer.Start();
            }
            else
            {
                if (isChanging) return;
                SelectedIndex = 1;
            }
        }
    }
}
