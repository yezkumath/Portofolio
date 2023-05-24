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

namespace DCNRBBAPADM
{
    public partial class Login : Form
    {
        private clsUtility activateUt = new clsUtility();
        private clsCon activateCon = new clsCon();
        public bool getSession = false;

        public Login()
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
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName;
            try
            {
                if (txtUsername.Text != "" && txtPassword.Text != "")
                {
                    string tempMsgs = activateCon.ambilDataUser(txtUsername.Text.Replace("'", "").ToUpper().Trim(), txtPassword.Text, activateUt.getProgName);
                    if (tempMsgs == "OK")
                    {

                        getSession = activateCon.getSessionLoggedOn;
                        Properties.Settings.Default.Username = txtUsername.Text.Replace("'", "").Trim();
                        Properties.Settings.Default.Save();
                        userName = txtUsername.Text.Replace("'", "").ToUpper().Trim();
                        MainMenu mm = new MainMenu(userName);
                        mm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show(tempMsgs);
                        txtUsername.Text = "";
                        txtPassword.Text = "";
                        this.txtUsername.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Username and Password is null.");
                    this.txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
    }
}
