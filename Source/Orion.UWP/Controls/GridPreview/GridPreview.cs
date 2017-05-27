using System.Collections;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// ReSharper disable once CheckNamespace

namespace Orion.UWP.Controls
{
    [TemplatePart(Name = "RootGrid", Type = typeof(VariableSizedWrapGrid))]
    public sealed class GridPreview : Control
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(GridPreview), new PropertyMetadata(null, OnItemsSourceChanged));

        public static readonly DependencyProperty ItemsTemplatePropert =
            DependencyProperty.Register(nameof(ItemsTemplate), typeof(DataTemplate), typeof(GridPreview), new PropertyMetadata(null));

        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(GridPreview), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register(nameof(ItemWidth), typeof(double), typeof(GridPreview), new PropertyMetadata(default(double)));

        private bool _isInitialized;

        private VariableSizedWrapGrid _rootGrid;

        public object ItemsSource
        {
            get => GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemsTemplate
        {
            get => (DataTemplate) GetValue(ItemsTemplatePropert);
            set => SetValue(ItemsTemplatePropert, value);
        }

        public double ItemHeight
        {
            get => (double) GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        public double ItemWidth
        {
            get => (double) GetValue(ItemWidthProperty);
            set => SetValue(ItemWidthProperty, value);
        }

        private static void OnItemsSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var sender = dependencyObject as GridPreview;
            sender?.UpdateItems();
        }

        protected override void OnApplyTemplate()
        {
            _rootGrid = (VariableSizedWrapGrid) GetTemplateChild("RootGrid");
            _isInitialized = true;

            UpdateItems();
        }

        private void UpdateItems()
        {
            if (!_isInitialized)
                return;

            var itemsSource = ItemsSource as IList;
            if (itemsSource == null || itemsSource.Count == 0)
                return;

            _rootGrid.Children.Clear();

            if (itemsSource.Count == 1)
            {
                var content = (FrameworkElement) ItemsTemplate.LoadContent();
                content.DataContext = itemsSource[0];
                VariableSizedWrapGrid.SetColumnSpan(content, 2);
                VariableSizedWrapGrid.SetRowSpan(content, 2);
                _rootGrid.Children.Add(content);
            }
            else if (itemsSource.Count == 2)
            {
                foreach (var item in itemsSource)
                {
                    var content = (FrameworkElement) ItemsTemplate.LoadContent();
                    content.DataContext = item;
                    VariableSizedWrapGrid.SetRowSpan(content, 2);
                    _rootGrid.Children.Add(content);
                }
            }
            else if (itemsSource.Count == 3)
            {
                for (var i = 0; i < itemsSource.Count; i++)
                {
                    var content = (FrameworkElement) ItemsTemplate.LoadContent();
                    content.DataContext = itemsSource[i];
                    if (i == 0)
                        VariableSizedWrapGrid.SetRowSpan(content, 2);
                    _rootGrid.Children.Add(content);
                }
            }
            else if (itemsSource.Count == 4)
            {
                foreach (var item in itemsSource)
                {
                    var content = (FrameworkElement) ItemsTemplate.LoadContent();
                    content.DataContext = item;
                    _rootGrid.Children.Add(content);
                }
            }
        }
    }
}