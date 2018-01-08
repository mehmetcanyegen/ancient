using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ancient.Ctr
{
    public partial class DateChooser : UserControl
    {
        public DateChooser()
        {
            InitializeComponent();
        }

        public event EventHandler ValueChanged;

        public DateTime DateTimeValue
        {
            get { return (DateTime)dateTimePicker1.Value; }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            try
            {
                dateTimePicker1.Value = ((DateTime)dateTimePicker1.Value).AddDays(-1);
            }
            catch (Exception ex)
            {
                // ignored
            }
            ((Button)sender).Enabled = true;

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            try
            {
                dateTimePicker1.Value = ((DateTime)dateTimePicker1.Value).AddDays(1);

            }
            catch (Exception ex)
            {
                // ignored
            }
            ((Button)sender).Enabled = true;
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            try
            {
                dateTimePicker1.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                // ignored
            }
            ((Button)sender).Enabled = true;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(new object(), new EventArgs());
        }
    }
}
