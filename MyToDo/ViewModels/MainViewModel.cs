using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;


namespace MyToDo.ViewModels
{
    public class MainViewModel : BindableBase, IConfigureService
    {

        public MainViewModel(IContainerProvider containerProvider,
            IRegionManager  regionManager)
        {
            menuBars = [];
            selectedIndex = 0;
            previousVisibility = "Hidden";
            nextVisibility = "Hidden";
            username = string.Empty;
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            LogOutCommand = new DelegateCommand(LogOut);

            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                {
                    journal.GoBack();
                    SelectedIndex = menuBars.Select((menu, i) => new { Menu = menu, Index = i })
                                            .FirstOrDefault(x => x.Menu.NameSpace == journal.CurrentEntry.Uri.OriginalString)?.Index ?? -1;
                    PreviousVisibility = journal.CanGoBack ? "Visible" : "Hidden";
                    NextVisibility = journal.CanGoForward ? "Visible" : "Hidden";
                    
                }
            });

            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                {
                    journal.GoForward();
                    SelectedIndex = menuBars.Select((menu, i) => new { Menu = menu, Index = i })
                                            .FirstOrDefault(x => x.Menu.NameSpace == journal.CurrentEntry.Uri.OriginalString)?.Index ?? -1;
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

        private int selectedIndex;

        private string previousVisibility;

        private string nextVisibility;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }

        public string Username
        {
            get { return username; }
            set { username = value; RaisePropertyChanged(); }
        }


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
            MenuBars.Add(new MenuBar() { Icon = "NotebookOutline", Title = "所有事项", NameSpace = "ToDoView" });
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
            SelectedIndex = 0;
            App.LoginOut(containerProvider);
        }

        public void Configure()
        {
            _regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("IndexView");
            var journal = _regionManager.Regions[PrismManager.MainViewRegionName].NavigationService.Journal;
            journal.Clear();
            PreviousVisibility = NextVisibility = "Hidden";

            if (MenuBars.Count == 0)
                CreateMenuBar();
            Username = AppSession.Username;
        }
    }
}
