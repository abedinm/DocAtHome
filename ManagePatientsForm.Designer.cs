using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class ManagePatientsForm : Form
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelSidebar;
        private Label lblLogo;
        private Button btnLogout;
        private Panel panelHeader;
        private Label lblTitle;
        private Button btnClose;
        private Panel panelMain;
        private Panel panelGridCard;
        private DataGridView dgvPatients;
        private Panel panelActionCard;
        private Label lblActionTitle;
        private Panel panelStatArea;
        private Label lblStatIcon;
        private Label lblStatLabel;
        internal Label lblPatientCount;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnRefresh;
        private Button btnDelete;
        private Label lblSectionSearch;
        private Label lblSectionSelected;
        private Label lblSectionDanger;
        internal Label lblSelectedName;
        internal Label lblSelectedEmail;
        internal Label lblSelectedPhone;
        private Button btnMaximize;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSidebar    = new Panel();
            this.lblLogo         = new Label();
            this.btnLogout       = new Button();
            this.panelHeader     = new Panel();
            this.lblTitle        = new Label();
            this.btnClose        = new Button();
            this.panelMain       = new Panel();
            this.panelGridCard   = new Panel();
            this.dgvPatients     = new DataGridView();
            this.panelActionCard = new Panel();
            this.lblActionTitle  = new Label();
            this.panelStatArea   = new Panel();
            this.lblStatIcon     = new Label();
            this.lblStatLabel    = new Label();
            this.lblPatientCount = new Label();
            this.txtSearch       = new TextBox();
            this.btnSearch       = new Button();
            this.btnRefresh      = new Button();
            this.btnDelete       = new Button();
            this.lblSectionSearch   = new Label();
            this.lblSectionSelected = new Label();
            this.lblSectionDanger   = new Label();
            this.lblSelectedName    = new Label();
            this.lblSelectedEmail   = new Label();
            this.lblSelectedPhone   = new Label();
            this.btnMaximize        = new Button();

            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelGridCard.SuspendLayout();
            this.panelActionCard.SuspendLayout();
            this.panelStatArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).BeginInit();
            this.SuspendLayout();

            // ── Sidebar ───────────────────────────────────────────────────────
            this.panelSidebar.BackColor = Color.FromArgb(15, 23, 42);
            this.panelSidebar.Controls.Add(this.lblLogo);
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Dock  = DockStyle.Left;
            this.panelSidebar.Width = 260;

            this.lblLogo.Font      = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.White;
            this.lblLogo.Size      = new Size(260, 100);
            this.lblLogo.Text      = "DocAtHome";
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;

            this.btnLogout.Dock      = DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.Font      = new Font("Segoe UI Semibold", 11F);
            this.btnLogout.ForeColor = Color.FromArgb(239, 68, 68);
            this.btnLogout.Height    = 60;
            this.btnLogout.Text      = "   🚪  Logout";
            this.btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            this.btnLogout.Cursor    = Cursors.Hand;
            this.btnLogout.Click    += new EventHandler(this.btnLogout_Click);

            // ── Header ────────────────────────────────────────────────────────
            this.panelHeader.BackColor = Color.FromArgb(15, 23, 42);
            this.panelHeader.Controls.Add(this.btnMaximize);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock   = DockStyle.Top;
            this.panelHeader.Height = 64;
            this.panelHeader.MouseDown += new MouseEventHandler(this.Header_MouseDown);

            this.lblTitle.AutoSize  = true;
            this.lblTitle.Font      = new Font("Segoe UI Semibold", 13F);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location  = new Point(30, 20);
            this.lblTitle.Text      = "Patient Management";

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
            this.btnClose.FlatStyle  = FlatStyle.Flat;
            this.btnClose.Font       = new Font("Arial", 13F, FontStyle.Bold);
            this.btnClose.ForeColor  = Color.FromArgb(148, 163, 184);
            this.btnClose.Location   = new Point(976, 17);
            this.btnClose.Size       = new Size(30, 30);
            this.btnClose.Text       = "×";
            this.btnClose.Cursor     = Cursors.Hand;
            this.btnClose.Anchor     = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.Click     += new EventHandler(this.btnClose_Click);

            // ── Action card (RIGHT, fixed 360px) ──────────────────────────────
            this.panelActionCard.BackColor = Color.White;
            this.panelActionCard.Dock      = DockStyle.Right;
            this.panelActionCard.Width     = 360;
            this.panelActionCard.Controls.Add(this.lblActionTitle);
            this.panelActionCard.Controls.Add(this.panelStatArea);
            this.panelActionCard.Controls.Add(this.lblSectionSearch);
            this.panelActionCard.Controls.Add(this.txtSearch);
            this.panelActionCard.Controls.Add(this.btnSearch);
            this.panelActionCard.Controls.Add(this.btnRefresh);
            this.panelActionCard.Controls.Add(this.lblSectionSelected);
            this.panelActionCard.Controls.Add(this.lblSelectedName);
            this.panelActionCard.Controls.Add(this.lblSelectedEmail);
            this.panelActionCard.Controls.Add(this.lblSelectedPhone);
            this.panelActionCard.Controls.Add(this.lblSectionDanger);
            this.panelActionCard.Controls.Add(this.btnDelete);

            this.lblActionTitle.Font      = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.lblActionTitle.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblActionTitle.Location  = new Point(20, 20);
            this.lblActionTitle.Size      = new Size(310, 30);
            this.lblActionTitle.Text      = "Patient Overview";

            // Stat banner (full width strip at top of action card)
            this.panelStatArea.BackColor = Color.FromArgb(241, 245, 249);
            this.panelStatArea.Location  = new Point(0, 65);
            this.panelStatArea.Size      = new Size(360, 105);
            this.panelStatArea.Controls.Add(this.lblStatIcon);
            this.panelStatArea.Controls.Add(this.lblStatLabel);
            this.panelStatArea.Controls.Add(this.lblPatientCount);

            this.lblStatIcon.Text      = "👥";
            this.lblStatIcon.Font      = new Font("Segoe UI", 28F);
            this.lblStatIcon.Location  = new Point(295, 20);
            this.lblStatIcon.Size      = new Size(55, 65);
            this.lblStatIcon.TextAlign = ContentAlignment.MiddleCenter;

            this.lblStatLabel.Text      = "Total Patients";
            this.lblStatLabel.Font      = new Font("Segoe UI", 9F);
            this.lblStatLabel.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblStatLabel.Location  = new Point(20, 18);
            this.lblStatLabel.AutoSize  = true;

            this.lblPatientCount.Text      = "—";
            this.lblPatientCount.Font      = new Font("Segoe UI", 30F, FontStyle.Bold);
            this.lblPatientCount.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblPatientCount.Location  = new Point(18, 38);
            this.lblPatientCount.AutoSize  = true;

            // Search section
            this.lblSectionSearch.Text      = "SEARCH PATIENTS";
            this.lblSectionSearch.Font      = new Font("Segoe UI Semibold", 8F);
            this.lblSectionSearch.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSectionSearch.Location  = new Point(20, 190);
            this.lblSectionSearch.AutoSize  = true;

            this.txtSearch.Font        = new Font("Segoe UI", 10F);
            this.txtSearch.Location    = new Point(20, 210);
            this.txtSearch.Size        = new Size(310, 32);
            this.txtSearch.BorderStyle = BorderStyle.FixedSingle;
            this.txtSearch.BackColor   = Color.FromArgb(241, 245, 249);

            this.btnSearch.Text      = "🔍  Search";
            this.btnSearch.Font      = new Font("Segoe UI Semibold", 10F);
            this.btnSearch.Location  = new Point(20, 256);
            this.btnSearch.Size      = new Size(310, 42);
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.BackColor = Color.FromArgb(59, 130, 246);
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.Cursor    = Cursors.Hand;
            this.btnSearch.Click    += new EventHandler(this.btnSearch_Click);

            this.btnRefresh.Text      = "↺  Refresh All";
            this.btnRefresh.Font      = new Font("Segoe UI Semibold", 10F);
            this.btnRefresh.Location  = new Point(20, 310);
            this.btnRefresh.Size      = new Size(310, 38);
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(100, 116, 139);
            this.btnRefresh.FlatAppearance.BorderSize  = 1;
            this.btnRefresh.BackColor = Color.Transparent;
            this.btnRefresh.ForeColor = Color.FromArgb(100, 116, 139);
            this.btnRefresh.Cursor    = Cursors.Hand;
            this.btnRefresh.Click    += new EventHandler(this.btnRefresh_Click);

            // Selected patient section
            this.lblSectionSelected.Text      = "SELECTED PATIENT";
            this.lblSectionSelected.Font      = new Font("Segoe UI Semibold", 8F);
            this.lblSectionSelected.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSectionSelected.Location  = new Point(20, 370);
            this.lblSectionSelected.AutoSize  = true;

            this.lblSelectedName.Text      = "—";
            this.lblSelectedName.Font      = new Font("Segoe UI Semibold", 11F);
            this.lblSelectedName.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblSelectedName.Location  = new Point(20, 390);
            this.lblSelectedName.Size      = new Size(310, 24);

            this.lblSelectedEmail.Text      = "—";
            this.lblSelectedEmail.Font      = new Font("Segoe UI", 9F);
            this.lblSelectedEmail.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSelectedEmail.Location  = new Point(20, 416);
            this.lblSelectedEmail.Size      = new Size(310, 20);

            this.lblSelectedPhone.Text      = "—";
            this.lblSelectedPhone.Font      = new Font("Segoe UI", 9F);
            this.lblSelectedPhone.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSelectedPhone.Location  = new Point(20, 438);
            this.lblSelectedPhone.Size      = new Size(310, 20);

            // Danger zone
            this.lblSectionDanger.Text      = "DANGER ZONE";
            this.lblSectionDanger.Font      = new Font("Segoe UI Semibold", 8F);
            this.lblSectionDanger.ForeColor = Color.FromArgb(239, 68, 68);
            this.lblSectionDanger.Location  = new Point(20, 480);
            this.lblSectionDanger.AutoSize  = true;

            this.btnDelete.Text      = "🗑  Remove Selected Patient";
            this.btnDelete.Font      = new Font("Segoe UI Semibold", 10F);
            this.btnDelete.Location  = new Point(20, 500);
            this.btnDelete.Size      = new Size(310, 42);
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.FlatAppearance.BorderColor = Color.FromArgb(239, 68, 68);
            this.btnDelete.FlatAppearance.BorderSize  = 1;
            this.btnDelete.BackColor = Color.Transparent;
            this.btnDelete.ForeColor = Color.FromArgb(239, 68, 68);
            this.btnDelete.Cursor    = Cursors.Hand;
            this.btnDelete.Click    += new EventHandler(this.btnDelete_Click);

            // ── Grid card (FILL) ──────────────────────────────────────────────
            this.panelGridCard.BackColor = Color.White;
            this.panelGridCard.Controls.Add(this.dgvPatients);
            this.panelGridCard.Dock    = DockStyle.Fill;
            this.panelGridCard.Padding = new Padding(20);

            this.dgvPatients.BackgroundColor     = Color.White;
            this.dgvPatients.BorderStyle         = BorderStyle.None;
            this.dgvPatients.Dock                = DockStyle.Fill;
            this.dgvPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPatients.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPatients.ReadOnly            = true;

            // ── panelMain — grid FIRST (Fill), action SECOND (Right docks first) ─
            this.panelMain.BackColor = Color.FromArgb(248, 250, 252);
            this.panelMain.Controls.Add(this.panelGridCard);   // Fill
            this.panelMain.Controls.Add(this.panelActionCard); // Right
            this.panelMain.Dock = DockStyle.Fill;

            // ── Form ──────────────────────────────────────────────────────────
            this.ClientSize      = new Size(1280, 720);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.Text            = "Manage Patients";

            this.panelSidebar.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelGridCard.ResumeLayout(false);
            this.panelActionCard.ResumeLayout(false);
            this.panelActionCard.PerformLayout();
            this.panelStatArea.ResumeLayout(false);
            this.panelStatArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
