using MyToDo.Common.Models;
using MyToDo.Service;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class MemoViewModel : BindableBase
    {
        public MemoViewModel(IMemoService service)
        {
            this.service = service;
            memoDtos = new ObservableCollection<MemoDto>();
            AddCommand = new DelegateCommand(Add);
            CreateToDoList();
        }

        private IMemoService service;

        private ObservableCollection<MemoDto> memoDtos;

        public DelegateCommand AddCommand { get; private set; }

        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get => isRightDrawerOpen;
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }


        public ObservableCollection<MemoDto> MemoDtos
        {
            get => memoDtos;
            set { memoDtos = value; RaisePropertyChanged(); }
        }

        private void Add()
        {
            IsRightDrawerOpen = true;
        }

        async void CreateToDoList()
        {
            var memoRet = await service.GetAllasync(new PageOptions() { pageNum = 1, pageSize = 15 });
            foreach (var item in memoRet.data.list)
            {
                MemoDtos.Add(item);
            }
        }
    }
}
