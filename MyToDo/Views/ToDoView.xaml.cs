using MyToDo.Service;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MyToDo.Views
{
    /// <summary>
    /// ToDoView.xaml 的交互逻辑
    /// </summary>
    public partial class ToDoView : UserControl
    {

        private readonly ObservableCollection<string> comboBoxItems;


        Timer? getNameTimer;

        IToDoService toDoService { get; set; }

        public ToDoView(IToDoService toDoService)
        {
            InitializeComponent();
            comboBoxItems = [];
            this.toDoService = toDoService;
            dynamicComboBox.ItemsSource = comboBoxItems;
        }

        private void DynamicComboBox_SelectionChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            // 设置防抖标志位
            if (getNameTimer == null)
                getNameTimer = new Timer(e =>
                {
                    // 定时器线程交给ui线程处理
                    Application.Current.Dispatcher.Invoke(async() =>
                    {
                        // 获取当前文本并通知后端模糊查询
                        var textBox = (TextBox)routedEventArgs.Source;
                        var text = textBox.Text;
                        if (string.IsNullOrEmpty(text))
                        {
                            comboBoxItems.Clear();
                            return;
                        }
                        var ret = await toDoService.queryFullNameByFragment(text);

                        // 更新ComboBox的选项
                        UpdateComboBoxItems(ret.data!);
                        receiverTextBox.Text = text;
                        // 释放当前定时器资源
                        getNameTimer?.Dispose();
                        getNameTimer = null;
                    });
                    
                }, null, 500, Timeout.Infinite);
            else
            {
                getNameTimer.Change(500, Timeout.Infinite);
            }

        }


        private void UpdateComboBoxItems(IEnumerable<string> items)
        {
            // 删除重复项
            foreach (var item in comboBoxItems)
            {
                items = items.Where(s => s != item)
                             .ToList();
            }

            foreach (var item in items)
            {
                comboBoxItems.Add(item);
            }
        }
        
    }
}
