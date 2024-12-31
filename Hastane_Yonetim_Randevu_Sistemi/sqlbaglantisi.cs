using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Drawing;

namespace Hastane_Yonetim_Randevu_Sistemi
{
    internal class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source = ASUS\\SQLEXPRESS; Initial Catalog = HastaneYonetim; Integrated Security = True");
            baglan.Open();
            return baglan;
        }
    }
}
