using System.Windows.Input;

using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;

using Microsoft.Xaml.Interactivity;

namespace Orion.UWP.Behaviors
{
    public class KeyEventTriggerBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(KeyEventTriggerBehavior), new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty CtrlKeyProperty =
            DependencyProperty.Register(nameof(CtrlKey), typeof(bool), typeof(KeyEventTriggerBehavior), new PropertyMetadata(false));

        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.Register(nameof(Key), typeof(VirtualKey), typeof(KeyEventTriggerBehavior), new PropertyMetadata(default(VirtualKey)));

        private bool _isFocused;

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public bool CtrlKey
        {
            get => (bool) GetValue(CtrlKeyProperty);
            set => SetValue(CtrlKeyProperty, value);
        }

        public VirtualKey Key
        {
            get => (VirtualKey) GetValue(KeyProperty);
            set => SetValue(KeyProperty, value);
        }

        protected override void OnAttached()
        {
            AssociatedObject.GotFocus += AssociatedObjectOnGotFocus;
            AssociatedObject.LostFocus += AssociatedObjectOnLostFocus;
            Dispatcher.AcceleratorKeyActivated += DispatcherOnAcceleratorKeyActivated;
            base.OnAttached();
        }

        private void AssociatedObjectOnGotFocus(object sender, RoutedEventArgs args)
        {
            _isFocused = args.OriginalSource == AssociatedObject;
        }

        private void AssociatedObjectOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            _isFocused = false;
        }

        private void DispatcherOnAcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {
            if (!_isFocused)
                return;

            if (args.KeyStatus.RepeatCount != 1)
                return;

            if (args.EventType != CoreAcceleratorKeyEventType.KeyUp)
                return;

            if (CtrlKey && (Window.Current.CoreWindow.GetKeyState(VirtualKey.Control) & CoreVirtualKeyStates.Down) != CoreVirtualKeyStates.Down)
                return;

            if (Key == args.VirtualKey && Command.CanExecute(null))
                Command.Execute(null);
        }

        protected override void OnDetaching()
        {
            Dispatcher.AcceleratorKeyActivated -= DispatcherOnAcceleratorKeyActivated;
            AssociatedObject.LostFocus -= AssociatedObjectOnLostFocus;
            AssociatedObject.GotFocus -= AssociatedObjectOnGotFocus;
            base.OnDetaching();
        }
    }
}