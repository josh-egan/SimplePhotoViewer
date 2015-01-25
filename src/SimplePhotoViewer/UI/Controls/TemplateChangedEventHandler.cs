using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePhotoViewer.UI.Controls
{
    public delegate void TemplateChangedEventHandler (object sender, TemplateChangedEventArgs eventArgs);

    public class TemplateChangedEventArgs : EventArgs
    {
        public readonly ControlTemplate OldTemplate;
        public readonly ControlTemplate NewTemplate;

        public TemplateChangedEventArgs(ControlTemplate oldTemplate, ControlTemplate newTemplate)
        {
            OldTemplate = oldTemplate;
            NewTemplate = newTemplate;
        }
    }
}
