using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Prism.Ioc;
using ShowPic.Picture.ViewModels;
using ShowPic.Entity;
using ShowPic.Utils.HttpUtils;
namespace ShowPic.Picture.Views
{
    /// <summary>
    /// VisitorPictureView.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class VisitorPictureView : UserControl
    {
        IContainerProvider _containerProvider;
        public VisitorPictureView(IContainerProvider containerProvider)
        {
            this._containerProvider = containerProvider;
            InitializeComponent();
            this.DataContext = _containerProvider.Resolve<PictureViewModel>();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            MessageBoxResult dialogresult = MessageBox.Show("确认修改评价吗？", "评论", MessageBoxButton.YesNo);
            if (dialogresult == MessageBoxResult.Yes)
            {

                ComboBoxItem item = e.AddedItems[0] as ComboBoxItem;
                string str = item.Content.ToString();
                string rate = str.Substring(0, str.Length - 1);
                Pictureentity picture = item.DataContext as Pictureentity;
                PictureHttp.RatePicture(picture.PicId, int.Parse(rate));
                PictureViewModel pictureViewModel = _containerProvider.Resolve<PictureViewModel>();
                pictureViewModel.LoadPic();
                MessageBox.Show("修改成功！");
            }
            else
            {
                return;
            }
        }
    }
}
