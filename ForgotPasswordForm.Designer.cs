using System;
using System.Drawing;
using System.Windows.Forms;

namespace DocAtHome
{
    partial class ForgotPasswordForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label   lblDocTitle;
        private Label   lblTagline;
        private Panel   panelForm;
        private Label   lblHeader;
        private Label   lblSubtitle;
        private Label   lblEmail;
        private TextBox txtEmail;
        private Label   lblNewPassword;
        private TextBox txtNewPassword;
        private Label   lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private Button  btnChangePassword;
        private Button  btnCancel;
        private Button  btnClose;
        private Button  btnSendCode;
        private Label   lblOtp;
        private TextBox txtOtp;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnClose          = new Button();
            this.lblDocTitle       = new Label();
            this.lblTagline        = new Label();
            this.panelForm         = new Panel();
            this.lblHeader         = new Label();
            this.lblSubtitle       = new Label();
            this.lblEmail          = new Label();
            this.txtEmail          = new TextBox();
            this.lblNewPassword    = new Label();
            this.txtNewPassword    = new TextBox();
            this.lblConfirmPassword = new Label();
            this.txtConfirmPassword = new TextBox();
            this.btnChangePassword = new Button();
            this.btnCancel         = new Button();
            this.btnSendCode       = new Button();
            this.lblOtp            = new Label();
            this.txtOtp            = new TextBox();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();

            // ── App name ──────────────────────────────────────────────────
            this.lblDocTitle.Text      = "DocAtHome";
            this.lblDocTitle.Font      = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblDocTitle.ForeColor = Color.White;
            this.lblDocTitle.Location  = new Point(0, 22);
            this.lblDocTitle.Size      = new Size(480, 46);
            this.lblDocTitle.TextAlign = ContentAlignment.MiddleCenter;

            this.lblTagline.Text      = "Reset your password securely.";
            this.lblTagline.Font      = new Font("Segoe UI", 9.5F);
            this.lblTagline.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblTagline.Location  = new Point(0, 70);
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
            this.btnClose.Click    += new EventHandler(this.btnCancel_Click);

            // ── White card ────────────────────────────────────────────────
            this.panelForm.BackColor = Color.White;
            this.panelForm.Location  = new Point(40, 100);
            this.panelForm.Size      = new Size(400, 514);
            this.panelForm.Controls.Add(this.lblHeader);
            this.panelForm.Controls.Add(this.lblSubtitle);
            this.panelForm.Controls.Add(this.lblEmail);
            this.panelForm.Controls.Add(this.txtEmail);
            this.panelForm.Controls.Add(this.btnSendCode);
            this.panelForm.Controls.Add(this.lblOtp);
            this.panelForm.Controls.Add(this.txtOtp);
            this.panelForm.Controls.Add(this.lblNewPassword);
            this.panelForm.Controls.Add(this.txtNewPassword);
            this.panelForm.Controls.Add(this.lblConfirmPassword);
            this.panelForm.Controls.Add(this.txtConfirmPassword);
            this.panelForm.Controls.Add(this.btnChangePassword);
            this.panelForm.Controls.Add(this.btnCancel);
            this.panelForm.MouseDown += new MouseEventHandler(this.Header_MouseDown);

            // ── Card: heading ─────────────────────────────────────────────
            this.lblHeader.Text      = "Reset Password";
            this.lblHeader.Font      = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblHeader.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblHeader.Location  = new Point(40, 26);
            this.lblHeader.AutoSize  = true;

            this.lblSubtitle.Text      = "Enter your email to receive a verification code.";
            this.lblSubtitle.Font      = new Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSubtitle.Location  = new Point(40, 68);
            this.lblSubtitle.AutoSize  = true;

            // ── Email ─────────────────────────────────────────────────────
            this.lblEmail.Text      = "REGISTERED EMAIL";
            this.lblEmail.Font      = new Font("Segoe UI Semibold", 8F);
            this.lblEmail.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblEmail.Location  = new Point(40, 106);
            this.lblEmail.AutoSize  = true;

            this.txtEmail.BackColor   = Color.FromArgb(241, 245, 249);
            this.txtEmail.BorderStyle = BorderStyle.FixedSingle;
            this.txtEmail.Font        = new Font("Segoe UI", 11F);
            this.txtEmail.Location    = new Point(40, 126);
            this.txtEmail.Size        = new Size(320, 38);
            this.txtEmail.TabIndex    = 1;

            // ── Send code button ──────────────────────────────────────────
            this.btnSendCode.BackColor  = Color.FromArgb(59, 130, 246);
            this.btnSendCode.ForeColor  = Color.White;
            this.btnSendCode.FlatStyle  = FlatStyle.Flat;
            this.btnSendCode.FlatAppearance.BorderSize = 0;
            this.btnSendCode.Font       = new Font("Segoe UI Semibold", 10F);
            this.btnSendCode.Location   = new Point(40, 180);
            this.btnSendCode.Size       = new Size(320, 46);
            this.btnSendCode.Text       = "Send Verification Code";
            this.btnSendCode.Cursor     = Cursors.Hand;
            this.btnSendCode.TabIndex   = 2;
            this.btnSendCode.UseVisualStyleBackColor = false;
            this.btnSendCode.Click     += new EventHandler(this.btnSendCode_Click);

