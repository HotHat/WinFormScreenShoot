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
using System.Diagnostics;
using System.Drawing.Imaging;

namespace WinFormScreenShoot
{
    public partial class RegionCaptureForm : Form
    {

        private TextureBrush m_backgroundBrush;
        private Bitmap m_canvas;
        private Pen m_borderDotStaticPen;

        private Point m_startPoint, m_endPoint;
        private bool m_isMouseDown = false;
        private Rectangle m_rect;

        public Rectangle ClientArea { get; private set; }
        public Bitmap Canvas { get; private set; }
        public Rectangle CanvasRectangle { get; internal set; }

        public RegionCaptureForm()
        {
            InitializeComponent();

            AutoScaleMode = AutoScaleMode.None;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.None;
            Bounds = CaptureHelpers.GetScreenBounds();
            ShowInTaskbar = false;




            m_borderDotStaticPen = new Pen(Color.Red, 3) { DashPattern = new float[] { 2, 2 } };

            ClientArea = CaptureHelpers.GetScreenBounds0Based();
            CanvasRectangle = ClientArea;

            InitBackground(FullScreenCapture());

            MouseDown += this.OnMouseDown;
            MouseMove += this.OnMouseMove;
            MouseUp += this.OnMouseUp;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Update();

            Graphics g = e.Graphics;


            g.CompositingMode = CompositingMode.SourceCopy;
            g.FillRectangle(m_backgroundBrush, CanvasRectangle);
            g.CompositingMode = CompositingMode.SourceOver;

            // Draw(g);
            var Buttons = Control.MouseButtons;
            var Position = Control.MousePosition;

            Debug.WriteLine("Buttons: {0}", Buttons);
            Debug.WriteLine("Position: {0}", Position);

            // Invalidate();
            if (m_isMouseDown)
            {
                g.DrawRectangle(m_borderDotStaticPen, m_rect);
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            m_isMouseDown = true;
            m_startPoint.X = e.X;
            m_startPoint.Y = e.Y;

            Debug.WriteLine("Mouse Click!");
            Debug.WriteLine("Start At {0}", m_startPoint);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {

            var with = Math.Abs(e.X - m_startPoint.X);
            var heigh = Math.Abs(e.Y - m_startPoint.Y);
            int x, y;

            x = m_startPoint.X < e.X ? m_startPoint.X : e.X;
            y = m_startPoint.Y < e.Y ? m_startPoint.Y : e.Y;

            m_rect.X = x;
            m_rect.Y = y;
            m_rect.Size = new Size(with, heigh);

            this.Invalidate();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            m_isMouseDown = false;


            // this.Close();
            this.Invalidate();
            if (m_rect.Width <= 0 || m_rect.Height <= 0) { return; }

            var bmp = m_canvas.Clone(m_rect, m_canvas.PixelFormat);
            //bmp.Save("D:\\555.png", ImageFormat.Png);
            Debug.WriteLine("Send image");
            String ocrStr = "";
            try
            {
                ocrStr =  BaiduOcr.GetInstance().send(CaptureHelpers.Bitmap2Byte(bmp));

                if ("" == ocrStr)
                {
                    throw new Exception("返回结果为空");
                }
            } catch (Exception exp)
            {
                MessageBox.Show("OCR解析出错误: " + exp.Message);
            }

            var result = new BaiduTranslate().translate(ocrStr);
            Console.Write("解析结果: ");
            Console.WriteLine(result);
            
            var form = new TranslateResultForm(result.src, result.dst);
            form.ShowDialog();

            this.Close();
            //this.Invalidate(new Region(new Rectangle((int)x, (int)y, (int)rect.Width, (int)rect.Height)));
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
