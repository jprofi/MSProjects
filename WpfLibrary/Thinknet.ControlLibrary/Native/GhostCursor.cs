﻿namespace Thinknet.ControlLibrary.Native
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Ghost cursor for e.g. drag and drop.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "*", Justification = "Native calls")]
    public class GhostCursor
    {
        // ReSharper disable InconsistentNaming
        public const uint BI_BITFIELDS = 3;
        public const uint CF_DIBV5 = 17;
        internal const int DIB_RGB_COLORS = 0;
        public const uint GHND = GMEM_MOVEABLE | GMEM_ZEROINIT;
        public const uint GMEM_DDESHARE = 0x00002000;
        public const uint GMEM_MOVEABLE = 0x00000002;
        public const uint GMEM_ZEROINIT = 0x00000040;
        public const uint LCS_GM_IMAGES = 4;
        public const uint LCS_WINDOWS_COLOR_SPACE = 2;

        private readonly double _scaleX;
        private readonly double _scaleY;
        private readonly double _opacity;
        
        private Cursor _ghostCursor;
        private IconHandle _iconHandle;

        public GhostCursor(Visual visual) : this(visual, 1.0, 1.0, 1.0)
        {
        }

        public GhostCursor(Visual visual, double scaleX, double scaleY, double opacity)
        {
            _scaleX = scaleX;
            _scaleY = scaleY;
            _opacity = opacity;

            BitmapSource renderBitmap = CaptureScreen(visual);

            int width = renderBitmap.PixelWidth;
            int height = renderBitmap.PixelHeight;
            int stride = width * 4;

            // unfortunately, this byte array will get placed on the large object heap more than likely ... 
            byte[] pixels = GetBitmapPixels(renderBitmap, width, height);

            // -height specifies top-down bitmap
            BITMAPV5HEADER bInfo = new BITMAPV5HEADER(width, -height, 32);
            IntPtr ppvBits = IntPtr.Zero;
            BitmapHandle dibSectionHandle = null;

            try
            {
                dibSectionHandle = new BitmapHandle(CreateDIBSection(IntPtr.Zero, ref bInfo, DIB_RGB_COLORS, out ppvBits, IntPtr.Zero, 0));

                // copy the pixels into the DIB section now ...
                Marshal.Copy(pixels, 0, ppvBits, pixels.Length);

                if (!dibSectionHandle.IsInvalid && ppvBits != IntPtr.Zero)
                {
                    CreateCursor(width, height, dibSectionHandle);
                }
            }
            finally
            {
                if (dibSectionHandle != null)
                {
                    dibSectionHandle.Dispose();
                }
            }
        }

        public Cursor Cursor
        {
            get { return _ghostCursor; }
        }

        [DllImport("gdi32.dll", EntryPoint = "CreateBitmap", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        internal static extern IntPtr CreateBitmap(int width, int height, int planes, int bitsPerPixel, IntPtr bits);

        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BITMAPV5HEADER pbmi, 
            uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        private static extern IconHandle CreateIconIndirect([In] ref ICONINFO iconInfo);

        private static byte[] GetBitmapPixels(BitmapSource renderBitmap, int width, int height)
        {
            // The stride is the width of a single row of pixels (a scan line), rounded up to a four-byte boundary. 
            // The stride is always greater than or equal to the actual pixel width. If the stride is positive, 
            // the bitmap is top-down. If the stride is negative, the bitmap is bottom-up.
            int stride = width * 4;
            FormatConvertedBitmap bitmap = new FormatConvertedBitmap();
            bitmap.BeginInit();
            bitmap.DestinationFormat = PixelFormats.Bgra32;
            bitmap.Source = renderBitmap;
            bitmap.EndInit();

            byte[] pixels = new byte[stride * height];
            bitmap.CopyPixels(Int32Rect.Empty, pixels, stride, 0);
            return pixels;
        }

        private static RenderTargetBitmap GetRenderTargetBitmap(Visual visual)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap(200, 200, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            return bitmap;
        }

        private BitmapSource CaptureScreen(Visual target)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);

            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(800, 600, 96, 96, PixelFormats.Pbgra32);

            BitmapImage bim = new BitmapImage(new Uri(@"pack://application:,,,/Thinknet.ControlLibrary;component/Images/cursor-16.png", UriKind.Absolute)); 

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(target);
                
                ////LinearGradientBrush opacityMask = new LinearGradientBrush(Color.FromArgb(255, 1, 1, 1), Color.FromArgb(0, 1, 1, 1), 30);
                ////ctx.PushOpacityMask(opacityMask);

                vb.Opacity = _opacity;
                //vb.Transform = new ScaleTransform(scaleX, scaleY);

                Rect scaledBounds = new Rect(bounds.Location, new Size(bounds.Width * _scaleX, bounds.Height * _scaleY));

                ctx.DrawImage(bim, new Rect(0, 0, bim.Width, bim.Height));
                ctx.DrawRectangle(vb, null, new Rect(new Point(bim.Width, bim.Height), scaledBounds.Size));
                
                ////ctx.Pop();
            }



            renderBitmap.Render(dv);

            return renderBitmap;
        }

        private void CreateCursor(int width, int height, BitmapHandle dibSectionHandle)
        {
            BitmapHandle monoBitmapHandle = null;
            try
            {
                monoBitmapHandle = new BitmapHandle(CreateBitmap(width, height, 1, 1, IntPtr.Zero));

                ICONINFO icon = new ICONINFO();
                icon.IsIcon = false;
                icon.xHotspot = 0;
                icon.yHotspot = 0;
                icon.ColorBitmap = dibSectionHandle;
                icon.MaskBitmap = monoBitmapHandle;

                _iconHandle = CreateIconIndirect(ref icon);
                if (!_iconHandle.IsInvalid)
                {
                    _ghostCursor = CursorInteropHelper.Create(_iconHandle);
                }
            }
            finally
            {
                // destroy the temporary mono bitmap now ...
                if (monoBitmapHandle != null)
                {
                    monoBitmapHandle.Dispose();
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITFIELDS
        {
            public uint BlueMask;
            public uint GreenMask;
            public uint RedMask;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct BITMAPV5HEADER
        {
            [FieldOffset(0)]
            public uint Size;
            [FieldOffset(4)]
            public int Width;
            [FieldOffset(8)]
            public int Height;
            [FieldOffset(12)]
            public ushort Planes;
            [FieldOffset(14)]
            public ushort BitCount;
            [FieldOffset(16)]
            public uint Compression;
            [FieldOffset(20)]
            public uint SizeImage;
            [FieldOffset(24)]
            public int XPelsPerMeter;
            [FieldOffset(28)]
            public int YPelsPerMeter;
            [FieldOffset(32)]
            public uint ClrUsed;
            [FieldOffset(36)]
            public uint ClrImportant;
            [FieldOffset(40)]
            public uint RedMask;
            [FieldOffset(44)]
            public uint GreenMask;
            [FieldOffset(48)]
            public uint BlueMask;
            [FieldOffset(52)]
            public uint AlphaMask;
            [FieldOffset(56)]
            public uint CSType;
            [FieldOffset(60)]
            public CIEXYZTRIPLE Endpoints;
            [FieldOffset(96)]
            public uint GammaRed;
            [FieldOffset(100)]
            public uint GammaGreen;
            [FieldOffset(104)]
            public uint GammaBlue;
            [FieldOffset(108)]
            public uint Intent;
            [FieldOffset(112)]
            public uint ProfileData;
            [FieldOffset(116)]
            public uint ProfileSize;
            [FieldOffset(120)]
            public uint Reserved;

            public BITMAPV5HEADER(int width, int height, ushort bpp)
            {
                Size = (uint)Marshal.SizeOf(typeof(BITMAPV5HEADER));
                Width = width;
                Height = height;
                Planes = 1;
                BitCount = bpp;
                Compression = BI_BITFIELDS;
                RedMask = 0x00FF0000;
                GreenMask = 0x0000FF00;
                BlueMask = 0x000000FF;
                AlphaMask = 0xFF000000;

                // zeroed by default per struct needs ...
                SizeImage = 0;
                XPelsPerMeter = 0;
                YPelsPerMeter = 0;
                ClrUsed = 0;
                ClrImportant = 0;
                CSType = LCS_WINDOWS_COLOR_SPACE;
                Endpoints.ciexyzBlue.ciexyzX = 0;
                Endpoints.ciexyzBlue.ciexyzY = 0;
                Endpoints.ciexyzBlue.ciexyzZ = 0;
                Endpoints.ciexyzGreen.ciexyzX = 0;
                Endpoints.ciexyzGreen.ciexyzY = 0;
                Endpoints.ciexyzGreen.ciexyzZ = 0;
                Endpoints.ciexyzRed.ciexyzX = 0;
                Endpoints.ciexyzRed.ciexyzY = 0;
                Endpoints.ciexyzRed.ciexyzZ = 0;
                GammaRed = 0;
                GammaGreen = 0;
                GammaBlue = 0;
                Intent = LCS_GM_IMAGES;
                ProfileData = 0;
                ProfileSize = 0;
                Reserved = 0;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CIEXYZ
        {
            public uint ciexyzX; //FXPT2DOT30 
            public uint ciexyzY; //FXPT2DOT30 
            public uint ciexyzZ; //FXPT2DOT30 
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CIEXYZTRIPLE
        {
            public CIEXYZ ciexyzRed;
            public CIEXYZ ciexyzGreen;
            public CIEXYZ ciexyzBlue;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        internal struct BITMAPINFO
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;

            // do the easy stuff ...
            public BITMAPINFO(int width, int height, short bpp)
            {
                biSize = Marshal.SizeOf(typeof(BITMAPINFO));
                biWidth = width;
                biHeight = height;
                biPlanes = 1;
                biBitCount = bpp;
                biCompression = 0;
                biSizeImage = 0;
                biXPelsPerMeter = 0;
                biYPelsPerMeter = 0;
                biClrUsed = 0;
                biClrImportant = 0;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ICONINFO
        {
            public bool IsIcon;
            public int xHotspot;
            public int yHotspot;
            public BitmapHandle MaskBitmap;
            public BitmapHandle ColorBitmap;
        }
    }
}