            // ── OTP field (hidden until code sent) ────────────────────────
            this.lblOtp.Text      = "VERIFICATION CODE";
            this.lblOtp.Font      = new Font("Segoe UI Semibold", 8F);
            this.lblOtp.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblOtp.Location  = new Point(40, 180);
            this.lblOtp.AutoSize  = true;
            this.lblOtp.Visible   = false;

            this.txtOtp.BackColor   = Color.FromArgb(241, 245, 249);
            this.txtOtp.BorderStyle = BorderStyle.FixedSingle;
            this.txtOtp.Font        = new Font("Segoe UI", 11F);
            this.txtOtp.Location    = new Point(40, 200);
            this.txtOtp.Size        = new Size(320, 38);
            this.txtOtp.TabIndex    = 3;
            this.txtOtp.Visible     = false;

            // ── New password ──────────────────────────────────────────────
            this.lblNewPassword.Text      = "NEW PASSWORD";
            this.lblNewPassword.Font      = new Font("Segoe UI Semibold", 8F);
            this.lblNewPassword.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblNewPassword.Location  = new Point(40, 254);
            this.lblNewPassword.AutoSize  = true;
            this.lblNewPassword.Visible   = false;

            this.txtNewPassword.BackColor   = Color.FromArgb(241, 245, 249);
            this.txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
            this.txtNewPassword.Font        = new Font("Segoe UI", 11F);
            this.txtNewPassword.Location    = new Point(40, 274);
            this.txtNewPassword.Size        = new Size(320, 38);
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.TabIndex    = 4;
            this.txtNewPassword.Visible     = false;

            // ── Confirm password ──────────────────────────────────────────
            this.lblConfirmPassword.Text      = "CONFIRM NEW PASSWORD";
            this.lblConfirmPassword.Font      = new Font("Segoe UI Semibold", 8F);
            this.lblConfirmPassword.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblConfirmPassword.Location  = new Point(40, 328);
            this.lblConfirmPassword.AutoSize  = true;
            this.lblConfirmPassword.Visible   = false;

            this.txtConfirmPassword.BackColor   = Color.FromArgb(241, 245, 249);
            this.txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            this.txtConfirmPassword.Font        = new Font("Segoe UI", 11F);
            this.txtConfirmPassword.Location    = new Point(40, 348);
            this.txtConfirmPassword.Size        = new Size(320, 38);
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.TabIndex    = 5;
            this.txtConfirmPassword.Visible     = false;

            // ── Reset button ──────────────────────────────────────────────
            this.btnChangePassword.BackColor  = Color.FromArgb(16, 185, 129);
            this.btnChangePassword.ForeColor  = Color.White;
            this.btnChangePassword.FlatStyle  = FlatStyle.Flat;
            this.btnChangePassword.FlatAppearance.BorderSize = 0;
            this.btnChangePassword.Font       = new Font("Segoe UI Semibold", 10F);
            this.btnChangePassword.Location   = new Point(40, 402);
            this.btnChangePassword.Size       = new Size(320, 46);
            this.btnChangePassword.Text       = "Reset Password";
            this.btnChangePassword.Cursor     = Cursors.Hand;
            this.btnChangePassword.TabIndex   = 6;
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Visible    = false;
            this.btnChangePassword.Click     += new EventHandler(this.btnChangePassword_Click);

            // ── Back link ─────────────────────────────────────────────────
            this.btnCancel.BackColor  = Color.Transparent;
            this.btnCancel.ForeColor  = Color.FromArgb(100, 116, 139);
            this.btnCancel.FlatStyle  = FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Font       = new Font("Segoe UI", 9F);
            this.btnCancel.Location   = new Point(40, 464);
            this.btnCancel.Size       = new Size(320, 28);
            this.btnCancel.Text       = "← Back to Sign In";
            this.btnCancel.Cursor     = Cursors.Hand;
            this.btnCancel.TabIndex   = 7;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click     += new EventHandler(this.btnCancel_Click);

            // ── Form ──────────────────────────────────────────────────────
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode       = AutoScaleMode.Font;
            this.BackColor           = Color.FromArgb(15, 23, 42);
            this.ClientSize          = new Size(480, 650);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.lblTagline);
            this.Controls.Add(this.lblDocTitle);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name            = "ForgotPasswordForm";
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.Text            = "DocAtHome — Reset Password";
            this.MouseDown      += new MouseEventHandler(this.Header_MouseDown);
            this.Load           += new EventHandler(this.ForgotPasswordForm_Load);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
