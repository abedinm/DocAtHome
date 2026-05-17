using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocAtHome
{
    partial class ManageNursesForm
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
        private DataGridView dgvNurses;
        private Panel panelActionCard;
        private Label lblActionTitle;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtEmail;
        private TextBox txtLicense;
        private Button btnAdd;
        private Button btnDelete;
        private Label lbl1;
        private Label lbl2;
        private Label lbl3;
        private Label lbl4;
        private Button btnMaximize;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components   = new System.ComponentModel.Container();
            this.btnMaximize  = new System.Windows.Forms.Button();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.lblLogo      = new System.Windows.Forms.Label();
            this.btnLogout    = new System.Windows.Forms.Button();
            this.panelHeader  = new System.Windows.Forms.Panel();
            this.btnClose     = new System.Windows.Forms.Button();
            this.lblTitle     = new System.Windows.Forms.Label();
            this.panelMain    = new System.Windows.Forms.Panel();
            this.panelGridCard   = new System.Windows.Forms.Panel();
            this.dgvNurses       = new System.Windows.Forms.DataGridView();
            this.panelActionCard = new System.Windows.Forms.Panel();
            this.lblActionTitle  = new System.Windows.Forms.Label();
            this.txtFirstName    = new System.Windows.Forms.TextBox();
            this.txtLastName     = new System.Windows.Forms.TextBox();
            this.txtEmail        = new System.Windows.Forms.TextBox();
            this.txtLicense      = new System.Windows.Forms.TextBox();
            this.btnAdd          = new System.Windows.Forms.Button();
            this.btnDelete       = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();

            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelGridCard.SuspendLayout();
            this.panelActionCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNurses)).BeginInit();
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
            this.btnLogout.Font      = new Font("Segoe UI Semibold", 10F);
            this.btnLogout.ForeColor = Color.FromArgb(239, 68, 68);
            this.btnLogout.Height    = 60;
            this.btnLogout.Text      = "   🚪  Logout";
            this.btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            this.btnLogout.Click    += new EventHandler(this.btnLogout_Click);

            // ── Header ────────────────────────────────────────────────────────
            this.panelHeader.BackColor = Color.FromArgb(15, 23, 42);
            this.panelHeader.Controls.Add(this.btnMaximize);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock     = DockStyle.Top;
            this.panelHeader.Height   = 64;
            this.panelHeader.Size     = new Size(1020, 64);
            this.panelHeader.MouseDown += new MouseEventHandler(this.Header_MouseDown);

            this.lblTitle.Font      = new Font("Segoe UI Semibold", 13F);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location  = new Point(30, 20);
            this.lblTitle.Text      = "Nurse Management";
            this.lblTitle.AutoSize  = true;

            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.FlatStyle = FlatStyle.Flat;
            this.btnMaximize.Font      = new Font("Arial", 11F, FontStyle.Bold);
            this.btnMaximize.ForeColor = Color.FromArgb(148, 163, 184);
            this.btnMaximize.Location  = new Point(940, 17);
            this.btnMaximize.Size      = new Size(30, 30);
            this.btnMaximize.Text      = "□";
            this.btnMaximize.Cursor    = Cursors.Hand;
            this.btnMaximize.Anchor    = AnchorStyles.Top | AnchorStyles.Right;
            this.btnMaximize.Click    += new EventHandler(this.btnMaximize_Click);

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

            // ── Action card (RIGHT, fixed 360px) ──────────────────────────────
            this.panelActionCard.BackColor = Color.White;
            this.panelActionCard.Controls.Add(this.lblActionTitle);
            this.panelActionCard.Controls.Add(this.lbl1);
            this.panelActionCard.Controls.Add(this.txtFirstName);
            this.panelActionCard.Controls.Add(this.lbl2);
            this.panelActionCard.Controls.Add(this.txtLastName);
            this.panelActionCard.Controls.Add(this.lbl3);
            this.panelActionCard.Controls.Add(this.txtEmail);
            this.panelActionCard.Controls.Add(this.lbl4);
            this.panelActionCard.Controls.Add(this.txtLicense);
            this.panelActionCard.Controls.Add(this.btnAdd);
            this.panelActionCard.Controls.Add(this.btnDelete);
            this.panelActionCard.Dock  = DockStyle.Right;
            this.panelActionCard.Width = 360;

            this.lblActionTitle.Font      = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblActionTitle.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblActionTitle.Location  = new Point(20, 20);
            this.lblActionTitle.Size      = new Size(310, 30);
            this.lblActionTitle.Text      = "Register New Nurse";

            this.lbl1.Text      = "FIRST NAME";
            this.lbl1.Font      = new Font("Segoe UI Semibold", 8F);
            this.lbl1.ForeColor = Color.FromArgb(100, 116, 139);
            this.lbl1.Location  = new Point(20, 70);
            this.lbl1.AutoSize  = true;

            this.txtFirstName.Font        = new Font("Segoe UI", 10F);
            this.txtFirstName.BackColor   = Color.FromArgb(241, 245, 249);
            this.txtFirstName.Location    = new Point(20, 90);
            this.txtFirstName.Size        = new Size(310, 30);
            this.txtFirstName.BorderStyle = BorderStyle.FixedSingle;

            this.lbl2.Text      = "LAST NAME";
            this.lbl2.Font      = new Font("Segoe UI Semibold", 8F);
            this.lbl2.ForeColor = Color.FromArgb(100, 116, 139);
            this.lbl2.Location  = new Point(20, 135);
            this.lbl2.AutoSize  = true;

            this.txtLastName.Font        = new Font("Segoe UI", 10F);
            this.txtLastName.BackColor   = Color.FromArgb(241, 245, 249);
            this.txtLastName.Location    = new Point(20, 155);
            this.txtLastName.Size        = new Size(310, 30);
            this.txtLastName.BorderStyle = BorderStyle.FixedSingle;

            this.lbl3.Text      = "EMAIL ADDRESS";
            this.lbl3.Font      = new Font("Segoe UI Semibold", 8F);
            this.lbl3.ForeColor = Color.FromArgb(100, 116, 139);
            this.lbl3.Location  = new Point(20, 200);
            this.lbl3.AutoSize  = true;

            this.txtEmail.Font        = new Font("Segoe UI", 10F);
            this.txtEmail.BackColor   = Color.FromArgb(241, 245, 249);
            this.txtEmail.Location    = new Point(20, 220);
            this.txtEmail.Size        = new Size(310, 30);
            this.txtEmail.BorderStyle = BorderStyle.FixedSingle;

            this.lbl4.Text      = "LICENSE NUMBER";
            this.lbl4.Font      = new Font("Segoe UI Semibold", 8F);
            this.lbl4.ForeColor = Color.FromArgb(100, 116, 139);
            this.lbl4.Location  = new Point(20, 265);
            this.lbl4.AutoSize  = true;

            this.txtLicense.Font        = new Font("Segoe UI", 10F);
            this.txtLicense.BackColor   = Color.FromArgb(241, 245, 249);
            this.txtLicense.Location    = new Point(20, 285);
            this.txtLicense.Size        = new Size(310, 30);
            this.txtLicense.BorderStyle = BorderStyle.FixedSingle;

            this.btnAdd.BackColor = Color.FromArgb(16, 185, 129);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Font      = new Font("Segoe UI Semibold", 10F);
            this.btnAdd.Cursor    = Cursors.Hand;
            this.btnAdd.Location  = new Point(20, 360);
            this.btnAdd.Size      = new Size(310, 45);
            this.btnAdd.Text      = "✚  Register Nurse";
            this.btnAdd.Click    += new EventHandler(this.btnAdd_Click);

            this.btnDelete.FlatAppearance.BorderColor = Color.FromArgb(239, 68, 68);
            this.btnDelete.FlatAppearance.BorderSize  = 1;
            this.btnDelete.FlatStyle  = FlatStyle.Flat;
            this.btnDelete.BackColor  = Color.Transparent;
            this.btnDelete.ForeColor  = Color.FromArgb(239, 68, 68);
            this.btnDelete.Font       = new Font("Segoe UI Semibold", 10F);
            this.btnDelete.Cursor     = Cursors.Hand;
            this.btnDelete.Location   = new Point(20, 418);
            this.btnDelete.Size       = new Size(310, 40);
            this.btnDelete.Text       = "🗑  Remove Selected";
            this.btnDelete.Click     += new EventHandler(this.btnDelete_Click);

            // ── Grid card (FILL) ──────────────────────────────────────────────
            this.panelGridCard.BackColor = Color.White;
            this.panelGridCard.Controls.Add(this.dgvNurses);
            this.panelGridCard.Dock    = DockStyle.Fill;
            this.panelGridCard.Padding = new Padding(20);

            this.dgvNurses.BackgroundColor = Color.White;
            this.dgvNurses.BorderStyle     = BorderStyle.None;
            this.dgvNurses.Dock            = DockStyle.Fill;
            this.dgvNurses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNurses.SelectionMode   = DataGridViewSelectionMode.FullRowSelect;

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
            this.Load           += new EventHandler(this.LoadNurses);

            this.panelSidebar.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelGridCard.ResumeLayout(false);
            this.panelActionCard.ResumeLayout(false);
            this.panelActionCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNurses)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
