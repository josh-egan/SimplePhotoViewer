using System;
using System.Windows;
using System.Windows.Input;
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
        void HandleKeyDown(KeyEventArgs eventArgs);
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

            UpdateVisibilites();
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

        public Visibility CurrentImageVisibility
        {
            get { return currentImageVisibility; }
            set
            {
                currentImageVisibility = value;
                NotifyOfPropertyChange(() => CurrentImageVisibility);
            }
        }

        public BitmapSource CurrentImage
        {
            get { return currentImage; }
            set
            {
                var isOldImageNull = currentImage == null;
                var isNewImageNull = value == null;

                currentImage = value;
                NotifyOfPropertyChange(() => CurrentImage);

                if (isOldImageNull == isNewImageNull) return;

                UpdateVisibilites();
                IsImageSelectedChanged(this, IsImageSelected);
            }
        }

        private bool IsImageSelected
        {
            get { return CurrentImage != null; }
        }

        public event EventHandler<bool> IsImageSelectedChanged = delegate { };

        public void HandleKeyDown(KeyEventArgs eventArgs)
        {
            var key = eventArgs.Key;
            if (key == Key.Down || key == Key.Right)
                Next();
            else if (key == Key.Up || key == Key.Left)
                Previous();
        }

        public void ReSelectFile()
        {
            UpdateCurrentImage(fileTraverser.SelectFile(Constants.SupportedImageExtensions));
        }

        public void SelectFile()
        {
            UpdateCurrentImage(fileTraverser.SelectFile(Constants.SupportedImageExtensions));
        }

        public void Next()
        {
            if (IsImageSelected)
                UpdateCurrentImage(fileTraverser.GetNextFile());
        }

        public void Previous()
        {
            if (IsImageSelected)
                UpdateCurrentImage(fileTraverser.GetPreviousFile());
        }

        private void UpdateCurrentImage(string filePath)
        {
            if (filePath == null) return;
            CurrentImage = new BitmapImage(new Uri(filePath));
        }

        private void UpdateVisibilites()
        {
            var selectVisibility = BoolToVisibilityConverter.ToVisibleOrCollapsed(!IsImageSelected);
            var otherVisibility = BoolToVisibilityConverter.ToVisibleOrCollapsed(IsImageSelected);

            SelectFileVisibility = selectVisibility;
            PreviousVisibility = otherVisibility;
            NextVisibility = otherVisibility;
            CurrentImageVisibility = otherVisibility;
        }

    }
}