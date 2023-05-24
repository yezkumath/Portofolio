using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace KirimEmailOtomatis
{
    class ServiceLog
    {
        string[] reg = new String[3];
        string Constroracle;
        SettingLibMulti.Class1 NewEncrypt = new SettingLibMulti.Class1();

        public ServiceLog(string varCon)
        {
            try
            {
                reg[0] = NewEncrypt.GetVariabel("UserOrcl", NewEncrypt.GetListKey(Convert.ToInt32(varCon)));
                reg[1] = NewEncrypt.GetVariabel("PasswordOrcl", NewEncrypt.GetListKey(Convert.ToInt32(varCon)));
                reg[2] = NewEncrypt.GetVariabel("ODPOrcl", NewEncrypt.GetListKey(Convert.ToInt32(varCon)));
                this.Constroracle = "Data Source= " + reg[2] + ";User ID=" + reg[0] + ";Password = " + reg[1] + ";";
            }
            catch(Exception ex)
            {
                WriteErrorLog(ex);
            }
        }

        public void Process()
        {
            get_date _date = new get_date();
            clsCon cls = new clsCon();
            _date = cls.getDate(Constroracle);
            if (_date.SysDate > _date.TgtDate)
            {
                WriteErrorLog("date pass");
                if (cls.CekTime(Constroracle) != _date.LastTime)
                {
                    WriteErrorLog("date cek pass because " + cls.CekTime(Constroracle) + "!= " + _date.LastTime);
                    SendEmail();
                }
                else
                {
                    WriteErrorLog("fail because " + cls.CekTime(Constroracle) + " == " + _date.LastTime);
                }
            }
            else
            {
                WriteErrorLog("fail because " + _date.SysDate + " < " + _date.TgtDate);
            }
        }

        
        
        public void SendEmail()
        {

            SmtpClient SmtpServer = new SmtpClient();
            MailMessage msg = new MailMessage();
            var today = DateTime.Now;
            clsCon activateCon = new clsCon();
            bool success = activateCon.SetLastUpdate(Constroracle);
            if(success == true)
            {
                try
                {

                    SmtpServer.Credentials = new System.Net.NetworkCredential("oracle_mail@indomaret.co.id", "oracle_mail");
                    SmtpServer.Host = "192.168.2.240";
                    SmtpServer.Port = 587;
                    msg = new MailMessage
                    {
                        From = new MailAddress("no-Reply_ITSD03@indomaret.co.id", "no-Reply")
                    };
                    
                    foreach (DataRow dataRow in activateCon.setEmail(Constroracle).Rows)
                    {
                        foreach (string item in dataRow.ItemArray)
                        {
                            msg.To.Add(new MailAddress(item));
                            Console.WriteLine(item);
                        }
                    }
                    //msg.To.Add(new MailAddress("purwanto.ali@indomaret.co.id"));
                    //msg.To.Add(new MailAddress("purwanto4li@outlook.com"));
                    msg.Subject = "Notifikasi NRB Sepihak";
                    msg.IsBodyHtml = true;
                    string message = "Kepada <br />DCM/DDCM<br /><br />" +
                                     "Pemberitahuan, adanya informasi mengenailist item yang perlu dibuatkan NRB dan NRB sepihak yang perlu dibuatkan BAP<br /><br />" +
                                     "<table>" +
                                     "<tr>" +
                                     "<td>Item yang perlu dibuatkan <b>NRB sepihak</b> </td>" +
                                     "<td>   </td>" +
                                     "<td>: " + activateCon.NRBsepihak(Constroracle) + " item</td>" +
                                     "</tr>" +
                                     "<tr>" +
                                     "<td>Listing <b>NRB</b> sepihak yang perlu <b>dibuatkan BAP</b> (tanggal NRB sepihak > 7 hari) </td>" +
                                     "<td>   </td>" +
                                     "<td>: " + activateCon.NRBsepihakBAP(Constroracle) + " item</td>" +
                                     "</tr>" +
                                     "</table>" +
                                     "<br />Segera lakukan proses pembuatan NRB sepihak dan atau BAP.<br /><br />" +
                                     "Harap jangan membalas email ini, karena email ini dikirim secara otomatis oleh sistem.<br /><br />" +
                                     "Auto-Message-Program<br />" +
                                     today.ToString("dd/MMM/yyyy") + " " + today.ToString("HH:mm");
                    msg.Body = message;
                    SmtpServer.Send(msg);

                }
                catch (Exception ex)
                {
                    WriteErrorLog(ex.Message);
                }
                finally
                {
                    msg.Dispose();
                    SmtpServer.Dispose();
                }
            }
            else
            {
                WriteErrorLog("update fail");
            }

            
        }

        public static void WriteErrorLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public static void WriteErrorLog(string Message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

    }
}
