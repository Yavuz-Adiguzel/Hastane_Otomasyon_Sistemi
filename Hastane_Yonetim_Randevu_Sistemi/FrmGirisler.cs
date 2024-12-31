using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Yonetim_Randevu_Sistemi
{
    public partial class FrmGirisler : Form
    {
        public FrmGirisler()
        {
            InitializeComponent();
        }

        private void BtnHasta_Click(object sender, EventArgs e)
        {
            FrmHastaGiris fr=new FrmHastaGiris();
            fr.Show();
            this.Hide();
        }

        private void BtnDoktor_Click(object sender, EventArgs e)
        {
            FrmDoktorGiris fr = new FrmDoktorGiris();
            fr.Show();
            this.Hide();
        }

        private void BtnSekreter_Click(object sender, EventArgs e)
        {
            FrmSekreter fr = new FrmSekreter();
            fr.Show();
            this.Hide();
        }
    }
}
