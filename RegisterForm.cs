using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            Theme.Primary(btnSignUp);
            Theme.Ghost(btnBack);
            Theme.Input(txtLastName);
            Theme.Input(txtEmail);
            Theme.Input(txtPhone);
            Theme.Input(txtPassword);

            txtLastName.MaxLength = 100;
            txtEmail.MaxLength    = 150;
            txtPhone.MaxLength    = 20;
            txtPassword.MaxLength = 128;

            txtPassword.TextChanged += TxtPassword_TextChanged;
            this.AcceptButton = btnSignUp;
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            int score = ValidationHelper.PasswordStrength(txtPassword.Text);
            lblStrength.Text      = ValidationHelper.PasswordStrengthLabel(score);
            lblStrength.ForeColor = ValidationHelper.PasswordStrengthColor(score);
        }

        private void Header_MouseDown(object sender, MouseEventArgs e) =>
            WindowHelper.StartDrag(this.Handle);

        private void btnClose_Click(object sender, EventArgs e) => Application.Exit();

        private void btnBack_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string fullName = txtLastName.Text.Trim();
            string email    = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string phone    = txtPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Name, Email, and Password are required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidationHelper.IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidationHelper.IsStrongEnough(password))
            {
                MessageBox.Show(
                    "Your password is too weak.\n\n" +
                    ValidationHelper.PasswordStrengthLabel(ValidationHelper.PasswordStrength(password)) +
                    "\n\nTry adding uppercase letters, numbers, or symbols.",
                    "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int spaceIndex = fullName.IndexOf(' ');
            string firstName = spaceIndex > 0 ? fullName.Substring(0, spaceIndex) : fullName;
            string lastName  = spaceIndex > 0 ? fullName.Substring(spaceIndex + 1).Trim() : "";

            try
            {
                using (SqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    using (SqlCommand cmdCheck = new SqlCommand(
                        "SELECT COUNT(*) FROM Patients WHERE Email = @E", conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@E", email);
                        if ((int)cmdCheck.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("This email is already registered.", "Duplicate",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string sql = @"
                        INSERT INTO Patients
                        (FirstName, LastName, Email, Password, PhoneNumber, Address, City, State, ZipCode, DateOfBirth, MedicalHistory)
                        VALUES
                        (@FN, @LN, @Email, @Pass, @Phone, 'N/A', 'N/A', 'N/A', '0000', '2000-01-01', 'None')";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FN",    firstName);
                        cmd.Parameters.AddWithValue("@LN",    lastName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Pass",  PasswordHelper.Hash(password));
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Registration successful! Please log in.", "Welcome",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new LoginForm().Show();
                    this.Hide();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Registration failed. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
