using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Yonetim_Randevu_Sistemi
{
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;

        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;

            //Hasta Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("SELECT HastaAd,HastaSoyad From Tbl_Hastalar where HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //Randevu Geçmişi Çekme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular where HastaTC="+tc, bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;

            //Branşları Çekme
            SqlCommand komut2 = new SqlCommand("SELECT BransAd FROM Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

            
            
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("SELECT DoktorAd,DoktorSoyad FROM Tbl_Doktor where DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular where RandevuBrans='"+ CmbBrans.Text+"'" + " and RandevuDoktor='" + CmbDoktor.Text + "'and RandevuDurum=0" , bgl.baglanti());
            da.Fill(dt);
            dataGridView3.DataSource = dt;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.TCno = LblTC.Text;
            fr.Show();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView3.SelectedCells[0].RowIndex;
            Txtİd.Text = dataGridView3.Rows[secilen].Cells[0].Value.ToString();

        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Randevular Set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 where Randevuİd=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            komut.Parameters.AddWithValue("@p2", RchSikayet.Text);
            komut.Parameters.AddWithValue("@p3", Txtİd.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
