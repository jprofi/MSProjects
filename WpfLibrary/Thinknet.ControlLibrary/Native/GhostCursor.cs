namespace Thinknet.ControlLibrary.Native
{
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Ghost cursor for e.g. drag and drop.
    /// </summary>
    public class GhostCursor
    {
        private readonly double _scaleX;
        private readonly double _scaleY;
        private readonly double _opacity;
        private readonly Cursor _cursor;

        /// <summary>
        /// Initializes a new instance of the <see cref="GhostCursor"/> class.
        /// </summary>
        /// <param name="visual">The visual to attach to the cursor.</param>
        /// <param name="cursor">The cursor symbol.</param>
        public GhostCursor(Visual visual, FrameworkElement cursor)
            : this(visual, 1.0, 1.0, 1.0, cursor)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GhostCursor"/> class.
        /// </summary>
        /// <param name="visual">The visual to attach to the cursor.</param>
        /// <param name="scaleX">The horizontal scaling factor.</param>
        /// <param name="scaleY">The vertical scaling factor.</param>
        /// <param name="opacity">The opacity.</param>
        /// <param name="cursor">The cursor symbol.</param>
        public GhostCursor(Visual visual, double scaleX, double scaleY, double opacity, FrameworkElement cursor)
        {
            _scaleX = scaleX;
            _scaleY = scaleY;
            _opacity = opacity;

            BitmapSource renderBitmap = CreateBitmapFromVisuals(visual, cursor);

            int width = renderBitmap.PixelWidth;
            int height = renderBitmap.PixelHeight;
            
            CursorGenerator cursorGenerator = new CursorGenerator();
            _cursor = cursorGenerator.CreateCursor(renderBitmap, width, height);
        }

        /// <summary>
        /// Gets the newly created cursor.
        /// </summary>
        public Cursor Cursor
        {
            get { return _cursor; }
        }

        private BitmapSource CreateBitmapFromVisuals(Visual target, FrameworkElement cursor)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);

            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(800, 600, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(target);
                VisualBrush vbCursor = new VisualBrush(cursor);
                
                // For linear gradient over ghost image.
                ////LinearGradientBrush opacityMask = new LinearGradientBrush(Color.FromArgb(255, 1, 1, 1), Color.FromArgb(0, 1, 1, 1), 30);
                ////ctx.PushOpacityMask(opacityMask);

                vb.Opacity = _opacity;

                Rect scaledBounds = new Rect(bounds.Location, new Size(bounds.Width * _scaleX, bounds.Height * _scaleY));

                ctx.DrawRectangle(vbCursor, null, new Rect(0, 0, cursor.ActualWidth, cursor.ActualHeight));
                ctx.DrawRectangle(vb, null, new Rect(new Point(cursor.ActualWidth, cursor.ActualHeight), scaledBounds.Size));
                
                ////ctx.Pop();
            }

            renderBitmap.Render(dv);
            return renderBitmap;
        }
    }
}
