using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;
using ShowPic.Views;
using ShowPic.Picture.ViewModels;
using ShowPic.Picture.Views;
using ShowPic.ViewModels;
using Prism.Regions;

namespace ShowPic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<LoginView>();
            containerRegistry.RegisterDialogWindow<SubWindow>();
            containerRegistry.RegisterDialog<RegisterAdmin>();
            containerRegistry.RegisterDialog<AddComment>();
            containerRegistry.RegisterDialog<SinglePictureView, SinglePictureViewModel>();

            //使用单列保证生命周期 
            containerRegistry.RegisterSingleton<PictureView>();
            containerRegistry.RegisterSingleton<VisitorPictureView>();
            containerRegistry.RegisterSingleton<PictureViewModel>();
            containerRegistry.RegisterSingleton<NaviViewModel>();
            containerRegistry.RegisterSingleton<SinglePictureView>();
            containerRegistry.RegisterSingleton<RegionManager>();
            containerRegistry.RegisterSingleton<UserList>();
            containerRegistry.RegisterSingleton<UserListViewModel>();


        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule(typeof(ShowPic.Picture.Module.PictureModule)) ;
        }
    }
}
