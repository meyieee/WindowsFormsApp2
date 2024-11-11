using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp2
{
    public partial class pomodoro : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;

        private DataSet ds = new DataSet();
        private string alamat, query;
        private string connectionString = "server=localhost;database=pomodoro_apk;username=root;password=;";
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
            RefreshDataGridView(); // Muat data saat form di-load
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
        {// Kosongkan input untuk Title dan Description
            titleText.Text = string.Empty;
            descriptionText.Text = string.Empty;

            // Fokus ke input Title untuk mempermudah pengguna
            titleText.Focus();

            isEditing = false; // Menandakan ini adalah mode 'Tambah Baru'

        }


        private void Editbutton_Click(object sender, EventArgs e)
        {
            if (todolistView.CurrentRow == null)
            {
                MessageBox.Show("Please select a task to edit.", "Edit Task");
                return;
            }

            titleText.Text = todolistView.CurrentRow.Cells["title"].Value.ToString();
            descriptionText.Text = todolistView.CurrentRow.Cells["description"].Value.ToString();
            isEditing = true; // Aktifkan mode Edit
        }


        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (todolistView.CurrentRow == null) return;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM tasks WHERE id_task=@id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", todolistView.CurrentRow.Cells["id_task"].Value);
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Task deleted successfully!");
            RefreshDataGridView();
        }


        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleText.Text) || string.IsNullOrWhiteSpace(descriptionText.Text))
            {
                MessageBox.Show("Title and Description cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    if (isEditing)
                    {
                        // Mode Edit: Update data berdasarkan id_task
                        string query = "UPDATE tasks SET title=@title, description=@description WHERE id_task=@id";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@title", titleText.Text);
                            cmd.Parameters.AddWithValue("@description", descriptionText.Text);
                            cmd.Parameters.AddWithValue("@id", todolistView.CurrentRow.Cells["id_task"].Value);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Task updated successfully!");
                    }
                    else
                    {
                        // Mode Tambah Baru: Insert data baru ke database
                        string query = "INSERT INTO tasks (title, description) VALUES (@title, @description)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@title", titleText.Text);
                            cmd.Parameters.AddWithValue("@description", descriptionText.Text);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Task added successfully!");
                    }
                }

                // Kosongkan input setelah menyimpan
                titleText.Text = string.Empty;
                descriptionText.Text = string.Empty;
                isEditing = false; // Reset mode ke 'Tambah Baru'
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving task: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void RefreshDataGridView()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM tasks";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        todolistView.DataSource = dt; // Bind ulang data dari database
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing data: " + ex.Message);
            }
        }



    }
}
