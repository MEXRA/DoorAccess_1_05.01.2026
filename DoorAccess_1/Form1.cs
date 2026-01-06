using System;
using System.Windows.Forms;
using DoorAccess_1.Business;
using DoorAccess_1.DataAccess;
using DoorAccess_1.Models;

namespace DoorAccess_1
{
    public partial class Form1 : Form
    {
        // UI tarafında connectionString sadece bir kere set edilecek
        private readonly string connectionString =
            @"Server=(localdb)\VerbTables;Database=Verbs;Trusted_Connection=True;";

        // UI sadece service çağırır
        private readonly AccessService _service = new AccessService();

        public Form1()
        {
            InitializeComponent();

            // DAL connection string'i burada ayarlıyoruz (Form içinde SQL YOK)
            Db.ConnectionString = connectionString;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDoors();
        }

        private void LoadDoors()
        {
            try
            {
                var doors = _service.GetDoors();

                cmbDoor.DisplayMember = "DoorName";
                cmbDoor.ValueMember = "DoorId";
                cmbDoor.DataSource = doors;

                cmbDoor.SelectedIndex = -1; // başta seçili olmasın
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doors yüklenemedi: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var rec = new AccessRecord
                {
                    PersonID = txtPersonID.Text,
                    PersonName = txtPersonName.Text,
                    AccesTimeDate = dtpAccessTimeDate.Value,
                    DoorId = cmbDoor.SelectedIndex == -1 ? (int?)null : (int)cmbDoor.SelectedValue
                };

                _service.AddRecord(rec);
                MessageBox.Show("Record added successfully.");

                // istersen listeyi otomatik yenile
                // RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSearchRecordID.Text, out int id))
            {
                MessageBox.Show("Search RecordID sayı olmalı.");
                return;
            }

            try
            {
                var rec = _service.SearchActiveById(id);

                if (rec == null)
                {
                    MessageBox.Show("No record found. ❌");
                    return;
                }

                txtRecordID.Text = rec.RecordID.ToString();
                txtPersonID.Text = rec.PersonID;
                txtPersonName.Text = rec.PersonName;
                dtpAccessTimeDate.Value = rec.AccesTimeDate;

                if (rec.DoorId.HasValue)
                    cmbDoor.SelectedValue = rec.DoorId.Value;
                else
                    cmbDoor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtRecordID.Text, out int recordId))
            {
                MessageBox.Show("Önce bir kayıt bul (Search) sonra Update yap.");
                return;
            }

            try
            {
                var rec = new AccessRecord
                {
                    RecordID = recordId,
                    PersonID = txtPersonID.Text,
                    PersonName = txtPersonName.Text,
                    AccesTimeDate = dtpAccessTimeDate.Value,
                    DoorId = cmbDoor.SelectedIndex == -1 ? (int?)null : (int)cmbDoor.SelectedValue
                };

                bool ok = _service.UpdateRecord(rec);
                MessageBox.Show(ok ? "Record updated." : "RecordID not found.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update error: " + ex.Message);
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            try
            {
                var rows = _service.ListRecords(chkShowDeleted.Checked);
                dgvRecords.DataSource = rows;
            }
            catch (Exception ex)
            {
                MessageBox.Show("List error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtRecordID.Text, out int recordId))
            {
                MessageBox.Show("Silmek için önce RecordID seç/ara.");
                return;
            }

            var result = MessageBox.Show(
                "Bu kayıt Soft Delete olacak. Emin misin?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                bool ok = _service.DeleteSoft(recordId);

                if (ok)
                {
                    MessageBox.Show("Record deleted (soft delete).");

                    txtRecordID.Clear();
                    txtPersonID.Clear();
                    txtPersonName.Clear();
                    cmbDoor.SelectedIndex = -1;

                    RefreshList();
                }
                else
                {
                    MessageBox.Show("Record not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete error: " + ex.Message);
            }
        }

        // Bunlar designer’ın bağladığı eventler — boş kalsa da sorun yok
        private void dtpAccessTimeDate_ValueChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void txtRecordID_TextChanged(object sender, EventArgs e) { }
        private void cmbDoor_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
