﻿using MyToDo.ViewModels;
using MyToDo.Views;
using Prism.DryIoc;
using System.Windows;

namespace MyToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IndexView,IndexViewModel>();
            containerRegistry.RegisterForNavigation<MemoView,MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView,SettingsViewModel>();
            containerRegistry.RegisterForNavigation<ToDoView,ToDoViewModel>();
        }
    }

}
