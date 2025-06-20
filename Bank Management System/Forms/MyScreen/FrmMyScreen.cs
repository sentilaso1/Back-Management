﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;



namespace Bank_Management_System.Forms.MyScreen
{
    public partial class FrmMyScreen : Form
    {
        public FrmMyScreen()
        {
            InitializeComponent();
        }

        private void MakePictureBoxCircular(PictureBox pb)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, pb.Width, pb.Height);
            pb.Region = new Region(gp);
        }

        private void FrmMyScreen_Load(object sender, EventArgs e)
        {
            MakePictureBoxCircular(pbMyPhoto);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGithub_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "",
                UseShellExecute = true
            });
        }

        private void btnLinkedin_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "",
                UseShellExecute = true
            });

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void lblMajor_Click(object sender, EventArgs e)
        {

        }
    }
}
