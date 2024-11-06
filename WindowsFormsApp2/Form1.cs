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

        DataTable todoList = new DataTable();
        bool isEditing = false;

        private void pomodoro_Load(object sender, EventArgs e)
        {
            //Create columns
            todoList.Columns.Add("Title");
            todoList.Columns.Add("Description");

            todolistView.DataSource = todoList;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 longBreakForm = new Form2();
            // Menampilkan Form2 sebagai form baru
            longBreakForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 shortBreakForm = new Form3();
            // Menampilkan Form3 sebagai form baru
            shortBreakForm.Show();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            titleText.Text = "";
            descriptionText.Text = "";

        }

        private void Editbutton_Click(object sender, EventArgs e)
        {
            isEditing = true;
            // Mengambil data dari baris yang dipilih untuk Title dan Description
            titleText.Text = todoList.Rows[todolistView.CurrentCell.RowIndex]["Title"].ToString();
            descriptionText.Text = todoList.Rows[todolistView.CurrentCell.RowIndex]["Description"].ToString();
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            try
            {
                todoList.Rows[todolistView.CurrentCell.RowIndex].Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }


        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleText.Text) || string.IsNullOrWhiteSpace(descriptionText.Text))
            {
                MessageBox.Show("Both Title and Description are required.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (isEditing)
            {
                // Update baris yang sedang diedit
                todoList.Rows[todolistView.CurrentCell.RowIndex]["Title"] = titleText.Text;
                todoList.Rows[todolistView.CurrentCell.RowIndex]["Description"] = descriptionText.Text;
            }
            else
            {
                // Tambahkan baris baru ke DataTable
                todoList.Rows.Add(titleText.Text, descriptionText.Text);
            }

            // Reset text fields
            titleText.Text = "";
            descriptionText.Text = "";
            isEditing = false;

            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            todolistView.DataSource = null; // Hapus DataSource sementara
            todolistView.DataSource = todoList; // Set ulang DataSource
        }


    }
}
