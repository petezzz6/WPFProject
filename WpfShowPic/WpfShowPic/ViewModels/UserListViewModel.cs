using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using ShowPic.Entity;
using Prism.Regions;
using ShowPic.Utils.HttpUtils;
using Prism.Commands;
using System.Windows;

namespace ShowPic.ViewModels
{
    public class UserListViewModel:BindableBase, INavigationAware
    {
        IEventAggregator _eventAggregator;
        IDialogService _dialogService;
        public ObservableCollection<Pictureeditor> userlist { get; set; } = new ObservableCollection<Pictureeditor>();


        public UserListViewModel()
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            List<Pictureeditor> list;
            list = EditorHttpUtil.GetEditors();
            userlist.AddRange(list);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
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
            MessageBoxResult dialogresult = MessageBox.Show("确认删除图片吗？", "删除", MessageBoxButton.YesNo);
            if (dialogresult == MessageBoxResult.Yes)
            {
                int id = (int)obj;
                var res = EditorHttpUtil.DeleteEditor(id);

                MessageBox.Show("删除成功！");   
            }
            userlist.Clear();
            OnNavigatedTo(null);
        }
    }
}
