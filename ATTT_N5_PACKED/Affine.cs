using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATTT_N5_PACKED
{
    public partial class Affine : Form
    {
        public Affine()
        {
            InitializeComponent();
        }

        //-----------------------------Cac nut-----------------------------------------------------

        private void btn_maHoa_Click(object sender, EventArgs e)
        {
            try
            {
                int a = int.Parse(txb_a.Text);
                int b = int.Parse(txb_b.Text);
                string vanBan = txb_vb.Text;
                string vanBanMaHoa = MaHoaAffine(vanBan, a, b);
                txb_kq.Text = vanBanMaHoa;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_giaiMa_Click(object sender, EventArgs e)
        {
            try
            {
                int a = int.Parse(txb_a.Text);
                int b = int.Parse(txb_b.Text);
                string vanBanMaHoa = txb_vb.Text;
                string vanBanGiaiMa = GiaiMaAffine(vanBanMaHoa, a, b);
                txb_kq.Text = vanBanGiaiMa;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu mn = new Menu();
            mn.Show();
        }

        //----------------------------- ham su ly cac nut-----------------------------------------------------


        private int TimNghichDao(int a, int m)
        {
            int r1 = m, r2 = a;
            int t1 = 0, t2 = 1;
            int q, r, t;
            while (r2 > 0)
            {
                q = r1 / r2;

                r = r1 - q * r2;
                r1 = r2;
                r2 = r;

                t = t1 - q * t2;
                t1 = t2;
                t2 = t;
            }

            if (r1 == 1)
                return (t1 + m) % m;

            return -1;
        }
        private string MaHoaAffine(string vanBan, int a, int b)
        {
            string ketQua = "";
            a = (a % 26 + 26) % 26;
            b = (b % 26 + 26) % 26;
            int a_nghichDao = TimNghichDao(a, 26);
            if (a_nghichDao == -1)
            {
                MessageBox.Show("Hãy nhập số a thoả mãn UCLN của a với 26 là 1 !!!");
                return "";
            }

            foreach (char kyTu in vanBan)
            {
                if (char.IsLetter(kyTu))
                {
                    char dichChuyen = char.IsUpper(kyTu) ? 'A' : 'a';
                    int x = kyTu - dichChuyen;
                    int kyTuMaHoa = (a * x + b) % 26;
                    ketQua += (char)(kyTuMaHoa + dichChuyen);
                }
                else
                {
                    ketQua += kyTu;
                }
            }
            return ketQua;
        }
        private string GiaiMaAffine(string vanBanMaHoa, int a, int b)
        {
            string ketQua = "";
            a = (a % 26 + 26) % 26;
            b = (b % 26 + 26) % 26;
            int a_nghichDao = TimNghichDao(a, 26);
            if (a_nghichDao == -1)
            {
                MessageBox.Show("Hãy nhập số a thoả mãn UCLN của a với 26 là 1 !!!");
                return "";
            }

            foreach (char kyTu in vanBanMaHoa)
            {
                if (char.IsLetter(kyTu))
                {
                    char dichChuyen = char.IsUpper(kyTu) ? 'A' : 'a';
                    int y = kyTu - dichChuyen;
                    int kyTuGiaiMa = (a_nghichDao * (y - b + 26)) % 26;
                    ketQua += (char)(kyTuGiaiMa + dichChuyen);
                }
                else
                {
                    ketQua += kyTu;
                }
            }
            return ketQua;
        }
    }
}
