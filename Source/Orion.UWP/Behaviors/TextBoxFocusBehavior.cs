using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Microsoft.Xaml.Interactivity;

namespace Orion.UWP.Behaviors
{
    internal class TextBoxFocusBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.Register(nameof(IsFocused), typeof(bool), typeof(TextBoxFocusBehavior),
                                                                                                  new PropertyMetadata(default(bool), OnIsFocusedChanged));

        public bool IsFocused
        {
            get => (bool) GetValue(IsFocusedProperty);
            set => SetValue(IsFocusedProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotFocus += TextBoxOnGotFocus;
            AssociatedObject.LostFocus += TextBoxOnLostFocus;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.LostFocus -= TextBoxOnLostFocus;
            AssociatedObject.GotFocus -= TextBoxOnGotFocus;
        }

        private void TextBoxOnGotFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            IsFocused = true;
        }

        private void TextBoxOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            IsFocused = false;
        }

        private static void OnIsFocusedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is TextBoxFocusBehavior behavior))
                return;
            if ((bool) e.NewValue && behavior.AssociatedObject.FocusState == FocusState.Unfocused)
            {
                behavior.AssociatedObject.Focus(FocusState.Programmatic);
                behavior.AssociatedObject.SelectionStart = behavior.AssociatedObject.Text.Length;
            }
        }
    }
}