namespace WinFormScreenShoot
{
    partial class TranslateResultForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TranslateResultForm));
            this.label1 = new System.Windows.Forms.Label();
            this.TranslateSrc = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TranslateDst = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "原文: ";
            // 
            // TranslateSrc
            // 
            this.TranslateSrc.AutoSize = true;
            this.TranslateSrc.Location = new System.Drawing.Point(92, 32);
            this.TranslateSrc.MaximumSize = new System.Drawing.Size(300, 100);
            this.TranslateSrc.Name = "TranslateSrc";
            this.TranslateSrc.Size = new System.Drawing.Size(83, 100);
            this.TranslateSrc.TabIndex = 1;
            this.TranslateSrc.Text = "label2\r\naoeaoeu\r\naoeuaoeu\r\naoeuaoeua\r\naoeuaoeuao\r\naoeuaoeuaoeua\r\naoeuaoeuaoeu\r\nao" +
    "euaoeuaoe\r\naoeuaoeuaoe\r\naoeuaoeuaoe\r\naoeuaoeuaoe\r\naoeuaoeuaoe\r\naoeuaoeuaoeu";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "译文: ";
            // 
            // TranslateDst
            // 
            this.TranslateDst.AutoSize = true;
            this.TranslateDst.Location = new System.Drawing.Point(92, 142);
            this.TranslateDst.MaximumSize = new System.Drawing.Size(300, 0);
            this.TranslateDst.Name = "TranslateDst";
            this.TranslateDst.Size = new System.Drawing.Size(299, 72);
            this.TranslateDst.TabIndex = 1;
            this.TranslateDst.Text = resources.GetString("TranslateDst.Text");
            // 
            // TranslateResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 276);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TranslateDst);
            this.Controls.Add(this.TranslateSrc);
            this.Controls.Add(this.label1);
            this.Name = "TranslateResultForm";
            this.Text = "翻译结果";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TranslateSrc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label TranslateDst;
    }
}