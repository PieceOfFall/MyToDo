using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Store;


namespace MyToDo.ViewModels
{
    public class MainViewModel : BindableBase, IConfigureService
    {

        public MainViewModel(IContainerProvider containerProvider,
            IRegionManager  regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            previousVisibility = "Hidden";
            nextVisibility = "Hidden";
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            LogOutCommand = new DelegateCommand(LogOut);

            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                {
                    journal.GoBack();
                    PreviousVisibility = journal.CanGoBack ? "Visible" : "Hidden";
                    NextVisibility = journal.CanGoForward ? "Visible" : "Hidden";
                }
                    
            });

            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                {
                    journal.GoForward();
                    PreviousVisibility = journal.CanGoBack ? "Visible" : "Hidden";
                    NextVisibility = journal.CanGoForward ? "Visible" : "Hidden";
                }
            });
            _regionManager = regionManager;
            this.containerProvider = containerProvider;
        }

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        public DelegateCommand LogOutCommand { get; private set; }

        private readonly IContainerProvider containerProvider;

        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }

        private readonly IRegionManager _regionManager;

        private IRegionNavigationJournal? journal;

        private ObservableCollection<MenuBar> menuBars;

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; RaisePropertyChanged(); }
        }

        private string previousVisibility;

        private string nextVisibility;


        public string NextVisibility
        {
            get => nextVisibility;
            set { nextVisibility = value;RaisePropertyChanged(); }
        }

        public string PreviousVisibility
        {
            get => previousVisibility;
            set { previousVisibility = value;RaisePropertyChanged();}
        }


        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        private void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookOutline", Title = "待办事项", NameSpace = "ToDoView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookPlus", Title = "备忘录", NameSpace = "MemoView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
        }

        private void Navigate(MenuBar bar)
        {
            if (bar == null || string.IsNullOrWhiteSpace(bar.Title))
                return;

            _regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(bar.NameSpace, back =>
            {
                if(back.Context!=null)
                    journal = back.Context.NavigationService.Journal;
            });

            if(journal != null)
            {
                PreviousVisibility  = journal.CanGoBack     ? "Visible" : "Hidden";
                NextVisibility      = journal.CanGoForward  ? "Visible" : "Hidden";
            }
            
        }
        private void LogOut()
        {
            App.LoginOut(containerProvider);
        }

        public void Configure()
        {
            if(MenuBars.Count == 0)
                CreateMenuBar();
            Username = AppSession.Username;
            _regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("IndexView");
        }
    }
}
