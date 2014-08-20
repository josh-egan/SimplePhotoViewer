using System.Windows;

namespace SimplePhotoViewer.UI
{
    public interface IWindowStateHelper
    {
        WindowState WindowState { get; set; }
    }

    public class WindowStateHelper : IWindowStateHelper
    {
        public WindowState WindowState
        {
            get { return Application.Current.MainWindow.WindowState; }
            set { Application.Current.MainWindow.WindowState = value; }
        }
    }
}