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
    public partial class Vingenere : Form
    {
        public Vingenere()
        {
            InitializeComponent();
        }

        //-----------------------------Cac nut-----------------------------------------------------

        private void btn_MaHoa_Click(object sender, EventArgs e)
        {
            string vanBan = txtVanBan.Text;
            string khoa = txtKhoa.Text;

            if (string.IsNullOrWhiteSpace(vanBan) || string.IsNullOrWhiteSpace(khoa))
            {
                MessageBox.Show("Vui lòng nhập văn bản và từ khóa!");
                return;
            }

            if (!KiemTraChuoiHopLe(khoa))
            {
                MessageBox.Show("Vui lòng chỉ nhập các chữ cái cho khóa!", "Lỗi đầu vào", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string ketQuaMaHoa = MaHoaVigenere(vanBan, khoa);
            txtKetQua.Text = ketQuaMaHoa;
        }

        private void btn_GiaiMa_Click(object sender, EventArgs e)
        {
            string vanBan = txtVanBan.Text;
            string khoa = txtKhoa.Text;

            if (string.IsNullOrWhiteSpace(vanBan) || string.IsNullOrWhiteSpace(khoa))
            {
                MessageBox.Show("Vui lòng nhập văn bản và từ khóa!");
                return;
            }

            if (!KiemTraChuoiHopLe(khoa))
            {
                MessageBox.Show("Vui lòng chỉ nhập các chữ cái cho khóa!", "Lỗi đầu vào", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string ketQuaGiaiMa = GiaiMaVigenere(vanBan, khoa);
            txtKetQua.Text = ketQuaGiaiMa;
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu mn = new Menu();
            mn.Show();
        }


        //----------------------------- ham su ly cac nut-----------------------------------------------------

        private string MaHoaVigenere(string vanBan, string khoa)
        {
            vanBan = vanBan.ToUpper();
            khoa = khoa.ToUpper();
            StringBuilder ketQua = new StringBuilder();

            for (int i = 0, j = 0; i < vanBan.Length; i++)
            {
                char kyTu = vanBan[i];

                if (!char.IsLetter(kyTu))
                {
                    ketQua.Append(kyTu);
                }
                else
                {
                    char kyTuKhoa = khoa[j % khoa.Length];
                    char kyTuMaHoa = (char)((kyTu + kyTuKhoa - 2 * 'A') % 26 + 'A');
                    ketQua.Append(kyTuMaHoa);
                    j++;

                }
            }

            return ketQua.ToString();
        }
        private string GiaiMaVigenere(string vanBan, string khoa)
        {
            vanBan = vanBan.ToUpper();
            khoa = khoa.ToUpper();
            StringBuilder ketQua = new StringBuilder();

            for (int i = 0, j = 0; i < vanBan.Length; i++)
            {
                char kyTu = vanBan[i];

                if (!char.IsLetter(kyTu))
                {
                    ketQua.Append(kyTu);
                }
                else
                {
                    char kyTuKhoa = khoa[j % khoa.Length];
                    char kyTuGiaiMa = (char)((kyTu - kyTuKhoa + 26) % 26 + 'A');
                    ketQua.Append(kyTuGiaiMa);
                    j++;
                }
            }

            return ketQua.ToString();
        }
        private bool KiemTraChuoiHopLe(string chuoi)
        {
            foreach (char kyTu in chuoi)
            {
                if (!char.IsLetter(kyTu))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
