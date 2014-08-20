using System.Windows;
using NUnit.Framework;
using SimplePhotoViewer.UI;

namespace SimplePhotoViewer.Tests.UI
{
    [TestFixture]
    public class BoolToVisibilityConverterTests
    {

        [Test]
        public void to_visible_or_collapsed_returns_collapsed_for_false()
        {
            Assert.AreEqual(Visibility.Collapsed, BoolToVisibilityConverter.ToVisibleOrCollapsed(false));
        }

        [Test]
        public void to_visible_or_collapsed_returns_visible_for_true()
        {
            Assert.AreEqual(Visibility.Visible, BoolToVisibilityConverter.ToVisibleOrCollapsed(true));
        }

        [Test]
        public void to_visible_or_hidden_returns_hidden_for_false()
        {
            Assert.AreEqual(Visibility.Hidden, BoolToVisibilityConverter.ToVisibleOrHidden(false));
        }

        [Test]
        public void to_visible_or_hidden_returns_visible_for_true()
        {
            Assert.AreEqual(Visibility.Visible, BoolToVisibilityConverter.ToVisibleOrHidden(true));
        }

    }
}