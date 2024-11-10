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
    public partial class Caesar : Form
    {
        CaeserCipher caeser = new CaeserCipher();
        public Caesar()
        {
            InitializeComponent();
        }

        private void btnMH_Click(object sender, EventArgs e)
        {
            string vanBan = txtVanBan.Text;
            if (int.TryParse(txtKey.Text, out int khoa))
            {
                string ketQua = caeser.MaHoa(vanBan, khoa);
                txtKetQua.Text = ketQua;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập khóa là số nguyên.");
            }
        }

        private void btnGM_Click(object sender, EventArgs e)
        {
            string vanBan = txtVanBan.Text;
            if (int.TryParse(txtKey.Text, out int khoa))
            {
                string ketQua = caeser.GiaiMa(vanBan, khoa);
                txtKetQua.Text = ketQua;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập khóa là số nguyên.");
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
