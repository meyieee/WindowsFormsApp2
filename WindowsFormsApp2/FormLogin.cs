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
using MySql.Data.MySqlClient;

namespace WindowsFormsApp2
{
    public partial class FormLogin : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;

        private DataSet ds = new DataSet();
        private string alamat, query;

        public FormLogin()
        {
            alamat = "server=localhost; database=pomodoro_apk; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
        }

        // FormLogin_Load method placed inside FormLogin class
        private void FormLogin_Load(object sender, EventArgs e)
        {
            // Code to initialize form, if needed
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Open the registration form
            Form4 registerForm = new Form4();
            registerForm.FormClosed += (s, args) => this.Show(); // Show login form when registration form is closed
            this.Hide(); // Hide the current login form
            registerForm.Show(); //
        }

        // btnLogin_Click method placed inside FormLogin cl
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                query = string.Format("select * from register where username = '{0}'", txtUsername.Text);
                ds.Clear();
                koneksi.Open();
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                adapter.Fill(ds);
                koneksi.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow kolom in ds.Tables[0].Rows)
                    {
                        string sandi;
                        sandi = kolom["password"].ToString();
                        if (sandi == txtPassword.Text)
                        {
                            pomodoro pomodoroForm = new pomodoro();
                            this.Hide();
                            pomodoroForm.ShowDialog(); // Menampilkan Form Pomodoro
                            this.Close();

                        }
                        else
                        {
                            MessageBox.Show("Anda salah input password");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Username tidak ditemukan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
