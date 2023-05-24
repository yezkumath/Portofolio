using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MONSRPS
{
    public partial class EmailGroupDivisi : Form
    {
        string username;
        clsCon conn = new clsCon();
        public EmailGroupDivisi(string user)
        {
            InitializeComponent();
            username = user;

            refresh();
        }

        public void refresh()
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            DataTable G_Divisi = conn.ShowData("SELECT DIVISI, DEPARTEMEN, KODE_DIVISIMD FROM DC_SRPS_DIV_T");
            foreach (DataRow item in G_Divisi.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["DIVISI"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["DEPARTEMEN"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["KODE_DIVISIMD"].ToString();

            }
            DataTable EG_Divisi = conn.ShowData("SELECT KODE_DIVISIMD, EMAIL FROM DC_SRPS_EMAIL_T");
            foreach (DataRow item in EG_Divisi.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = item["KODE_DIVISIMD"].ToString();
                dataGridView2.Rows[n].Cells[1].Value = item["EMAIL"].ToString();
            }
        }

        public static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                if (csv_file_path.EndsWith(".csv"))
                {
                    using (Microsoft.VisualBasic.FileIO.TextFieldParser csvReader = new Microsoft.VisualBasic.FileIO.TextFieldParser(csv_file_path))
                    {
                        csvReader.SetDelimiters(new string[] { "," }); // bisa pakai ; atau , tergantung file .csv nya
                        csvReader.HasFieldsEnclosedInQuotes = true;
                        //read column
                        string[] colFields = csvReader.ReadFields();
                        foreach (string column in colFields)
                        {
                            DataColumn datecolumn = new DataColumn(column);
                            datecolumn.AllowDBNull = true;
                            csvData.Columns.Add(datecolumn);
                        }
                        while (!csvReader.EndOfData)
                        {
                            string[] fieldData = csvReader.ReadFields();
                            for (int i = 0; i < fieldData.Length; i++)
                            {
                                if (fieldData[i] == "")
                                {
                                    fieldData[i] = null;
                                }
                            }
                            csvData.Rows.Add(fieldData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception " + ex);
            }
            return csvData;
        }

        public static void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        private void PrevClose(object sender, FormClosedEventArgs e)
        {
            refresh();
        }

        //-----------------Group Divisi-------------------------

        private void UploadGroup_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            int ImportedRecord = 0, inValidItem = 0;
            string SourceURl = "";

            if (dialog.FileName != "")
            {
                if (dialog.FileName.EndsWith(".csv"))
                {
                    DataTable dt = new DataTable();
                    dt = GetDataTabletFromCSVFile(dialog.FileName);
                    txt_DirGroupDivisi.Text = "UPLOAD FROM : " + dialog.FileName;
                    PreviewUpload Prev = new PreviewUpload(dt, "Group Divisi", username);
                    Prev.Show();
                    Prev.FormClosed += new FormClosedEventHandler(PrevClose);
                }
                else
                {
                    MessageBox.Show("Selected File Invalid, Pilih File dengan format (.CSV)", "PILIH FILE .CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DownloadGroup_btn_Click(object sender, EventArgs e)
        {
            string sql = "select '' as RECID, divisi as DIV, departemen as DEP, kode_divisimd as KODEDIVMD from dc_srps_div_t";
            DataTable dt = conn.ShowData(sql);
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                ToCSV(dt, dialog.FileName);
                txt_DirGroupDivisi.Text = "DOWNLOAD TO : " + dialog.FileName;
                MessageBox.Show("File telah di download");
            }
            
        }
        
        private void txt_GroupDivisiProduk_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            DataTable G_Divisi = conn.ShowData(clsPrev.group_Search(txt_GroupDivisiProduk.Text));
            foreach (DataRow item in G_Divisi.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["DIVISI"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["DEPARTEMEN"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["KODE_DIVISIMD"].ToString();

            }
        }

        //-----------------Email GroupDivisi-------------------------
        private void UploadMail_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            int ImportedRecord = 0, inValidItem = 0;
            string SourceURl = "";

            if (dialog.FileName != "")
            {
                if (dialog.FileName.EndsWith(".csv"))
                {
                    DataTable dt = new DataTable();
                    dt = GetDataTabletFromCSVFile(dialog.FileName);
                    txt_DirEmail.Text = "UPLOAD FROM : " + dialog.FileName;
                    PreviewUpload Prev = new PreviewUpload(dt, "Email Group Divisi", username);
                    Prev.Show();
                    Prev.FormClosed += new FormClosedEventHandler(PrevClose);
                }
                else
                {
                    MessageBox.Show("Selected File Invalid, Pilih File dengan format (.CSV)", "PILIH FILE .CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DownloadMail_btn_Click(object sender, EventArgs e)
        {
            string sql = "select '' as RECID, KODE_DIVISIMD as KODEDIVMD, EMAIL from DC_SRPS_EMAIL_T";
            DataTable dt = conn.ShowData(sql);
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                ToCSV(dt, dialog.FileName);
                txt_DirEmail.Text = "DOWNLOAD TO : " + dialog.FileName;
                MessageBox.Show("File telah di download");
            }
        }

        private void txt_SearchGroupEmail_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            DataTable G_Divisi = conn.ShowData(clsPrev.email_Search(txt_SearchGroupEmail.Text));
            foreach (DataRow item in G_Divisi.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = item["KODE_DIVISIMD"].ToString();
                dataGridView2.Rows[n].Cells[1].Value = item["EMAIL"].ToString();
            }
        }

        private void txt_EmailLAM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                if (conn.Input_Email("A01", txt_EmailLAM.Text, username))
                    MessageBox.Show("Email berhasil di Input");
                else
                    MessageBox.Show("Email gagal di Input");
                txt_EmailLAM.Text = "";
                refresh();
            }
        }

        private void txtEmailLM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
               if(conn.Input_Email("B01", txtEmailLM.Text, username))
                    MessageBox.Show("Email berhasil di Input");
                else
                    MessageBox.Show("Email gagal di Input");
                txtEmailLM.Text = "";
                refresh();
            }
        }







        //
    }
}
