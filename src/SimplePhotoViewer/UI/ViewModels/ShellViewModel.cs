using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Ninject;
using SimplePhotoViewer.UI.Util;

namespace SimplePhotoViewer.UI.ViewModels
{
    public interface IShellViewModel : IScreen {}

    [Export(typeof (IShellViewModel))]
    public sealed class ShellViewModel : WindowViewModel, IShellViewModel
    {
        private IImageViewModel imageViewModel;
        private Visibility selectFileVisibility;

        public ShellViewModel(IImageViewModel imageViewModel)
        {
            ImageViewModel = imageViewModel;
            ImageViewModel.IsImageSelectedChanged +=
                (s, b) => SelectFileVisibility = BoolToVisibilityConverter.ToVisibleOrHidden(b);

            SelectFileVisibility = Visibility.Hidden;
        }

        public Visibility SelectFileVisibility
        {
            get { return selectFileVisibility; }
            set
            {
                selectFileVisibility = value;
                NotifyOfPropertyChange(() => SelectFileVisibility);
            }
        }

        public IImageViewModel ImageViewModel
        {
            get { return imageViewModel; }
            set
            {
                imageViewModel = value;
                NotifyOfPropertyChange(() => ImageViewModel);
            }
        }

        public void HandleKeyDown(KeyEventArgs eventArgs)
        {
            ImageViewModel.HandleKeyDown(eventArgs);
        }

        public void SelectFile()
        {
            ImageViewModel.SelectFile();
        }
    }
}