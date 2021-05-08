using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormScreenShoot
{
    public partial class TranslateResultForm : Form
    {
        
        public TranslateResultForm(String src, String dst)
        {
            InitializeComponent();
            this.TranslateSrc.Text = src;
            this.TranslateDst.Text = dst;
        }

    }
}
