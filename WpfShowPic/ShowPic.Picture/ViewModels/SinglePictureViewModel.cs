using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowPic.Entity;
using Prism.Regions;
using Prism.Mvvm;
using Prism.Services.Dialogs;
namespace ShowPic.Picture.ViewModels
{
    public class SinglePictureViewModel:BindableBase,INavigationAware,IDialogAware
    {
        private byte[] imgData;
        public byte[] ImgData
        {
            get { return imgData; }
            set
            {
                SetProperty(ref imgData, value);
            }
        }


        private string imgName;
        public string ImgName
        {
            get { return imgName; }
            set
            {
                SetProperty(ref imgName, value);
            }
        }


        private string imgTag;
        public string ImgTag
        {
            get { return imgTag; }
            set
            {
                SetProperty(ref imgTag, value);
            }
        }


        private string imgComment;

        public event Action<IDialogResult> RequestClose;

        public string ImgComment
        {
            get { return imgComment; }
            set
            {
                SetProperty(ref imgComment, value);
            }
        }

        public string Title => "图片详情";

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Pictureentity Picture = navigationContext.Parameters.GetValue<Pictureentity>("picture");
            ImgData = Picture.PicData;
            ImgName = Picture.PicName;
            imgTag = Picture.PicTag;
            ImgComment = Picture.PicDescription;
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
            Pictureentity Picture = parameters.GetValue<Pictureentity>("picture");
            ImgData = Picture.PicData;
            ImgName = Picture.PicName;
            imgTag = Picture.PicTag;
            ImgComment = Picture.PicDescription;
        }
    }
}
