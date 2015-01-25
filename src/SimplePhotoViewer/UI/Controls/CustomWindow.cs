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
        //public const string PartMinimizeButtonName = "Minimize";
        //public const string PartNormalButtonName = "Normal";
        //public const string PartMaximizeButtonName = "Maximize";
        //public const string PartExitButtonName = "Exit";
        
        //private Button minimize;
        //private Button normal;
        //private Button maximize;
        //private Button exit;

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

        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();

        //    if (minimize != null) minimize.Click -= Minimize;
        //    minimize = GetRequiredTemplateChild(PartMinimizeButtonName) as Button;
        //    minimize.Click += Minimize;

        //    if (normal != null) normal.Click -= Normal;
        //    normal = GetRequiredTemplateChild(PartNormalButtonName) as Button;
        //    normal.Click += Normal;

        //    if (maximize != null) maximize.Click -= Maximize;
        //    maximize = GetRequiredTemplateChild(PartMaximizeButtonName) as Button;
        //    maximize.Click += Maximize;

        //    if (exit != null) exit.Click -= Exit;
        //    exit = GetRequiredTemplateChild(PartExitButtonName) as Button;
        //    exit.Click += Exit;

        //    UpdateWindowControlVisibilities();
        //}       

        //public void Minimize(object sender, EventArgs args)
        //{
        //    WindowState = WindowState.Minimized;
        //}

        //public void Normal(object sender, EventArgs args)
        //{
        //    WindowState = WindowState.Normal;
        //}

        //public void Maximize(object sender, EventArgs args)
        //{
        //    WindowState = WindowState.Maximized;
        //}

        //public void Exit(object sender, EventArgs args)
        //{
        //    Close();
        //}

        //public void UpdateWindowControlVisibilities()
        //{
        //    if (WindowState == WindowState.Maximized)
        //    {
        //        maximize.Visibility = Visibility.Collapsed;
        //        normal.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        maximize.Visibility = Visibility.Visible;
        //        normal.Visibility = Visibility.Collapsed;
        //    }
        //}

        //private object GetRequiredTemplateChild(string name)
        //{
        //    var templateChild = GetTemplateChild(name);
        //    if (templateChild == null)
        //        throw new ArgumentException("The " + name + " template part is required!");
        //    return templateChild;
        //}

    }
}
