using System;
using System.IO;
using System.Linq;
using Ninject;

namespace SimplePhotoViewer.IO
{
    public interface IFileTraverser : ICanSelectFile
    {
        string CurrentFile { get; }

        string GetNextFile();
        string GetPreviousFile();
    }

    public class FileTraverser : IFileTraverser
    {
        private readonly ICanSelectFile fileSelector;
        private int currentFileIndex;
        private string[] directoryFiles;

        [Inject]
        public FileTraverser(ICanSelectFile fileSelector)
        {
            this.fileSelector = fileSelector;
        }

        public string CurrentFile
        {
            get { return directoryFiles != null ? directoryFiles[currentFileIndex] : null; }
        }

        public string SelectFile(params string[] extensions)
        {
            var selectedFile = fileSelector.SelectFile(extensions);
            UpdateDirectoryFilesAndCurrentIndex(selectedFile);
            return CurrentFile;
        }

        public string GetNextFile()
        {
            if (directoryFiles == null) return null;

            if (currentFileIndex + 1 >= directoryFiles.Length) currentFileIndex = 0;
            else currentFileIndex++;

            return CurrentFile;
        }

        public string GetPreviousFile()
        {
            if (directoryFiles == null) return null;

            if (currentFileIndex - 1 < 0) currentFileIndex = directoryFiles.Length - 1;
            else currentFileIndex--;

            return CurrentFile;
        }

        private void UpdateDirectoryFilesAndCurrentIndex(string selectedFile)
        {
            if (selectedFile == null) return;

            UpdateDirectoryFiles(selectedFile);
            UpdateCurrentFileIndex(selectedFile);
        }

        private void UpdateDirectoryFiles(string selectedFile)
        {
            var parentDir = Path.GetDirectoryName(selectedFile);
            if (parentDir == null)
                throw new ArgumentException(
                    "What? The file does not have a parent directory! This should not be possible...");

            directoryFiles = Directory.GetFiles(parentDir).Where(f =>
            {
                var ext = Path.GetExtension(f);
                return ext != null && Constants.SupportedImageExtensions.Any(supported => ext.Equals("." + supported));
            }).ToArray();
        }

        private void UpdateCurrentFileIndex(string selectedFile)
        {
            for (var i = 0; i < directoryFiles.Length; i++)
                if (directoryFiles[i].Equals(selectedFile))
                {
                    currentFileIndex = i;
                    break;
                }
        }

    }
}