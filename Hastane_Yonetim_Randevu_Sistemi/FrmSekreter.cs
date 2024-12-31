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
    public partial class FrmSekreter : Form
    {
        public FrmSekreter()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM Tbl_Sekreter where SekreterTC=@p1 and SekreterSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read()) 
            {
                FrmSekreterDetay fr = new FrmSekreterDetay();
                fr.TCnumara = MskTC.Text;
                fr.Show();
            }
            else
            {
                MessageBox.Show("TC veya şifre yanlış.Tekrar giriniz.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    }
}
