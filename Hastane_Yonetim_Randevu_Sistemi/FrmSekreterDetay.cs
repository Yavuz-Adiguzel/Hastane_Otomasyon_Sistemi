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
using System.Data.Common;

namespace Hastane_Yonetim_Randevu_Sistemi
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string TCnumara;
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = TCnumara;

            //Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("SELECT SekreterAdSoyad FROM Tbl_Sekreter where SekreterTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            //Branşları Çekme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;

            //Doktorları Listeye Aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT (DoktorAd+ ' ' +DoktorSoyad) as 'Doktorlar',DoktorBrans FROM Tbl_Doktor", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView3.DataSource = dt2;

            //Branşları Çekme
            SqlCommand komut3 = new SqlCommand("SELECT BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbBrans.Items.Add(dr3[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutyap = new SqlCommand("INSERT INTO Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
            komutyap.Parameters.AddWithValue("@p1", MskTarih.Text);
            komutyap.Parameters.AddWithValue("@p2", MskSaat.Text);
            komutyap.Parameters.AddWithValue("@p3", CmbBrans.Text);
            komutyap.Parameters.AddWithValue("@p4", CmbDoktor.Text);
            komutyap.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevunuz oluşturuldu");
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();

            SqlCommand komut2 = new SqlCommand("SELECT DoktorAd,DoktorSoyad From Tbl_Doktor where DoktorBrans=@p1", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbDoktor.Items.Add(dr2[0] + " " + dr2[1]);
            }
            bgl.baglanti().Close();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Duyurular (Duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", RchSikayet.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru oluşturuldu");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli frd = new FrmDoktorPaneli();
            frd.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmBrans fr = new FrmBrans();
            fr.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi fr = new FrmRandevuListesi();
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
        }
    }
}
