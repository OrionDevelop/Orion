using System.Diagnostics;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Microsoft.Xaml.Interactivity;

using WinRTXamlToolkit.Controls.Extensions;

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

        private static async void HorizontalScrollOffsetChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var behavior = dependencyObject as ScrollingBehavior;
            if (behavior?.AssociatedObject == null)
                return;

            await Task.Delay(1);
            await behavior.AssociatedObject.ScrollToHorizontalOffsetWithAnimationAsync((double) args.NewValue);
        }

        private static async void VerticalScrollOffsetChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var behavior = dependencyObject as ScrollingBehavior;
            if (behavior?.AssociatedObject == null)
                return;

            await Task.Delay(1);
            await behavior.AssociatedObject.ScrollToVerticalOffsetWithAnimationAsync((double) args.NewValue);
        }
    }
}