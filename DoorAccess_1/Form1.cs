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
    public partial class Form1 : Form

    {
        string connectionString =
            @"Server=(localdb)\VerbTables;Database=Verbs;Trusted_Connection=True;";



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            LoadDoors();

        }

        private void dtpAccessTimeDate_ValueChanged(object sender, EventArgs e)
        {

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbDoor.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a door first.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand who = new SqlCommand("SELECT DB_NAME() AS Db, @@SERVERNAME AS Srv", con))
                using (SqlDataReader r = who.ExecuteReader())
                {
                    if (r.Read())
                        MessageBox.Show($"DB={r["Db"]}\nServer={r["Srv"]}");
                }
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                /*Burada isDeleted toptan silme yerine softdelete yapiyo sonra kaydi siliyor ama ayni zamanada arkada tutuyor, yani kaybolmuyoo */
                string query = @"
INSERT INTO dbo.Table_1 (PersonID, PersonName, AccesTimeDate, DoorId, IsDeleted, CreatedDate, UpdatedDate)
VALUES (@PersonID, @PersonName, @AccessTime, @DoorId, 0, GETDATE(), NULL)";



                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PersonID", txtPersonID.Text);
                cmd.Parameters.AddWithValue("@PersonName", txtPersonName.Text);
                cmd.Parameters.AddWithValue("@AccessTime", dtpAccessTimeDate.Value);
                object doorIdValue = DBNull.Value;
                if (cmbDoor.SelectedIndex != -1)
                    doorIdValue = cmbDoor.SelectedValue;

                cmd.Parameters.AddWithValue("@DoorId", doorIdValue);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Record added successfully.");
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                UPDATE dbo.Table_1
                SET PersonID = @PersonID,
                    PersonName = @PersonName,
                    AccesTimeDate = @AccessTime,
                    UpdatedDate = GETDATE()
                WHERE RecordID = @RecordID";


                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@PersonID", txtPersonID.Text);
                cmd.Parameters.AddWithValue("@PersonName", txtPersonName.Text);
                cmd.Parameters.AddWithValue("@AccessTime", dtpAccessTimeDate.Value);
                cmd.Parameters.AddWithValue("@RecordID", txtRecordID.Text);

                con.Open();
                int affected = cmd.ExecuteNonQuery();
                con.Close();

                if (affected > 0)
                    MessageBox.Show("Record updated.");
                else
                    MessageBox.Show("RecordID not found.");
            }
        }





        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void txtRecordID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT RecordID, PersonID, PersonName, AccesTimeDate
        FROM dbo.Table_1
       WHERE RecordID = @RecordID AND IsDeleted = 0";


                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@RecordID", txtSearchRecordID.Text);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtRecordID.Text = reader["RecordID"].ToString();
                    txtPersonID.Text = reader["PersonID"].ToString();
                    txtPersonName.Text = reader["PersonName"].ToString();
                    dtpAccessTimeDate.Value =
                        Convert.ToDateTime(reader["AccesTimeDate"]);
                }
                else
                {
                    MessageBox.Show("No record found. ");
                }

                con.Close();
            }
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnList_Click(object sender, EventArgs e)
        {
            string query;

            if (chkShowDeleted.Checked)
            {
                query = @"
        SELECT 
            t.RecordID, t.PersonID, t.PersonName, t.AccesTimeDate,
            d.DoorName,
            t.IsDeleted, t.CreatedDate, t.UpdatedDate
        FROM dbo.Table_1 t
        LEFT JOIN dbo.Door d ON d.DoorId = t.DoorId
        ORDER BY t.AccesTimeDate DESC";

            }
            else
            {
                        query = @"
        SELECT 
            t.RecordID, t.PersonID, t.PersonName, t.AccesTimeDate,
            d.DoorName,
            t.IsDeleted, t.CreatedDate, t.UpdatedDate 
        FROM dbo.Table_1 t
        LEFT JOIN dbo.Door d ON d.DoorId = t.DoorId
        WHERE t.IsDeleted = 0
        ORDER BY t.AccesTimeDate DESC"; 
               

            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvRecords.DataSource = dt;
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRecordID.Text))
            {
                MessageBox.Show("Please choose ID to delete.(Search RecordID kismina bir id yaz )");
                return;
            }

            DialogResult result = MessageBox.Show(
                "This record will be deleted. Are you sure lad?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;
            DialogResult result2 = MessageBox.Show(
    "Bu sefer de turkce soruyorum emin misin?",
    "Son Onay",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Error
);

            if (result2 != DialogResult.Yes)
                return;
            DialogResult result3 = MessageBox.Show(
    "Bu kayıt pasif hale alınacak (Soft Delete). Devam edilsin mi?",

    "KESİN SİLME",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Stop
);

            if (result3 != DialogResult.Yes)
                return;



            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                UPDATE dbo.Table_1
                SET IsDeleted = 1,
                    UpdatedDate = GETDATE()
                WHERE RecordID = @RecordID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@RecordID", txtRecordID.Text);

                con.Open();
                int affected = cmd.ExecuteNonQuery();
                con.Close();

                if (affected > 0)
                {
                    MessageBox.Show("Record Deleted.(Silindi, bu saatten sonra geri donus yok)");


                    txtRecordID.Clear();
                    txtPersonID.Clear();
                    txtPersonName.Clear();
                }
                else
                {
                    MessageBox.Show("Record not found.");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cmbDoor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadDoors()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT DoorId, DoorName FROM dbo.Door ORDER BY DoorName";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbDoor.DisplayMember = "DoorName"; // -------kullanıcı bunu goruyor-------------
                cmbDoor.ValueMember = "DoorId";     
                cmbDoor.DataSource = dt;

                cmbDoor.SelectedIndex = -1; 
            }
        }

    }
}
