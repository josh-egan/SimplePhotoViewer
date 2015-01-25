using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace SimplePhotoViewer.UI.ViewModels
{
    public interface IWindowViewModel : IScreen { }

    public class WindowViewModel : Screen, IWindowViewModel
    {
        private Window window;
        private Visibility maximizeVisibility;
        private Visibility normalVisibility;

        public void WindowLoaded(Window source)
        {
            window = source;
            UpdateWindowControlVisibilities();
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
            get { return normalVisibility; }
            set
            {
                normalVisibility = value;
                NotifyOfPropertyChange(() => NormalVisibility);
            }
        }

        public void Minimize()
        {
            window.WindowState = WindowState.Minimized;
        }

        public void Normal()
        {
            window.WindowState = WindowState.Normal;
        }

        public void Maximize()
        {
            window.WindowState = WindowState.Maximized;
        }

        public void Exit()
        {
            TryClose();
        }

        public void UpdateWindowControlVisibilities()
        {
            if (window.WindowState == WindowState.Maximized)
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
