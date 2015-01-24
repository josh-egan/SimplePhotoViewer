using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimplePhotoViewer.UI.Util
{
    public static class BoolToVisibilityConverter
    {
        public static Visibility ToVisibleOrCollapsed(bool b)
        {
            return b ? Visibility.Visible : Visibility.Collapsed;
        }

        public static Visibility ToVisibleOrHidden(bool b)
        {
            return b ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
