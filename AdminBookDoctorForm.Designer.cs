namespace DocAtHome
{
    partial class AdminBookDoctorForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.ComboBox cmbPatients;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.ComboBox cmbDoctors;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDescription;
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
            this.lblDoctor = new System.Windows.Forms.Label();
            this.cmbDoctors = new System.Windows.Forms.ComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblTime = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
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
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Bold", 13F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 22);
            this.lblTitle.Text = "New Doctor Appointment";
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.btnClose.Location = new System.Drawing.Point(435, 18);
            this.btnClose.Size = new System.Drawing.Size(30, 30);
            this.btnClose.Text = "×";
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Form Controls
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

            this.lblDoctor.Text      = "SELECT DOCTOR";
            this.lblDoctor.Font      = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.lblDoctor.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblDoctor.Location  = new System.Drawing.Point(40, 155);
            this.lblDoctor.AutoSize  = true;

            this.cmbDoctors.Font        = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbDoctors.Location    = new System.Drawing.Point(40, 175);
            this.cmbDoctors.Size        = new System.Drawing.Size(400, 30);
            this.cmbDoctors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblDate.Text      = "DATE";
            this.lblDate.Font      = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblDate.AutoSize  = true;
            this.lblDate.Location  = new System.Drawing.Point(40, 220);

            this.dtpDate.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDate.Location = new System.Drawing.Point(40, 240);
            this.dtpDate.Size     = new System.Drawing.Size(190, 30);
            this.dtpDate.Format   = System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblTime.Text      = "TIME (HH:MM)";
            this.lblTime.Font      = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTime.AutoSize  = true;
            this.lblTime.Location  = new System.Drawing.Point(250, 220);

            this.txtTime.Font        = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTime.BackColor   = System.Drawing.Color.FromArgb(241, 245, 249);
            this.txtTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTime.Location    = new System.Drawing.Point(250, 240);
            this.txtTime.Size        = new System.Drawing.Size(190, 30);

            this.lblDesc.Text      = "REASON FOR VISIT";
            this.lblDesc.Font      = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.lblDesc.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblDesc.AutoSize  = true;
            this.lblDesc.Location  = new System.Drawing.Point(40, 288);

            this.txtDescription.Font        = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.BackColor   = System.Drawing.Color.FromArgb(241, 245, 249);
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Location    = new System.Drawing.Point(40, 308);
            this.txtDescription.Size        = new System.Drawing.Size(400, 75);
            this.txtDescription.Multiline   = true;

            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Font      = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnConfirm.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.Location  = new System.Drawing.Point(260, 408);
            this.btnConfirm.Size      = new System.Drawing.Size(180, 45);
            this.btnConfirm.Text      = "Book Now";
            this.btnConfirm.Click    += new System.EventHandler(this.btnConfirm_Click);

            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnCancel.FlatAppearance.BorderSize  = 1;
            this.btnCancel.FlatStyle  = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.BackColor  = System.Drawing.Color.Transparent;
            this.btnCancel.ForeColor  = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnCancel.Font       = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnCancel.Cursor     = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location   = new System.Drawing.Point(120, 408);
            this.btnCancel.Size       = new System.Drawing.Size(130, 45);
            this.btnCancel.Text       = "Cancel";
            this.btnCancel.Click     += new System.EventHandler(this.btnCancel_Click);

            // AdminBookDoctorForm
            this.BackColor  = System.Drawing.Color.FromArgb(248, 250, 252);
            this.ClientSize = new System.Drawing.Size(480, 475);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.cmbDoctors);
            this.Controls.Add(this.lblDoctor);
            this.Controls.Add(this.cmbPatients);
            this.Controls.Add(this.lblPatient);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.AdminBookDoctorForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
