using DryIoc;
using MyToDo.Common;
using MyToDo.Service;
using MyToDo.Service.impl;
using MyToDo.ViewModels;
using MyToDo.ViewModels.Dialogs;
using MyToDo.Views;
using MyToDo.Views.Dialogs;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System.Windows;

namespace MyToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {

        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }


        public static void LoginOut(IContainerProvider provider)
        {
            var mainViewModel = provider.Resolve<MainViewModel>();
            
            Current.MainWindow.Hide();

            var dialogService = provider.Resolve<IDialogService>();

            dialogService.ShowDialog("LoginView", cb =>
            {
                if (cb.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }
                if (Current.MainWindow.DataContext is IConfigureService service)
                    service.Configure();
                Current.MainWindow.Show();
            });
        }

        protected override void OnInitialized()
        {
            var dialogService = Container.Resolve<IDialogService>();
            
            dialogService.ShowDialog("LoginView", cb =>
            {
                if(cb.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }
                if (Current.MainWindow.DataContext is IConfigureService service)
                    service.Configure();
                base.OnInitialized();
            });
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer()
                .Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));
            containerRegistry.GetContainer().RegisterInstance(@"http://192.168.3.219:8989/", serviceKey: "webUrl");

            // 服务
            containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.Register<IToDoService, ToDoService>();
            containerRegistry.Register<IMemoService, MemoService>();
            containerRegistry.Register<IDialogHostService, DialogHostService>();

            // 自定义对话框
            containerRegistry.RegisterForNavigation<MsgView,MsgViewModel>();
            containerRegistry.RegisterForNavigation<AddToDoView,AddToDoViewModel>();
            containerRegistry.RegisterForNavigation<AddMemoView,AddMemoViewModel>();

            containerRegistry.RegisterDialog<LoginView,LoginViewModel>();

            // 窗口
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>();

            // 子窗口
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<AboutView>();

        }
    }

}
