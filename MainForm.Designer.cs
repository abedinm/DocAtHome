using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocAtHome
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelSidebar;
        private Label lblLogo;
        private Button btnNavAppointments;
        private Button btnLogout;
        private Panel panelHeader;
        private Label lblTitle;
        private Button btnClose;
        private Panel panelMain;
        private Label lblWelcomePatient;
        private Label lblClock;
        private Panel panelGridCard;
        private DataGridView dgvPatientHistory;
        private Button btnBookDoctor;
        private Button btnBookNurse;
        private Button btnPay;
        private Timer timerClock;
        private Button btnMaximize;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnNavAppointments = new System.Windows.Forms.Button();
            this.lblLogo = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnMaximize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.btnPay = new System.Windows.Forms.Button();
            this.btnBookNurse = new System.Windows.Forms.Button();
            this.btnBookDoctor = new System.Windows.Forms.Button();
            this.panelGridCard = new System.Windows.Forms.Panel();
            this.dgvPatientHistory = new System.Windows.Forms.DataGridView();
            this.lblClock = new System.Windows.Forms.Label();
            this.lblWelcomePatient = new System.Windows.Forms.Label();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatientHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Controls.Add(this.btnNavAppointments);
            this.panelSidebar.Controls.Add(this.lblLogo);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(260, 720);
            this.panelSidebar.TabIndex = 2;
            // 
            // btnLogout
            // 
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnLogout.Location = new System.Drawing.Point(0, 660);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(260, 60);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "   🚪  Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnNavAppointments
            // 
            this.btnNavAppointments.FlatAppearance.BorderSize = 0;
            this.btnNavAppointments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavAppointments.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.btnNavAppointments.ForeColor = System.Drawing.Color.White;
            this.btnNavAppointments.Location = new System.Drawing.Point(0, 120);
            this.btnNavAppointments.Name = "btnNavAppointments";
            this.btnNavAppointments.Size = new System.Drawing.Size(260, 55);
            this.btnNavAppointments.TabIndex = 1;
            this.btnNavAppointments.Text = "   📅  My History";
            this.btnNavAppointments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavAppointments.Click += new System.EventHandler(this.btnViewAppointments_Click);
            // 
            // lblLogo
            // 
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(0, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(260, 100);
            this.lblLogo.TabIndex = 2;
            this.lblLogo.Text = "DocAtHome";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelHeader.Controls.Add(this.btnMaximize);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(260, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1020, 64);
            this.panelHeader.TabIndex = 1;
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Header_MouseDown);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximize.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.btnMaximize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnMaximize.Location = new System.Drawing.Point(940, 17);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(30, 30);
            this.btnMaximize.TabIndex = 0;
            this.btnMaximize.Text = "□";
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClose.Location = new System.Drawing.Point(976, 17);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "×";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(165, 25);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Patient Dashboard";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelMain.Controls.Add(this.btnPay);
            this.panelMain.Controls.Add(this.btnBookNurse);
            this.panelMain.Controls.Add(this.btnBookDoctor);
            this.panelMain.Controls.Add(this.panelGridCard);
            this.panelMain.Controls.Add(this.lblClock);
            this.panelMain.Controls.Add(this.lblWelcomePatient);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(260, 64);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1020, 656);
            this.panelMain.TabIndex = 0;
            // 
            // btnPay
            // 
            this.btnPay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPay.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnPay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnPay.Location = new System.Drawing.Point(40, 1076);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(145, 38);
            this.btnPay.TabIndex = 0;
            this.btnPay.Text = "💳 Pay Selected";
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // btnBookNurse
            // 
            this.btnBookNurse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBookNurse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnBookNurse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBookNurse.FlatAppearance.BorderSize = 0;
            this.btnBookNurse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBookNurse.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnBookNurse.ForeColor = System.Drawing.Color.White;
            this.btnBookNurse.Location = new System.Drawing.Point(1425, 1076);
            this.btnBookNurse.Name = "btnBookNurse";
            this.btnBookNurse.Size = new System.Drawing.Size(185, 45);
            this.btnBookNurse.TabIndex = 1;
            this.btnBookNurse.Text = "👩‍⚕️  Book Nurse";
            this.btnBookNurse.UseVisualStyleBackColor = false;
            this.btnBookNurse.Click += new System.EventHandler(this.btnBookNurse_Click);
            // 
            // btnBookDoctor
            // 
            this.btnBookDoctor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBookDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnBookDoctor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBookDoctor.FlatAppearance.BorderSize = 0;
            this.btnBookDoctor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBookDoctor.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnBookDoctor.ForeColor = System.Drawing.Color.White;
            this.btnBookDoctor.Location = new System.Drawing.Point(1620, 1076);
            this.btnBookDoctor.Name = "btnBookDoctor";
            this.btnBookDoctor.Size = new System.Drawing.Size(185, 45);
            this.btnBookDoctor.TabIndex = 2;
            this.btnBookDoctor.Text = "👨‍⚕️  Book Doctor";
            this.btnBookDoctor.UseVisualStyleBackColor = false;
            this.btnBookDoctor.Click += new System.EventHandler(this.btnBookDoctor_Click);
            // 
            // panelGridCard
            // 
            this.panelGridCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGridCard.BackColor = System.Drawing.Color.White;
            this.panelGridCard.Controls.Add(this.dgvPatientHistory);
            this.panelGridCard.Location = new System.Drawing.Point(40, 120);
            this.panelGridCard.Name = "panelGridCard";
            this.panelGridCard.Size = new System.Drawing.Size(1760, 936);
            this.panelGridCard.TabIndex = 3;
            // 
            // dgvPatientHistory
            // 
            this.dgvPatientHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPatientHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvPatientHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPatientHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPatientHistory.Location = new System.Drawing.Point(0, 0);
            this.dgvPatientHistory.Name = "dgvPatientHistory";
            this.dgvPatientHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPatientHistory.Size = new System.Drawing.Size(1760, 936);
            this.dgvPatientHistory.TabIndex = 0;
            // 
            // lblClock
            // 
            this.lblClock.AutoSize = true;
            this.lblClock.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblClock.ForeColor = System.Drawing.Color.SlateGray;
            this.lblClock.Location = new System.Drawing.Point(40, 75);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(101, 19);
            this.lblClock.TabIndex = 4;
            this.lblClock.Text = "Loading clock...";
            // 
            // lblWelcomePatient
            // 
            this.lblWelcomePatient.AutoSize = true;
            this.lblWelcomePatient.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblWelcomePatient.Location = new System.Drawing.Point(35, 30);
            this.lblWelcomePatient.Name = "lblWelcomePatient";
            this.lblWelcomePatient.Size = new System.Drawing.Size(231, 41);
            this.lblWelcomePatient.TabIndex = 5;
            this.lblWelcomePatient.Text = "Welcome back!";
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelSidebar.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatientHistory)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
