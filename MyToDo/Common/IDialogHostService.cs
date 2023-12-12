using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common
{
    public interface IDialogHostService : IDialogService
    {
        Task<IDialogService> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root");
    }
}
