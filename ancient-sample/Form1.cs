﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ancient.Ctr;

namespace ancient_sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dateChooser1_ValueChanged(object sender, EventArgs e)
        {
            label1.Text =  dateChooser1.DateTimeValue.ToShortDateString();
        }
    }
}
