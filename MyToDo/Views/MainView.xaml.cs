using MyToDo.Common;
using MyToDo.Extensions;
using Prism.Events;
using System.Windows;
using System.Windows.Input;


namespace MyToDo.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(IEventAggregator aggregator, IDialogHostService dialogHostService)
        {
            // 注册提示消息
            aggregator.RegisterMessage(arg =>
            {
                try
                {
                    SnackBar?.MessageQueue?.Enqueue(arg);
                } catch(Exception e) 
                {

                }
            });

            // 注册Toast通知
            aggregator.RegisterToast(arg =>
            {
                using(var toast = new NotifyIcon(arg.Title))
                {
                    toast.AppendContentText(arg.Message);
                    toast.Show();
                }
                WindowsExtensions.Flash(this);
            });


            // 注册等待消息窗口
            aggregator.Register(arg =>
            {
                arg.IsWindowOpen = DialogHost.IsOpen;

                if (DialogHost.IsOpen)
                    DialogHost.DialogContent = new ProgressView();
            });

            InitializeComponent();

            /* 初始化窗口事件 */
            // 最小化
            btnMin.Click += (s, e) => { WindowState = WindowState.Minimized; };
            // 最大化
            btnMax.Click += (s, e) =>
            {
                WindowState = WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
            };
            // 关闭窗口
            btnClose.Click += (s, e) => Hide();

            // 拖动窗口
            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    DragMove();
            };
            // 路由变化时收起侧边栏
            menuBar.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };
           
        }

    }
}
