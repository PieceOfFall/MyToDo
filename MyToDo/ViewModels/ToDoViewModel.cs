﻿using MyToDo.Common.Models;
using MyToDo.Common.Models.db;
using MyToDo.Service;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace MyToDo.ViewModels
{
    public class ToDoViewModel : NavigationViewModel
    {
        public ToDoViewModel(IToDoService service,IContainerProvider provider):base(provider)
        {
            toDoDtos = new ObservableCollection<ToDoDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectCommand = new DelegateCommand<ToDoDto>(Selected);
            CurrentTodo = new ToDoDto();
            this.service = service;
        }

        private IToDoService service;

        private ObservableCollection<ToDoDto> toDoDtos;

        public DelegateCommand<string> ExecuteCommand { get; private set; }

        public DelegateCommand<ToDoDto> SelectCommand { get; private set; }

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

        private void Execute(string operation)
        {
            switch(operation)
            {
                case "新增":Add(); break;
                case "查询":Query();break;
                case "保存":Save();break;
            }
        }

        private async void Query()
        {
            UpdateLoading(true);
            try
            {
                ToDoDtos.Clear();
                var todoRet = await service.QueryAsync(new QueryToDo() { pageNum = 1, pageSize = 15, Title = Search });

                if (todoRet.data != null)
                    foreach (var item in todoRet.data.list)
                    {
                        ToDoDtos.Add(item);
                    }
            }
            catch (Exception ex)
            {

            }
            finally { UpdateLoading(false); }
        }

        private async void Save()
        {
            
                if (string.IsNullOrWhiteSpace(CurrentTodo.Title) ||
                string.IsNullOrWhiteSpace(CurrentTodo.Content))
                    return;

                UpdateLoading(true);
            try
            {
                if (CurrentTodo.Id != 0)
                {
                    var ret = await service.UpdateAsync(CurrentTodo);
                    if (ret.data != 0)
                    {
                        GetTodoAsync();
                        IsRightDrawerOpen = false;
                    }
                }
                else
                {
                    var ret = await service.AddAsync(CurrentTodo);
                    if (ret.data != 0)
                    {
                        GetTodoAsync();
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

        private void Add()
        {
            IsRightDrawerOpen = true;
            CurrentTodo = new ToDoDto();
        }

        private async void Selected(ToDoDto obj)
        {
            UpdateLoading(true);
            try
            {
                IsRightDrawerOpen = true;

                var todoRet = await service.GetFirstOfDefaultAsync(obj.Id);
                if (todoRet.status == 200 && todoRet.data != null)
                {
                    CurrentTodo = todoRet.data;
                    IsRightDrawerOpen = true;
                }
            } catch (Exception ex)
            {

            } finally { UpdateLoading(false); }
            
        }
        
        async void GetTodoAsync()
        {
            UpdateLoading(true);
            try
            {
                ToDoDtos.Clear();
                var todoRet = await service.GetAllasync(new PageOptions() { pageNum = 1, pageSize = 15 });

                if (todoRet.data != null)
                    foreach (var item in todoRet.data.list)
                    {
                        ToDoDtos.Add(item);
                    }
            } catch(Exception ex)
            {

            } finally { UpdateLoading(false); }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            GetTodoAsync();
        }
    }
}
