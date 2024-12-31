using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hastane_Yonetim_Randevu_Sistemi
{
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }
        public string TCno;
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = TCno;

            SqlCommand komut = new SqlCommand("Select * From Tbl_Hastalar where HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAd.Text = dr[1].ToString();
                LblSoyad.Text = dr[2].ToString();
                MskTelefon.Text = dr[4].ToString();
                TxtSifre.Text = dr[5].ToString();
                CmbCinsiyet.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("UPDATE Tbl_Hastalar SET HastaAd=@p1, HastaSoyad=@p2,HastaTelefon=@p3,HastaSifre=@p4,HastaCinsiyet=@p5 where HastaTC=@p6", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", LblAd.Text);
            komut2.Parameters.AddWithValue("@p2", LblSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", MskTelefon.Text);
            komut2.Parameters.AddWithValue("@p4", TxtSifre.Text);
            komut2.Parameters.AddWithValue("@p5", CmbCinsiyet.Text);
            komut2.Parameters.AddWithValue("@p6", MskTC.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellenmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
