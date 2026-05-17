namespace DocAtHome
{
    partial class NurseDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Button btnNavRequests;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.CheckBox chkAvailable;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelTableContainer;
        private System.Windows.Forms.DataGridView dgvNurseRequests;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMaximize;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSidebar        = new System.Windows.Forms.Panel();
            this.btnLogout           = new System.Windows.Forms.Button();
            this.btnNavRequests      = new System.Windows.Forms.Button();
            this.lblLogo             = new System.Windows.Forms.Label();
            this.panelHeader         = new System.Windows.Forms.Panel();
            this.btnMaximize         = new System.Windows.Forms.Button();
            this.btnClose            = new System.Windows.Forms.Button();
            this.chkAvailable        = new System.Windows.Forms.CheckBox();
            this.lblTitle            = new System.Windows.Forms.Label();
            this.panelMain           = new System.Windows.Forms.Panel();
            this.btnReject           = new System.Windows.Forms.Button();
            this.btnAccept           = new System.Windows.Forms.Button();
            this.panelTableContainer = new System.Windows.Forms.Panel();
            this.dgvNurseRequests    = new System.Windows.Forms.DataGridView();

            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelTableContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNurseRequests)).BeginInit();
            this.SuspendLayout();

            // ── Sidebar ───────────────────────────────────────────────────────
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Controls.Add(this.btnNavRequests);
            this.panelSidebar.Controls.Add(this.lblLogo);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Size = new System.Drawing.Size(260, 720);

            this.lblLogo.Font      = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Size      = new System.Drawing.Size(260, 100);
            this.lblLogo.Text      = "DocAtHome";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.btnNavRequests.FlatAppearance.BorderSize = 0;
            this.btnNavRequests.FlatStyle  = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavRequests.Font       = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.btnNavRequests.ForeColor  = System.Drawing.Color.White;
            this.btnNavRequests.Location   = new System.Drawing.Point(0, 120);
            this.btnNavRequests.Size       = new System.Drawing.Size(260, 55);
            this.btnNavRequests.Text       = "   📋  Requests";
            this.btnNavRequests.TextAlign  = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavRequests.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.btnNavRequests.Click     += new System.EventHandler(this.btnNavRequests_Click);

            this.btnLogout.Dock      = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font      = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.btnLogout.Size      = new System.Drawing.Size(260, 60);
            this.btnLogout.Text      = "   🚪  Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.Click    += new System.EventHandler(this.btnLogout_Click);

            // ── Header ────────────────────────────────────────────────────────
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.panelHeader.Controls.Add(this.btnMaximize);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.chkAvailable);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock     = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Size     = new System.Drawing.Size(1020, 64);
            this.panelHeader.Location = new System.Drawing.Point(260, 0);
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Header_MouseDown);

            this.lblTitle.AutoSize  = true;
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI Semibold", 13F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location  = new System.Drawing.Point(30, 20);
            this.lblTitle.Text      = "Nurse Workspace";

            this.chkAvailable.Font       = new System.Drawing.Font("Segoe UI Semibold", 9.5F);
            this.chkAvailable.ForeColor  = System.Drawing.Color.FromArgb(148, 163, 184);
            this.chkAvailable.Location   = new System.Drawing.Point(790, 22);
            this.chkAvailable.Size       = new System.Drawing.Size(120, 22);
            this.chkAvailable.Text       = "Available";
            this.chkAvailable.Anchor     = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.chkAvailable.CheckedChanged += new System.EventHandler(this.chkAvailable_CheckedChanged);

            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.FlatStyle  = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximize.Font       = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.btnMaximize.ForeColor  = System.Drawing.Color.FromArgb(148, 163, 184);
            this.btnMaximize.Location   = new System.Drawing.Point(940, 17);
            this.btnMaximize.Size       = new System.Drawing.Size(30, 30);
            this.btnMaximize.Text       = "□";
            this.btnMaximize.Cursor     = System.Windows.Forms.Cursors.Hand;
            this.btnMaximize.Anchor     = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnMaximize.Click     += new System.EventHandler(this.btnMaximize_Click);

            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font      = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.btnClose.Location  = new System.Drawing.Point(976, 17);
            this.btnClose.Size      = new System.Drawing.Size(30, 30);
            this.btnClose.Text      = "×";
            this.btnClose.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Click    += new System.EventHandler(this.btnClose_Click);

            // ── panelMain ─────────────────────────────────────────────────────
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(248, 249, 252);
            this.panelMain.Controls.Add(this.btnReject);
            this.panelMain.Controls.Add(this.btnAccept);
            this.panelMain.Controls.Add(this.panelTableContainer);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;

            // Grid container — Anchor=None, positions driven by RepositionMainControls
            this.panelTableContainer.BackColor = System.Drawing.Color.White;
            this.panelTableContainer.Controls.Add(this.dgvNurseRequests);
            this.panelTableContainer.Location  = new System.Drawing.Point(40, 163);
            this.panelTableContainer.Size      = new System.Drawing.Size(940, 430);
            this.panelTableContainer.Anchor    = System.Windows.Forms.AnchorStyles.None;

            this.dgvNurseRequests.BackgroundColor     = System.Drawing.Color.White;
            this.dgvNurseRequests.BorderStyle         = System.Windows.Forms.BorderStyle.None;
            this.dgvNurseRequests.Dock                = System.Windows.Forms.DockStyle.Fill;
            this.dgvNurseRequests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // Action buttons — Anchor=None, positions driven by RepositionMainControls
            this.btnAccept.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.btnAccept.FlatAppearance.BorderSize = 0;
            this.btnAccept.FlatStyle  = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.Font       = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnAccept.ForeColor  = System.Drawing.Color.White;
            this.btnAccept.Cursor     = System.Windows.Forms.Cursors.Hand;
            this.btnAccept.Location   = new System.Drawing.Point(800, 607);
            this.btnAccept.Size       = new System.Drawing.Size(185, 45);
            this.btnAccept.Text       = "✓  Accept Visit";
            this.btnAccept.Anchor     = System.Windows.Forms.AnchorStyles.None;
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click     += new System.EventHandler(this.btnAccept_Click);

            this.btnReject.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.btnReject.FlatStyle  = System.Windows.Forms.FlatStyle.Flat;
            this.btnReject.Font       = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnReject.ForeColor  = System.Drawing.Color.FromArgb(239, 68, 68);
            this.btnReject.BackColor  = System.Drawing.Color.Transparent;
            this.btnReject.Cursor     = System.Windows.Forms.Cursors.Hand;
            this.btnReject.Location   = new System.Drawing.Point(600, 607);
            this.btnReject.Size       = new System.Drawing.Size(185, 45);
            this.btnReject.Text       = "✕  Reject Visit";
            this.btnReject.Anchor     = System.Windows.Forms.AnchorStyles.None;
            this.btnReject.Click     += new System.EventHandler(this.btnReject_Click);

            // ── Form ──────────────────────────────────────────────────────────
            this.ClientSize      = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load           += new System.EventHandler(this.NurseDashboard_Load);

            this.panelSidebar.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelTableContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNurseRequests)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
