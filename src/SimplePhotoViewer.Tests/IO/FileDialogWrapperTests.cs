using System;
using NUnit.Framework;
using SimplePhotoViewer.IO;

namespace SimplePhotoViewer.Tests.IO
{
    [TestFixture]
    public class FileDialogWrapperTests
    {
        private FileDialogWrapper fileDialog;

        [SetUp]
        public void Setup()
        {
            fileDialog = new FileDialogWrapper();
        }

        [Test, STAThread, Explicit("Creates a modal dialog.")]
        public void can_select_file()
        {
            Console.WriteLine(fileDialog.SelectFile(Constants.SupportedImageExtensions));
        }

    }
}