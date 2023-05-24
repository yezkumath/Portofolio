using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace MONSRPS
{
    public partial class login_MONSRPS : Form
    {
        private clsUtility activateUt = new clsUtility();
        private clsCon activateCon = new clsCon();
        public bool getSession = false;

        public login_MONSRPS()
        {
            string temp;
            temp = activateCon.Cek_Program(activateCon.getKodeDC(), activateUt.getProgNameFull, activateUt.getProgVer, activateUt.getClientIp);
            InitializeComponent();
            if (temp.Contains("OKE") == false)
            {
                MessageBox.Show(temp);
                Environment.Exit(0);
            }

        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            clsCon conn = new clsCon();
            try
            {
                if(username_txt.Text != "" && password_txt.Text != "")
                {
                    
                    string tempMsgs = activateCon.ambilDataUser(username_txt.Text.Replace("'", "").ToUpper().Trim(), password_txt.Text, activateUt.getProgName);
                    if (tempMsgs == "OK")
                    {
                        Console.WriteLine("2");
                        getSession = activateCon.getSessionLoggedOn;
                        Properties.Settings.Default.Username = username_txt.Text.Replace("'", "").Trim();
                        Properties.Settings.Default.Save();
                        this.Hide();
                        main_MONSRPS a = new main_MONSRPS(username_txt.Text.ToUpper());
                        a.Show();
                        Console.WriteLine("1");
                    }
                }
            }
            catch(Exception ex)
            {
                clsCon.WriteErrorGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show("Exception :" + ex);
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            username_txt.Text = "";
            password_txt.Text = "";
        }


        //
    }
}
