using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace MyToDo.Views.Dialogs
{
    /// <summary>
    /// AddToDoView.xaml 的交互逻辑
    /// </summary>
    public partial class AddToDoView : UserControl
    {
        [DllImport("User32.dll")]
        private static extern IntPtr SetFocus(IntPtr hWnd);

        public AddToDoView()
        {
            InitializeComponent();
        }

        private void MyPopup_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            var source = (HwndSource)PresentationSource.FromVisual(textBox);
            if (source != null)
            {
                SetFocus(source.Handle);
                textBox!.Focus();  // 解决需要两次点击 TextBox 才能获取焦点的问题
            }
        }

    }
}
