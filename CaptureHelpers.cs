using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormScreenShoot
{
    class CaptureHelpers
    {
        public static Rectangle GetScreenBounds()
        {
            return SystemInformation.VirtualScreen;
        }

        public static Point ScreenToClient(Point p)
        {
            int screenX = NativeMethods.GetSystemMetrics(76);
            int screenY = NativeMethods.GetSystemMetrics(77);
            return new Point(p.X - screenX, p.Y - screenY);
        }

        public static Rectangle ScreenToClient(Rectangle r)
        {
            return new Rectangle(ScreenToClient(r.Location), r.Size);
        }

        public static Rectangle GetScreenBounds0Based()
        {
            return ScreenToClient(GetScreenBounds());
        }
    }
}
