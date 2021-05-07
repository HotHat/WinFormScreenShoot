using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormScreenShoot
{
    public partial class Form3 : Form
    {

        private TextureBrush m_backgroundBrush;
        private Bitmap m_canvas;
        private Pen m_borderDotStaticPen;

        public Rectangle ClientArea { get; private set; }
        public Bitmap Canvas { get; private set; }
        public Rectangle CanvasRectangle { get; internal set; }

        public Form3()
        {
            InitializeComponent();

            AutoScaleMode = AutoScaleMode.None;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.None;
            Bounds = CaptureHelpers.GetScreenBounds();
            ShowInTaskbar = false;




            m_borderDotStaticPen = new Pen(Color.White) { DashPattern = new float[] { 5, 5 } };

            ClientArea = CaptureHelpers.GetScreenBounds0Based();
            CanvasRectangle = ClientArea;

            InitBackground(FullScreenCapture());
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Update();

            Graphics g = e.Graphics;


            g.CompositingMode = CompositingMode.SourceCopy;
            g.FillRectangle(m_backgroundBrush, CanvasRectangle);
            g.CompositingMode = CompositingMode.SourceOver;

            // Draw(g);

        
        }


        internal void InitBackground(Bitmap canvas)
        {
            m_canvas = canvas;

            m_backgroundBrush = new TextureBrush(m_canvas) { WrapMode = WrapMode.Clamp };
     
        }


        private Bitmap FullScreenCapture()
        {
            var rect = CaptureHelpers.GetScreenBounds();

            IntPtr handle = NativeMethods.GetDesktopWindow();

            var bmp = CaptureRectangleNative(handle, rect);

            return bmp;
            // bmp.Save("D://55.png", System.Drawing.Imaging.ImageFormat.Png);
        }


        


        private Bitmap CaptureRectangleNative(IntPtr handle, Rectangle rect)
        {
            if (rect.Width == 0 || rect.Height == 0)
            {
                return null;
            }

            IntPtr hdcSrc = NativeMethods.GetWindowDC(handle);
            IntPtr hdcDest = NativeMethods.CreateCompatibleDC(hdcSrc);
            IntPtr hBitmap = NativeMethods.CreateCompatibleBitmap(hdcSrc, rect.Width, rect.Height);
            IntPtr hOld = NativeMethods.SelectObject(hdcDest, hBitmap);
            NativeMethods.BitBlt(hdcDest, 0, 0, rect.Width, rect.Height, hdcSrc, rect.X, rect.Y, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);


            NativeMethods.SelectObject(hdcDest, hOld);
            NativeMethods.DeleteDC(hdcDest);
            NativeMethods.ReleaseDC(handle, hdcSrc);
            Bitmap bmp = Image.FromHbitmap(hBitmap);
            NativeMethods.DeleteObject(hBitmap);

            return bmp;
        }
    }
}
