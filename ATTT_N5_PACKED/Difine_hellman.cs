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
    public partial class Difine_hellman : Form
    {
        public Difine_hellman()
        {
            InitializeComponent();
        }

        //-----------------------------Cac nut-----------------------------------------------------

        private void btn_tinhKhoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các giá trị đầu vào có hợp lệ không
                if (string.IsNullOrWhiteSpace(txbq.Text) || string.IsNullOrWhiteSpace(txb_a.Text) ||
                    string.IsNullOrWhiteSpace(txb_xa.Text) || string.IsNullOrWhiteSpace(txb_xb.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ giá trị cho q, alpha, khóa riêng của A và B.");
                    return;
                }

                long q = long.Parse(txbq.Text);
                long alpha = long.Parse(txb_a.Text);
                long khoaRiengXA = long.Parse(txb_xa.Text);
                long khoaRiengXB = long.Parse(txb_xb.Text);

                if (q <= 0 || alpha <= 0 || khoaRiengXA <= 0 || khoaRiengXB <= 0)
                {
                    MessageBox.Show("Giá trị của q, alpha và các khóa riêng phải là số dương.");
                    return;
                }

                // Tính khóa công khai YA và YB
                long khoaCongKhaiYA = TinhKhoaCongKhai(alpha, khoaRiengXA, q);
                long khoaCongKhaiYB = TinhKhoaCongKhai(alpha, khoaRiengXB, q);

                txb_ya.Text = khoaCongKhaiYA.ToString();
                txb_yb.Text = khoaCongKhaiYB.ToString();

                // Tính khóa bí mật chung K
                long khoaBiMatA = TinhKhoaBiMat(khoaCongKhaiYB, khoaRiengXA, q);
                long khoaBiMatB = TinhKhoaBiMat(khoaCongKhaiYA, khoaRiengXB, q);

                if (khoaBiMatA == khoaBiMatB)
                {
                    txb_khoa.Text = khoaBiMatA.ToString();
                }
                else
                {
                    MessageBox.Show("Có lỗi trong tính toán! Khóa bí mật của hai bên không khớp.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tính toán: {ex.Message}");
            }
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu mn = new Menu();
            mn.Show();
        }

        //----------------------------- ham su ly cac nut-----------------------------------------------------

        private long TinhKhoaCongKhai(long alpha, long khoaRieng, long q)
        {
            return LuyThuaMod(alpha, khoaRieng, q);
        }

        // Hàm tính khóa bí mật K = Y^X mod q
        private long TinhKhoaBiMat(long khoaCongKhai, long khoaRieng, long q)
        {
            return LuyThuaMod(khoaCongKhai, khoaRieng, q);
        }

        // Hàm tính lũy thừa theo mod bằng phương pháp bình phương và nhân
        private long LuyThuaMod(long coSo, long soMu, long mod)
        {
            long ketQua = 1;
            coSo = coSo % mod;

            while (soMu > 0)
            {
                if ((soMu & 1) == 1) // Kiểm tra nếu số mũ là lẻ
                {
                    ketQua = (ketQua * coSo) % mod;
                }

                soMu = soMu >> 1; // Chia số mũ cho 2
                coSo = (coSo * coSo) % mod;
            }
            return ketQua;
        }
    }
}
