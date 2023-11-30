using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyToDo.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
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
            btnClose.Click += (s, e) => { Close(); };
            // 拖动窗口
            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
            // 路由变化时收起侧边栏
            menuBar.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };
        }


    }
}
