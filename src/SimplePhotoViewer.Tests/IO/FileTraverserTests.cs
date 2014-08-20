using NUnit.Framework;
using Rhino.Mocks;
using SimplePhotoViewer.IO;
using SimplePhotoViewer.Tests.TestFiles;

namespace SimplePhotoViewer.Tests.IO
{
    [TestFixture]
    public class FileTraverserTests
    {
        private ICanSelectFile fileSelector;
        private FileTraverser fileTraverser;

        [SetUp]
        public void Setup()
        {
            fileSelector = MockRepository.GenerateStub<ICanSelectFile>();

            fileTraverser = new FileTraverser(fileSelector);
        }

        [Test]
        public void get_next_returns_next_file()
        {
            fileSelector.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(TestFile.KoalaFilePath);

            fileTraverser.SelectFile(Constants.SupportedImageExtensions);

            Assert.AreEqual(TestFile.LighthouseFilePath, fileTraverser.GetNextFile());
        }

        [Test]
        public void get_next_returns_null_if_file_is_not_selected()
        {
            Assert.IsNull(fileTraverser.GetNextFile());
        }

        [Test]
        public void get_next_updates_current_file()
        {
            fileSelector.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(TestFile.KoalaFilePath);

            fileTraverser.SelectFile(Constants.SupportedImageExtensions);
            fileTraverser.GetNextFile();

            Assert.AreEqual(TestFile.LighthouseFilePath, fileTraverser.CurrentFile);
        }

        [Test]
        public void get_next_wraps_around_if_at_end_of_file_list()
        {
            fileSelector.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(TestFile.TulipsFilePath);

            fileTraverser.SelectFile(Constants.SupportedImageExtensions);

            Assert.AreEqual(TestFile.JellyfishFilePath, fileTraverser.GetNextFile());
        }

        [Test]
        public void get_previous_returns_null_if_file_is_not_selected()
        {
            Assert.IsNull(fileTraverser.GetPreviousFile());
        }

        [Test]
        public void get_previous_returns_previous_file()
        {
            fileSelector.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(TestFile.KoalaFilePath);

            fileTraverser.SelectFile(Constants.SupportedImageExtensions);

            Assert.AreEqual(TestFile.JellyfishFilePath, fileTraverser.GetPreviousFile());
        }

        [Test]
        public void get_previous_updates_current_file()
        {
            fileSelector.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(TestFile.KoalaFilePath);

            fileTraverser.SelectFile(Constants.SupportedImageExtensions);
            fileTraverser.GetPreviousFile();

            Assert.AreEqual(TestFile.JellyfishFilePath, fileTraverser.CurrentFile);
        }

        [Test]
        public void get_previous_wraps_around_if_at_beginning_of_file_list()
        {
            fileSelector.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(TestFile.JellyfishFilePath);

            fileTraverser.SelectFile(Constants.SupportedImageExtensions);

            Assert.AreEqual(TestFile.TulipsFilePath, fileTraverser.GetPreviousFile());
        }

        [Test]
        public void select_file_calls_file_dialog_wrapper()
        {
            fileTraverser.SelectFile(Constants.SupportedImageExtensions);

            fileSelector.AssertWasCalled(f => f.SelectFile(Constants.SupportedImageExtensions));
        }

        [Test]
        public void select_file_returns_file()
        {
            fileSelector.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(TestFile.KoalaFilePath);

            Assert.AreEqual(TestFile.KoalaFilePath, fileTraverser.SelectFile(Constants.SupportedImageExtensions));
        }

        [Test]
        public void select_file_returns_null_if_selector_returns_null()
        {
            fileSelector.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(null);

            Assert.AreEqual(null, fileTraverser.SelectFile(Constants.SupportedImageExtensions));
        }

        [Test]
        public void select_file_returns_previous_file_if_null()
        {
            fileSelector.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(TestFile.KoalaFilePath)
                .Repeat
                .Once();
            fileSelector.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(null).Repeat.Once();

            Assert.AreEqual(TestFile.KoalaFilePath, fileTraverser.SelectFile(Constants.SupportedImageExtensions));
            Assert.AreEqual(TestFile.KoalaFilePath, fileTraverser.SelectFile(Constants.SupportedImageExtensions));
        }

        [Test]
        public void select_file_updates_current_file()
        {
            fileSelector.Stub(f => f.SelectFile(Constants.SupportedImageExtensions)).Return(TestFile.KoalaFilePath);

            fileTraverser.SelectFile(Constants.SupportedImageExtensions);

            Assert.AreEqual(TestFile.KoalaFilePath, fileTraverser.CurrentFile);
        }

    }
}