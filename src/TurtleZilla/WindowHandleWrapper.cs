using System;
using System.Windows.Forms;

namespace TurtleZilla
{
    internal sealed class WindowHandleWrapper : IWin32Window
    {
        public IntPtr Handle { get; private set; }

        public static WindowHandleWrapper TryCreate(IntPtr handle)
        {
            return handle != IntPtr.Zero ? new WindowHandleWrapper(handle) : null;
        }

        private WindowHandleWrapper(IntPtr handle)
        {
            Handle = handle;
        }
    }
}
