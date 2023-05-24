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
    public partial class main_MONSRPS : Form
    {
        string username;
        public main_MONSRPS(string user)
        {
            InitializeComponent();
            username = user;
        }

        private void main_MONSRPS_Load(object sender, EventArgs e)
        {
            Monitoring MM = new Monitoring(username);
            MM.TopLevel = false;
            MM.Dock = DockStyle.Fill;
            this.tabPage1.Controls.Add(MM);
            this.tabPage1.Tag = MM;
            MM.Show();

            EmailGroupDivisi EE = new EmailGroupDivisi(username);
            EE.TopLevel = false;
            EE.Dock = DockStyle.Fill;
            this.tabPage2.Controls.Add(EE);
            this.tabPage2.Tag = EE;
            EE.Show();
        }

        private void main_MONSRPS_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
