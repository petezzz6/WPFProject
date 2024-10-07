using Prism.Commands;
using Prism.Services.Dialogs;
using ShowPic.Picture.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using ShowPic.Picture.ViewModels;
using Prism.Regions;
using ShowPic.Views;

namespace ShowPic.ViewModels
{
    class NaviViewModel
    {
        IContainerProvider _containerProvider;
        IDialogService _dialogService;
        IRegionManager _regionManager;
        public NaviViewModel(IContainerProvider containerProvider, IDialogService dialogService, IRegionManager regionManager)
        {
            this._regionManager = regionManager;
            this._containerProvider = containerProvider;
            this._dialogService = dialogService; 
        }
        private DelegateCommand<string> addCommand;

        public DelegateCommand<string> AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new DelegateCommand<string>(AddPicture);
                }
                return addCommand;
            }
        }

        private void AddPicture(string ViewName)
        {
            this._dialogService.ShowDialog(nameof(AddPictureView), null, AddPicCallBack, "AddPictureWindow");
        }

        public void AddPicCallBack(IDialogResult dialogResult)
        {
            if (dialogResult != null && dialogResult.Result == ButtonResult.OK)
            {
                //刷新列表
                var Pic = this._containerProvider.Resolve<PictureViewModel>();
                Pic.LoadPic();
            }
        }



        public DelegateCommand<string> jumpCommand;

        public DelegateCommand<string> JumpCommand
        {
            get
            {
                if (jumpCommand == null)
                {
                    jumpCommand = new DelegateCommand<string>(OnJump);
                }
                return jumpCommand;
            }
        }
        public void OnJump(string obj)
        {
            
            _regionManager.Regions["ContentRegion"].RequestNavigate(obj);

        }


        public DelegateCommand loginCommand;

        public DelegateCommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new DelegateCommand(Login);
                }
                return loginCommand;
            }
        }
        public void Login()
        {

            this._dialogService.ShowDialog(nameof(LoginView));
        }

    }
}
