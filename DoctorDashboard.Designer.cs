using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocAtHome
{
    partial class DoctorDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelSidebar;
        private Label lblLogo;
        private Button btnRefresh;
        private Button btnLogout;
        private Panel panelHeader;
        private Label lblTitle;
        private Button btnClose;
        private CheckBox chkAvailable;
        private Panel panelMain;
        private Panel panelGridCard;
        private DataGridView dgvAppointments;
        private Button btnAccept;
        private Button btnReject;
        private Button btnMaximize;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.lblLogo      = new System.Windows.Forms.Label();
            this.btnRefresh   = new System.Windows.Forms.Button();
            this.btnLogout    = new System.Windows.Forms.Button();
            this.btnMaximize  = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.chkAvailable = new System.Windows.Forms.CheckBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.panelGridCard = new System.Windows.Forms.Panel();
            this.dgvAppointments = new System.Windows.Forms.DataGridView();

            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.SuspendLayout();

            // Sidebar
            this.panelSidebar.BackColor = Color.FromArgb(15, 23, 42);
            this.panelSidebar.Controls.Add(this.btnRefresh);
            this.panelSidebar.Controls.Add(this.lblLogo);
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Dock = DockStyle.Left;
            this.panelSidebar.Size = new Size(260, 720);

            this.lblLogo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.White;
            this.lblLogo.Size = new Size(260, 100);
            this.lblLogo.Text = "DocAtHome";
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;

            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.Font = new Font("Segoe UI Semibold", 11F);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Location = new Point(0, 120);
            this.btnRefresh.Size = new Size(260, 55);
            this.btnRefresh.Text = "   📋  Appointments";
            this.btnRefresh.TextAlign = ContentAlignment.MiddleLeft;
            this.btnRefresh.Cursor = Cursors.Hand;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            this.btnLogout.Dock = DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.Font = new Font("Segoe UI Semibold", 11F);
            this.btnLogout.ForeColor = Color.FromArgb(239, 68, 68);
            this.btnLogout.Size = new Size(260, 60);
            this.btnLogout.Text = "   🚪  Logout";
            this.btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            this.btnLogout.Cursor = Cursors.Hand;
            this.btnLogout.Click += new EventHandler(this.btnLogout_Click);

            // Header
            this.panelHeader.BackColor = Color.FromArgb(15, 23, 42);
            this.panelHeader.Controls.Add(this.btnMaximize);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.chkAvailable);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Size = new Size(1020, 64);
            this.panelHeader.Location = new Point(260, 0);
            this.panelHeader.MouseDown += new MouseEventHandler(this.Header_MouseDown);

            this.lblTitle.Font = new Font("Segoe UI Semibold", 13F);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(30, 20);
            this.lblTitle.Text = "Doctor Workspace";
            this.lblTitle.AutoSize = true;

            this.chkAvailable.Font       = new Font("Segoe UI Semibold", 9.5F);
            this.chkAvailable.ForeColor  = Color.FromArgb(148, 163, 184);
            this.chkAvailable.Location   = new Point(790, 22);
            this.chkAvailable.Size       = new Size(150, 22);
            this.chkAvailable.Text       = "Available";
            this.chkAvailable.Anchor     = AnchorStyles.Top | AnchorStyles.Right;
            this.chkAvailable.CheckedChanged += new EventHandler(this.chkAvailable_CheckedChanged);

            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.FlatStyle  = FlatStyle.Flat;
            this.btnMaximize.Font       = new Font("Arial", 11F, FontStyle.Bold);
            this.btnMaximize.ForeColor  = Color.FromArgb(148, 163, 184);
            this.btnMaximize.Location   = new Point(940, 17);
            this.btnMaximize.Size       = new Size(30, 30);
            this.btnMaximize.Text       = "□";
            this.btnMaximize.Cursor     = Cursors.Hand;
            this.btnMaximize.Anchor     = AnchorStyles.Top | AnchorStyles.Right;
            this.btnMaximize.Click     += new EventHandler(this.btnMaximize_Click);

            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font      = new Font("Arial", 13F, FontStyle.Bold);
            this.btnClose.ForeColor = Color.FromArgb(148, 163, 184);
            this.btnClose.Location  = new Point(976, 17);
            this.btnClose.Size      = new Size(30, 30);
            this.btnClose.Text      = "×";
            this.btnClose.Cursor    = Cursors.Hand;
            this.btnClose.Anchor    = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.Click    += new EventHandler(this.btnClose_Click);

            // Main
            this.panelMain.BackColor = Color.FromArgb(248, 250, 252);
            this.panelMain.Controls.Add(this.btnReject);
            this.panelMain.Controls.Add(this.btnAccept);
            this.panelMain.Controls.Add(this.panelGridCard);
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Location = new Point(260, 80);

            this.panelGridCard.BackColor = Color.White;
            this.panelGridCard.Controls.Add(this.dgvAppointments);
            this.panelGridCard.Location  = new Point(40, 40);
            this.panelGridCard.Size      = new Size(940, 500);
            this.panelGridCard.Anchor    = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;

            this.dgvAppointments.BackgroundColor = Color.White;
            this.dgvAppointments.BorderStyle = BorderStyle.None;
            this.dgvAppointments.Dock = DockStyle.Fill;
            this.dgvAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAppointments.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvAppointments_CellFormatting);

            this.btnAccept.BackColor = Color.FromArgb(16, 185, 129);
            this.btnAccept.FlatAppearance.BorderSize = 0;
            this.btnAccept.FlatStyle  = FlatStyle.Flat;
            this.btnAccept.Font       = new Font("Segoe UI Semibold", 10F);
            this.btnAccept.ForeColor  = Color.White;
            this.btnAccept.Cursor     = Cursors.Hand;
            this.btnAccept.Location   = new Point(800, 560);
            this.btnAccept.Size       = new Size(180, 45);
            this.btnAccept.Text       = "✓  Accept Visit";
            this.btnAccept.Anchor     = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click     += new EventHandler(this.btnAccept_Click);

            this.btnReject.FlatAppearance.BorderColor = Color.FromArgb(239, 68, 68);
            this.btnReject.FlatStyle  = FlatStyle.Flat;
            this.btnReject.Font       = new Font("Segoe UI Semibold", 10F);
            this.btnReject.ForeColor  = Color.FromArgb(239, 68, 68);
            this.btnReject.BackColor  = Color.Transparent;
            this.btnReject.Cursor     = Cursors.Hand;
            this.btnReject.Location   = new Point(610, 560);
            this.btnReject.Size       = new Size(180, 45);
            this.btnReject.Text       = "✕  Reject Visit";
            this.btnReject.Anchor     = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnReject.Click     += new EventHandler(this.btnReject_Click);

            // Form
            this.ClientSize = new Size(1280, 720);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new EventHandler(this.DoctorDashboard_Load);
            this.panelSidebar.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
