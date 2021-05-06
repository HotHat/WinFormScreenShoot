using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace WinFormScreenShoot
{
    public partial class Form2 : Form
    {
        private double x;
        private double y;
        private double width;
        private double height;

        private bool isMouseDown = false;

      

        public Form2()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Location = new System.Drawing.Point(0, 0);
            MouseDown += this.Form2_MouseClick;
            
        }


        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            x = e.X;
            y = e.Y;
        }

        private void CaptureWindow_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isMouseDown)
            {
                // 1. 通过一个矩形来表示目前截图区域
                System.Drawing.Imaging.Rectangle rect = new System.Drawing.Rectangle();
                double dx = e.GetPosition(null).X;
                double dy = e.GetPosition(null).Y;
                double rectWidth = Math.Abs(dx - x);
                double rectHeight = Math.Abs(dy - y);
                SolidColorBrush brush = new SolidColorBrush(Colors.White);
                rect.Width = (int)rectWidth;
                rect.Height = (int)rectHeight;
                rect.Fill = brush;
                rect.Stroke = brush;
                rect.StrokeThickness = 1;
                if (dx < x)
                {
                    Canvas.SetLeft(rect, dx);
                    Canvas.SetTop(rect, dy);
                }
                else
                {
                    Canvas.SetLeft(rect, x);
                    Canvas.SetTop(rect, y);
                }

                CaptureCanvas.Children.Clear();
                CaptureCanvas.Children.Add(rect);

                if (e.LeftButton == MouseButtonState.Released)
                {
                    CaptureCanvas.Children.Clear();
                    // 2. 获得当前截图区域
                    width = Math.Abs(e.GetPosition(null).X - x);
                    height = Math.Abs(e.GetPosition(null).Y - y);

                    if (e.GetPosition(null).X > x)
                    {
                        CaptureScreen(x, y, width, height);
                    }
                    else
                    {
                        CaptureScreen(e.GetPosition(null).X, e.GetPosition(null).Y, width, height);
                    }


                    isMouseDown = false;
                    x = 0.0;
                    y = 0.0;
                    this.Close();
                }
            }
        }


        private void CaptureScreen(double x, double y, double width, double height)
        {
            int ix = Convert.ToInt32(x);
            int iy = Convert.ToInt32(y);
            int iw = Convert.ToInt32(width);
            int ih = Convert.ToInt32(height);

            System.Drawing.Bitmap bitmap = new Bitmap(iw, ih);
            using (System.Drawing.Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(ix, iy, 0, 0, new System.Drawing.Size(iw, ih));

                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Png Files|*.png";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bitmap.Save(dialog.FileName, ImageFormat.Png);
                }
            }
        }
    }
}
