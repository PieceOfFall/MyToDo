using MyToDo.Service;
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
        [LibraryImport("User32.dll")]
        private static partial IntPtr SetFocus(IntPtr hWnd);

        Timer? getNameTimer;

        IToDoService ToDoService { get; set; }

        List<string> names = [];

        public AddToDoView(IToDoService toDoService)
        {
            ToDoService = toDoService;
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


        int index = 0;
        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                if (names.Count == 0)
                    return;
                if (index >= names.Count)
                    index = 0;
                receiverBox.Text = names[index];
                index++;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs routedEventArgs)
        {
            // 设置防抖标志位
            if (getNameTimer == null)
                getNameTimer = new Timer(e =>
                {
                    // 定时器线程交给ui线程处理
                    Application.Current.Dispatcher.Invoke(async () =>
                    {
                        // 获取当前文本并通知后端模糊查询
                        var textBox = (TextBox)routedEventArgs.Source;
                        var text = textBox.Text;
                        if (names.Contains(text))
                            return;
                        if (string.IsNullOrEmpty(text))
                            return;
                        var ret = await ToDoService.queryFullNameByFragment(text);
                        names = ret.data!;
                        index = 0;

                        // 释放当前定时器资源
                        getNameTimer?.Dispose();
                        getNameTimer = null;
                    });

                }, null, 1000, Timeout.Infinite);
            else
            {
                getNameTimer.Change(1000, Timeout.Infinite);
            }
        }
    }
}
