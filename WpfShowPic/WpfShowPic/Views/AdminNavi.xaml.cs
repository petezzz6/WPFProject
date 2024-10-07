using Prism.Ioc;
using ShowPic.ViewModels;
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

namespace ShowPic.Views
{
    /// <summary>
    /// AdminNavi.xaml 的交互逻辑
    /// </summary>
    public partial class AdminNavi : UserControl
    {
        IContainerProvider _containerProvider;
        public AdminNavi(IContainerProvider containerProvider)
        {
            InitializeComponent();
            this._containerProvider = containerProvider;
            this.DataContext = this._containerProvider.Resolve<NaviViewModel>();
        }
    }
}
