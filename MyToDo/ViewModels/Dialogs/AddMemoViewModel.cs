using Prism.Services.Dialogs;

namespace MyToDo.ViewModels.Dialogs
{
    public class AddMemoViewModel : IDialogAware
    {
        public string Title { get; set; }

        public event Action<IDialogResult> RequestClose;

        public AddMemoViewModel()
        {

        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
