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
        private ShellViewModel shellViewModel;
        private IWindowStateHelper windowStateHelper;

        [SetUp]
        public void Setup()
        {
            windowStateHelper = MockRepository.GenerateStub<IWindowStateHelper>();

            shellViewModel = new ShellViewModel(windowStateHelper);
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