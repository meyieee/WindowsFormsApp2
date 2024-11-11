using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;

namespace WindowsFormsApp2
{
    public partial class Form4 : Form
    {
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private MySqlConnection koneksi;
        private string alamat, query;
        public Form4()
        {
            alamat = "server=localhost; database=pomodoro_apk; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void txtNamaPengguna_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLogin_Click(object sender, EventArgs e)
        {
            FormLogin loginForm = new FormLogin();
            loginForm.Show();
            this.Close();

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNamaPengguna.Text) ||
                    string.IsNullOrEmpty(txtAlamat.Text) ||
                    string.IsNullOrEmpty(txtUsername.Text) ||
                    string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                query = "INSERT INTO register (nama_pengguna, alamat, username, password) VALUES (@nama_pengguna, @alamat, @username, @password)";
                koneksi.Open();
                using (MySqlCommand perintah = new MySqlCommand(query, koneksi))
                {
                    perintah.Parameters.AddWithValue("@nama_pengguna", txtNamaPengguna.Text);
                    perintah.Parameters.AddWithValue("@alamat", txtAlamat.Text);
                    perintah.Parameters.AddWithValue("@username", txtUsername.Text);
                    perintah.Parameters.AddWithValue("@password", txtPassword.Text);

                    int rowsAffected = perintah.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration successful!");
                        FormLogin loginForm = new FormLogin();
                        loginForm.Show();
                        this.Close(); // Close the registration form after successful registration
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. Please try again.");
                    }
                }
                koneksi.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

    }
    }

