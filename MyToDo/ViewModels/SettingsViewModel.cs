using MyToDo.Common.Models;
using MyToDo.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace MyToDo.ViewModels
{
    public class SettingsViewModel : BindableBase
    {


        private readonly IRegionManager _regionManager;

        private ObservableCollection<MenuBar> menuBars;

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }


        public SettingsViewModel(IRegionManager regionManager)
        {
            menuBars = [];
            CreateMenuBar();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            _regionManager = regionManager;
        }

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        private void Navigate(MenuBar bar)
        {
            if (bar == null || string.IsNullOrWhiteSpace(bar.Title))
                return;

            _regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(bar.NameSpace);
        }

        private void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Brush", Title = "个性化", NameSpace = "SkinView" });
            MenuBars.Add(new MenuBar() { Icon = "Cogs", Title = "系统设置", NameSpace = "" });
            MenuBars.Add(new MenuBar() { Icon = "Xml", Title = "联系开发者", NameSpace = "AboutView" });
        }
    }
}
