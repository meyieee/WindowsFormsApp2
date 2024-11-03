using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class pomodoro : Form
    {
        private int seconds;

        public pomodoro()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (seconds > 0)
            {
                seconds--;
                TimeSpan time = TimeSpan.FromSeconds(seconds);
                // Format time as mm:ss
                label1.Text = time.ToString(@"mm\:ss");
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Pomodoro complete!", "Timer");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            seconds = 1500; // Set timer for 25 minutes (1500 seconds)
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            label1.Text = time.ToString(@"mm\:ss"); // Initialize label with 25:00
            timer1.Start(); // Start the timer
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }

        private void pomodoro_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();

        }
    }
}
