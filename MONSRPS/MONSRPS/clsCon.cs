using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace MONSRPS
{
    class clsCon
    {
        string[] reg = new String[3];
        string Constroracle;
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

        public bool getSessionLoggedOn
        {
            get { return this.cSessionLoggedOn; }

            set { this.cSessionLoggedOn = value; }
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
                clsCon.WriteErrorGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show("Exception :" + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
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

        public string getKodeDC()
        {
            return execScalarString(Constroracle, "SELECT tbl_dc_kode FROM dc_tabel_dc_t");
        }

        public string getCon()
        {
            return Constroracle;
        }
        
        public DataTable ShowData (string sql)
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
            catch(Exception ex)
            {
                clsCon.WriteErrorGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show("Exception :" + ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public bool Input_Email(string kode_divisiMD, string email, string user)
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
                cmd.CommandText = clsPrev.email_InsertSp(getKodeDC(), kode_divisiMD, email, user);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    result = true;
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
            return result;
        }



        //
    }
}
