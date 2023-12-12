using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common
{
    internal interface IDialogHostAware : IDialogAware
    {
        // 所属dialogHost名称
        string DialogHostName { get;set; }

        // 打开过程中执行
        void onDialogOpen(IDialogParameters parameters);

        // 确定
        DelegateCommand SaveCommand { get; set; }

        // 取消
        DelegateCommand CancelCommand { get; set; }
    }
}
