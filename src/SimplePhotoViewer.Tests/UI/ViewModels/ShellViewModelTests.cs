using System;
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
        private IWindowStateHelper windowStateHelper;

        [SetUp]
        public void Setup()
        {
            windowStateHelper = MockRepository.GenerateStub<IWindowStateHelper>();
            imageViewModel = MockRepository.GenerateStub<IImageViewModel>();

            shellViewModel = new ShellViewModel(windowStateHelper, imageViewModel);
        }

        [Test]
        public void image_view_model_is_assigned()
        {
            Assert.IsNotNull(shellViewModel.ImageViewModel);
        }

        [Test]
        public void maximize_sets_window_state()
        {
            shellViewModel.Maximize();

            Assert.AreEqual(WindowState.Maximized, windowStateHelper.WindowState);
        }

        [Test]
        public void minimize_sets_window_state()
        {
            shellViewModel.Minimize();

            Assert.AreEqual(WindowState.Minimized, windowStateHelper.WindowState);
        }

        [Test]
        public void normal_sets_window_state()
        {
            shellViewModel.Normal();

            Assert.AreEqual(WindowState.Normal, windowStateHelper.WindowState);
        }

        [Test]
        public void reselect_file_calls_image_view_model()
        {
            shellViewModel.ReSelectFile();

            imageViewModel.AssertWasCalled(i => i.ReSelectFile());
        }

        [Test]
        public void reselect_file_visibility_is_initialized_to_hidden()
        {
            Assert.AreEqual(Visibility.Hidden, shellViewModel.ReSelectFileVisibility);
        }

        [Test]
        public void when_is_image_selected_changed_event_is_raised_visibility_is_updated()
        {
            Assert.AreEqual(Visibility.Hidden, shellViewModel.ReSelectFileVisibility);

            imageViewModel.Raise(i => i.IsImageSelectedChanged += null, imageViewModel, true);
            Assert.AreEqual(Visibility.Visible, shellViewModel.ReSelectFileVisibility);

            imageViewModel.Raise(i => i.IsImageSelectedChanged += null, imageViewModel, false);
            Assert.AreEqual(Visibility.Hidden, shellViewModel.ReSelectFileVisibility);
        }

        [Test]
        public void update_window_control_visibilities_correctly_assigns_visibilities()
        {
            AssertThatVisibilitiesAreCorrectlyUpdated(WindowState.Maximized, Visibility.Collapsed, Visibility.Visible);
            AssertThatVisibilitiesAreCorrectlyUpdated(WindowState.Normal, Visibility.Visible, Visibility.Collapsed);
            AssertThatVisibilitiesAreCorrectlyUpdated(WindowState.Minimized, Visibility.Visible, Visibility.Collapsed);
        }

        private void AssertThatVisibilitiesAreCorrectlyUpdated(WindowState windowState, Visibility expectedMaximize,
            Visibility expectedNormal)
        {
            windowStateHelper.WindowState = windowState;

            shellViewModel.UpdateWindowControlVisibilities();

            Assert.AreEqual(expectedMaximize, shellViewModel.MaximizeVisibility);
            Assert.AreEqual(expectedNormal, shellViewModel.NormalVisibility);
        }

    }
}