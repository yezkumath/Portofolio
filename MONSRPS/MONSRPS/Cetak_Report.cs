using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace MONSRPS
{
    public partial class Cetak_Report : Form
    {
        DataTable SRPS;
        string username;
        public Cetak_Report(DataTable data, string user)
        {
            InitializeComponent();
            SRPS = data;
            username = user;
        }

        private void Cetak_Report_Load(object sender, EventArgs e)
        {
            ReportParameterCollection reportParameter = new ReportParameterCollection();
            reportParameter.Add(new ReportParameter("ReportParameter1", username));
            this.reportViewer1.LocalReport.SetParameters(reportParameter);

            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Margins = new System.Drawing.Printing.Margins(1,1,1,1);
            ps.Landscape = true;
            ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
            ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
            

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", SRPS));
            this.reportViewer1.RefreshReport();
            this.reportViewer1.SetPageSettings(ps);
        }
    }
}
