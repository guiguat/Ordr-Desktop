﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrdrDesktop
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
            btnSure.Visible = false;
            lblBaseURL.Text = ApiHelper.baseUrl;
        }

        private void btnAlterarURL_Click(object sender, EventArgs e)
        {
            btnAlterarURL.Visible = false;
            btnSure.Visible = true;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            ApiHelper.baseUrl = txbURL.Text;
            btnAlterarURL.Visible = true;
            btnSure.Visible = false;
            txbURL.Text = "";
        }
    }
}
