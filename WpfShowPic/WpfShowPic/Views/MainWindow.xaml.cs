using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ShowPic.ViewModels;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShowPic.Picture.Views;
using Prism.Ioc;
using Prism.Regions;

namespace ShowPic.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IContainerProvider _containerProvider;
        IRegionManager _regionManager;

        public MainWindow(IContainerProvider container, IRegionManager regionManager)
        {
            this._containerProvider = container;
            this._regionManager = regionManager;
            InitializeComponent();
        }

    }



}


