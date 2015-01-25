using System;
using System.Windows;
using System.Windows.Media.Imaging;
using NUnit.Framework;
using Rhino.Mocks;
using SimplePhotoViewer.IO;
using SimplePhotoViewer.Tests.TestFiles;
using SimplePhotoViewer.UI.ViewModels;

namespace SimplePhotoViewer.Tests.UI.ViewModels
{
    [TestFixture]
    public class ImageViewModelTests
    {
        private IFileTraverser fileTraverser;
        private ImageViewModel imageViewModel;

        [SetUp]
        public void Setup()
        {
            fileTraverser = MockRepository.GenerateStub<IFileTraverser>();
            fileTraverser.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(TestFile.KoalaFilePath);
            fileTraverser.Stub(f => f.GetPreviousFile()).Return(TestFile.JellyfishFilePath);
            fileTraverser.Stub(f => f.GetNextFile()).Return(TestFile.LighthouseFilePath);

            imageViewModel = new ImageViewModel(fileTraverser);
        }

        [Test]
        public void _00_visibilities_are_initialized_correctly()
        {
            const bool isImageSelected = false;
            AssertThatVisibilitesAreCorrect(isImageSelected);
        }

        [Test]
        public void _01_select_file_calls_file_traverser()
        {
            imageViewModel.SelectFile();

            fileTraverser.AssertWasCalled(f => f.SelectFile(Constants.SupportedImageExtensions));
        }

        [Test]
        public void _02_select_file_assigns_current_image_if_file_is_returned()
        {
            Assert.IsNull(imageViewModel.CurrentImage);

            imageViewModel.SelectFile();

            Assert.IsNotNull(imageViewModel.CurrentImage);
        }

        [Test]
        public void _03_select_file_returns_if_null_is_returned()
        {
            fileTraverser.BackToRecord();
            fileTraverser.Replay();

            Assert.DoesNotThrow(() => imageViewModel.SelectFile());
        }

        [Test]
        public void _04_select_file_updates_visibilities()
        {
            imageViewModel.SelectFile();

            const bool isImageSelected = true;
            AssertThatVisibilitesAreCorrect(isImageSelected);
        }

        [Test]
        public void _05_next_calls_file_traverser()
        {
            imageViewModel.SelectFile();

            imageViewModel.Next();

            fileTraverser.AssertWasCalled(f => f.GetNextFile());
        }

        [Test]
        public void _06_next_assigns_current_image_if_file_is_returned()
        {
            imageViewModel.SelectFile();
            var img = imageViewModel.CurrentImage;

            imageViewModel.Next();

            Assert.AreNotEqual(img, imageViewModel.CurrentImage);
        }

        [Test]
        public void _07_next_returns_if_null_is_returned()
        {
            fileTraverser.BackToRecord();
            fileTraverser.Replay();

            Assert.DoesNotThrow(() => imageViewModel.Next());
        }

        [Test]
        public void _08_previous_calls_file_traverser()
        {
            imageViewModel.SelectFile();

            imageViewModel.Previous();

            fileTraverser.AssertWasCalled(f => f.GetPreviousFile());
        }

        [Test]
        public void _09_previous_assigns_current_image_if_file_is_returned()
        {
            imageViewModel.SelectFile();
            var img = imageViewModel.CurrentImage;

            imageViewModel.Previous();

            Assert.AreNotEqual(img, imageViewModel.CurrentImage);
        }

        [Test]
        public void _10_previous_returns_if_null_is_returned()
        {
            fileTraverser.BackToRecord();
            fileTraverser.Replay();

            Assert.DoesNotThrow(() => imageViewModel.Previous());
        }

        [Test]
        public void _11_reselect_file_calls_file_traverser()
        {
            imageViewModel.SelectFile();

            fileTraverser.AssertWasCalled(f => f.SelectFile(Constants.SupportedImageExtensions));
        }

        [Test]
        public void _12_reselect_file_assigns_current_image_if_file_is_returned()
        {
            imageViewModel.SelectFile();
            var img = imageViewModel.CurrentImage;

            imageViewModel.SelectFile();

            Assert.AreNotEqual(img, imageViewModel.CurrentImage);
        }

        [Test]
        public void _13_reselect_file_returns_if_null_is_returned()
        {
            fileTraverser.BackToRecord();
            fileTraverser.Replay();

            Assert.DoesNotThrow(() => imageViewModel.SelectFile());
        }

        [Test]
        public void _14_is_image_selected_changed_event_is_raised_when_image_is_selected()
        {
            var c = 0;
            imageViewModel.IsImageSelectedChanged += delegate { c++; };

            imageViewModel.SelectFile();

            Assert.AreEqual(1, c);
        }

        [Test]
        public void _15_is_image_selected_changed_event_is_raised_only_once()
        {
            fileTraverser.BackToRecord();
            fileTraverser.Replay();

            fileTraverser.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(null).Repeat.Twice();
            fileTraverser.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(TestFile.TulipsFilePath);

            var c = 0;
            imageViewModel.IsImageSelectedChanged += delegate { c++; };

            imageViewModel.SelectFile();
            imageViewModel.SelectFile();
            imageViewModel.SelectFile();
            imageViewModel.Next();
            imageViewModel.Next();
            imageViewModel.SelectFile();
            imageViewModel.Previous();

            Assert.AreEqual(1, c);
        }

        [Test]
        public void _16_programmatically_assigning_current_image_raises_event()
        {
            var c = 0;
            imageViewModel.IsImageSelectedChanged += delegate { c++; };
            Assert.IsNull(imageViewModel.CurrentImage);

            imageViewModel.CurrentImage = new BitmapImage(new Uri(TestFile.JellyfishFilePath));

            Assert.AreEqual(1, c);
        }

        [Test]
        public void _17_programmatically_assigning_current_image_updates_visibilities()
        {
            Assert.IsNull(imageViewModel.CurrentImage);

            imageViewModel.CurrentImage = new BitmapImage(new Uri(TestFile.JellyfishFilePath));

            const bool isImageSelected = true;
            AssertThatVisibilitesAreCorrect(isImageSelected);
        }

        [Test]
        public void _18_is_image_selected_changed_event_is_raised_when_changed_from_null()
        {
            var c = 0;
            imageViewModel.IsImageSelectedChanged += delegate { c++; };
            Assert.IsNull(imageViewModel.CurrentImage);

            imageViewModel.CurrentImage = null;
            imageViewModel.CurrentImage = null;
            imageViewModel.CurrentImage = new BitmapImage(new Uri(TestFile.JellyfishFilePath));
            imageViewModel.CurrentImage = new BitmapImage(new Uri(TestFile.KoalaFilePath));
            imageViewModel.CurrentImage = null;
            imageViewModel.CurrentImage = new BitmapImage(new Uri(TestFile.JellyfishFilePath));

            Assert.AreEqual(3, c);
        }

        private void AssertThatVisibilitesAreCorrect(bool isImageSelected)
        {
            var selectFileButtonVisibility = isImageSelected ? Visibility.Collapsed : Visibility.Visible;
            var otherControlsVisibility = isImageSelected ? Visibility.Visible : Visibility.Collapsed;

            Assert.AreEqual(selectFileButtonVisibility, imageViewModel.SelectFileVisibility);
            Assert.AreEqual(otherControlsVisibility, imageViewModel.PreviousVisibility);
            Assert.AreEqual(otherControlsVisibility, imageViewModel.NextVisibility);
            Assert.AreEqual(otherControlsVisibility, imageViewModel.CurrentImageVisibility);
        }

    }
}