using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using ShowPic.Entity;
using ShowPic.Utils.HttpUtils;

namespace ShowPic.ViewModels
{
    class RegisterAdminViewModel : BindableBase, IDialogAware
    {

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


        public string Title => "注册管理员";

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
        }

        private DelegateCommand<object> regsiterAdminCommand;

        public DelegateCommand<object> RegsiterAdminCommand
        {
            get
            {
                if (regsiterAdminCommand == null)
                {
                    regsiterAdminCommand = new DelegateCommand<object>(RegisterAdmin);
                }
                return regsiterAdminCommand;
            }
        }

        public void RegisterAdmin(object obj)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("用户名或密码为空，请确认");
                return;
            }

           var res= LoginHttpUtil.Register(new Pictureeditor() { Name = UserName, Password = Password, Role = "Admin" });
            if(res == 0)
            {
                MessageBox.Show("注册成功！请输入登录账户密码");
            }
            else if(res==1)
            {
                MessageBox.Show("注册失败！用户已经存在");

            }
            RequestClose?.Invoke((new DialogResult(ButtonResult.OK)));
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
