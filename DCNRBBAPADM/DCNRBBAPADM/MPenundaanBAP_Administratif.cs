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
    public partial class MPenundaanBAP_Administratif : Form
    {
        clsCon con = new clsCon();
        public MPenundaanBAP_Administratif()
        {
            InitializeComponent();
            refresh();
        }

        private void refresh()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            string sql = clsSQL.PenundaanBAP_Administratif();
            DataTable dt = con.ShowData(sql);

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["KODE_DC"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["UPDREC_DATE"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["PLUID"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["TAG"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["TGL_BAPSEPIHAK"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["JML_HARI"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["USULAN_BARU_BAP"].ToString();
            }

        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void Find()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            string sql = clsSQL.FindPenundaanBAP_Administratif(txt_Find.Text);
            DataTable dt = con.ShowData(sql);

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["KODE_DC"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["UPDREC_DATE"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["PLUID"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["TAG"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["TGL_BAPSEPIHAK"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["JML_HARI"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["USULAN_BARU_BAP"].ToString();
            }
        }


        private void btn_Find_Click(object sender, EventArgs e)
        {
            Find();
        }

        private void txt_Find_TextChanged(object sender, EventArgs e)
        {
            Find();
        }
    }
}
