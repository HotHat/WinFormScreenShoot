namespace WinFormScreenShoot
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_paint = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel_paint
            // 
            this.panel_paint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_paint.AutoSize = true;
            this.panel_paint.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel_paint.Location = new System.Drawing.Point(0, 0);
            this.panel_paint.Name = "panel_paint";
            this.panel_paint.Size = new System.Drawing.Size(974, 575);
            this.panel_paint.TabIndex = 0;
            this.panel_paint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseDown);
            this.panel_paint.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseMove);
            this.panel_paint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseUp);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 579);
            this.Controls.Add(this.panel_paint);
            this.Name = "Form2";
            this.Opacity = 0.5D;
            this.Text = "Form2";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseUp);
            this.Resize += new System.EventHandler(this.Form2_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_paint;
    }
}