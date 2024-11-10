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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void playfairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Playfair pl = new Playfair();
            this.Hide();
            pl.ShowDialog();
        }

        private void affineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Affine af = new Affine();
            this.Hide();
            af.ShowDialog();
        }

        private void vingenereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vingenere vg = new Vingenere();
            this.Hide();
            vg.ShowDialog();
        }

        private void hillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hill hill = new Hill();
            this.Hide();
            hill.ShowDialog();
        }
        private void rSAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rsa rsa = new Rsa();
            this.Hide();
            rsa.ShowDialog();
        }

        private void diffinehellmanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Difine_hellman dh = new Difine_hellman();
            this.Hide();
            dh.ShowDialog();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void đơnBảngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
