using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Reporting.WinForms;

namespace DCNRBBAPADM
{
    public partial class Rekapitulasi_NRB : Form
    {
        clsCon conn = new clsCon();
        public string tempBulan, tempTahun, tempNamaSupp, tempKodeNamaSupp, tempKodeDC, tempNamaDC, user;

        public Rekapitulasi_NRB(string bulan, string tahun, string namaSupp, string tampungKodeNamaSupp, string kodedc, string namadc, string username)
        {
            InitializeComponent();
            tempBulan = bulan;
            tempTahun = tahun;
            tempNamaSupp = namaSupp;
            tempKodeNamaSupp = tampungKodeNamaSupp;
            tempKodeDC = kodedc;
            tempNamaDC = namadc;
            user = username;
        }

        private void Rekapitulasi_NRB_Load(object sender, EventArgs e)
        {
            DataTable DT = new DataTable();
            OracleConnection con = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            ReportParameterCollection reportParameters = new ReportParameterCollection();

            try
            {
                con = new OracleConnection(conn.GetConStr());
                cmd = new OracleCommand("", con);

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                if (tempNamaSupp != "ALL")
                {
                    cmd.CommandText = clsSQL.Rekapitulasi_NRB_PerSUP(tempBulan, tempTahun, tempNamaSupp);
                }
                else
                {
                    cmd.CommandText = clsSQL.Rekapitulasi_NRB_ALL(tempBulan, tempTahun);
                    tempKodeNamaSupp = " ";
                }

                cmd.CommandType = CommandType.Text;

                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.Fill(DT);

                if (DT.Rows.Count == 0)
                {
                    MessageBox.Show("Supplier tidak memiliki nrb partial");
                    this.Hide();
                }
                this.reportViewer1.LocalReport.DataSources.Clear();
                reportParameters.Add(new ReportParameter("User", user));
                reportParameters.Add(new ReportParameter("Bulan", tempBulan));
                reportParameters.Add(new ReportParameter("Tahun", tempTahun));
                reportParameters.Add(new ReportParameter("KodeNamaSupp", tempKodeNamaSupp));
                reportParameters.Add(new ReportParameter("NamaSupp", tempNamaSupp));
                reportParameters.Add(new ReportParameter("KodeDC", tempKodeDC));
                reportParameters.Add(new ReportParameter("NamaDC", tempNamaDC));
                this.reportViewer1.LocalReport.SetParameters(reportParameters);
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DT_LaporanNRB", DT));
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
