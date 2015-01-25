using System.Windows;
using NUnit.Framework;
using Rhino.Mocks;
using SimplePhotoViewer.UI;
using SimplePhotoViewer.UI.ViewModels;

namespace SimplePhotoViewer.Tests.UI.ViewModels
{
    [TestFixture]
    public class ShellViewModelTests
    {
        private IImageViewModel imageViewModel;
        private ShellViewModel shellViewModel;

        [SetUp]
        public void Setup()
        {
            imageViewModel = MockRepository.GenerateStub<IImageViewModel>();

            shellViewModel = new ShellViewModel(imageViewModel);
        }

        [Test]
        public void image_view_model_is_assigned()
        {
            Assert.IsNotNull(shellViewModel.ImageViewModel);
        }

        [Test]
        public void reselect_file_calls_image_view_model()
        {
            shellViewModel.SelectFile();

            imageViewModel.AssertWasCalled(i => i.SelectFile());
        }

        [Test]
        public void reselect_file_visibility_is_initialized_to_hidden()
        {
            Assert.AreEqual(Visibility.Hidden, shellViewModel.SelectFileVisibility);
        }

        [Test]
        public void when_is_image_selected_changed_event_is_raised_visibility_is_updated()
        {
            Assert.AreEqual(Visibility.Hidden, shellViewModel.SelectFileVisibility);

            imageViewModel.Raise(i => i.IsImageSelectedChanged += null, imageViewModel, true);
            Assert.AreEqual(Visibility.Visible, shellViewModel.SelectFileVisibility);

            imageViewModel.Raise(i => i.IsImageSelectedChanged += null, imageViewModel, false);
            Assert.AreEqual(Visibility.Hidden, shellViewModel.SelectFileVisibility);
        }
    }
}