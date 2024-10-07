using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Services.Dialogs;
using ShowPic.Utils;
using ShowPic.Utils.HttpUtils;

namespace ShowPic.Picture.ViewModels
{
    public class AddCommentViewModel : IDialogAware
    {   

        public string ?Comment { get; set; }
        public int Id { get; set; }


        public string Title => "添加评论";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            string value = parameters.GetValue<string>("Id");
            this.Id = int.Parse(value);
        }

        private DelegateCommand addCommentCommand;


        public DelegateCommand AddCommentCommand
        {
            get
            {
                if (addCommentCommand == null)
                {
                    addCommentCommand = new DelegateCommand(UpdateComment);
                }
                return addCommentCommand;
            }
        }

        public void UpdateComment()
        {
            LoggerHelper.loggerHelper.Trace("UpdateComment ");
            var res = PictureHttp.UpdateComment(this.Id, this.Comment);
            if (res)
            {
                LoggerHelper.loggerHelper.Trace("UpdateComment  success");

                MessageBox.Show("修改评论成功！");
                RequestClose?.Invoke((new DialogResult(ButtonResult.OK)));
            }
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
    }



}
