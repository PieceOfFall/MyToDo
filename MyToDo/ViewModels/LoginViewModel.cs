using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using MyToDo.Service;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace MyToDo.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {

        public LoginViewModel(ILoginService loginService, IEventAggregator aggregator)
        {
            Account = Properties.Settings.Default.Account;
            Password = Properties.Settings.Default.Password;
            this.loginService = loginService;
            this.aggregator = aggregator;
            ExecuteCommand = new DelegateCommand<string>(Execute);
        }

        private readonly IEventAggregator aggregator;

        private readonly ILoginService loginService;

        public string Title { get; set; } = "骑鲸协同";

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand<string> ExecuteCommand { get; set; }

        private string account;

        public string Account
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            LogOut();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        private void Execute(string operation)
        {
            switch (operation)
            {
                case "Login": Login(); break;
                case "LogOut": LogOut(); break;
            }
        }

        private void LogOut()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        private async void Login()
        {
            if (string.IsNullOrWhiteSpace(Account) ||
                string.IsNullOrWhiteSpace(Password))
                return;

            var ret = await loginService.LoginAsync(new UserDto()
            {
                Account = Account,
                Password = Password
            });

            if(ret.status == 200 && ret.data != null)
            {
                AppSession.Token = ret.data;
                AppSession.Username = Account;
                Properties.Settings.Default.Account = Account;
                Properties.Settings.Default.Password = Password;
                Properties.Settings.Default.Save();

                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            }
            else
            {
                aggregator.SendMessage("账号或密码错误","Login");
            }
        }
    }
}
