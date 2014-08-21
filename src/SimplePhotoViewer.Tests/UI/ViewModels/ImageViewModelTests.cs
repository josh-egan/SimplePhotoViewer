using System.Windows;
using NUnit.Framework;
using Rhino.Mocks;
using SimplePhotoViewer.IO;
using SimplePhotoViewer.Tests.TestFiles;
using SimplePhotoViewer.UI.ViewModels;

namespace SimplePhotoViewer.Tests.UI.ViewModels
{
//    [TestFixture]
//    public class ImageViewModelTests
//    {
//        private IFileTraverser fileTraverser;
//        private ImageViewModel imageViewModel;
//
//        [SetUp]
//        public void Setup()
//        {
//            fileTraverser = MockRepository.GenerateStub<IFileTraverser>();
//            fileTraverser.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(TestFile.KoalaFilePath)
//                .Repeat.Once();
//            fileTraverser.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(TestFile.TulipsFilePath);
//            fileTraverser.Stub(f => f.GetPreviousFile()).Return(TestFile.JellyfishFilePath);
//            fileTraverser.Stub(f => f.GetNextFile()).Return(TestFile.LighthouseFilePath);
//
//            imageViewModel = new ImageViewModel(fileTraverser);
//        }
//
//        [Test]
//        public void _00_visibilities_are_initialized_correctly()
//        {
//            const bool isImageSelected = false;
//            AssertThatVisibilitesAreCorrect(isImageSelected);
//        }
//
//        [Test]
//        public void _01_select_file_calls_file_traverser()
//        {
//            imageViewModel.SelectFile();
//
//            fileTraverser.AssertWasCalled(f => f.SelectFile(Constants.SupportedImageExtensions));
//        }
//
//        [Test]
//        public void _02_select_file_assigns_current_image_if_file_is_returned()
//        {
//            Assert.IsNull(imageViewModel.CurrentImage);
//
//            imageViewModel.SelectFile();
//
//            Assert.IsNotNull(imageViewModel.CurrentImage);
//        }
//
//        [Test]
//        public void _03_select_file_returns_if_null_is_returned()
//        {
//            fileTraverser.BackToRecord();
//            fileTraverser.Replay();
//
//            Assert.DoesNotThrow(() => imageViewModel.SelectFile());
//        }
//
//        [Test]
//        public void _04_select_file_updates_visibilities()
//        {
//            imageViewModel.SelectFile();
//
//            const bool isImageSelected = true;
//            AssertThatVisibilitesAreCorrect(isImageSelected);
//        }
//
//        [Test]
//        public void _05_next_calls_file_traverser()
//        {
//            imageViewModel.Next();
//
//            fileTraverser.AssertWasCalled(f => f.GetNextFile());
//        }
//
//        [Test]
//        public void _06_next_assigns_current_image_if_file_is_returned()
//        {
//            imageViewModel.SelectFile();
//            var img = imageViewModel.CurrentImage;
//
//            imageViewModel.Next();
//
//            Assert.AreNotEqual(img, imageViewModel.CurrentImage);
//        }
//
//        [Test]
//        public void _07_next_returns_if_null_is_returned()
//        {
//            fileTraverser.BackToRecord();
//            fileTraverser.Replay();
//
//            Assert.DoesNotThrow(() => imageViewModel.Next());
//        }
//
//        [Test]
//        public void _08_previous_calls_file_traverser()
//        {
//            imageViewModel.Previous();
//
//            fileTraverser.AssertWasCalled(f => f.GetPreviousFile());
//        }
//
//        [Test]
//        public void _09_previous_assigns_current_image_if_file_is_returned()
//        {
//            imageViewModel.SelectFile();
//            var img = imageViewModel.CurrentImage;
//
//            imageViewModel.Previous();
//
//            Assert.AreNotEqual(img, imageViewModel.CurrentImage);
//        }
//
//        [Test]
//        public void _10_previous_returns_if_null_is_returned()
//        {
//            fileTraverser.BackToRecord();
//            fileTraverser.Replay();
//
//            Assert.DoesNotThrow(() => imageViewModel.Previous());
//        }
//
//        [Test]
//        public void _11_reselect_file_calls_file_traverser()
//        {
//            imageViewModel.ReSelectFile();
//
//            fileTraverser.AssertWasCalled(f => f.SelectFile(Constants.SupportedImageExtensions));
//        }
//
//        [Test]
//        public void _12_reselect_file_assigns_current_image_if_file_is_returned()
//        {
//            imageViewModel.SelectFile();
//            var img = imageViewModel.CurrentImage;
//
//            imageViewModel.ReSelectFile();
//
//            Assert.AreNotEqual(img, imageViewModel.CurrentImage);
//        }
//
//        [Test]
//        public void _13_reselect_file_returns_if_null_is_returned()
//        {
//            fileTraverser.BackToRecord();
//            fileTraverser.Replay();
//
//            Assert.DoesNotThrow(() => imageViewModel.ReSelectFile());
//        }
//
//        [Test]
//        public void _14_is_image_selected_changed_event_is_raised_when_image_is_selected()
//        {
//            var c = 0;
//            imageViewModel.IsImageSelectedChanged += delegate { c++; };
//
//            imageViewModel.SelectFile();
//
//            Assert.AreEqual(1, c);
//        }
//
//        [Test]
//        public void _15_is_image_selected_changed_event_is_raised_only_once()
//        {
//            var c = 0;
//            imageViewModel.IsImageSelectedChanged += delegate { c++; };
//
//            imageViewModel.SelectFile();
//            imageViewModel.Next();
//            imageViewModel.Next();
//            imageViewModel.ReSelectFile();
//            imageViewModel.Previous();
//
//            Assert.AreEqual(1, c);
//        }
//
//        private void AssertThatVisibilitesAreCorrect(bool isImageSelected)
//        {
//            var selectFileButtonVisibility = isImageSelected ? Visibility.Collapsed : Visibility.Visible;
//            var otherControlsVisibility = isImageSelected ? Visibility.Visible : Visibility.Collapsed;
//
//            Assert.AreEqual(selectFileButtonVisibility, imageViewModel.SelectFileVisibility);
//            Assert.AreEqual(otherControlsVisibility, imageViewModel.PreviousVisibility);
//            Assert.AreEqual(otherControlsVisibility, imageViewModel.NextVisibility);
//            Assert.AreEqual(otherControlsVisibility, imageViewModel.CurrentImageVisibility);
//        }
//
//    }
}