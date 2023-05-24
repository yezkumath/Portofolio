using SD3Fungsi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCNRBBAPADM
{
    class clsUtility
    {
        private string clientIP;
        private string progName;
        private string progVer;

        public clsUtility()
        {
            clientIP = IpKomp.GetIPAddress();
            progName = Application.ProductName;
            progVer = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public string getProgVer
        {
            get { return progVer; }

            set { progVer = value; }
        }

        public string getProgNameFull
        {
            get { return progName + ".EXE"; }

            set { progName = value; }
        }

        public string getProgName
        {
            get { return progName; }

            set { progName = value; }
        }

        public string getClientIp
        {
            get { return clientIP; }

            set { clientIP = value; }
        }

        public bool AlreadyRunning()
        {
            try
            {
                Process my_proc = Process.GetCurrentProcess();
                string my_name = my_proc.ProcessName;

                Process[] procs = Process.GetProcessesByName(my_name);

                if (procs.Length == 1)
                    return false;

                int i;
                for (i = 0; i < procs.Length; i++)
                {
                    if (procs[i].StartTime < my_proc.StartTime)
                    {
                        MessageBox.Show("Another instance is already running.", "Already Running", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Access Denied")
                {
                    MessageBox.Show("Another instance is already running.", "Already Running", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
