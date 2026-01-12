using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;


namespace DoorAccess_1
{
    public partial class AdminLoginForm : Form
    {
        private readonly string cs =
            @"Server=(localdb)\VerbTables;Database=Verbs;Trusted_Connection=True;";

        public AdminLoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (var con = new SqlConnection(cs))
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM dbo.Users WHERE Username=@u AND [Password]=@p AND IsActive=1", con))
                {
                    cmd.Parameters.AddWithValue("@u", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@p", txtPassword.Text);

                    con.Open();
                    int ok = (int)cmd.ExecuteScalar();

                    if (ok != 1)
                    {
                        MessageBox.Show("Hatalı admin kullanıcı/şifre.");
                        return;
                    }
                    //  Session bilgisi
                    Session.CurrentRole = AppRole.Admin;
                    Session.CurrentUser = txtUsername.Text; // admin

                }

                var f = new Form1(AppRole.Admin);
                f.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            btnLogin_Click(sender, e); //  ana login
        }



    }
}

