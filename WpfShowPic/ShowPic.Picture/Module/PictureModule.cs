using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ShowPic.Picture.Views;

namespace ShowPic.Picture.Module
{
    public class PictureModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
           // IRegionManager regionManager = containerProvider.Resolve<IRegionManager>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PictureView>();
            containerRegistry.RegisterForNavigation<VisitorPictureView>();
            containerRegistry.RegisterForNavigation<SinglePictureView>();
        }
    }
}
