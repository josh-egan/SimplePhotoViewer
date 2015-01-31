using Caliburn.Micro;
using Ninject.Modules;
using SimplePhotoViewer.UI;
using SimplePhotoViewer.UI.ViewModels;

namespace SimplePhotoViewer.UI.Ninjection
{
    public class UIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWindowManager>().To<WindowManager>();
            Bind<IShellViewModel>().To<ShellViewModel>();
            Bind<IImageViewModel>().To<ImageViewModel>();
        }
    }
}