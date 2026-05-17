namespace DocAtHome
{
    partial class AdminBookNurseForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.ComboBox cmbPatients;
        private System.Windows.Forms.Label lblNurse;
        private System.Windows.Forms.ComboBox cmbNurses;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblService;
        private System.Windows.Forms.ComboBox cmbService;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblPatient = new System.Windows.Forms.Label();
            this.cmbPatients = new System.Windows.Forms.ComboBox();
            this.lblNurse = new System.Windows.Forms.Label();
            this.cmbNurses = new System.Windows.Forms.ComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblService = new System.Windows.Forms.Label();
            this.cmbService = new System.Windows.Forms.ComboBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Size = new System.Drawing.Size(480, 70);
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Header_MouseDown);
            // 
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Bold", 13F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 22);
            this.lblTitle.Text = "New Nurse Booking";
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClose.Location = new System.Drawing.Point(435, 18);
            this.btnClose.Size = new System.Drawing.Size(30, 30);
            this.btnClose.Text = "×";
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // Select Patient
            //
            this.lblPatient.Text      = "SELECT PATIENT";
            this.lblPatient.Font      = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.lblPatient.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblPatient.Location  = new System.Drawing.Point(40, 90);
            this.lblPatient.AutoSize  = true;
            this.cmbPatients.Font        = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPatients.Location    = new System.Drawing.Point(40, 110);
            this.cmbPatients.Size        = new System.Drawing.Size(400, 30);
            this.cmbPatients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            //
            // Select Nurse
            //
            this.lblNurse.Text      = "SELECT NURSE";
            this.lblNurse.Font      = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.lblNurse.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblNurse.Location  = new System.Drawing.Point(40, 155);
            this.lblNurse.AutoSize  = true;
            this.cmbNurses.Font        = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNurses.Location    = new System.Drawing.Point(40, 175);
            this.cmbNurses.Size        = new System.Drawing.Size(400, 30);
            this.cmbNurses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            //
            // Requested Date
            //
            this.lblDate.Text      = "VISIT DATE";
            this.lblDate.Font      = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblDate.AutoSize  = true;
            this.lblDate.Location  = new System.Drawing.Point(40, 220);
            this.dtpDate.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDate.Location  = new System.Drawing.Point(40, 240);
            this.dtpDate.Size      = new System.Drawing.Size(190, 30);
            this.dtpDate.Format    = System.Windows.Forms.DateTimePickerFormat.Short;
            //
            // Service Type
            //
            this.lblService.Text      = "SERVICE TYPE";
            this.lblService.Font      = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.lblService.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblService.AutoSize  = true;
            this.lblService.Location  = new System.Drawing.Point(250, 220);
            this.cmbService.Font        = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbService.Location    = new System.Drawing.Point(250, 240);
            this.cmbService.Size        = new System.Drawing.Size(190, 30);
            this.cmbService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            //
            // Buttons
            //
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font      = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.Location  = new System.Drawing.Point(260, 310);
            this.btnConfirm.Size      = new System.Drawing.Size(180, 45);
            this.btnConfirm.Text      = "Assign Nurse";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);

            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnCancel.FlatAppearance.BorderSize  = 1;
            this.btnCancel.FlatStyle  = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.BackColor  = System.Drawing.Color.Transparent;
            this.btnCancel.ForeColor  = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnCancel.Font       = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnCancel.Cursor     = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location   = new System.Drawing.Point(120, 310);
            this.btnCancel.Size       = new System.Drawing.Size(130, 45);
            this.btnCancel.Text       = "Cancel";
            this.btnCancel.Click     += new System.EventHandler(this.btnCancel_Click);

            // AdminBookNurseForm Setup
            this.BackColor  = System.Drawing.Color.FromArgb(248, 250, 252);
            this.ClientSize = new System.Drawing.Size(480, 390);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.cmbService);
            this.Controls.Add(this.lblService);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.cmbNurses);
            this.Controls.Add(this.lblNurse);
            this.Controls.Add(this.cmbPatients);
            this.Controls.Add(this.lblPatient);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.AdminBookNurseForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
