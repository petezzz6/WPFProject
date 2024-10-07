using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using ShowPic.Entity;
using ShowPic.Utils;
using ShowPic.Utils.HttpUtils;

namespace ShowPic.Picture.ViewModels
{

    public class AddPictureViewModel : BindableBase, IDialogAware
    {
        private string sourcepath;
        public string PicSourcePath
        {
            get { return sourcepath; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("图片不能为空！");
                    return;
                }
                SetProperty(ref sourcepath, value);
            }
        }

        public string Tag { get; set; }
        public string Name { get; set; }

        public Pictureentity Picture { get; set; }

        public event Action<IDialogResult> RequestClose;

        public string Title => "添加图片界面";

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Ratings = new List<Rating>();
            for (int i=1; i <= 5; i++)
            {
                Ratings.Add(new Rating(i));
            }
        }


        public AddPictureViewModel()
        {
            Picture = new Pictureentity();
        }
        #region Command
        private DelegateCommand<object> selectPictureCommand;


        public DelegateCommand<object> SelectPictureCommand
        {
            get
            {
                if (selectPictureCommand == null)
                {
                    selectPictureCommand = new DelegateCommand<object>(SelectPicture);
                }
                return selectPictureCommand;
            }
        }



        private DelegateCommand saveCommand;


        public DelegateCommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new DelegateCommand(SavePicture);
                }
                return saveCommand;
            }
        }


        public void SelectPicture(object obj)
        {
            LoggerHelper.loggerHelper.Trace("SelectPicture ");


            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp*.jfif";
            if (openFileDialog.ShowDialog() == true)
            {
                PicSourcePath = openFileDialog.FileName;
            }
        }

        public void SavePicture()
        {
            if(PicSourcePath==null|| Name==null||Tag==null|| TheRating.rateNum==-1)
            {
                MessageBox.Show("请输入完整数据！");
                return;
            }
            Picture = new Pictureentity();
            try
            {
                using (FileStream fileStream = File.OpenRead(PicSourcePath))
                {
                    byte[] PicBytes = new byte[fileStream.Length];
                    fileStream.Read(PicBytes, 0, PicBytes.Length);
                    fileStream.Close();
                    Picture.PicName = Name;
                    Picture.PicTag = Tag;
                    Picture.PicRate = (sbyte?)TheRating.rateNum;
                    Picture.PicData = PicBytes;
                }
            }
            catch (FileNotFoundException ex)
            {
                LoggerHelper.loggerHelper.Trace("FileNotFoundException,entered an invalid path ");
                MessageBox.Show("未找到图片文件！请确认图片名称和路径是否正确！");
                return;
            }

            var res=  PictureHttp.AddSPicutre(Picture);
            if (res)
            {
                MessageBox.Show("添加成功！");
                LoggerHelper.loggerHelper.Trace(" AddSPicutre success");

                RequestClose?.Invoke((new DialogResult(ButtonResult.OK)));
            }
        }

        private Rating rating = new Rating(-1);
        public Rating TheRating
        {
            get {  return rating;}
            set {
                if (value==null)
                {
                    MessageBox.Show("评分不能为空！");
                    return;
                }
                SetProperty(ref rating, value); }
        }


        private List<Rating> ratings;

        public List<Rating> Ratings
        {   
            get { return ratings; }
            set { SetProperty(ref ratings, value); }
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
            RequestClose?.Invoke((new DialogResult(ButtonResult.OK)));
        }
        #endregion

    }

    public class Rating
    {

        public int rateNum { get; set; }

        public Rating(int n)
        {
            rateNum = n; 
        }


    }
}
