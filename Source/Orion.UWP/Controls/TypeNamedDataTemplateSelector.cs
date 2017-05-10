using System;
using System.Linq;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Orion.UWP.Controls
{
    [ContentProperty(Name = "Templates")]
    public class TypeNamedDataTemplateSelector : DataTemplateSelector
    {
        // ReSharper disable once CollectionNeverUpdated.Global
        public DataTemplateCollection Templates { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return Templates.SingleOrDefault(w => GetTarget(w).Name == item.GetType().Name);
        }

        #region Target

        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.RegisterAttached("Target", typeof(Type), typeof(TypeNamedDataTemplateSelector), new PropertyMetadata(null));

        public static Type GetTarget(DependencyObject obj)
        {
            return (Type) obj.GetValue(TargetProperty);
        }

        public static void SetTarget(DependencyObject obj, Type value)
        {
            obj.SetValue(TargetProperty, value);
        }

        #endregion
    }
}