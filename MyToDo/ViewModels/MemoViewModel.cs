using MyToDo.Common.Models;
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
        public MemoViewModel()
        {
            memoDtos = [];
            CreateToDoList();
            AddCommand = new DelegateCommand(Add);
        }

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

        void CreateToDoList()
        {
            for (int i = 0; i < 20; i++)
            {
                MemoDtos.Add(new MemoDto()
                {
                    Title = "标题" + i,
                    Content = "测试数据"
                });
            }
        }
    }
}
