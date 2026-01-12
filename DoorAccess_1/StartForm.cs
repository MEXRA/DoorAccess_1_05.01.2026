using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DoorAccess_1
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            Session.CurrentRole = AppRole.User;
            Session.CurrentUser = "User";

            var f = new Form1(AppRole.User);
            f.Show();
            this.Hide();
        }



        private void btnAdmin_Click(object sender, EventArgs e)
        {
            // Admin → AdminLoginForm
            var f = new AdminLoginForm();
            f.Show();
            this.Hide();
        }

        
    }
}

