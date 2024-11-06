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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Membuka Form Pomodoro setelah login
            pomodoro pomodoroForm = new pomodoro();
            this.Hide(); // Menyembunyikan FormLogin setelah login
            pomodoroForm.ShowDialog(); // Menampilkan Form Pomodoro
            this.Close(); // Menutup FormLogin setelah Form Pomodoro ditutup
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
