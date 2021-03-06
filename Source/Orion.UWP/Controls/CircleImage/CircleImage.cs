﻿using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// ReSharper disable once CheckNamespace

namespace Orion.UWP.Controls
{
    [TemplatePart(Name = "ImageBorder", Type = typeof(Border))]
    public class CircleImage : Control
    {
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(nameof(Source), typeof(object), typeof(CircleImage), new PropertyMetadata(default(object), OnSourceChanged));

        public static readonly DependencyProperty CircleBorderBrushProperty =
            DependencyProperty.Register(nameof(CircleBorderBrush), typeof(Brush), typeof(CircleImage), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty CircleBorderThicknessProperty =
            DependencyProperty.Register(nameof(CircleBorderThickness), typeof(Thickness), typeof(CircleImage), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CircleImage), new PropertyMetadata(default(CornerRadius)));

        public static readonly DependencyProperty IsCenteredProperty =
            DependencyProperty.Register(nameof(IsCentered), typeof(bool), typeof(CircleImage), new PropertyMetadata(default(bool)));

        private Border _imageBorder;
        private bool _isInitialized;

        public object Source
        {
            get => GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public Brush CircleBorderBrush
        {
            get => (Brush) GetValue(CircleBorderBrushProperty);
            set => SetValue(CircleBorderBrushProperty, value);
        }

        public Thickness CircleBorderThickness
        {
            get => (Thickness) GetValue(CircleBorderThicknessProperty);
            set => SetValue(CircleBorderThicknessProperty, value);
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public bool IsCentered
        {
            get => (bool) GetValue(IsCenteredProperty);
            set => SetValue(IsCenteredProperty, value);
        }

        private static void OnSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as CircleImage;
            if (e.OldValue == null || e.NewValue == null || !e.OldValue.Equals(e.NewValue))
                control?.SetSource(e.NewValue);
        }

        protected override void OnApplyTemplate()
        {
            _imageBorder = (Border) GetTemplateChild("ImageBorder");
            _isInitialized = true;
            SetSource(Source);
        }

        private void SetSource(object source)
        {
            if (!_isInitialized)
                return;
            if (source == null)
                return;

            ImageBrush brush;
            if (source is BitmapImage bitmap)
                brush = new ImageBrush {ImageSource = bitmap};
            else
                brush = new ImageBrush {ImageSource = new BitmapImage(new Uri(source.ToString()))};

            if (IsCentered)
            {
                brush.AlignmentX = AlignmentX.Center;
                brush.AlignmentY = AlignmentY.Center;
                brush.Stretch = Stretch.UniformToFill;
            }
            _imageBorder.Background = brush;
        }

        private static bool IsHttpUri(Uri uri)
        {
            return uri.IsAbsoluteUri && (uri.Scheme == "http" || uri.Scheme == "https");
        }
    }
}