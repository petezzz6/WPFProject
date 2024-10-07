using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Ioc;
using ShowPic.Entity;
using ShowPic.Utils.HttpUtils;
using Prism.Regions;
using ShowPic.Picture.Views;

namespace ShowPic.Picture.ViewModels
{
   public  class SerachBoxViewModel:BindableBase
    {
        IRegionManager _regionManager;
        IContainerProvider _containerProvider;
        public SerachBoxViewModel(RegionManager regionManager,IContainerProvider containerProvider)
        {
            _regionManager = regionManager;
            _containerProvider = containerProvider;
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(SinglePictureView));

        }

        public string SearchString { get; set; }

        private DelegateCommand searchPictureCommand;
        public DelegateCommand SearchPictureCommand
        {
            get
            {
                if (searchPictureCommand == null)
                {
                    searchPictureCommand = new DelegateCommand(SearchPicture);
                }
                return searchPictureCommand;
            }
        }

        public void SearchPicture()
        {
           Pictureentity picture= PictureHttp.GetPicture(SearchString);
            if (picture == null)
            {
                MessageBox.Show("图片不存在！");
            }
            else
            {   
                NavigationParameters Params = new NavigationParameters();
                Params.Add("picture",picture);
                _regionManager.RequestNavigate("ContentRegion", nameof(SinglePictureView), Params);
            }
        }







    }


}
