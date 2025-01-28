using System;
using System.Windows;
using System.Windows.Controls;

namespace EmptyKeys.UserInterface.Designer
{
    /// <summary>
    /// Implements Radial Panel for Visual Studio designer
    /// </summary>
    public class RadialPanel : Panel
    {
        private double radius;

        public RadialPanel() : base()
        {
        }

        /// <inheritdoc />
        protected override Size MeasureOverride(Size availableSize)
        {
            if (Children.Count == 0)
            {
                return new Size();
            }

            Size maxDesiredSize = new Size();
            for (int i = 0; i < Children.Count; ++i)
            {
                UIElement child = Children[i];
                if (child == null)
                {
                    continue;
                }

                child.Measure(availableSize);
                maxDesiredSize.Width = Math.Max(maxDesiredSize.Width, child.DesiredSize.Width);
                maxDesiredSize.Height = Math.Max(maxDesiredSize.Height, child.DesiredSize.Height);
            }

            double halfMax = 0;
            double maxOther = 0;
            if (availableSize.Height > availableSize.Width)
            {
                halfMax = maxDesiredSize.Width / 2;
                maxOther = maxDesiredSize.Height;
            }
            else
            {
                halfMax = maxDesiredSize.Height / 2;
                maxOther = maxDesiredSize.Width;
            }

            double increment = 360f / Children.Count;
            double inner = (halfMax / Math.Tan(increment * (Math.PI / 360f)));
            double outer = inner + maxOther;
            radius = Math.Sqrt((halfMax * halfMax) + (outer * outer));

            double width = Math.Min(radius * 2, availableSize.Width - maxDesiredSize.Width);
            double height = Math.Min(radius * 2, availableSize.Height - maxDesiredSize.Height);
            radius = Math.Min(width, height) / 2;

            return new Size(radius * 2, radius * 2);
        }

        /// <inheritdoc />
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count == 0)
            {
                return finalSize;
            }

            double increment = (float)((2 * Math.PI) / Children.Count);
            double angle = 0;

            Rect childRect = new Rect(0, 0, finalSize.Width, finalSize.Height);
            for (int i = 0; i < Children.Count; ++i)
            {
                UIElement child = Children[i];
                if (child == null)
                {
                    continue;
                }

                childRect.X = Math.Cos(angle) * radius;
                childRect.Y = -Math.Sin(angle) * radius;
                childRect.Width = Math.Max(finalSize.Width, child.DesiredSize.Width);
                childRect.Height = Math.Max(finalSize.Height, child.DesiredSize.Height);

                child.Arrange(childRect);

                angle += increment;
            }

            return finalSize;
        }
    }
}
