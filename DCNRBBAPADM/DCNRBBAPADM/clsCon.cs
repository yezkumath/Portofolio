using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DCNRBBAPADM
{
    class clsCon
    {
        string Constroracle;
        string[] reg = new String[3];
        private SettingLib.Class1 NewEncrypt = new SettingLib.Class1();

        private bool cSessionLoggedOn;

        public clsCon()
        {
            try
            {
                reg[0] = NewEncrypt.GetVariabel("UserOrcl");
                reg[1] = NewEncrypt.GetVariabel("PasswordOrcl");
                reg[2] = NewEncrypt.GetVariabel("ODPOrcl");
                this.Constroracle = "Data Source= " + reg[2] + ";User ID=" + reg[0] + ";Password = " + reg[1] + ";";
            }
            catch (Exception ex)
            {
                WriteErrorGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public string GetConStr()
        {
            return Constroracle;
        }

        public static void WriteErrorGeneral(string fromSub, string errDetail)
        {
            try
            {
                if (!System.IO.Directory.Exists(Application.StartupPath + @"\ErrorLog"))
                {
                    System.IO.Directory.CreateDirectory(Application.StartupPath + @"\ErrorLog");
                }

                System.IO.StreamWriter sw = new System.IO.StreamWriter(Application.StartupPath + @"\ErrorLog\ErrGen " + DateTime.Now.ToString("dd-MM-yyyy") + ".Txt", true);
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

        private static string InputToString(string str, int length)
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
                MessageBox.Show("Exception :" + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public string Cek_Program(string KodeDc, string ProgName, string ProgVer, string ClientIP)
        {
            try
            {
                string Ret;
                //Ret = NewEncrypt.GetVersi(getConStrOLE(), KodeDc, ProgName, ProgVer, ClientIP);
                Ret = NewEncrypt.GetVersiODP(Constroracle, KodeDc, ProgName, ProgVer, ClientIP);

                return Ret;

            }
            catch (Exception ex)
            {
                WriteErrorGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return ex.Message;
            }
        }

        public bool getSessionLoggedOn
        {
            get { return this.cSessionLoggedOn; }

            set { this.cSessionLoggedOn = value; }
        }

        public string ambilDataUser(string tangkapUsername, string tangkapPassword, string tangkapExe, bool isDC = true)
        {
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();

            try
            {
                conn = new OracleConnection(Constroracle);
                cmd = new OracleCommand("", conn);
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }

                conn.Open();
                if (isDC)
                {
                    cmd.CommandText = "SELECT COUNT(*) as Ketemu FROM dc_user_t c " +
                                      " WHERE " +
                                      " UPPER(USER_NAME) = '" + tangkapUsername + "' " +
                                      " AND USER_PASSWORD = '" + tangkapPassword + "' " +
                                      " AND USER_PRIVS = 'Y' " +
                                      " AND EXISTS " +
                                      " ( " +
                                      " SELECT IP_FK_USER_NAME FROM DC_SECURITY_T a, DC_IPADDRESS_T b " +
                                      " WHERE a.FK_USER_NAME = b.IP_FK_USER_NAME " +
                                      " AND c.USER_NAME = a.FK_USER_NAME " +
                                      " AND a.SEC_APP_NAME = '" + tangkapExe + "' " +
                                      " )";
                }
                else
                {
                    cmd.CommandText = "SELECT COUNT(*) as Ketemu FROM ic_user_t c " +
                                      " WHERE " +
                                      " UPPER(USER_NAME) = '" + tangkapUsername + "' " +
                                      " AND USER_PASSWORD = '" + tangkapPassword + "' " +
                                      " AND USER_PRIVS = 'Y' " +
                                      " AND EXISTS " +
                                      " ( " +
                                      " SELECT IP_FK_USER_NAME FROM DC_SECURITY_T a, DC_IPADDRESS_T b " +
                                      " WHERE a.FK_USER_NAME = b.IP_FK_USER_NAME " +
                                      " AND c.USER_NAME = a.FK_USER_NAME " +
                                      " AND a.SEC_APP_NAME = '" + tangkapExe + "' " +
                                      " )";
                }

                int results = Convert.ToInt32(cmd.ExecuteScalar());
                Console.WriteLine(results);
                if (results > 0)
                {
                    cSessionLoggedOn = true;
                    return "OK";
                }
                else
                {

                    cSessionLoggedOn = false;
                    return "Data akun tidak ditemukan.";
                }

            }
            catch (Exception ex)
            {
                WriteErrorGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                cSessionLoggedOn = false;
                return ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable ShowData(string sql)
        {
            DataTable dt = new DataTable();
            OracleConnection conn = new OracleConnection();
            conn = new OracleConnection(Constroracle);
            OracleCommand cmd = new OracleCommand();
            cmd = new OracleCommand("", conn);

            try
            {
                conn.Open();
                cmd.CommandText = sql;
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                WriteErrorGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show("Exception :" + ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public string getBelumProses()
        {
            return execScalarString(Constroracle, "SELECT COUNT(*)-COUNT(FLAG_PROSES) FROM DC_NRBPARTIAL_T");
        }

        public string getNoSRPS(string pluid)
        {
            return execScalarString(Constroracle, "SELECT NO_SRPS FROM DC_SRPS_T WHERE PLUID = '" + pluid + "'");
        }

        public bool Input_NRP_Partial_Dtl(string KODE_DC, string NO_NRBSEPIHAK, string THN_NRB, string PLUID, string KUANTITAS)
        {
            bool result = false;
            DataTable dt = new DataTable();
            OracleConnection conn = new OracleConnection();
            conn = new OracleConnection(Constroracle);
            OracleCommand cmd = new OracleCommand();
            cmd = new OracleCommand("", conn);
            try
            {
                conn.Open();
                cmd.CommandText = "INSERT INTO DC_NRBPARTIAL_DTL_T (KODE_DC, NO_NRBSEPIHAK, THN_NRB, PLUID, TANGGAL, KUANTITAS) VALUES ('" + KODE_DC + "', " + NO_NRBSEPIHAK + ", " + THN_NRB + ", " + PLUID + ", sysdate, " + KUANTITAS + ") ";
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                WriteErrorGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show("Exception :" + ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public bool Update_NRP_Partial(string pluid, string no_SRPS, string No_NRBSEPIHAK)
        {
            bool result = false;
            DataTable dt = new DataTable();
            OracleConnection conn = new OracleConnection();
            conn = new OracleConnection(Constroracle);
            OracleCommand cmd = new OracleCommand();
            cmd = new OracleCommand("", conn);
            try
            {
                conn.Open();
                cmd.CommandText = "UPDATE DC_NRBPARTIAL_T SET NO_SRPS = '" + no_SRPS + "', FLAG_PROSES = 'P' WHERE PLUID = '" + pluid + "' AND NO_NRBSEPIHAK = '" + No_NRBSEPIHAK + "' ";
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                WriteErrorGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show("Exception :" + ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public string getKodeDC()
        {
            return execScalarString(Constroracle, "SELECT tbl_dc_kode FROM dc_tabel_dc_t");
        }

        public string getNamaDC()
        {
            return execScalarString(Constroracle, "SELECT tbl_dc_nama FROM dc_tabel_dc_t");
        }

        public string getYear()
        {
            return execScalarString(Constroracle, "SELECT TO_CHAR(SYSDATE,'YYYY') FROM dual");
        }

        public string getMonth()
        {
            return execScalarString(Constroracle, "SELECT TO_CHAR(SYSDATE,'MM') FROM dual");
        }

        public string getNamaSupp(string kodesup)
        {
            return execScalarString(Constroracle, "SELECT SUP_NAMA FROM DC_SUPPLIER_T a, DC_SUPPLIER_DC_T b WHERE a.SUP_SUPID = b.SUP_FK_SUPID AND b.SUP_SUPKODE = '" + kodesup + "'");
        }

        public DataTable getKodeSupp()
        {
            DataTable dt = new DataTable();
            OracleConnection con = new OracleConnection();
            OracleCommand cmd = new OracleCommand();

            try
            {
                con = new OracleConnection(Constroracle);
                cmd = new OracleCommand("", con);

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                cmd.CommandText = "SELECT SUP_SUPKODE FROM DC_SUPPLIER_DC_T";

                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return dt;
        }
        
    }
}
