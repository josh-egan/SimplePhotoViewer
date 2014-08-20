using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace SimplePhotoViewer
{
    public interface IFileDialogWrapper
    {
        string SelectFile(params string[] extensions);
    }

    public class FileDialogWrapper : IFileDialogWrapper
    {

        public string SelectFile(params string[] extensions)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select an image.",
                Filter = CreateFileFilter(extensions),
                Multiselect = false
            };
            var result = dialog.ShowDialog();

            if (result != null && (bool)result)
                return dialog.FileNames.First();
            return null;
        }

        private static string CreateFileFilter(ICollection<string> extensions)
        {
            if (extensions == null || extensions.Count == 0)
                return "";

            var filter = extensions.Count > 1 ? "All Available|" + CreateExtensionList(extensions) : "";
            var fullFilter = extensions.Aggregate(filter,
                (current, ext) =>
                    current + "|" + ext.ToUpper() + " Files|*." + ext.ToLower());
            return fullFilter.Trim('|');
        }

        private static string CreateExtensionList(IEnumerable<string> extensions)
        {
            var extList = extensions.Aggregate("", (current, ext) => current + ("*." + ext.ToLower() + ";"));
            return extList.Trim(';');
        }

    }
}
