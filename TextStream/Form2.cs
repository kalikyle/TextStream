using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextStream
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "https://www.facebook.com/KTP23o/"; // Replace with your link
            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Important for .NET Core and .NET 5+
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open the link: " + ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/kalikyle"; // Replace with your link
            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Important for .NET Core and .NET 5+
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open the link: " + ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string url = "www.linkedin.com/in/kyle-pintor-8a17a417b"; // Replace with your link
            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Important for .NET Core and .NET 5+
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open the link: " + ex.Message);
            }

        }
    }
}
