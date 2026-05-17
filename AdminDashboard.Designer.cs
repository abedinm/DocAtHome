using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DocAtHome
{
    public partial class AdminDashboard : Form
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelSidebar;
        private Label lblLogo;
        private Button btnManageDoctors;
        private Button btnManageNurses;
        private Button btnManagePatients;
        private Button btnViewAppointments;
        private Button btnViewNurseBookings;
        private Button btnLogout;
        private Panel panelHeader;
        private Label lblTitle;
        private Button btnClose;
        private Panel panelMain;
        private Panel card1;
        private Label lblDocTitle;
        private Label lblDocCount;
        private Label labelIcon1;
        private Panel card2;
        private Label lblPatientTitle;
        private Label lblPatientCount;
        private Label labelIcon2;
        private Panel card3;
        private Label lblNurseTitle;
        private Label lblNurseCount;
        private Label labelIcon3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartActivity;
        private Button btnMaximize;
        private Panel card4;
        private Label lblRevenueTitle;
        private Label lblRevenue;
        private Label labelIcon4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ChartArea chartArea1 = new ChartArea();
            Series series1 = new Series();
            this.panelSidebar = new Panel();
            this.lblLogo = new Label();
            this.btnManageDoctors = new Button();
            this.btnManageNurses = new Button();
            this.btnManagePatients = new Button();
            this.btnViewAppointments = new Button();
            this.btnViewNurseBookings = new Button();
            this.btnLogout = new Button();
            this.panelHeader = new Panel();
            this.lblTitle = new Label();
            this.btnClose = new Button();
            this.panelMain = new Panel();
            this.card1 = new Panel();
            this.lblDocTitle = new Label();
            this.lblDocCount = new Label();
            this.labelIcon1 = new Label();
            this.card2 = new Panel();
            this.lblPatientTitle = new Label();
            this.lblPatientCount = new Label();
            this.labelIcon2 = new Label();
            this.card3 = new Panel();
            this.lblNurseTitle = new Label();
            this.lblNurseCount = new Label();
            this.labelIcon3 = new Label();
            this.chartActivity  = new Chart();
            this.btnMaximize    = new Button();
            this.card4          = new Panel();
            this.lblRevenueTitle = new Label();
            this.lblRevenue     = new Label();
            this.labelIcon4     = new Label();

            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.card1.SuspendLayout();
            this.card2.SuspendLayout();
            this.card3.SuspendLayout();
            this.card4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartActivity)).BeginInit();
            this.SuspendLayout();

            // panelSidebar
            this.panelSidebar.BackColor = Color.FromArgb(15, 23, 42);
            this.panelSidebar.Controls.Add(this.btnViewNurseBookings);
            this.panelSidebar.Controls.Add(this.btnViewAppointments);
            this.panelSidebar.Controls.Add(this.btnManagePatients);
            this.panelSidebar.Controls.Add(this.btnManageNurses);
            this.panelSidebar.Controls.Add(this.btnManageDoctors);
            this.panelSidebar.Controls.Add(this.lblLogo);
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Dock = DockStyle.Left;
            this.panelSidebar.Size = new Size(260, 720);

            // lblLogo
            this.lblLogo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.White;
            this.lblLogo.Size = new Size(260, 100);
            this.lblLogo.Text = "DocAtHome";
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;

            // Individual Button Styling (NO LOOPS)
            this.btnManageDoctors.FlatStyle = FlatStyle.Flat;
            this.btnManageDoctors.FlatAppearance.BorderSize = 0;
            this.btnManageDoctors.Font = new Font("Segoe UI Semibold", 11F);
            this.btnManageDoctors.ForeColor = Color.FromArgb(148, 163, 184);
            this.btnManageDoctors.Location = new Point(0, 120);
            this.btnManageDoctors.Size = new Size(260, 55);
            this.btnManageDoctors.Text = "   👨‍⚕️  Manage Doctors";
            this.btnManageDoctors.TextAlign = ContentAlignment.MiddleLeft;
            this.btnManageDoctors.Cursor = Cursors.Hand;
            this.btnManageDoctors.Click += new EventHandler(this.btnManageDoctors_Click);

            this.btnManageNurses.FlatStyle = FlatStyle.Flat;
            this.btnManageNurses.FlatAppearance.BorderSize = 0;
            this.btnManageNurses.Font = new Font("Segoe UI Semibold", 11F);
            this.btnManageNurses.ForeColor = Color.FromArgb(148, 163, 184);
            this.btnManageNurses.Location = new Point(0, 175);
            this.btnManageNurses.Size = new Size(260, 55);
            this.btnManageNurses.Text = "   👩‍⚕️  Manage Nurses";
            this.btnManageNurses.TextAlign = ContentAlignment.MiddleLeft;
            this.btnManageNurses.Cursor = Cursors.Hand;
            this.btnManageNurses.Click += new EventHandler(this.btnManageNurses_Click);

            this.btnManagePatients.FlatStyle = FlatStyle.Flat;
            this.btnManagePatients.FlatAppearance.BorderSize = 0;
            this.btnManagePatients.Font = new Font("Segoe UI Semibold", 11F);
            this.btnManagePatients.ForeColor = Color.FromArgb(148, 163, 184);
            this.btnManagePatients.Location = new Point(0, 230);
            this.btnManagePatients.Size = new Size(260, 55);
            this.btnManagePatients.Text = "   👤  Manage Patients";
            this.btnManagePatients.TextAlign = ContentAlignment.MiddleLeft;
            this.btnManagePatients.Cursor = Cursors.Hand;
            this.btnManagePatients.Click += new EventHandler(this.btnManagePatients_Click);

            this.btnViewAppointments.FlatStyle = FlatStyle.Flat;
            this.btnViewAppointments.FlatAppearance.BorderSize = 0;
            this.btnViewAppointments.Font = new Font("Segoe UI Semibold", 11F);
            this.btnViewAppointments.ForeColor = Color.FromArgb(148, 163, 184);
            this.btnViewAppointments.Location = new Point(0, 285);
            this.btnViewAppointments.Size = new Size(260, 55);
            this.btnViewAppointments.Text = "   📅  Doctor Bookings";
            this.btnViewAppointments.TextAlign = ContentAlignment.MiddleLeft;
            this.btnViewAppointments.Cursor = Cursors.Hand;
            this.btnViewAppointments.Click += new EventHandler(this.btnViewAppointments_Click);

            this.btnViewNurseBookings.FlatStyle = FlatStyle.Flat;
            this.btnViewNurseBookings.FlatAppearance.BorderSize = 0;
            this.btnViewNurseBookings.Font = new Font("Segoe UI Semibold", 11F);
            this.btnViewNurseBookings.ForeColor = Color.FromArgb(148, 163, 184);
            this.btnViewNurseBookings.Location = new Point(0, 340);
            this.btnViewNurseBookings.Size = new Size(260, 55);
            this.btnViewNurseBookings.Text = "   🏠  Nurse Bookings";
            this.btnViewNurseBookings.TextAlign = ContentAlignment.MiddleLeft;
            this.btnViewNurseBookings.Cursor = Cursors.Hand;
            this.btnViewNurseBookings.Click += new EventHandler(this.btnViewNurseBookings_Click);

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

            // panelHeader
            this.panelHeader.BackColor = Color.FromArgb(15, 23, 42);
            this.panelHeader.Controls.Add(this.btnMaximize);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Location = new Point(260, 0);
            this.panelHeader.Size = new Size(1020, 64);
            this.panelHeader.MouseDown += new MouseEventHandler(this.Header_MouseDown);

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI Semibold", 13F);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(30, 20);
            this.lblTitle.Text = "Admin Dashboard";

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

            // panelMain
            this.panelMain.BackColor = Color.FromArgb(248, 250, 252);
            this.panelMain.Controls.Add(this.chartActivity);
            this.panelMain.Controls.Add(this.card4);
            this.panelMain.Controls.Add(this.card3);
            this.panelMain.Controls.Add(this.card2);
            this.panelMain.Controls.Add(this.card1);
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Location = new Point(260, 80);

            // card1 (Doctors)
            this.card1.BackColor = Color.White;
            this.card1.Controls.Add(this.labelIcon1);
            this.card1.Controls.Add(this.lblDocCount);
            this.card1.Controls.Add(this.lblDocTitle);
            this.card1.Location = new Point(20, 30);
            this.card1.Size = new Size(228, 130);
            this.labelIcon1.Text = "🩺";
            this.labelIcon1.Font = new Font("Segoe UI", 28F);
            this.labelIcon1.Location = new Point(155, 25);
            this.labelIcon1.Size = new Size(60, 65);
            this.labelIcon1.ForeColor = Color.FromArgb(59, 130, 246);
            this.lblDocCount.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            this.lblDocCount.Location = new Point(15, 20);
            this.lblDocCount.Size = new Size(120, 50);
            this.lblDocCount.Text = "0";
            this.lblDocTitle.Text = "Doctors";
            this.lblDocTitle.Font = new Font("Segoe UI Semibold", 10F);
            this.lblDocTitle.Location = new Point(18, 80);
            this.lblDocTitle.Size = new Size(120, 20);
            this.lblDocTitle.ForeColor = Color.SlateGray;

            // card2 (Patients)
            this.card2.BackColor = Color.White;
            this.card2.Controls.Add(this.labelIcon2);
            this.card2.Controls.Add(this.lblPatientCount);
            this.card2.Controls.Add(this.lblPatientTitle);
            this.card2.Location = new Point(268, 30);
            this.card2.Size = new Size(228, 130);
            this.labelIcon2.Text = "👥";
            this.labelIcon2.Font = new Font("Segoe UI", 28F);
            this.labelIcon2.Location = new Point(155, 25);
            this.labelIcon2.Size = new Size(60, 65);
            this.labelIcon2.ForeColor = Color.FromArgb(16, 185, 129);
            this.lblPatientCount.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            this.lblPatientCount.Location = new Point(15, 20);
            this.lblPatientCount.Size = new Size(120, 50);
            this.lblPatientCount.Text = "0";
            this.lblPatientTitle.Text = "Patients";
            this.lblPatientTitle.Font = new Font("Segoe UI Semibold", 10F);
            this.lblPatientTitle.Location = new Point(18, 80);
            this.lblPatientTitle.Size = new Size(120, 20);
            this.lblPatientTitle.ForeColor = Color.SlateGray;

            // card3 (Nurses)
            this.card3.BackColor = Color.White;
            this.card3.Controls.Add(this.labelIcon3);
            this.card3.Controls.Add(this.lblNurseCount);
            this.card3.Controls.Add(this.lblNurseTitle);
            this.card3.Location = new Point(516, 30);
            this.card3.Size = new Size(228, 130);
            this.labelIcon3.Text = "👩‍⚕️";
            this.labelIcon3.Font = new Font("Segoe UI", 28F);
            this.labelIcon3.Location = new Point(155, 25);
            this.labelIcon3.Size = new Size(60, 65);
            this.labelIcon3.ForeColor = Color.FromArgb(245, 158, 11);
            this.lblNurseCount.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            this.lblNurseCount.Location = new Point(15, 20);
            this.lblNurseCount.Size = new Size(120, 50);
            this.lblNurseCount.Text = "0";
            this.lblNurseTitle.Text = "Nurses";
            this.lblNurseTitle.Font = new Font("Segoe UI Semibold", 10F);
            this.lblNurseTitle.Location = new Point(18, 80);
            this.lblNurseTitle.Size = new Size(120, 20);
            this.lblNurseTitle.ForeColor = Color.SlateGray;

            // card4 (Revenue)
            this.card4.BackColor = Color.White;
            this.card4.Controls.Add(this.labelIcon4);
            this.card4.Controls.Add(this.lblRevenue);
            this.card4.Controls.Add(this.lblRevenueTitle);
            this.card4.Location = new Point(764, 30);
            this.card4.Size = new Size(228, 130);
            this.labelIcon4.Text = "💰";
            this.labelIcon4.Font = new Font("Segoe UI", 28F);
            this.labelIcon4.Location = new Point(155, 25);
            this.labelIcon4.Size = new Size(60, 65);
            this.labelIcon4.ForeColor = Color.FromArgb(239, 68, 68);
            this.lblRevenue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblRevenue.Location = new Point(15, 22);
            this.lblRevenue.Size = new Size(140, 46);
            this.lblRevenue.Text = "0";
            this.lblRevenueTitle.Text = "Total Revenue (BDT)";
            this.lblRevenueTitle.Font = new Font("Segoe UI Semibold", 9F);
            this.lblRevenueTitle.Location = new Point(18, 80);
            this.lblRevenueTitle.Size = new Size(140, 20);
            this.lblRevenueTitle.ForeColor = Color.SlateGray;

            // chartActivity
            chartArea1.Name = "ChartArea1";
            this.chartActivity.ChartAreas.Add(chartArea1);
            this.chartActivity.Location = new Point(40, 190);
            this.chartActivity.Size     = new Size(940, 410);
            this.chartActivity.Anchor   = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            series1.ChartType = SeriesChartType.SplineArea;
            series1.Color = Color.FromArgb(180, 59, 130, 246);
            series1.Name = "Bookings";
            this.chartActivity.Series.Add(series1);

            // AdminDashboard Form
            this.ClientSize = new Size(1280, 720);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "AdminDashboard";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new EventHandler(this.AdminDashboard_Load);
            this.panelSidebar.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.card1.ResumeLayout(false);
            this.card1.PerformLayout();
            this.card2.ResumeLayout(false);
            this.card2.PerformLayout();
            this.card3.ResumeLayout(false);
            this.card3.PerformLayout();
            this.card4.ResumeLayout(false);
            this.card4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartActivity)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
