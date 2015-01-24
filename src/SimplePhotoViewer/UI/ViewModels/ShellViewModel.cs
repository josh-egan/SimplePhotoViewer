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
    public sealed class ShellViewModel : Screen, IShellViewModel
    {
        private readonly IWindowStateHelper windowStateHelper;
        private IImageViewModel imageViewModel;

        private Visibility maximizeVisibility;
        private Visibility reSelectFileVisibility;
        private Visibility restoreDownVisibility;

        public ShellViewModel(IWindowStateHelper windowStateHelper, IImageViewModel imageViewModel)
        {
            this.windowStateHelper = windowStateHelper;
            ImageViewModel = imageViewModel;
            ImageViewModel.IsImageSelectedChanged +=
                (s, b) => ReSelectFileVisibility = BoolToVisibilityConverter.ToVisibleOrHidden(b);

            ReSelectFileVisibility = Visibility.Hidden;
        }

        public Visibility ReSelectFileVisibility
        {
            get { return reSelectFileVisibility; }
            set
            {
                reSelectFileVisibility = value;
                NotifyOfPropertyChange(() => ReSelectFileVisibility);
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

        public Visibility MaximizeVisibility
        {
            get { return maximizeVisibility; }
            set
            {
                maximizeVisibility = value;
                NotifyOfPropertyChange(() => MaximizeVisibility);
            }
        }

        public Visibility NormalVisibility
        {
            get { return restoreDownVisibility; }
            set
            {
                restoreDownVisibility = value;
                NotifyOfPropertyChange(() => NormalVisibility);
            }
        }

        public void Minimize()
        {
            windowStateHelper.WindowState = WindowState.Minimized;
        }

        public void Normal()
        {
            windowStateHelper.WindowState = WindowState.Normal;
        }

        public void Maximize()
        {
            windowStateHelper.WindowState = WindowState.Maximized;
        }

        public void Exit()
        {
            TryClose();
        }

        public void UpdateWindowControlVisibilities()
        {
            if (windowStateHelper.WindowState == WindowState.Maximized)
            {
                MaximizeVisibility = Visibility.Collapsed;
                NormalVisibility = Visibility.Visible;
            }
            else
            {
                MaximizeVisibility = Visibility.Visible;
                NormalVisibility = Visibility.Collapsed;
            }
        }

        public void HandleKeyDown(KeyEventArgs eventArgs)
        {
            ImageViewModel.HandleKeyDown(eventArgs);
        }

        public void ReSelectFile()
        {
            ImageViewModel.ReSelectFile();
        }
    }
}