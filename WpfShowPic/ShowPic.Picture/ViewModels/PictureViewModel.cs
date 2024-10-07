using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowPic.Entity;
using Prism.Events;
using ShowPic.Utils.Event;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;
using Prism.Commands;
using System.Windows;
using ShowPic.Utils.HttpUtils;
using ShowPic.Utils;
using Prism.Services.Dialogs;
using ShowPic.Picture.Views;
using System.Windows.Controls;
using Prism.Mvvm;

namespace ShowPic.Picture.ViewModels
{

    public class PictureViewModel : BindableBase
    {

        IEventAggregator _eventAggregator;
        IDialogService _dialogService;
        public ObservableCollection<Pictureentity> PictureObjects1 { get; set; } = new ObservableCollection<Pictureentity>();

        public ObservableCollection<Pictureentity> PictureObjects2 { get; set; } = new ObservableCollection<Pictureentity>();


        public PictureViewModel(IEventAggregator eventAggregator, IDialogService dialogService)
        {
            this._eventAggregator = eventAggregator;
            this._dialogService = dialogService;
            this._eventAggregator.GetEvent<AddEvent>().Subscribe(OpenFolder);

            PageNum = 1;
            PageSize = 20;
        }


        public void OpenFolder()
        {
            this._dialogService.ShowDialog(nameof(AddPictureView));
        }

        public static byte[] BitmapImageToBtyes(BitmapImage bitmap)
        {
            Stream s = bitmap.StreamSource;
            s.Position = 0;
            using (BinaryReader binaryReader = new BinaryReader(s))
            {
                byte[] bytes = binaryReader.ReadBytes((int)s.Length);
                return bytes;
            }
        }

        #region command

        private DelegateCommand<object> commentCommand;

        public DelegateCommand<object> CommentCommand
        {
            get
            {
                if (commentCommand == null)
                {
                    commentCommand = new DelegateCommand<object>(Comment);
                }
                return commentCommand;
            }
        }

        public void Comment(object obj)
        {
            int Id = (int)obj;
            DialogParameters para = new DialogParameters();
            para.Add("Id", Id);
            this._dialogService.ShowDialog(nameof(AddComment),para, AddCommentCallback);
        }

        public void AddCommentCallback(IDialogResult dialogResult)
        {
            if (dialogResult != null && dialogResult.Result == ButtonResult.OK)
            {
                LoadPic();
            }
        }

        private DelegateCommand<object> ratePictureCommand;

        public DelegateCommand<object> RatePictureCommand
        {
            get
            {
                if (ratePictureCommand == null)
                {
                    ratePictureCommand = new DelegateCommand<object>(Rate);
                }
                return ratePictureCommand;
            }
        }

        public void Rate(object obj)
        {

        }


        private DelegateCommand<object> deleteCommand;

        public DelegateCommand<object> DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new DelegateCommand<object>(Delete);
                }
                return deleteCommand;
            }
        }

        public void Delete(object obj)
        {

            MessageBoxResult dialogresult = MessageBox.Show("确认删除图片吗？", "删除",MessageBoxButton.YesNo);
            if(dialogresult == MessageBoxResult.Yes)
            {
                int id = (int)obj;
                var res = PictureHttp.DeletePicture(id);
                this.LoadPic();
                MessageBox.Show("删除成功！");
            }
        }



        private DelegateCommand loadCommand;

        public DelegateCommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                {
                    loadCommand = new DelegateCommand(LoadPic);
                }
                return loadCommand;
            }
        }



        private DelegateCommand<object> showCommand;

        public DelegateCommand<object> ShowCommand
        {
            get
            {
                if (showCommand == null)
                {
                    showCommand = new DelegateCommand<object>(Show);
                }
                return showCommand;
            }
        }
        public void Show(object obj)
        {
            DialogParameters para = new DialogParameters();
            Pictureentity pictureentity = PictureHttp.GetPicture(obj.ToString());
            para.Add("picture", pictureentity);
            this._dialogService.ShowDialog(nameof(SinglePictureView), para, null, "DetailWindow");
        }




        #endregion
        public  void LoadPic()
        {
            this.PictureObjects1.Clear();
            this.PictureObjects2.Clear();
            var pictures = PictureHttp.GetPictures(null, null, this.pageNum, this.pageSize);
            this.TotalCount = pictures.count;
            this.TotalPage = ((int)Math.Ceiling(TotalCount * 1.0 / pageSize));
            AddToList(pictures);
        }
        public  void AddToList(Page<Pictureentity> page)
        {
            foreach (var pic in page.items)
            {
                if (PictureObjects1.Count >= PictureObjects2.Count)
                {
                    PictureObjects2.Add(pic);
                }
                else
                {
                    PictureObjects1.Add(pic);
                }
            }
          }

        #region 分页


        private int pageNum;

        public int PageNum
        {
            get { return pageNum; }
            set { SetProperty(ref pageNum, value); }
        }


        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { SetProperty(ref pageSize, value); }
        }


        private int totalCount;

        public int TotalCount
        {
            get { return totalCount; }
            set { SetProperty(ref totalCount, value); }
        }


        private int totalPage;

        public int TotalPage
        {
            get { return totalPage; }
            set
            {
                totalPage = value;
                RaisePropertyChanged(nameof(TotalPage));
            }
        }



        private int jumpNum;

        public int JumpNum
        {
            get { return jumpNum; }
            set { SetProperty(ref jumpNum, value); }
        }


        private DelegateCommand firstPageCommand;

        public DelegateCommand FirstPageCommand
        {
            get
            {
                if (firstPageCommand == null)
                {
                    firstPageCommand = new DelegateCommand(FirstPage);
                }
                return firstPageCommand;
            }

        }

        private void FirstPage()
        {
            this.PageNum = 1;
            this.LoadPic();
        }


        private DelegateCommand jumpPageCommand;

        public DelegateCommand JumpPageCommand
        {
            get
            {
                if (jumpPageCommand == null)
                {
                    jumpPageCommand = new DelegateCommand(JumpPage);
                }
                return jumpPageCommand;
            }
        }

        private void JumpPage()
        {
            if (jumpNum < 1)
            {
                MessageBox.Show("请输入跳转页");
                return;
            }
            if (jumpNum > this.totalPage)
            {
                MessageBox.Show($"跳转页面必须在[1,{this.totalPage}]之间，请确认。");
                return;
            }
            this.PageNum = jumpNum;

            this.LoadPic();
        }


        private DelegateCommand prevPageCommand;

        public DelegateCommand PrevPageCommand
        {
            get
            {
                if (prevPageCommand == null)
                {
                    prevPageCommand = new DelegateCommand(PrevPage);
                }
                return prevPageCommand;
            }
        }

        private void PrevPage()
        {
            this.PageNum--;
            if (this.PageNum < 1)
            {
                this.PageNum = 1;
            }
            this.LoadPic();
        }


        private DelegateCommand nextPageCommand;

        public DelegateCommand NextPageCommand
        {
            get
            {
                if (nextPageCommand == null)
                {
                    nextPageCommand = new DelegateCommand(NextPage);
                }
                return nextPageCommand;
            }
        }

        private void NextPage()
        {
            this.PageNum++;
            if (this.PageNum > this.TotalPage)
            {
                this.PageNum = this.TotalPage;
            }
            this.LoadPic();
        }

        #endregion
    }
}