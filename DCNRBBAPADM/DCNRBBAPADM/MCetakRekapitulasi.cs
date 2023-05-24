using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCNRBBAPADM
{
    public partial class MCetakRekapitulasi : Form
    {
        clsCon con = new clsCon();
        string tampungKodeNamaSupp;
        string user;
        public MCetakRekapitulasi(string username)
        {
            InitializeComponent();
            user = username;
        }

        private void MCetakRekapitulasi_Load(object sender, EventArgs e)
        {
            txtKodeDC_NRB.Text = con.getKodeDC();
            txtNamaDC_NRB.Text = con.getNamaDC();
            txtTahun_NRB.Text = con.getYear();
            cbBulan_NRB.SelectedIndex = Convert.ToInt32(con.getMonth()) - 1;
            cbKodeSupp_NRB.DataSource = con.getKodeSupp();
            cbKodeSupp_NRB.DisplayMember = "SUP_SUPKODE";
            cbKodeSupp_NRB.SelectedIndex = -1;
            txtNamaSupp_NRB.Text = "ALL";

            txtKodeDC_BAP.Text = con.getKodeDC();
            txtNamaDC_BAP.Text = con.getNamaDC();
            txtTahun_BAP.Text = con.getYear();
            cbBulan_BAP.SelectedIndex = Convert.ToInt32(con.getMonth()) - 1;
            cbKodeSupp_BAP.DataSource = con.getKodeSupp();
            cbKodeSupp_BAP.DisplayMember = "SUP_SUPKODE";
            cbKodeSupp_BAP.SelectedIndex = -1;
            txtNamaSupp_BAP.Text = "ALL";
        }

        private void btn_NRB_Partial_Click(object sender, EventArgs e)
        {
            Rekapitulasi_NRB NRB = new Rekapitulasi_NRB(cbBulan_NRB.SelectedItem.ToString(), txtTahun_NRB.Text, txtNamaSupp_NRB.Text, cbKodeSupp_NRB.Text.Replace("'", "").ToUpper().Trim(), txtKodeDC_NRB.Text, txtNamaDC_NRB.Text, user);
            NRB.Show();
        }

        private void btn_BAP_Penundaan_Click(object sender, EventArgs e)
        {
            Rekapitulasi_BAP BAP = new Rekapitulasi_BAP(cbBulan_BAP.SelectedItem.ToString(), txtTahun_BAP.Text, txtNamaSupp_BAP.Text, cbKodeSupp_BAP.Text.Replace("'", "").ToUpper().Trim(), txtKodeDC_BAP.Text, txtNamaDC_BAP.Text, user);
            BAP.Show();
        }

        private void cbKodeSupp_NRB_TextChanged(object sender, EventArgs e)
        {
            if (cbKodeSupp_NRB.Text == "")
                txtNamaSupp_NRB.Text = "ALL";
            else
            {
                if (cbKodeSupp_NRB.Text.Length == 4)
                    txtNamaSupp_NRB.Text = con.getNamaSupp(cbKodeSupp_NRB.Text.Replace("'", "").ToUpper().Trim());
            }
                
        }

        private void cbKodeSupp_BAP_TextChanged(object sender, EventArgs e)
        {
            if (cbKodeSupp_BAP.Text == "")
                txtNamaSupp_BAP.Text = "ALL";
            else
            {
                if (cbKodeSupp_BAP.Text.Length == 4)
                    txtNamaSupp_BAP.Text = con.getNamaSupp(cbKodeSupp_BAP.Text.Replace("'", "").ToUpper().Trim());
            }
        }
    }
}
