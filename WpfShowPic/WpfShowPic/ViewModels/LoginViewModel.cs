using System;
using Prism.Services.Dialogs;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using Prism.Events;
using ShowPic.Picture.Views;
using ShowPic.Utils.Event;
using Prism.Mvvm;
using System.Windows;
using ShowPic.Utils.HttpUtils;
using ShowPic.Utils;
using ShowPic.Views;
namespace ShowPic.ViewModels
{
    internal class LoginViewModel : BindableBase,IDialogAware
    {
        public string Title => "登录界面";

        public event Action<IDialogResult> RequestClose;

        private IContainerProvider _containerProvider;
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        private IDialogService _dialogService;
        public LoginViewModel(IRegionManager regionManager, IContainerProvider containerProvider, IEventAggregator eventAggregator, IDialogService dialogService)
        {
            this._containerProvider = containerProvider;
            this._regionManager = regionManager;
            this._eventAggregator = eventAggregator;    
            this._dialogService = dialogService;
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        private DelegateCommand loginCommand;

        public DelegateCommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new DelegateCommand(AdminLogin);
                }
                return loginCommand;
            }
        }


        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }



        private void AdminLogin()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("用户名或密码为空，请确认");
                return;
            }

            int res = LoginHttpUtil.Login(UserName, Password);

            if(res > 0)
            {   
                UserInfo.Instance.Id = res;
                UserInfo.Instance.UserName = userName;
            }
            else
            {
                MessageBox.Show("密码错误！");
                return;
            }
           
            this._eventAggregator.GetEvent<NavEvent>().Publish();
            RequestClose?.Invoke((new DialogResult(ButtonResult.OK)));

        }


        private DelegateCommand<object> visitorLoginCommand;

        public DelegateCommand<object> VisitorLoginCommand
        {
            get
            {
                if (visitorLoginCommand == null)
                {
                    visitorLoginCommand = new DelegateCommand<object>(visitorLogin);
                }
                return visitorLoginCommand;
            }
        }

        private void visitorLogin(object obj)
        {

            this._eventAggregator.GetEvent<NavEvent2>().Publish();
            RequestClose?.Invoke((new DialogResult(ButtonResult.OK)));
        }

        private DelegateCommand registerAdminCommand;

        public DelegateCommand RegisterAdminCommand
        {
            get
            {
                if (registerAdminCommand == null)
                {
                    registerAdminCommand = new DelegateCommand(Register);
                }
                return registerAdminCommand;
            }
        }

        public void Register()
        {
            this._dialogService.ShowDialog(nameof(RegisterAdmin));
        }


        private DelegateCommand cancelCommand;

        public DelegateCommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new DelegateCommand(Cancel);
                }
                return cancelCommand;
            }
        }

        public void Cancel()
        {
            System.Environment.Exit(0);
        }

    }
}
