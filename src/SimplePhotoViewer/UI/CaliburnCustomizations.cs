using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;

namespace SimplePhotoViewer.UI
{
    public static class CaliburnCustomizations
    {
        public static void AssignCustomBindingFunction()
        {
            var baseBindingFunction = ViewModelBinder.Bind;
            ViewModelBinder.Bind = (viewModel, view, context) =>
            {
                baseBindingFunction(viewModel, view, context);
                BindPropertiesOfNamedElements(viewModel, view);
            };
        }

        private static void BindPropertiesOfNamedElements(object viewModel, DependencyObject view)
        {
            var frameworkElement = View.GetFirstNonGeneratedView(view) as FrameworkElement;
            if (frameworkElement == null) return;

            var viewModelType = viewModel.GetType();
            var namedElements = BindingScope.GetNamedElements(frameworkElement);

            const BindingFlags flags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;
            var properties = viewModelType.GetProperties(flags);

            foreach (var element in namedElements)
            {
                var trimmed = element.Name.Trim('_');
                var parts = trimmed.Split(new[] {'_'}, StringSplitOptions.RemoveEmptyEntries);
                var baseElementName = parts[0];

                var matchingProperties = properties
                    .Where(p => p.Name.StartsWith(baseElementName) && !p.Name.Equals(baseElementName));

                foreach (var matchingProperty in matchingProperties)
                    ConventionManager.SetBinding(viewModelType, matchingProperty.Name, matchingProperty, element, null,
                        GetDependencyProperty(matchingProperty.Name.Remove(0, baseElementName.Length)));
            }
        }

        private static DependencyProperty GetDependencyProperty(string dependencyPropertyName)
        {
            if (dependencyPropertyName.Equals("Visibility")) return UIElement.VisibilityProperty;
            if (dependencyPropertyName.Equals("IsEnabled")) return UIElement.IsEnabledProperty;

            var message = "Support for the property '" + dependencyPropertyName + "' has not been added. " +
                          "Please correct bad spelling or add support for this dependency property.";
            throw new NotSupportedException(message);
        }
    }
}