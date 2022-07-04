using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Giris_Sistemi
{
    public partial class Form4 : Form
    {
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\girissistemi.mdb");         

        public Form4()
        {
            InitializeComponent();
        }

        private void btnkayıtol_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;

            if (tbşifre.Text == tbsifreonay.Text)
            {                         
                cmd.CommandText = "insert into kullanici(adi,soyadi,kadi,sifre,gmail) values (@adi,@soyadi,@kadi,@sifre,@gmail)";
                cmd.Parameters.AddWithValue("@adi", tbadi.Text);
                cmd.Parameters.AddWithValue("@soyadi", tbsoyadi.Text);
                cmd.Parameters.AddWithValue("@kadi", tbkadi.Text);
                cmd.Parameters.AddWithValue("@sifre", tbşifre.Text);
                cmd.Parameters.AddWithValue("@gmail", tbmail.Text);

                MessageBox.Show("Kayıt İşleminiz Başarılı Bir Şekilde Gerçekleşti.", "Bilgi Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbadi.Enabled = tbsoyadi.Enabled = tbmail.Enabled = tbkadi.Enabled = tbsifreonay.Enabled = tbşifre.Enabled = false;
                btngirisyap.Visible = true;
            }
            else
            {                
                MessageBox.Show("Şifreler Birbiri ile Uyuşmuyor!", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);              
                tbsifreonay.Clear();  tbşifre.Clear();
            }

            cmd.ExecuteNonQuery();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox2.Text = "Şifreyi Gizle";
                tbşifre.PasswordChar = '\0'; // tbsifre'deki metni Gösterir.
                tbsifreonay.PasswordChar = '\0';
            }
            else
            {
                checkBox2.Text = "Şifreyi Göster";
                tbşifre.PasswordChar = '*';//tbsifre'deki metni * şekli ile gizler(maskeler).
                tbsifreonay.PasswordChar = '*';
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            btngirisyap.Visible = false;
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            conn.Close();
        }

        private void btngirisyap_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.ShowDialog();
        }
    }
}
