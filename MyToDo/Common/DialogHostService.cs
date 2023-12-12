using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Windows;

namespace MyToDo.Common
{
    public class DialogHostService : DialogService, IDialogHostService
    {
        private readonly IContainerExtension _containerExtension;

        public DialogHostService(IContainerExtension containerExtension) : base(containerExtension)
        {
            _containerExtension = containerExtension;
        }

        public async Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root")
        {
            if(parameters == null) 
                parameters = new DialogParameters();

            // 从容器当中去除弹出窗口的实例
            var content = _containerExtension.Resolve<object>(name);

            // 验证实例的有效性
            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("content must be a Framework Element");

            // 
            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
                ViewModelLocator.SetAutoWireViewModel(view, true);

            if (!(dialogContent.DataContext is IDialogHostAware viewModel))
                throw new NullReferenceException("a dialog's viewmodel must be implemented");

            viewModel.DialogHostName = dialogHostName;

            DialogOpenedEventHandler eventHandler = (sender, eventArgs) =>
            {
                if (viewModel is IDialogHostAware aware)
                {
                    aware.OnDialogOpened(parameters);
                }
                eventArgs.Session.UpdateContent(content);
            };

            return (IDialogResult)await DialogHost.Show(dialogContent,viewModel.DialogHostName,eventHandler);
        }
    }
}
