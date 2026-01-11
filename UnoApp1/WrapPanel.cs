using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Windows.Foundation;

namespace UnoApp1;

public enum WrapDirection
{
    LeftToRight,
    RightToLeft,
    TopToBottom,
    BottomToTop
}

public class CustomWrapPanel : Panel
{
    public WrapDirection Direction { get => (WrapDirection)GetValue(DirectionProperty); set => SetValue(DirectionProperty, value); }
    public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(nameof(Direction), typeof(WrapDirection), typeof(CustomWrapPanel), new PropertyMetadata(WrapDirection.LeftToRight, OnLayoutPropertyChanged));

    public double ItemSpacingX { get => (double)GetValue(ItemSpacingXProperty); set => SetValue(ItemSpacingXProperty, value); }
    public static readonly DependencyProperty ItemSpacingXProperty = DependencyProperty.Register(nameof(ItemSpacingX), typeof(double), typeof(CustomWrapPanel), new PropertyMetadata(0.0, OnLayoutPropertyChanged));

    public double ItemSpacingY { get => (double)GetValue(ItemSpacingYProperty); set => SetValue(ItemSpacingYProperty, value); }
    public static readonly DependencyProperty ItemSpacingYProperty = DependencyProperty.Register(nameof(ItemSpacingY), typeof(double), typeof(CustomWrapPanel), new PropertyMetadata(0.0, OnLayoutPropertyChanged));

    private static void OnLayoutPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CustomWrapPanel p) p.InvalidateMeasure();
    }

    private bool IsHorizontal => Direction == WrapDirection.LeftToRight || Direction == WrapDirection.RightToLeft;

    protected override Size MeasureOverride(Size availableSize)
    {
        double contentWidth = 0;
        double contentHeight = 0;

        double linePrimary = 0;   // Running length on primary axis
        double lineThickness = 0; // Max thickness on secondary axis

        foreach (UIElement child in Children)
        {
            child.Measure(availableSize);
            var s = child.DesiredSize;

            if (IsHorizontal)
            {
                bool canWrap = !double.IsInfinity(availableSize.Width);
                double needed = linePrimary + (linePrimary > 0 ? ItemSpacingX : 0) + s.Width;

                if (canWrap && needed > availableSize.Width && linePrimary > 0)
                {
                    // Commit line
                    contentWidth = Math.Max(contentWidth, linePrimary);
                    contentHeight += (contentHeight > 0 ? ItemSpacingY : 0) + lineThickness;

                    // Start new line
                    linePrimary = 0;
                    lineThickness = 0;
                    needed = s.Width; // recompute for first item in new line
                }

                linePrimary = needed;
                lineThickness = Math.Max(lineThickness, s.Height);
            }
            else
            {
                bool canWrap = !double.IsInfinity(availableSize.Height);
                double needed = linePrimary + (linePrimary > 0 ? ItemSpacingY : 0) + s.Height;

                if (canWrap && needed > availableSize.Height && linePrimary > 0)
                {
                    // Commit column
                    contentHeight = Math.Max(contentHeight, linePrimary);
                    contentWidth += (contentWidth > 0 ? ItemSpacingX : 0) + lineThickness;

                    // Start new column
                    linePrimary = 0;
                    lineThickness = 0;
                    needed = s.Height; // first item in new column
                }

                linePrimary = needed;
                lineThickness = Math.Max(lineThickness, s.Width);
            }
        }

        // Commit the last line/column
        if (IsHorizontal)
        {
            contentWidth = Math.Max(contentWidth, linePrimary);
            contentHeight += (contentHeight > 0 ? ItemSpacingY : 0) + lineThickness;

            double desiredWidth = double.IsInfinity(availableSize.Width) ? contentWidth : Math.Min(contentWidth, availableSize.Width);
            double desiredHeight = double.IsInfinity(availableSize.Height) ? contentHeight : Math.Min(contentHeight, availableSize.Height);
            return new Size(desiredWidth, desiredHeight);
        }
        else
        {
            contentHeight = Math.Max(contentHeight, linePrimary);
            contentWidth += (contentWidth > 0 ? ItemSpacingX : 0) + lineThickness;

            double desiredWidth = double.IsInfinity(availableSize.Width) ? contentWidth : Math.Min(contentWidth, availableSize.Width);
            double desiredHeight = double.IsInfinity(availableSize.Height) ? contentHeight : Math.Min(contentHeight, availableSize.Height);
            return new Size(desiredWidth, desiredHeight);
        }
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        double x = 0, y = 0;
        double lineThickness = 0;
        bool firstInLine = true;

        foreach (UIElement child in Children)
        {
            var s = child.DesiredSize;

            if (IsHorizontal)
            {
                if (!firstInLine && x + s.Width > finalSize.Width)
                {
                    // Wrap
                    x = 0;
                    y += lineThickness + ItemSpacingY;
                    lineThickness = 0;
                    firstInLine = true;
                }

                if (!firstInLine)
                    x += ItemSpacingX; // spacing before every item except the first

                double posX = Direction == WrapDirection.RightToLeft
                    ? finalSize.Width - x - s.Width
                    : x;

                child.Arrange(new Rect(posX, y, s.Width, s.Height));

                x += s.Width;
                lineThickness = Math.Max(lineThickness, s.Height);
                firstInLine = false;
            }
            else
            {
                if (!firstInLine && y + s.Height > finalSize.Height)
                {
                    // Wrap
                    y = 0;
                    x += lineThickness + ItemSpacingX;
                    lineThickness = 0;
                    firstInLine = true;
                }

                if (!firstInLine)
                    y += ItemSpacingY; // spacing before every item except the first

                double posY = Direction == WrapDirection.BottomToTop
                    ? finalSize.Height - y - s.Height
                    : y;

                child.Arrange(new Rect(x, posY, s.Width, s.Height));

                y += s.Height;
                lineThickness = Math.Max(lineThickness, s.Width);
                firstInLine = false;
            }
        }

        return finalSize;
    }
}


