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
    public partial class Form3 : Form
    {
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\girissistemi.mdb");
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();
        bool yenikayitmi;
        int kacinci;

        public Form3()
        {
            InitializeComponent();
        }

        void Veri_Cek()
        {
            string sec = "select * from kullanici ";
            OleDbDataAdapter da = new OleDbDataAdapter(sec, conn);
            if (ds.Tables["kullanici"] != null) ds.Tables["kullanici"].Clear();
            da.Fill(ds, "kullanici");
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            tbkayitno.Enabled = tbadi.Enabled = tbsoyadi.Enabled = tbkadi.Enabled = tbşifre.Enabled = tbmail.Enabled = false;
            btnkaydet.Visible = btniptal.Visible = false;
            if (conn.State == ConnectionState.Closed) conn.Open();
            Veri_Cek();
            bs.DataSource = ds.Tables["kullanici"];
            dataGridView1.DataSource = bs;

            tbkayitno.DataBindings.Add("Text", bs, "kayitno");
            tbadi.DataBindings.Add("Text", bs, "adi");
            tbsoyadi.DataBindings.Add("Text", bs, "soyadi");
            tbkadi.DataBindings.Add("Text", bs, "kadi");
            tbşifre.DataBindings.Add("Text", bs, "sifre");
            tbmail.DataBindings.Add("Text", bs, "gmail");

            toolStripLabel1.Text = "Kayıt Sayısı: " + ds.Tables["kullanici"].Rows.Count;
        }

        private void btnkayit_Click(object sender, EventArgs e)
        {
            tbadi.Enabled = tbsoyadi.Enabled = tbkadi.Enabled = tbşifre.Enabled = tbmail.Enabled = true;
            btnkaydet.Visible = btniptal.Visible = true;
            tbkayitno.Clear(); tbadi.Clear(); tbsoyadi.Clear(); tbkadi.Clear(); tbşifre.Clear(); tbmail.Clear();
            tbkayitno.Text = "Bu Alan Otamatiktir!";
            tbadi.Focus();
            yenikayitmi = true;
            kacinci = ds.Tables["kullanici"].Rows.Count;
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            tbadi.Enabled = tbsoyadi.Enabled = tbkadi.Enabled = tbşifre.Enabled = tbmail.Enabled = true;
            btnkaydet.Visible = btniptal.Visible = true;
            tbadi.Focus();
            yenikayitmi = false;
            kacinci = bs.Position;
        }


        private void btnkaydet_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            if (yenikayitmi)
            {
                cmd.CommandText = "insert into kullanici(adi,soyadi,kadi,sifre,gmail) values (@adi,@soyadi,@kadi,@sifre,@gmail)";
                cmd.Parameters.AddWithValue("@adi", tbadi.Text);
                cmd.Parameters.AddWithValue("@soyadi", tbsoyadi.Text);
                cmd.Parameters.AddWithValue("@kadi", tbkadi.Text);
                cmd.Parameters.AddWithValue("@sifre", tbşifre.Text);
                cmd.Parameters.AddWithValue("@gmail", tbmail.Text);
            }
            else
            {
                cmd.CommandText = "update kullanici set adi=@adi,soyadi=@soyadi,kadi=@kadi,sifre=@sifre,gmail=@gmail where kayitno=@kayitno";
                cmd.Parameters.AddWithValue("@adi", tbadi.Text);
                cmd.Parameters.AddWithValue("@soyadi", tbsoyadi.Text);
                cmd.Parameters.AddWithValue("@kadi", tbkadi.Text);
                cmd.Parameters.AddWithValue("@sifre", tbşifre.Text);
                cmd.Parameters.AddWithValue("@gmail", tbmail.Text);
                cmd.Parameters.AddWithValue("@kayitno", tbkayitno.Text);
            }

            cmd.ExecuteNonQuery();
            MessageBox.Show("İşlem Gerçekleştirildi");
            Veri_Cek();
            bs.Position = kacinci;
            tbadi.Enabled = tbsoyadi.Enabled = tbkadi.Enabled = tbşifre.Enabled = tbmail.Enabled = false;
            btnkaydet.Visible = btniptal.Visible = false;

            toolStripLabel1.Text = "Kayıt Sayısı: " + ds.Tables["kullanici"].Rows.Count;
        }

        private void btniptal_Click(object sender, EventArgs e)
        {
            tbadi.Enabled = tbsoyadi.Enabled = tbkadi.Enabled = tbşifre.Enabled = tbmail.Enabled = false;
            btnkaydet.Visible = btniptal.Visible = false;
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            kacinci = bs.Position;
            DialogResult cevap = MessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Onay Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete from kullanici where kayitno=@kayitno";
                cmd.Parameters.AddWithValue("@kayitno", tbkayitno.Text);
                cmd.ExecuteNonQuery();
                Veri_Cek();
                bs.Position = kacinci;

                toolStripLabel1.Text = "Kayıt Sayısı: " + ds.Tables["kullanici"].Rows.Count;
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            conn.Close();
        }
    }
}
