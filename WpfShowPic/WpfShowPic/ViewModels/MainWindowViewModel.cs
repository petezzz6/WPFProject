using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Services.Dialogs;
using Prism.Ioc;
using Prism.Regions;
using Prism.Events;
using Prism.Commands;
using ShowPic.Views;
using ShowPic.Utils.Event;
using Microsoft.Win32;
using System.IO;
using ShowPic.Picture.Views;
using ShowPic.Picture.ViewModels;
namespace ShowPic.ViewModels
{
    public class MainWindowViewModel:IRegionMemberLifetime
    {

        private IEventAggregator? _eventAggregator;
        private IContainerProvider? _containerProvider;
        private IRegionManager? _regionManager;
        private IDialogService? _dialogService;

        public MainWindowViewModel(IEventAggregator eventAggregator,
            IContainerProvider _containerProvider,
            IRegionManager _regionManager,
            IDialogService _dialogService)
        {
            this._eventAggregator = eventAggregator;
            this._containerProvider = _containerProvider;
            this._regionManager = _regionManager;
            this._dialogService = _dialogService;
            _eventAggregator.GetEvent<NavEvent2>().Subscribe(VisitorNavigation);
            _eventAggregator.GetEvent<NavEvent>().Subscribe(AdminNavigation);
            this._dialogService.ShowDialog(nameof(LoginView), null, null, "LoginWindow");

        }

        #region Command

        private DelegateCommand loadCommand;

        public DelegateCommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                {
                    loadCommand = new DelegateCommand(LoadWindow);
                }
                return loadCommand;
            }
        }

        private void LoadWindow()
        {

            _regionManager.RegisterViewWithRegion("HeaderRegion", typeof(SerachBox));
            _regionManager.RequestNavigate("HeaderRegion", nameof(SerachBox));

            _regionManager.RegisterViewWithRegion("FootRegion", typeof(PageBox));
            _regionManager.RequestNavigate("FootRegion", nameof(PageBox));

            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(UserList));
        }


        private DelegateCommand<string> naviCommand;

        public DelegateCommand<string> NaviCommand
        {
            get
            {
                if (naviCommand == null)
                {
                    naviCommand = new DelegateCommand<string>(Navigation);
                }
                return naviCommand;
            }
        }

        public bool KeepAlive => true;

        private void Navigation(string ViewName)
        {
            _regionManager.RequestNavigate("HeaderRegion", ViewName);
            var Pic = this._containerProvider.Resolve<PictureViewModel>();
        }
        #endregion


        public void VisitorNavigation()
        {

            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(VisitorPictureView));

            _regionManager.RegisterViewWithRegion("NaviRegion", typeof(VisitorNavi));

            _regionManager.RequestNavigate("ContentRegion", nameof(VisitorPictureView));

            _regionManager.RequestNavigate("NaviRegion", nameof(VisitorNavi));

        }

        public void AdminNavigation()
        {
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(PictureView));

            _regionManager.RegisterViewWithRegion("NaviRegion", typeof(AdminNavi));

            _regionManager.RequestNavigate("ContentRegion", nameof(PictureView));

            _regionManager.RequestNavigate("NaviRegion", nameof(AdminNavi));
        }
    }

}
