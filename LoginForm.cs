using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
            Theme.Primary(btnLogin);
            Theme.Ghost(btnRegister);
            Theme.Input(txtEmail);
            Theme.Input(txtPassword);
        }

        private void Header_MouseDown(object sender, MouseEventArgs e) =>
            WindowHelper.StartDrag(this.Handle);

        private void btnClose_Click(object sender, EventArgs e) => Application.Exit();

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e) =>
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email    = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidationHelper.IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (LoginAttemptTracker.IsLocked(email))
            {
                int mins = (int)Math.Ceiling(LoginAttemptTracker.LockRemaining(email).TotalMinutes);
                MessageBox.Show(
                    $"This account is temporarily locked after too many failed attempts.\nPlease try again in {mins} minute{(mins == 1 ? "" : "s")}.",
                    "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                bool authenticated = false;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    if (CheckUser(conn, "Admins", email, password))
                    { new AdminDashboard().Show(); this.Hide(); authenticated = true; }
                    else if (CheckUser(conn, "Patients", email, password))
                    { new MainForm(email).Show(); this.Hide(); authenticated = true; }
                    else if (CheckUser(conn, "Doctors", email, password))
                    { new DoctorDashboard(email).Show(); this.Hide(); authenticated = true; }
                    else if (CheckUser(conn, "Nurses", email, password))
                    { new NurseDashboard(email).Show(); this.Hide(); authenticated = true; }
                }

                if (authenticated)
                {
                    LoginAttemptTracker.RecordSuccess(email);
                }
                else
                {
                    LoginAttemptTracker.RecordFailure(email);
                    int remaining = LoginAttemptTracker.AttemptsRemaining(email);
                    if (remaining > 0)
                        MessageBox.Show(
                            $"Invalid email or password. {remaining} attempt{(remaining == 1 ? "" : "s")} remaining before lockout.",
                            "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show(
                            "Too many failed attempts. Account locked for 15 minutes.",
                            "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Unable to connect to the server. Please check your connection and try again.",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckUser(SqlConnection conn, string tableName, string email, string password)
        {
            if (tableName != "Admins" && tableName != "Patients" &&
                tableName != "Doctors" && tableName != "Nurses")
                return false;

            using (var cmd = new SqlCommand($"SELECT Password FROM {tableName} WHERE Email = @E", conn))
            {
                cmd.Parameters.AddWithValue("@E", email);
                object result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value) return false;

                string stored = result.ToString();
                return stored == password;
            }
        }

        private void linkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var f = new ForgotPasswordForm())
            {
                if (f.ShowDialog() == DialogResult.OK)
                    MessageBox.Show("Password changed. You can now log in with your new password.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            new RegisterForm().Show();
            this.Hide();
        }

    }
}
