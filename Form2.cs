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
using System.Windows.Input;
using System.Windows.Media;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using System.Diagnostics;

namespace WinFormScreenShoot
{
    public partial class Form2 : Form
    {
        private double x;
        private double y;
        private Rectangle rect;

        private bool isMouseDown = false;
        private SolidBrush brush;

        private Graphics paint;
        private Graphics finalPaint;
        private Bitmap bitmap;
        private System.Drawing.Pen pen;

        public Form2()
        {

            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Location = new System.Drawing.Point(0, 0);
            // this.panel_paint.Size = this.Size;
            // Paint += this.Form2_Paint;
            
            brush = new SolidBrush(System.Drawing.Color.FromArgb(10, 0, 0, 0));
            paint = panel_paint.CreateGraphics();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲

            bitmap = new Bitmap(this.Width, this.Height);
            paint = Graphics.FromImage(bitmap);
            finalPaint = panel_paint.CreateGraphics();

            pen = new System.Drawing.Pen(System.Drawing.Color.Blue);

        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    Graphics g = e.Graphics;
        //    if (isMouseDown)
        //    {
        //        g.Clear(Form2.ActiveForm.BackColor);
        //        g.FillRectangle(brush, this.rect);
        //        Debug.WriteLine("draw rectanger");
        //    }
        //    else
        //    {

        //        Debug.WriteLine("draw clear");
        //        g.Clear(Form2.ActiveForm.BackColor);
        //    }

        //}

        //private void Form2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        //{
        //    // Create a local version of the graphics object for the PictureBox.
        //    Graphics g = e.Graphics;
        //    if (isMouseDown)
        //    {
        //        g.Clear(System.Drawing.Color.Transparent);
        //        g.FillRectangle(brush, this.rect);
        //        Debug.WriteLine("draw rectanger");
        //    } else
        //    {

        //        Debug.WriteLine("draw clear");
        //        g.Clear(System.Drawing.Color.FromName("white"));
        //    }


        // Draw a string on the PictureBox.
        // g.DrawString("This is a diagonal line drawn on the control",
        //    fnt, System.Drawing.Brushes.Blue, new Point(30, 30));
        // Draw a line in the PictureBox.
        // g.DrawLine(System.Drawing.Pens.Red, pictureBox1.Left, pictureBox1.Top,
        //    pictureBox1.Right, pictureBox1.Bottom);
        //}

        public void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            x = e.X;
            y = e.Y;

            Debug.WriteLine("Mouse Click!");
            Debug.WriteLine("Start At X:{0} Y:{1}", x, y);
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        { 
            ClearBackgroup();

            if (e.X > x)
            {
                CaptureScreen(x, y, this.rect.Width, this.rect.Height);
            }
            else
            {
                CaptureScreen(e.X, e.Y, this.rect.Width, this.rect.Height);
            }


            isMouseDown = false;
            x = 0.0;
            y = 0.0;
            // this.Close();
           
            //this.Invalidate(new Region(new Rectangle((int)x, (int)y, (int)rect.Width, (int)rect.Height)));
        }

        private void ClearBackgroup()
        {
            this.paint.Clear(System.Drawing.Color.FromArgb(100, 0, 0, 0));
        }

        private void DrawRect()
        {
            paint.DrawRectangle(this.pen, this.rect);
            
            finalPaint.DrawImage(this.bitmap, 0, 0);
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Debug.WriteLine("Mouse Move!");
                // 1. 通过一个矩形来表示目前截图区域
                System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle();
                double dx = e.X;
                double dy = e.Y;

                Debug.WriteLine("Move At X:{0} Y:{1}", dx, dy);

                double rectWidth = Math.Abs(dx - x);
                double rectHeight = Math.Abs(dy - y);
                SolidColorBrush brush = new SolidColorBrush(Colors.White);
                rect.Width = (int)rectWidth;
                rect.Height = (int)rectHeight;
                rect.Fill = brush;
                rect.Stroke = brush;
                rect.StrokeThickness = 1;

                this.rect.Width = Convert.ToInt32(rect.Width);
                this.rect.Height = Convert.ToInt32(rect.Height);

                if (dx < x)
                {
                    this.rect.X = Convert.ToInt32(dx);
                    this.rect.Y = Convert.ToInt32(dy);

                    // Canvas.SetLeft(rect, dx);
                    // Canvas.SetTop(rect, dy);

                    //this.Invalidate(new Region(new Rectangle((int)dx, (int)dy, (int)rect.Width, (int)rect.Height)));
                }
                else
                {

                    this.rect.X = Convert.ToInt32(x);
                    this.rect.Y = Convert.ToInt32(y);
                   // Canvas.SetLeft(rect, x);
                   // Canvas.SetTop(rect, y);
                    //this.Invalidate(new Region(new Rectangle((int)x, (int)y, (int)rect.Width, (int)rect.Height)));
                }

                ClearBackgroup();
                DrawRect();

                // CaptureCanvas.Children.Clear();
                // CaptureCanvas.Children.Add(rect);

             
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

        private void Form2_Resize(object sender, EventArgs e)
        {
            finalPaint = panel_paint.CreateGraphics();
            Control control = (Control)sender;

            //bitmap.Width = control.Width;
            //bitmap.Height = control.Height;
            bitmap = new Bitmap(control.Width, control.Height);
            paint = Graphics.FromImage(bitmap);
        }
    }
}
