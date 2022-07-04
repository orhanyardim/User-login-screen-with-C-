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
    public partial class Form2 : Form
    {
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\girissistemi.mdb");

        public Form2()
        {
            InitializeComponent();
        }
       // public static string bilgi_aktar;// formlar arası veri aktarımı için bu tanımlamayı yaptık

        private void btngiris_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("select * from kullanici where kadi= '" + tbkullaniciadi.Text + "' and sifre='" + tbsifre.Text + "'", conn);
            OleDbDataReader oku = cmd.ExecuteReader();
            if (oku.Read())
            {
                //bilgi_aktar = tbkullaniciadi.Text;//aktarmak istediğimiz veriyi ve şeklini kodladık.
                MessageBox.Show("Giriş Başarılı", "Bildirim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form5 f5 = new Form5();
                this.Hide();
                f5.ShowDialog();               
            }
            else if(tbkullaniciadi.Text=="")
            {
                MessageBox.Show("Kullanıcı Adı Alanı Boş Bırakılamaz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbkullaniciadi.Clear();
                tbsifre.Clear();
                tbkullaniciadi.Focus();
            }
            else if (tbsifre.Text == "")
            {
                MessageBox.Show("Şifre Alanı Boş Bırakılamaz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbkullaniciadi.Clear();
                tbsifre.Clear();
                tbkullaniciadi.Focus();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre Yanlış", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbkullaniciadi.Clear();
                tbsifre.Clear();
                tbkullaniciadi.Focus();
            }            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();           
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox2.Text = "Şifreyi Gizle";
                tbsifre.PasswordChar = '\0'; // tbsifre'deki metni Gösterir.
            }
            else
            {
                checkBox2.Text = "Şifreyi Göster";
                tbsifre.PasswordChar = '*';//tbsifre'deki metni * şekli ile gizler(maskeler).
            }
        }
    }
}
