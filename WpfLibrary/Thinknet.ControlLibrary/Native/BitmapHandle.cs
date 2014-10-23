namespace Thinknet.ControlLibrary.Native
{
    using System;
    using System.Runtime.InteropServices;

    internal class BitmapHandle : SafeHandle
    {
        internal BitmapHandle() : this(IntPtr.Zero)
        {
        }

        internal BitmapHandle(IntPtr ptr)
            : base(IntPtr.Zero, true)
        {
            handle = ptr;
        }

        public override bool IsInvalid
        {
            get { return handle == IntPtr.Zero; }
        }

        [DllImport("gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject);

        protected override bool ReleaseHandle()
        {
            return DeleteObject(handle);
        }
    }
}
