using System;
using System.ComponentModel;
using System.Windows.Forms;
using DoorAccess_1.Business;
using DoorAccess_1.DataAccess;
using DoorAccess_1.Models;
using System.Diagnostics;



namespace DoorAccess_1
{

    public partial class Form1 : Form
    {

        private readonly string connectionString =
            @"Server=(localdb)\VerbTables;Database=Verbs;Trusted_Connection=True;";

        private AccessService _service;
        private AppRole _role;


        private bool IsDesignTime =>
    LicenseManager.UsageMode == LicenseUsageMode.Designtime
    || (this.Site?.DesignMode ?? false)
    || Process.GetCurrentProcess().ProcessName.Equals("devenv", StringComparison.OrdinalIgnoreCase);


       
        public Form1()
        {
            InitializeComponent();

            if (IsDesignTime) return;

            _role = AppRole.User;
           
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            if (IsDesignTime) return;

            Db.ConnectionString = connectionString;
            _service = new AccessService();

            ApplyRole();
            LoadDoors();
        }


        //  Runtime
        public Form1(AppRole role) : this()
        {
            if (IsDesignTime) return;
            _role = role;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (IsDesignTime) return;
           
        }


        private void ApplyRole()
        {
            bool isAdmin = _role == AppRole.Admin;

           

            // User da kayıt ekleyebilsin
            btnAdd.Enabled = true;

            // Sadece admin 
            btnList.Enabled = isAdmin;
            btnSearch.Enabled = isAdmin;
            btnUpdate.Enabled = isAdmin;
            btnDelete.Enabled = isAdmin;
            chkShowDeleted.Enabled = isAdmin;

            // admin alanlarını gizle
            if (!isAdmin)
            {
                btnList.Visible = false;
                btnSearch.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                chkShowDeleted.Visible = false;

                txtSearchRecordID.Visible = false;
                dgvRecords.Visible = false;

                // sende label isimleri label4/label5 ise bunlar çalışır.
                if (label4 != null) label4.Visible = false;
                if (label5 != null) label5.Visible = false;
            }
        }

        private void LoadDoors()
        {
            try
            {
                var doors = _service.GetDoors();

                cmbDoor.DisplayMember = "DoorName";
                cmbDoor.ValueMember = "DoorId";
                cmbDoor.DataSource = doors;
                cmbDoor.SelectedIndex = -1;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add error: " + ex.Message);
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
                    MessageBox.Show("No record found.");
                    return;
                }

                txtRecordID.Text = rec.RecordID.ToString();
                txtPersonID.Text = rec.PersonID;
                txtPersonName.Text = rec.PersonName;
                dtpAccessTimeDate.Value = rec.AccesTimeDate;

                if (rec.DoorId.HasValue) cmbDoor.SelectedValue = rec.DoorId.Value;
                else cmbDoor.SelectedIndex = -1;
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
                MessageBox.Show("Önce Search ile kayıt bul, sonra Update.");
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
                if (!ok)
                {
                    MessageBox.Show("Record not found.");
                    return;
                }

                MessageBox.Show("Record deleted (soft delete).");

                txtRecordID.Clear();
                txtPersonID.Clear();
                txtPersonName.Clear();
                cmbDoor.SelectedIndex = -1;

                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete error: " + ex.Message);
            }
        }

        // Designer eventleri (boş olabilir)
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
