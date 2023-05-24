using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCNRBBAPADM
{
    public partial class MainMenu : Form
    {
        string user;

        public MainMenu(string username)
        {
            InitializeComponent();
            user = username;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            MCetakRekapitulasi CC = new MCetakRekapitulasi(user);
            CC.TopLevel = false;
            CC.Dock = DockStyle.Fill;
            this.tabPage1.Controls.Add(CC);
            this.tabPage1.Tag = CC;
            CC.Show();

            MProsesNRB_Partial PP = new MProsesNRB_Partial();
            PP.TopLevel = false;
            PP.Dock = DockStyle.Fill;
            this.tabPage2.Controls.Add(PP);
            this.tabPage2.Tag = PP;
            PP.Show();

            MPenundaanBAP_Administratif VV = new MPenundaanBAP_Administratif();
            VV.TopLevel = false;
            VV.Dock = DockStyle.Fill;
            this.tabPage3.Controls.Add(VV);
            this.tabPage3.Tag = VV;
            VV.Show();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
