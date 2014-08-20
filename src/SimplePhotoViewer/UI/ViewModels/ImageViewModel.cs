using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Ninject;
using SimplePhotoViewer.IO;

namespace SimplePhotoViewer.UI.ViewModels
{
    public interface IImageViewModel : INotifyPropertyChangedEx
    {
        event EventHandler<bool> IsImageSelectedChanged;
        void ReSelectFile();
    }

    public class ImageViewModel : PropertyChangedBase, IImageViewModel
    {

        public event EventHandler<bool> IsImageSelectedChanged;

        public void ReSelectFile()
        {
            throw new NotImplementedException();
        }

    }
}