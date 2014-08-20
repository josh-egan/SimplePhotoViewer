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
        void ReSelectFile();
        event EventHandler<bool> IsImageSelectedChanged;
    }

    public class ImageViewModel : PropertyChangedBase, IImageViewModel
    {
        private readonly IFileTraverser fileTraverser;

        private BitmapSource currentImage;
        private Visibility currentImageVisibility;
        private Visibility nextVisibility;
        private Visibility previousVisibility;
        private Visibility selectFileVisibility;

        [Inject]
        public ImageViewModel(IFileTraverser fileTraverser)
        {
            this.fileTraverser = fileTraverser;

            UpdateVisibilities();
        }

        public BitmapSource CurrentImage
        {
            get { return currentImage; }
            set
            {
                currentImage = value;
                NotifyOfPropertyChange(() => CurrentImage);
            }
        }

        public Visibility CurrentImageVisibility
        {
            get { return currentImageVisibility; }
            set
            {
                currentImageVisibility = value;
                NotifyOfPropertyChange(() => CurrentImageVisibility);
            }
        }

        public Visibility PreviousVisibility
        {
            get { return previousVisibility; }
            set
            {
                previousVisibility = value;
                NotifyOfPropertyChange(() => PreviousVisibility);
            }
        }

        public Visibility NextVisibility
        {
            get { return nextVisibility; }
            set
            {
                nextVisibility = value;
                NotifyOfPropertyChange(() => NextVisibility);
            }
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

        private bool IsImageSelected
        {
            get { return CurrentImage != null; }
        }

        public event EventHandler<bool> IsImageSelectedChanged = delegate { };

        public void ReSelectFile()
        {
            HandleNewFilePath(fileTraverser.SelectFile(Constants.SupportedImageExtensions));
        }

        public void SelectFile()
        {
            HandleNewFilePath(fileTraverser.SelectFile(Constants.SupportedImageExtensions));
        }

        public void Next()
        {
            HandleNewFilePath(fileTraverser.GetNextFile());
        }

        public void Previous()
        {
            HandleNewFilePath(fileTraverser.GetPreviousFile());
        }

        private void HandleNewFilePath(string filePath)
        {
            if (filePath == null) return;

            var wasOldImageNull = CurrentImage == null;
            CurrentImage = new BitmapImage(new Uri(filePath));

            if (!wasOldImageNull) return;

            UpdateVisibilities();
            IsImageSelectedChanged(this, IsImageSelected);
        }

        private void UpdateVisibilities()
        {
            var selectVisibility = BoolToVisibilityConverter.ToVisibleOrCollapsed(!IsImageSelected);
            var otherControlsVisibility = BoolToVisibilityConverter.ToVisibleOrCollapsed(IsImageSelected);

            SelectFileVisibility = selectVisibility;

            PreviousVisibility = otherControlsVisibility;
            NextVisibility = otherControlsVisibility;
            CurrentImageVisibility = otherControlsVisibility;
        }
    }
}