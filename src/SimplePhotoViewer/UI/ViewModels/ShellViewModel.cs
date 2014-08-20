using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;
using Ninject;

namespace SimplePhotoViewer.UI.ViewModels
{
    public interface IShellViewModel : IScreen {}

    [Export(typeof (IShellViewModel))]
    public sealed class ShellViewModel : Screen, IShellViewModel
    {
        private readonly IWindowStateHelper windowStateHelper;

        private Visibility maximizeVisibility;
        private Visibility restoreDownVisibility;

        [Inject]
        public ShellViewModel(IWindowStateHelper windowStateHelper)
        {
            this.windowStateHelper = windowStateHelper;

            DisplayName = "Simple Photo Viewer";
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
    }
}