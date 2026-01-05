using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoorAccess_1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

          #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPersonID = new System.Windows.Forms.TextBox();
            this.txtPersonName = new System.Windows.Forms.TextBox();
            this.dtpAccessTimeDate = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtRecordID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearchRecordID = new System.Windows.Forms.TextBox();
            this.dgvRecords = new System.Windows.Forms.DataGridView();
            this.btnList = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.chkShowDeleted = new System.Windows.Forms.CheckBox();
            this.cmbDoor = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "heyecan yok";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(19, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "PersonID";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "PersonName";
            // 
            // txtPersonID
            // 
            this.txtPersonID.Location = new System.Drawing.Point(103, 74);
            this.txtPersonID.Name = "txtPersonID";
            this.txtPersonID.Size = new System.Drawing.Size(100, 20);
            this.txtPersonID.TabIndex = 3;
            // 
            // txtPersonName
            // 
            this.txtPersonName.Location = new System.Drawing.Point(103, 109);
            this.txtPersonName.Name = "txtPersonName";
            this.txtPersonName.Size = new System.Drawing.Size(100, 20);
            this.txtPersonName.TabIndex = 4;
            // 
            // dtpAccessTimeDate
            // 
            this.dtpAccessTimeDate.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpAccessTimeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAccessTimeDate.Location = new System.Drawing.Point(22, 153);
            this.dtpAccessTimeDate.Name = "dtpAccessTimeDate";
            this.dtpAccessTimeDate.Size = new System.Drawing.Size(200, 20);
            this.dtpAccessTimeDate.TabIndex = 5;
            this.dtpAccessTimeDate.ValueChanged += new System.EventHandler(this.dtpAccessTimeDate_ValueChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(81, 200);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Register";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(309, 111);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtRecordID
            // 
            this.txtRecordID.Location = new System.Drawing.Point(347, 78);
            this.txtRecordID.Name = "txtRecordID";
            this.txtRecordID.ReadOnly = true;
            this.txtRecordID.Size = new System.Drawing.Size(100, 20);
            this.txtRecordID.TabIndex = 8;
            this.txtRecordID.TextChanged += new System.EventHandler(this.txtRecordID_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(268, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "RecordID";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(580, 111);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(514, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Search RecordID";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtSearchRecordID
            // 
            this.txtSearchRecordID.Location = new System.Drawing.Point(624, 82);
            this.txtSearchRecordID.Name = "txtSearchRecordID";
            this.txtSearchRecordID.Size = new System.Drawing.Size(100, 20);
            this.txtSearchRecordID.TabIndex = 12;
            // 
            // dgvRecords
            // 
            this.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecords.Location = new System.Drawing.Point(22, 263);
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.Size = new System.Drawing.Size(636, 158);
            this.dgvRecords.TabIndex = 13;
            this.dgvRecords.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(128, 237);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(75, 23);
            this.btnList.TabIndex = 14;
            this.btnList.Text = "List";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 242);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Click here to List ->";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(448, 191);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // chkShowDeleted
            // 
            this.chkShowDeleted.AutoSize = true;
            this.chkShowDeleted.Location = new System.Drawing.Point(448, 237);
            this.chkShowDeleted.Name = "chkShowDeleted";
            this.chkShowDeleted.Size = new System.Drawing.Size(136, 17);
            this.chkShowDeleted.TabIndex = 17;
            this.chkShowDeleted.Text = "Show Deleted Records";
            this.chkShowDeleted.UseVisualStyleBackColor = true;
            this.chkShowDeleted.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cmbDoor
            // 
            this.cmbDoor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoor.FormattingEnabled = true;
            this.cmbDoor.Location = new System.Drawing.Point(820, 172);
            this.cmbDoor.Name = "cmbDoor";
            this.cmbDoor.Size = new System.Drawing.Size(121, 21);
            this.cmbDoor.TabIndex = 18;
            this.cmbDoor.SelectedIndexChanged += new System.EventHandler(this.cmbDoor_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(784, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Door";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 585);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbDoor);
            this.Controls.Add(this.chkShowDeleted);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.dgvRecords);
            this.Controls.Add(this.txtSearchRecordID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRecordID);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dtpAccessTimeDate);
            this.Controls.Add(this.txtPersonName);
            this.Controls.Add(this.txtPersonID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        



        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPersonID;
        private System.Windows.Forms.TextBox txtPersonName;
        private System.Windows.Forms.DateTimePicker dtpAccessTimeDate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtRecordID;
        private System.Windows.Forms.Label label3;
        private Button btnSearch;
        private Label label4;
        private TextBox txtSearchRecordID;
        private DataGridView dgvRecords;
        private Button btnList;
        private Label label5;
        private Button btnDelete;
        private CheckBox chkShowDeleted;
        private ComboBox cmbDoor;
        private Label label6;
    }
}

