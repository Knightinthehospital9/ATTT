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
    public partial class DonBang : Form
    {
        DonBangCipher donbang = new DonBangCipher();
        public DonBang()
        {
            InitializeComponent();
        }

        private void btnMH_Click(object sender, EventArgs e)
        {
            try
            {
                string khoa = txtKey.Text.ToUpper();
                donbang.KhoiTaoBang(khoa);

                string vanBan = txtVanBan.Text;
                string ketQua = donbang.MaHoa(vanBan);
                txtKetQua.Text = ketQua;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnGM_Click(object sender, EventArgs e)
        {
            try
            {
                string khoa = txtKey.Text.ToUpper();
                donbang.KhoiTaoBang(khoa);

                string vanBan = txtVanBan.Text;
                string ketQua = donbang.GiaiMa(vanBan);
                txtKetQua.Text = ketQua;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
