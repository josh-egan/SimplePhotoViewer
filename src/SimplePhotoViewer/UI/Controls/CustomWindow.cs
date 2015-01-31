using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePhotoViewer.UI.Controls
{

    [TemplatePartAttribute(Name = "Minimize", Type = typeof(Button))]
    [TemplatePartAttribute(Name = "Normal", Type = typeof(Button))]
    [TemplatePartAttribute(Name = "Maximize", Type = typeof(Button))]
    [TemplatePartAttribute(Name = "Exit", Type = typeof(Button))]
    public class CustomWindow : Window
    {

        public CustomWindow()
        {
            MouseLeftButtonDown += (s, a) => DragMove();
        }

        public event TemplateChangedEventHandler TemplateChanged = delegate { };

        protected override void OnTemplateChanged(ControlTemplate oldTemplate, ControlTemplate newTemplate)
        {
            base.OnTemplateChanged(oldTemplate, newTemplate);

            TemplateChanged(this, new TemplateChangedEventArgs(oldTemplate, newTemplate));
        }

    }
}
