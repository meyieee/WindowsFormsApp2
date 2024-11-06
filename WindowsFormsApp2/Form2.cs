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
    public partial class Form2 : Form
    {
        private int seconds;

        public Form2()
        {
            InitializeComponent();
            timer1.Interval = 1000; // Set the timer interval to 1 second (1000 ms)
            timer1.Tick += new EventHandler(timer1_Tick); // Ensure the Tick event is attached

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            seconds = 300; // Set timer for 10 minutes (600 seconds)
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            label1.Text = time.ToString(@"mm\:ss"); // Initialize label with 10:00
            timer1.Start(); // Start the timer

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (seconds > 0)
            {
                seconds--; // Decrement the seconds
                TimeSpan time = TimeSpan.FromSeconds(seconds);
                label1.Text = time.ToString(@"mm\:ss"); // Display time as mm:ss
            }
            else
            {
                timer1.Stop(); // Stop the timer when it reaches zero
                MessageBox.Show("Timer complete!", "Timer");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
