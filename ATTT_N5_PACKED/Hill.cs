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
    public partial class Hill : Form
    {
        public Hill()
        {
            InitializeComponent();
        }

        //-----------------------------Cac nut-----------------------------------------------------

        private void btn_maHoa_Click(object sender, EventArgs e)
        {
            string vanBan = txb_vanBan.Text;
            int[,] maTranKhoa = LayMaTran(rtxb_maTranKhoa.Text);
            if (maTranKhoa != null)
            {
                string vanBanMaHoa = MaHoaHill(vanBan, maTranKhoa);
                txb_ketQua.Text = vanBanMaHoa;
            }
            else
            {
                MessageBox.Show("Ma trận không hợp lệ.");
            }
        }

        private void btn_giaiMa_Click(object sender, EventArgs e)
        {
            string vanBan = txb_vanBan.Text;
            int[,] maTranKhoa = LayMaTran(rtxb_maTranKhoa.Text);
            if (maTranKhoa != null)
            {
                string vanBanGiaiMa = GiaiMaHill(vanBan, maTranKhoa);
                txb_ketQua.Text = vanBanGiaiMa;
            }
            else
            {
                MessageBox.Show("Ma trận không hợp lệ.");
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu mn = new Menu();
            mn.Show();
        }

        //----------------------------- ham su ly cac nut-----------------------------------------------------

        private string MaHoaHill(string vanBan, int[,] maTranKhoa)
        {
            vanBan = vanBan.ToUpper();
            int N = maTranKhoa.GetLength(0);
            int[] vectorDauVao = new int[N];
            string ketQua = "";
            int indexVector = 0;

            for (int i = 0; i < vanBan.Length;)
            {
                if (char.IsWhiteSpace(vanBan[i]))
                {
                    ketQua += ' ';
                    i++;
                    continue;
                }

                for (int j = 0; j < N; j++)
                {
                    if (i < vanBan.Length && char.IsLetter(vanBan[i]))
                    {
                        vectorDauVao[j] = vanBan[i] - 'A';
                    }
                    else
                    {
                        vectorDauVao[j] = 'X' - 'A';
                    }
                    i++;
                }

                for (int j = 0; j < N; j++)
                {
                    int tong = 0;
                    for (int k = 0; k < N; k++)
                    {
                        tong += maTranKhoa[j, k] * vectorDauVao[k];
                    }
                    ketQua += (char)((tong % 26) + 'A');
                }
            }
            return ketQua;
        }

        private string GiaiMaHill(string vanBan, int[,] maTranKhoa)
        {
            int[,] maTranNghichDao = NghichDaoMaTran(maTranKhoa);
            if (maTranNghichDao == null)
            {
                MessageBox.Show("Không tìm được ma trận nghịch đảo.");
                return "";
            }
            return MaHoaHill(vanBan, maTranNghichDao);
        }

        private int[,] LayMaTran(string vanBanMaTran)
        {
            try
            {
                string[] dong = vanBanMaTran.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                int N = dong.Length;
                int[,] maTran = new int[N, N];

                for (int i = 0; i < N; i++)
                {
                    int[] giaTriDong = dong[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                                              .Select(int.Parse)
                                              .ToArray();
                    if (giaTriDong.Length != N) return null;

                    for (int j = 0; j < N; j++)
                    {
                        maTran[i, j] = giaTriDong[j];
                    }
                }
                return maTran;
            }
            catch
            {
                return null;
            }
        }

        private int[,] NghichDaoMaTran(int[,] maTran)
        {
            int N = maTran.GetLength(0);
            int dinhThuc = DinhThuc(maTran, N);
            int nghichDaoDinhThuc = TimNghichDaoMod(dinhThuc, 26);
            if (nghichDaoDinhThuc == -1) return null;

            int[,] maTranPhuHop = MaTranPhuHop(maTran);
            int[,] maTranNghichDao = new int[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    maTranNghichDao[i, j] = (maTranPhuHop[i, j] * nghichDaoDinhThuc) % 26;
                    if (maTranNghichDao[i, j] < 0) maTranNghichDao[i, j] += 26;
                }
            }
            return maTranNghichDao;
        }

        private int DinhThuc(int[,] maTran, int n)
        {
            if (n == 1) return maTran[0, 0];

            int dinhThuc = 0;
            int[,] temp = new int[n, n];
            int dau = 1;

            for (int i = 0; i < n; i++)
            {
                LayMaTranCon(maTran, temp, 0, i, n);
                dinhThuc += dau * maTran[0, i] * DinhThuc(temp, n - 1);
                dau = -dau;
            }
            return dinhThuc % 26;
        }

        private void LayMaTranCon(int[,] maTran, int[,] temp, int p, int q, int n)
        {
            int i = 0, j = 0;
            for (int hang = 0; hang < n; hang++)
            {
                for (int cot = 0; cot < n; cot++)
                {
                    if (hang != p && cot != q)
                    {
                        temp[i, j++] = maTran[hang, cot];
                        if (j == n - 1)
                        {
                            j = 0;
                            i++;
                        }
                    }
                }
            }
        }

        private int[,] MaTranPhuHop(int[,] maTran)
        {
            int N = maTran.GetLength(0);
            int[,] phuHop = new int[N, N];
            if (N == 1)
            {
                phuHop[0, 0] = 1;
                return phuHop;
            }

            int dau = 1;
            int[,] temp = new int[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    LayMaTranCon(maTran, temp, i, j, N);
                    dau = ((i + j) % 2 == 0) ? 1 : -1;
                    phuHop[j, i] = (dau * DinhThuc(temp, N - 1)) % 26;
                    if (phuHop[j, i] < 0) phuHop[j, i] += 26;
                }
            }
            return phuHop;
        }

        private int TimNghichDaoMod(int a, int m)
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
    }
}