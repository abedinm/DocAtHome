using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocAtHome
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label   lblDocTitle;
        private Label   lblTagline;
        private Panel   panelForm;
        private Label   lblCreate;
        private Label   lblSubtitle;
        private Label   lblLN;
        private TextBox txtLastName;
        private Label   lblEmail;
        private TextBox txtEmail;
        private Label   lblPhone;
        private TextBox txtPhone;
        private Label   lblPassword;
        private TextBox txtPassword;
        private Button  btnSignUp;
        private Button  btnBack;
        private Button  btnClose;
        private Label   lblStrength;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnClose    = new Button();
            this.lblDocTitle = new Label();
            this.lblTagline  = new Label();
            this.panelForm   = new Panel();
            this.lblCreate   = new Label();
            this.lblSubtitle = new Label();
            this.lblLN       = new Label();
            this.txtLastName = new TextBox();
            this.lblEmail    = new Label();
            this.txtEmail    = new TextBox();
            this.lblPhone    = new Label();
            this.txtPhone    = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.btnSignUp   = new Button();
            this.btnBack     = new Button();
            this.lblStrength = new Label();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();

            // ── App name ──────────────────────────────────────────────────
            this.lblDocTitle.Text      = "DocAtHome";
            this.lblDocTitle.Font      = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblDocTitle.ForeColor = Color.White;
            this.lblDocTitle.Location  = new Point(0, 20);
            this.lblDocTitle.Size      = new Size(480, 46);
            this.lblDocTitle.TextAlign = ContentAlignment.MiddleCenter;

            this.lblTagline.Text      = "Join thousands receiving care at home.";
            this.lblTagline.Font      = new Font("Segoe UI", 9.5F);
            this.lblTagline.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblTagline.Location  = new Point(0, 68);
            this.lblTagline.Size      = new Size(480, 22);
            this.lblTagline.TextAlign = ContentAlignment.MiddleCenter;

            // ── Close button ──────────────────────────────────────────────
            this.btnClose.Text      = "×";
            this.btnClose.Font      = new Font("Arial", 14F, FontStyle.Bold);
            this.btnClose.ForeColor = Color.FromArgb(100, 116, 139);
            this.btnClose.Location  = new Point(440, 10);
            this.btnClose.Size      = new Size(32, 32);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Cursor    = Cursors.Hand;
            this.btnClose.Click    += new EventHandler(this.btnClose_Click);

            // ── White card ────────────────────────────────────────────────
            this.panelForm.BackColor = Color.White;
            this.panelForm.Location  = new Point(40, 98);
            this.panelForm.Size      = new Size(400, 520);
            this.panelForm.Controls.Add(this.lblCreate);
            this.panelForm.Controls.Add(this.lblSubtitle);
            this.panelForm.Controls.Add(this.lblLN);
            this.panelForm.Controls.Add(this.txtLastName);
            this.panelForm.Controls.Add(this.lblEmail);
            this.panelForm.Controls.Add(this.txtEmail);
            this.panelForm.Controls.Add(this.lblPhone);
            this.panelForm.Controls.Add(this.txtPhone);
            this.panelForm.Controls.Add(this.lblPassword);
            this.panelForm.Controls.Add(this.txtPassword);
            this.panelForm.Controls.Add(this.lblStrength);
            this.panelForm.Controls.Add(this.btnSignUp);
            this.panelForm.Controls.Add(this.btnBack);
            this.panelForm.MouseDown += new MouseEventHandler(this.Header_MouseDown);

            // ── Card: heading ─────────────────────────────────────────────
            this.lblCreate.Text      = "Create Account";
            this.lblCreate.Font      = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblCreate.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblCreate.Location  = new Point(40, 26);
            this.lblCreate.AutoSize  = true;

            this.lblSubtitle.Text      = "Fill in your details to get started.";
            this.lblSubtitle.Font      = new Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSubtitle.Location  = new Point(40, 68);
            this.lblSubtitle.AutoSize  = true;

            // ── Full name ─────────────────────────────────────────────────
            this.lblLN.Text      = "FULL NAME";
            this.lblLN.Font      = new Font("Segoe UI Semibold", 8F);
            this.lblLN.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblLN.Location  = new Point(40, 104);
            this.lblLN.AutoSize  = true;

            this.txtLastName.BackColor   = Color.FromArgb(241, 245, 249);
            this.txtLastName.BorderStyle = BorderStyle.FixedSingle;
            this.txtLastName.Font        = new Font("Segoe UI", 11F);
            this.txtLastName.Location    = new Point(40, 124);
            this.txtLastName.Size        = new Size(320, 38);
            this.txtLastName.TabIndex    = 1;

            // ── Email ─────────────────────────────────────────────────────
            this.lblEmail.Text      = "EMAIL ADDRESS";
            this.lblEmail.Font      = new Font("Segoe UI Semibold", 8F);
            this.lblEmail.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblEmail.Location  = new Point(40, 178);
            this.lblEmail.AutoSize  = true;

            this.txtEmail.BackColor   = Color.FromArgb(241, 245, 249);
            this.txtEmail.BorderStyle = BorderStyle.FixedSingle;
            this.txtEmail.Font        = new Font("Segoe UI", 11F);
            this.txtEmail.Location    = new Point(40, 198);
            this.txtEmail.Size        = new Size(320, 38);
            this.txtEmail.TabIndex    = 2;

            // ── Phone ─────────────────────────────────────────────────────
            this.lblPhone.Text      = "PHONE NUMBER";
            this.lblPhone.Font      = new Font("Segoe UI Semibold", 8F);
            this.lblPhone.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblPhone.Location  = new Point(40, 252);
            this.lblPhone.AutoSize  = true;

            this.txtPhone.BackColor   = Color.FromArgb(241, 245, 249);
            this.txtPhone.BorderStyle = BorderStyle.FixedSingle;
            this.txtPhone.Font        = new Font("Segoe UI", 11F);
            this.txtPhone.Location    = new Point(40, 272);
            this.txtPhone.Size        = new Size(320, 38);
            this.txtPhone.TabIndex    = 3;

            // ── Password ─────────────────────────────────────────────────
            this.lblPassword.Text      = "PASSWORD";
            this.lblPassword.Font      = new Font("Segoe UI Semibold", 8F);
            this.lblPassword.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblPassword.Location  = new Point(40, 326);
            this.lblPassword.AutoSize  = true;

            this.txtPassword.BackColor             = Color.FromArgb(241, 245, 249);
            this.txtPassword.BorderStyle           = BorderStyle.FixedSingle;
            this.txtPassword.Font                  = new Font("Segoe UI", 11F);
            this.txtPassword.Location              = new Point(40, 346);
            this.txtPassword.Size                  = new Size(320, 38);
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TabIndex              = 4;

            // ── Password strength label ───────────────────────────────────
            this.lblStrength.AutoSize  = false;
            this.lblStrength.Font      = new Font("Segoe UI", 8.5F);
            this.lblStrength.ForeColor = Color.FromArgb(148, 163, 184);
            this.lblStrength.Location  = new Point(40, 390);
            this.lblStrength.Size      = new Size(320, 18);
            this.lblStrength.Text      = "";
            this.lblStrength.TabIndex  = 5;

            // ── Create account button ─────────────────────────────────────
            this.btnSignUp.BackColor  = Color.FromArgb(59, 130, 246);
            this.btnSignUp.ForeColor  = Color.White;
            this.btnSignUp.FlatStyle  = FlatStyle.Flat;
            this.btnSignUp.FlatAppearance.BorderSize = 0;
            this.btnSignUp.Font       = new Font("Segoe UI Semibold", 10F);
            this.btnSignUp.Location   = new Point(40, 416);
            this.btnSignUp.Size       = new Size(320, 46);
            this.btnSignUp.Text       = "Create Account";
            this.btnSignUp.Cursor     = Cursors.Hand;
            this.btnSignUp.TabIndex   = 6;
            this.btnSignUp.UseVisualStyleBackColor = false;
            this.btnSignUp.Click     += new EventHandler(this.btnSignUp_Click);

            // ── Back link ─────────────────────────────────────────────────
            this.btnBack.BackColor  = Color.Transparent;
            this.btnBack.ForeColor  = Color.FromArgb(100, 116, 139);
            this.btnBack.FlatStyle  = FlatStyle.Flat;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.Font       = new Font("Segoe UI", 9F);
            this.btnBack.Location   = new Point(40, 474);
            this.btnBack.Size       = new Size(320, 28);
            this.btnBack.Text       = "← Already have an account?  Sign In";
            this.btnBack.Cursor     = Cursors.Hand;
            this.btnBack.TabIndex   = 7;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click     += new EventHandler(this.btnBack_Click);

            // ── Form ──────────────────────────────────────────────────────
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode       = AutoScaleMode.Font;
            this.BackColor           = Color.FromArgb(15, 23, 42);
            this.ClientSize          = new Size(480, 642);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.lblTagline);
            this.Controls.Add(this.lblDocTitle);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name            = "RegisterForm";
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.Text            = "DocAtHome — Create Account";
            this.MouseDown      += new MouseEventHandler(this.Header_MouseDown);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
