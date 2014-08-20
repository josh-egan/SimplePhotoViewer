using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;

namespace SimplePhotoViewer.UI.ViewModels
{
    public interface IShellViewModel : IScreen {}

    [Export(typeof (IShellViewModel))]
    public sealed class ShellViewModel : Screen, IShellViewModel
    {
        private readonly Application application;

        private Visibility maximizeVisibility;
        private Visibility restoreDownVisibility;

        public ShellViewModel() : this(Application.Current){}

        public ShellViewModel(Application application)
        {
            this.application = application;

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

        public Visibility RestoreDownVisibility
        {
            get { return restoreDownVisibility; }
            set
            {
                restoreDownVisibility = value;
                NotifyOfPropertyChange(() => RestoreDownVisibility);
            }
        }

        public void Minimize()
        {
            application.MainWindow.WindowState = WindowState.Minimized;
        }

        public void RestoreDown()
        {
            application.MainWindow.WindowState = WindowState.Normal;
        }

        public void Maximize()
        {
            application.MainWindow.WindowState = WindowState.Maximized;
        }

        public void Exit()
        {
            TryClose();
        }

        public void UpdateWindowControlVisibilities()
        {
            if (application.MainWindow.WindowState == WindowState.Maximized)
            {
                MaximizeVisibility = Visibility.Collapsed;
                RestoreDownVisibility = Visibility.Visible;
            }
            else
            {
                MaximizeVisibility = Visibility.Visible;
                RestoreDownVisibility = Visibility.Collapsed;
            }
        }
    }
}