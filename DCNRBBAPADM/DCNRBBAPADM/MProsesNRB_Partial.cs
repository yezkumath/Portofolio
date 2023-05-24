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
    public partial class MProsesNRB_Partial : Form
    {
        clsCon con = new clsCon();
        DataTable data = new DataTable();

        public MProsesNRB_Partial()
        {
            InitializeComponent();
            refresh();
        }

        private void refresh()
        {
            data.Clear();
            txt_BelumProses.Text = con.getBelumProses();
            data = con.ShowData(clsSQL.ProsesNBR_Partial_BelumProses());
            
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            
            DataTable dt = con.ShowData(clsSQL.ProsesNBR_Partial_dataNRB());

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["FLAG_PROSES"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["FLAG_SYNC"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["KODE_DC"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["NO_NRBSEPIHAK"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["THN_NRB"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["PLUID"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["JUM_NRBPARTIAL"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["KETERANGAN"].ToString();
            }

            dt.Clear();
            

            dt = con.ShowData(clsSQL.ProsesNBR_Partial_dataNRB_Partial());
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = item["NO_NRBPARTIAL"].ToString();
                dataGridView2.Rows[n].Cells[1].Value = item["Kuantitas"].ToString();
            }
            dt.Clear();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void Find()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            DataTable dt = con.ShowData(clsSQL.FindProsesNBR_Partial_dataNRB(txt_Find.Text));

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["FLAG_PROSES"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["FLAG_SYNC"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["KODE_DC"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["NO_NRBSEPIHAK"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["THN_NRB"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["PLUID"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["JUM_NRBPARTIAL"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["KETERANGAN"].ToString();
            }

            dt.Clear();
        }

        private void btn_find_Click(object sender, EventArgs e)
        {
            Find();
        }

        private void txt_Find_TextChanged(object sender, EventArgs e)
        {
            Find();
        }

        private void btn_Process_All_Click(object sender, EventArgs e)
        {
            int update = 0, input = 0;

            string no_SRPS;
            foreach (DataRow item in data.Rows)
            {
                no_SRPS = con.getNoSRPS(item["PLUID"].ToString());
                if (con.Update_NRP_Partial(item["PLUID"].ToString(), no_SRPS, item["NO_NRBSEPIHAK"].ToString()))
                    update++;
                if (con.Input_NRP_Partial_Dtl(item["KODE_DC"].ToString(), item["NO_NRBSEPIHAK"].ToString(), item["THN_NRB"].ToString(), item["PLUID"].ToString(), item["JUM_NRBPARTIAL"].ToString()))
                    input++;
            }
            if (update != Int32.Parse(txt_BelumProses.Text) || update != input)
                MessageBox.Show("PERINGATAN \n hanya [" + update + " update dan " + input + " input] dari " + txt_BelumProses.Text + " yang berhasil di proses");
            else
                MessageBox.Show("Sejumlah " + update + "  data telah di proses ");
            refresh();
        }
    }
}
