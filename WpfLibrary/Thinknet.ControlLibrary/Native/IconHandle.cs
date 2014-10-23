namespace Thinknet.ControlLibrary.Native
{
    using System;
    using System.Runtime.InteropServices;

    internal class IconHandle : SafeHandle
    {
        internal IconHandle() : this(IntPtr.Zero)
        {
        }

        internal IconHandle(IntPtr ptr)
            : base(IntPtr.Zero, true)
        {
            handle = ptr;
        }

        /// <inheritdoc />
        public override bool IsInvalid
        {
            get
            {
                return handle == IntPtr.Zero;
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyIcon(IntPtr hIcon);
        protected override bool ReleaseHandle()
        {
            return DestroyIcon(handle);
        }
    }
}
