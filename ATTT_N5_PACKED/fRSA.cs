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
    public partial class fRSA : Form
    {
        private long khoaCongKhaiE;
        private long khoaCongKhaiN;
        private long khoaRiengD;
        public fRSA()
        {
            InitializeComponent();
        }

        private void sinhKhoa(int p, int q, int e)
        {
            long n = (long)p * q;                 // n = p * q
            long phi = (long)(p - 1) * (q - 1);   // phi(n) = (p - 1) * (q - 1)

            // Kiểm tra xem e có hợp lệ không
            if (e <= 1 || e >= phi || gcd(e, phi) != 1)
            {
                MessageBox.Show("Gia tri e khong hop le. Chon lai e sao cho 1 < e < φ(n) va gcd(e, φ(n)) = 1.");
                return;
            }

            khoaCongKhaiE = e;
            khoaCongKhaiN = n;
            khoaRiengD = nghichDaoMod(e, phi);     // Tính khóa riêng d
            if (khoaRiengD == -1)
            {
                MessageBox.Show("Khong the tinh khoa rieng d. Kiem tra lai gia tri cua e va φ(n).");
                return;
            }
        }

        // Hàm tính ước chung lớn nhất (GCD)
        private long gcd(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Hàm tính nghịch đảo modular
        private long nghichDaoMod(long e, long phi)
        {
            long d = 0, x1 = 0, x2 = 1, y1 = 1, tempPhi = phi;

            while (e > 0)
            {
                long temp1 = tempPhi / e;
                long temp2 = tempPhi - temp1 * e;
                tempPhi = e;
                e = temp2;

                long x = x2 - temp1 * x1;
                x2 = x1;
                x1 = x;

                long y = d - temp1 * y1;
                d = y1;
                y1 = y;
            }

            if (tempPhi == 1)
            {
                return (d + phi) % phi;  // Đảm bảo d là số dương
            }

            return -1; // Không tồn tại nghịch đảo modular
        }

        // Hàm mã hóa văn bản
        private long maHoa(long thongDiep, long e, long n)
        {
            return tinhLuyThuaMod(thongDiep, e, n);
        }

        // Hàm giải mã văn bản
        private long giaiMa(long banMa, long d, long n)
        {
            return tinhLuyThuaMod(banMa, d, n);
        }

        // Hàm tính lũy thừa modulus
        private long tinhLuyThuaMod(long giaTri, long soMu, long modulus)
        {
            long ketQua = 1;
            giaTri = giaTri % modulus;
            while (soMu > 0)
            {
                if ((soMu % 2) == 1)
                    ketQua = (ketQua * giaTri) % modulus;
                soMu = soMu >> 1;
                giaTri = (giaTri * giaTri) % modulus;
            }
            return ketQua;
        }

        // Kiểm tra số nguyên tố
        private bool kiemTraSoNguyenTo(int num)
        {
            if (num <= 1) return false;
            for (int i = 2; i <= Math.Sqrt(num); i++)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }

        private void tinhKhoa()
        {
            try
            {
                // Lấy giá trị từ TextBox
                int p = int.Parse(txb_p.Text);
                int q = int.Parse(txb_q.Text);
                int eValue = int.Parse(txb_e.Text);

                // Kiểm tra số nguyên tố p, q
                if (!kiemTraSoNguyenTo(p) || !kiemTraSoNguyenTo(q))
                {
                    MessageBox.Show("p va q phai la so nguyen to.");
                    return;
                }

                // Gọi hàm sinh khóa
                sinhKhoa(p, q, eValue);

                // Hiển thị khóa công khai và khóa riêng
                lb_khoaCK.Text = $"Khóa công khai (e, n): ({khoaCongKhaiE}, {khoaCongKhaiN})";
                lb_khoaBM.Text = $"Khóa bí mật (d, n): ({khoaRiengD}, {khoaCongKhaiN})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private void btn_maHoa_Click(object sender, EventArgs e)
        {
            tinhKhoa();
            try
            {
                if (!string.IsNullOrEmpty(txb_vanBan.Text))
                {
                    long plaintext = long.Parse(txb_vanBan.Text);
                    long ciphertext = maHoa(plaintext, khoaCongKhaiE, khoaCongKhaiN);
                    txb_ketQua.Text = ciphertext.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btn_giaiMa_Click(object sender, EventArgs e)
        {
            tinhKhoa();
            try
            {
                if (!string.IsNullOrEmpty(txb_vanBan.Text))
                {
                    long ciphertext = long.Parse(txb_vanBan.Text);
                    long plaintext = giaiMa(ciphertext, khoaRiengD, khoaCongKhaiN);
                    txb_ketQua.Text = plaintext.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
