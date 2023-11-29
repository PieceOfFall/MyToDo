using System.Windows;
using System.Windows.Input;


namespace MyToDo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
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
        }
    }
}