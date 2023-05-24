using Oracle.ManagedDataAccess.Client;
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
    public partial class PreviewUpload : Form
    {
        DataTable dt;
        string username;

        public PreviewUpload(DataTable data, string form, string user)
        {
            InitializeComponent();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dt = data;
            txtChage.Text = form;
            username = user;

            if (dt != null && dt.Rows.ToString() != String.Empty)
            {
                dataGridView1.DataSource = dt;
            }
            else
                MessageBox.Show("Tidak terdapat data","Data is Null",MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            clsCon con = new clsCon();
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            int count = dt.Rows.Count; // hitung jumlah row atau data
            int insert = 0, update = 0, delete = 0;
            try
            {
                conn = new OracleConnection(con.getCon());
                cmd = new OracleCommand("", conn);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
               // dataGridView1.Columns["RECID"].DefaultCellStyle.NullValue = "0";
                conn.Open();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (txtChage.Text == "Group Divisi")
                    {
                        
                        if (row.Cells[1].Value != DBNull.Value)
                        {
                            int cekHapus = 0;
                            if (row.Cells[0].Value != DBNull.Value)
                                cekHapus = Convert.ToInt32(row.Cells[0].Value);

                            if (cekHapus == 1) // delete
                            {
                                cmd.CommandText = clsPrev.group_Delete(row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value);
                                int i = cmd.ExecuteNonQuery();
                                delete += i;
                            }
                            else // update
                            {
                                int cekRange = Convert.ToInt32(row.Cells[3].Value);
                                if (cekRange > -1 && cekRange < 1000) // if out of range kode MD
                                {
                                    cmd.CommandText = clsPrev.group_Update(con.getKodeDC(), row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, username);
                                    int j = cmd.ExecuteNonQuery(); // 0 = gagal || 1 = berhasil
                                    if (j < 1) // update gagal -> insert
                                    {
                                        cmd.CommandText = clsPrev.group_Insert(con.getKodeDC(), row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, username);
                                        int k = cmd.ExecuteNonQuery();
                                        insert += k;
                                    }
                                    else // update success
                                        update += j;
                                }
                                else
                                {
                                    MessageBox.Show("Kode Divisi MD (" + row.Cells[3].Value + ") tidak di input/edit karena berada diluar range yang di izinkan", "Out of Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    
                                }
                            }
                        }
                    }
                    else if (txtChage.Text == "Email Group Divisi")
                    {
                        if (row.Cells[1].Value != DBNull.Value)
                        {
                            int cekHapus = 0;
                            if (row.Cells[0].Value != DBNull.Value)
                                cekHapus = Convert.ToInt32(row.Cells[0].Value);

                            if (cekHapus == 1) // delete
                            {
                                cmd.CommandText = clsPrev.email_Delete(row.Cells[1].Value, row.Cells[2].Value);
                                int i = cmd.ExecuteNonQuery();
                                delete += i;
                            }
                            else // update
                            {
                                object kode_divisiMD;
                                if(row.Cells[1].Value.ToString() == "A01" || row.Cells[1].Value.ToString() == "B01") // Insert dan Update Special karakter
                                {
                                    //Tidak Menggunakan Update
                                    //cmd.CommandText = clsPrev.email_UpdateSp(con.getKodeDC(), row.Cells[1].Value, row.Cells[2].Value, username);
                                    //int j = cmd.ExecuteNonQuery(); // 0 = gagal || 1 = berhasil
                                    //if (j < 1) // update gagal -> insert
                                    //{
                                        cmd.CommandText = clsPrev.email_InsertSp(con.getKodeDC(), row.Cells[1].Value, row.Cells[2].Value, username);
                                        int k = cmd.ExecuteNonQuery();
                                        insert += k;
                                    //}
                                    //else // update success
                                    //    update += j;

                                }
                                else // Insert dan Update Normal
                                {
                                    int cekRange = Convert.ToInt32(row.Cells[1].Value);
                                    if (cekRange > -1 && cekRange < 1000) // if out of range kode MD
                                    {
                                        cmd.CommandText = clsPrev.email_Update(con.getKodeDC(), row.Cells[1].Value, row.Cells[2].Value, username);
                                        int j = cmd.ExecuteNonQuery(); // 0 = gagal || 1 = berhasil
                                        if (j < 1) // update gagal -> insert
                                        {
                                            cmd.CommandText = clsPrev.email_Insert(con.getKodeDC(), row.Cells[1].Value, row.Cells[2].Value, username);
                                            int k = cmd.ExecuteNonQuery();
                                            insert += k;
                                        }
                                        else // update success
                                            update += j;
                                    }
                                    else
                                        MessageBox.Show("Kode Divisi MD (" + row.Cells[1].Value + ") tidak di input/edit karena berada diluar range yang di izinkan", "Out of Range", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsCon.WriteErrorGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show("Exception :" + ex);
            }
            finally
            {
                conn.Close();
            }
            MessageBox.Show("DATA yang\n Input baru\t: "+insert+"\n Berubah\t\t: "+update+"\n Terhapus\t: "+delete,"Upload Report",MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
