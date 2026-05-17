using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class ForgotPasswordForm : Form
    {
        public ForgotPasswordForm()
        {
            InitializeComponent();
            Theme.Primary(btnSendCode);
            Theme.SuccessBtn(btnChangePassword);
            Theme.Ghost(btnCancel);
            Theme.Input(txtEmail);
            Theme.Input(txtOtp);
            Theme.Input(txtNewPassword);
            Theme.Input(txtConfirmPassword);
        }

        private void Header_MouseDown(object sender, MouseEventArgs e) =>
            WindowHelper.StartDrag(Handle);

        private void ForgotPasswordForm_Load(object sender, EventArgs e) => txtEmail.Focus();

        private void btnSendCode_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            { MessageBox.Show("Please enter your email address.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            // Verify email exists in any role table
            bool found = false;
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    foreach (string table in new[] { "Patients", "Doctors", "Nurses", "Admins" })
                    {
                        using (var cmd = new SqlCommand($"SELECT COUNT(*) FROM {table} WHERE Email=@E", conn))
                        {
                            cmd.Parameters.AddWithValue("@E", email);
                            if ((int)cmd.ExecuteScalar() > 0) { found = true; break; }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to connect. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!found)
            {
                MessageBox.Show("No account found with that email address.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string code = OtpService.Generate(email);

            // In production this would be sent via email (SmtpClient).
            // Demo mode: display the code directly so the flow is demonstrable.
            MessageBox.Show(
                $"Verification code generated.\n\nCode:  {code}\n\n(In production this would be emailed to {email}.)\nThe code expires in 10 minutes.",
                "Verification Code Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reveal step 2
            btnSendCode.Visible         = false;
            txtEmail.Enabled            = false;
            lblOtp.Visible              = true;
            txtOtp.Visible              = true;
            lblNewPassword.Visible      = true;
            txtNewPassword.Visible      = true;
            lblConfirmPassword.Visible  = true;
            txtConfirmPassword.Visible  = true;
            btnChangePassword.Visible   = true;
            lblSubtitle.Text            = "Enter the verification code and your new password.";
            txtOtp.Focus();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string email   = txtEmail.Text.Trim();
            string otp     = txtOtp.Text.Trim();
            string newPass = txtNewPassword.Text.Trim();
            string confirm = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(otp) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirm))
            { MessageBox.Show("All fields are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (!OtpService.Verify(email, otp))
            { MessageBox.Show("Invalid or expired verification code.", "Code Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            if (newPass != confirm)
            { MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (!ValidationHelper.IsStrongEnough(newPass))
            {
                MessageBox.Show(
                    "Password is too weak — " + ValidationHelper.PasswordStrengthLabel(ValidationHelper.PasswordStrength(newPass)) + ".\n" +
                    "Please use at least 6 characters with letters and numbers.",
                    "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string hashed = PasswordHelper.Hash(newPass);

                    foreach (string table in new[] { "Patients", "Doctors", "Nurses", "Admins" })
                    {
                        using (var cmdChk = new SqlCommand($"SELECT COUNT(*) FROM {table} WHERE Email=@E", conn))
                        {
                            cmdChk.Parameters.AddWithValue("@E", email);
                            if ((int)cmdChk.ExecuteScalar() == 0) continue;
                        }
                        using (var cmdUpd = new SqlCommand($"UPDATE {table} SET Password=@P WHERE Email=@E", conn))
                        {
                            cmdUpd.Parameters.AddWithValue("@P", hashed);
                            cmdUpd.Parameters.AddWithValue("@E", email);
                            cmdUpd.ExecuteNonQuery();
                        }
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }
                    MessageBox.Show("Email not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to reset password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
