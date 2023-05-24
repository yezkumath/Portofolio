using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MONSRPS
{
    public partial class Monitoring : Form
    {
            clsCon conn = new clsCon();
            string username;
            DataTable data = new DataTable();

        public Monitoring(string user)
        {
            InitializeComponent();
            refresh();
            username = user;
        }

        private void refresh()
        {
            int i = 0;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            string date = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            string sql = "SELECT " +
                         "KODE_SUPP, NAMA_SUPP, PLUID, DESKRIPSI, DIVISI, DEPARTEMEN, KATEGORI, TAG, QTY, HPP, TO_CHAR(TGL_NRBSEPIHAK,'dd/mm/yyyy') AS NRBSEPIHAK,  TO_CHAR(TGL_BAPSEPIHAK,'dd/mm/yyyy') AS BAPSEPIHAK " +
                         "FROM DC_SRPS_T " +
                         "WHERE TRUNC(UPDREC_DATE) = TO_DATE('" + date + "','dd/mm/yyyy') ";
            DataTable dt = conn.ShowData(sql);
            data = dt;
            foreach (DataRow item in dt.Rows)
            {
                i++;
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["KODE_SUPP"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["NAMA_SUPP"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["PLUID"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["DESKRIPSI"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["DIVISI"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["DEPARTEMEN"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["KATEGORI"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["TAG"].ToString();
                dataGridView1.Rows[n].Cells[8].Value = item["QTY"].ToString();
                dataGridView1.Rows[n].Cells[9].Value = item["HPP"].ToString();
                dataGridView1.Rows[n].Cells[10].Value = item["NRBSEPIHAK"].ToString();
                dataGridView1.Rows[n].Cells[11].Value = item["BAPSEPIHAK"].ToString();
            }
            txt_Total.Text = i.ToString();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void btn_Cetak_Click(object sender, EventArgs e)
        {
            Cetak_Report form = new Cetak_Report(data, username);
            form.Show();
        }
    }
}
