using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Threading;
using System.Data;

namespace KirimEmailOtomatis
{
    class clsCon
    {
        

        private void WriteErrorGeneral(string fromSub, string errDetail)
        {
            try
            {
                if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\ErrorLog"))
                {
                    System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\ErrorLog");
                }

                System.IO.StreamWriter sw = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\ErrorLog\ErrGen " + DateTime.Now.ToString("dd-MM-yyyy") + ".Txt", true);
                sw.WriteLine("#Error Tgl : " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                sw.WriteLine("  From Sub : " + InputToString(fromSub, 25));
                sw.WriteLine("  Error Detail : " + InputToString(errDetail, 1000));
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string InputToString(string str, int length)
        {
            try
            {
                string hasil;
                const char Space = ' ';
                string X = new String(Space, length);
                int panjang;
                int Sisa;
                hasil = "";

                if (str == null || str.Trim() == "")
                {
                    hasil = new String(Space, length);
                    return hasil;
                }

                panjang = str.Length;

                if (length > panjang)
                {
                    Sisa = length - panjang;
                    hasil = str + new String(Space, Sisa);
                }
                else if (length < panjang)
                {
                    hasil = str.Substring(0, length);
                }
                else if (length == panjang)
                {
                    hasil = str;
                }

                return hasil;
            }
            catch (Exception)
            {
                throw;
            }
        }

        


        public string execScalarString(string constr, string sql)
        {
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = new OracleConnection(constr);
                cmd = new OracleCommand("", conn);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();

                conn.Open();
                cmd.CommandText = sql;
                return cmd.ExecuteScalar().ToString();

            }
            catch (Exception ex)
            {
                WriteErrorGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public get_date getDate(string constr)
        {
            get_date _date = new get_date();
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = new OracleConnection(constr);
                cmd = new OracleCommand("", conn);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();

                conn.Open();
                cmd.CommandText = "SELECT SYSDATE, " +
                                  "(select TRUNC(SYSDATE)+file_next_run_time " +
                                   "from dc_file_scheduler_t " +
                                   "where file_key = 'NRB_AUTO_EMAIL') AS tgtDate, " +
                                  "(SELECT TO_CHAR(file_last_run_time,'DD/MM/YYYY') " +
                                   "FROM dc_file_scheduler_t " +
                                   "WHERE file_key = 'NRB_AUTO_EMAIL') AS LASTTIME " +
                                  "FROM dual";
                OracleDataReader reader = cmd.ExecuteReader();
                
               while (reader.Read())
                {
                    _date.SysDate = reader.GetDateTime(0);
                    _date.TgtDate = reader.GetDateTime(1);
                    _date.LastTime = reader.GetString(2);
                }
                
            }
            catch (Exception ex)
            {
                WriteErrorGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
            return _date;
        }

        public string CekTime(string constr)
        {
            return execScalarString(constr, "SELECT TO_CHAR(SYSDATE, 'DD/MM/YYYY') " +
                                                    "FROM dual");
        }

        public string getKodeDC(string constr)
        {
            return execScalarString(constr, "SELECT tbl_dc_kode FROM dc_tabel_dc_t");
        }

        public DataTable setEmail(string constr)
        {
            OracleConnection conn = new OracleConnection(constr);
            DataTable dt = new DataTable();
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT b.MAIL_ADDRESS " +
                                                      "FROM DC_AUTO_EMAILHDR_T a " +
                                                      "JOIN DC_AUTO_EMAILDTL_T b " +
                                                      "ON(a.MAIL_HDR_ID = b.MAIL_FK_HDR_ID) " +
                                                      "WHERE a.MAIL_TYPE_ALERT = 'OTP' " +
                                                      "AND a.MAIL_FK_DCKODE = '" + getKodeDC(constr) + "' ", conn);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                ServiceLog.WriteErrorLog(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
            
        }
        public string setUserbyEmail(string userName)
        {
            userName = userName.Replace("@indomaret.co.id", "");
            userName = userName.Replace(".", " ");
            userName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(userName.ToLower());

            return userName;
        }

        public string NRBsepihak(string constr)
        {
            return execScalarString(constr, "SELECT COUNT(*) " +
                                                    "FROM DC_HISTORY_TAG_T a, " +
                                                         "(SELECT stk_fk_pluid AS pluid, NVL(stk_qty_sld_akhir,0)+NVL(stk_out_qty_pick,0) AS STOK " +
                                                         "FROM dc_allstok_akhir_retur_v " +
                                                         "WHERE NVL(stk_qty_sld_akhir,0)+NVL(stk_out_qty_pick,0)>0) b, " +
                                                         "DC_BARANG_T c " +
                                                    "WHERE (his_fk_pluid, his_tgl_tag) IN (SELECT his_fk_pluid, MAX(his_tgl_tag) " +
                                                                                         "FROM DC_HISTORY_TAG_T " +
                                                                                         "GROUP BY his_fk_pluid)" +
                                                    "AND his_tag_new IN ('N','F') " +
                                                    "AND (his_tgl_tag+31) < SYSDATE " +
                                                    "AND a.his_fk_pluid=b.pluid  " +
                                                    "AND a.his_fk_pluid=c.mbr_pluid");
        }

        public string NRBsepihakBAP(string constr)
        {
            return execScalarString(constr, "SELECT COUNT(*) " +
                                                    "FROM DC_TEMPCETAKRETURSUP_T aa," +
                                                         "(SELECT hdr_fk_dcid, hdr_hdr_id, hdr_tgl_doc, hdr_no_doc, hdr_ref_lokasiid, his_fk_pluid, his_qty " +
                                                          "FROM DC_HEADER_TRANSAKSI_T a, DC_HISTORY_TRANSAKSI_T b " +
                                                          "WHERE a.hdr_hdr_id = b.his_hdr_fk_id " +
                                                          "AND a.hdr_type_trans = 'NRB SUPPLIER' " +
                                                          "AND a.hdr_tgl_doc <= SYSDATE) bb, " +
                                                          " DC_BARANG_T ee " +
                                                    "WHERE aa.flag_retur_sepihak = 'Y' " +
                                                    "AND aa.hdr_ref_lokasiid = bb.hdr_ref_lokasiid " +
                                                    "AND bb.his_fk_pluid = ee.mbr_pluid " +
                                                    "AND NOT EXISTS (SELECT 1 " +
                                                                    "FROM DC_NRBHDR_SERAHTERIMA_T cc, DC_NRBDTL_SERAHTERIMA_T dd " +
                                                                    "WHERE cc.ID = dd.id_fk " +
                                                                    "AND dd.hdr_hdr_id = bb.hdr_hdr_id " +
                                                                    "AND dd.status = 'DIAMBIL') " +
                                                    "ORDER BY hdr_hdr_id, his_fk_pluid");
        }

        public bool SetLastUpdate(string constr)
        {
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            bool isSuccess = false;
            try
            {
                conn = new OracleConnection(constr);
                cmd = new OracleCommand("", conn);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();

                conn.Open();

                cmd.CommandText = "UPDATE dc_file_scheduler_t SET FILE_LAST_RUN_TIME = SYSDATE " +
                                  "WHERE FILE_KEY= 'NRB_AUTO_EMAIL'";

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                ServiceLog.WriteErrorLog(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool result(string constr, string result)
        {
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            bool isSuccess = false;
            try
            {
                conn = new OracleConnection(constr);
                cmd = new OracleCommand("", conn);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();

                conn.Open();

                cmd.CommandText = "UPDATE dc_file_scheduler_t SET FILE_LAST_RUN_RESULT = " + result + " " +
                                  "WHERE FILE_KEY= 'NRB_AUTO_EMAIL'";

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                ServiceLog.WriteErrorLog(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
