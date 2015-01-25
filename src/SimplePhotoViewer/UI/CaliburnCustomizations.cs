using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;

namespace SimplePhotoViewer.UI
{
    public static class CaliburnCustomizations
    {
        private static readonly Dictionary<string, DependencyProperty> CommonDependencyProperties = new Dictionary<string, DependencyProperty>
        {
            {GetMemberName(() => UIElement.IsEnabledProperty), UIElement.IsEnabledProperty}
        };

        public static void AssignCustomBindingFunction()
        {
            var baseBindingFunction = ViewModelBinder.Bind;
            ViewModelBinder.Bind = (viewModel, view, context) =>
            {
                baseBindingFunction(viewModel, view, context);
                BindPropertiesOfNamedElements(viewModel, view);
            };
        }

        public static void BindNamedElements(IEnumerable<FrameworkElement> elements, Type viewModelType)
        {
            var collection = elements as ICollection<FrameworkElement> ?? elements.ToArray();

            ViewModelBinder.BindActions(collection, viewModelType);
            ViewModelBinder.BindProperties(collection, viewModelType);
            CaliburnCustomizations.BindProperties(collection, viewModelType);
        }

        private static void BindPropertiesOfNamedElements(object viewModel, DependencyObject view)
        {
            var frameworkElement = View.GetFirstNonGeneratedView(view) as FrameworkElement;
            if (frameworkElement == null) return;

            var viewModelType = viewModel.GetType();
            var namedElements = BindingScope.GetNamedElements(frameworkElement);

            BindProperties(namedElements, viewModelType);
        }

        private static void BindProperties(IEnumerable<FrameworkElement> namedElements, Type viewModelType)
        {
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
                        GetDependencyProperty(element, matchingProperty.Name.Remove(0, baseElementName.Length)));
            }
        }

        private static DependencyProperty GetDependencyProperty(FrameworkElement element, string dependencyPropertyName)
        {
            var fieldName = dependencyPropertyName + "Property";

            if (CommonDependencyProperties.ContainsKey(fieldName))
                return CommonDependencyProperties[fieldName];
            else
            {
                var dp = UseReflectionToFindDependencyProperty(element, fieldName);
                CommonDependencyProperties.Add(fieldName, dp);
                return dp;
            }
        }

        private static DependencyProperty UseReflectionToFindDependencyProperty(FrameworkElement element, string fieldName)
        {
            var type = element.GetType();
            FieldInfo fieldInfo = null;

            do
            {
                fieldInfo = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
                type = type.BaseType;
            } while (fieldInfo == null && type != null);

            if (fieldInfo == null)
                throw new Exception("Could not find the the " + fieldName + " property on element of type " + element.GetType().Name);

            return (DependencyProperty)fieldInfo.GetValue(element);
        }

        private static string GetMemberName<T>(Expression<Func<T>> expr)
        {
            var memberExpr = expr.Body as MemberExpression;
            if (memberExpr == null)
                throw new ArgumentException("Expression body must be a MemberExpression");
            return memberExpr.Member.Name;
        }
    }
}