using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DocAtHome
{
    internal static class WindowHelper
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        public static void StartDrag(IntPtr handle)
        {
            ReleaseCapture();
            SendMessage(handle, 0x112, 0xf012, 0);
        }

        // Toggle between normal size and working-area fullscreen.
        // Saves the current bounds in f.Tag so restore is exact.
        public static void ToggleMaximize(Form f, Button btn = null)
        {
            if (f.Tag is Rectangle saved)
            {
                f.Bounds = saved;
                f.Tag    = null;
                if (btn != null) btn.Text = "□";
            }
            else
            {
                f.Tag = f.Bounds;
                Rectangle wa = Screen.FromControl(f).WorkingArea;
                f.SetBounds(wa.X, wa.Y, wa.Width, wa.Height);
                if (btn != null) btn.Text = "❐";
            }
        }
    }
}
