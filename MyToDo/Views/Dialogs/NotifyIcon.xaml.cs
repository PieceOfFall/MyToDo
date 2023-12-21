using HandyControl.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyToDo.Views.Dialogs
{
    /// <summary>
    /// NotifyIcon.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyIcon : UserControl
    {
        private readonly PushMainWindow2TopCommand pushMainWindow2Top;

        public NotifyIcon()
        {
            InitializeComponent();
            Extensions.NotifyIcon.ShowBalloonTip = notifyIcon.ShowBalloonTip;
            pushMainWindow2Top = new PushMainWindow2TopCommand();
        }
        private void ButtonPush_OnClick(object sender, RoutedEventArgs e) => pushMainWindow2Top.Execute(this);
    }
}
