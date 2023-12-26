using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace MyToDo.ViewModels.Dialogs
{
    public class AddToDoViewModel : BindableBase, IDialogHostAware
    {
        public AddToDoViewModel()
        {
            isEidt = false;
            urgencySelectedIndex = 0;
            username = AppSession.Username;
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        private ToDoDto model;

        /* 新增或编辑的实体 */
        public ToDoDto Model
        {
            get { return model; }
            set { model = value; RaisePropertyChanged(); }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; RaisePropertyChanged(); }
        }

        private int urgencySelectedIndex;

        private bool isEidt;

        public bool IsEdit
        {
            get { return isEidt; }
            set { isEidt = value; RaisePropertyChanged(); }
        }

        public int UrgencySelectedIndex
        {
            get { return urgencySelectedIndex; }
            set 
            {
                urgencySelectedIndex = value; 
                RaisePropertyChanged();
                Model.Urgency = (3 - urgencySelectedIndex);
            }
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Model.Title) ||
                string.IsNullOrWhiteSpace(Model.Content))
                return;

            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                var param = new DialogParameters
                {
                    { "Value", Model }
                };
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
            }
        }

        private void Cancel()
        {
            if(DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

            if (parameters.ContainsKey("Value"))
            {
                IsEdit = true;
                Model = parameters.GetValue<ToDoDto>("Value");
                Model.ReceiverName = AppSession.Username;
            }
            else
            {
                IsEdit = false;
                Model = new ToDoDto
                {
                    DueDate = DateTime.Now,
                    Urgency = 3
                };
            }

        }

    }
}
