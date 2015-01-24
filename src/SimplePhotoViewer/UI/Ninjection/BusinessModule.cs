using Ninject.Modules;
using SimplePhotoViewer.IO;

namespace SimplePhotoViewer.UI.Ninjection
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICanSelectFile>().To<FileDialogWrapper>();
            Bind<IFileTraverser>().To<FileTraverser>();
        }
    }
}