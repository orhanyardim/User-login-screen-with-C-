using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Giris_Sistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnüye_Click(object sender, EventArgs e)
        {
            Form2 f2= new Form2();
            this.Hide();
            f2.ShowDialog();
        }

        private void btnadmin_Click(object sender, EventArgs e)
        {
            string a = Interaction.InputBox("Giriş İçin Kullanıcı Adı Giriniz!", "Yönetici Girişi");
            string b = Interaction.InputBox("Giriş İçin Şifre Giriniz!", "Yönetici Girişi");
            if (a == "Orhan.123"&&b=="12345")
            {
                Form3 f3 = new Form3();
                this.Hide();
                f3.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı Veya Şifre!!!", "Uyarı Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

           
        }

        private void bntkayit_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            this.Hide();
            f4.ShowDialog();           
        }
    }
}
