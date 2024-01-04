using MyToDo.Common.Models;
using MyToDo.Extensions;
using MyToDo.Service;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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
        ILoginService loginService { get; set; }

        IEventAggregator aggregator { get; set; }

        public ToDoView(IToDoService toDoService,ILoginService loginService,
            IEventAggregator aggregator)
        {
            InitializeComponent();
            comboBoxItems = [];
            this.toDoService = toDoService;
            this.loginService = loginService;
            this.aggregator = aggregator;
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

        private void TreeViewBtnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var btn = sender as Button;
            aggregator.SendLogicToVm(new Common.Events.LogicToVmModel
            {
                VmName = "ToDoViewModel",
                Data = btn!.Content
            });
        }

        private async void TreeView_Expanded(object sender, RoutedEventArgs e)
        {
            var tree = (TreeViewItem)sender;
            var selectedItem = tree.Items;
            
            var vm = tree.DataContext as DepartmentDto;
            if (vm!.deptId > 101)
            {
                vm.children.Clear();
                // 根据部门id获取在这个部门的成员
                var ret = (await loginService.GetDeptEmployeesAsync(vm.deptId)).data;
                ret!.ForEach(e =>
                {
                    
                    vm.children.Add(new DepartmentDto
                    {
                        deptName = e,
                        isEmp = true,
                        deptId = -1
                    });
                });
            }

            
            e.Handled = true;
        }



    }
}
