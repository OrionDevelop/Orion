using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Microsoft.Xaml.Interactivity;

namespace Orion.UWP.Behaviors
{
    internal class ScrollingBehavior : Behavior<ScrollViewer>
    {
        public static readonly DependencyProperty HorizontalScrollOffsetProperty =
            DependencyProperty.Register(nameof(HorizontalScrollOffset), typeof(double), typeof(ScrollingBehavior),
                                        new PropertyMetadata(typeof(double), HorizontalScrollOffsetChanged));

        public static readonly DependencyProperty VerticalScrollOffsetProperty =
            DependencyProperty.Register(nameof(VerticalScrollOffset), typeof(double), typeof(ScrollingBehavior),
                                        new PropertyMetadata(typeof(double), VerticalScrollOffsetChanged));

        public double HorizontalScrollOffset
        {
            get => (double) GetValue(HorizontalScrollOffsetProperty);
            set => SetValue(HorizontalScrollOffsetProperty, value);
        }

        public double VerticalScrollOffset
        {
            get => (double) GetValue(VerticalScrollOffsetProperty);
            set => SetValue(VerticalScrollOffsetProperty, value);
        }

        private static void HorizontalScrollOffsetChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var behavior = dependencyObject as ScrollingBehavior;
            behavior?.AssociatedObject?.ChangeView((double?) args.NewValue, null, null);
        }

        private static void VerticalScrollOffsetChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var behavior = dependencyObject as ScrollingBehavior;
            behavior?.AssociatedObject?.ChangeView(null, (double?) args.NewValue, null);
        }
    }
}