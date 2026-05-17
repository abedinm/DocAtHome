using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class ManageDoctorsForm : Form
    {
        private int  _editDoctorId            = 0;
        private bool _suppressSelectionChange = false;

        public ManageDoctorsForm()
        {
            InitializeComponent();
            UIHelper.StyleGrid(dgvDoctors);
            Theme.SuccessBtn(btnAdd);
            Theme.Outlined(btnDelete, Theme.Danger);
            Theme.Input(txtFirstName);
            Theme.Input(txtLastName);
            Theme.Input(txtEmail);
            Theme.Input(txtSpecialty);

            lbl1.ForeColor = Theme.TextSec;
            lbl2.ForeColor = Theme.TextSec;
            lbl3.ForeColor = Theme.TextSec;
            lbl4.ForeColor = Theme.TextSec;

            dgvDoctors.SelectionChanged += DgvDoctors_SelectionChanged;
        }

        // ── Grid data ─────────────────────────────────────────────────────────

        private void LoadDoctors(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    var da = new SqlDataAdapter(
                        "SELECT DoctorID, FirstName, LastName, Email, Specialty FROM Doctors ORDER BY FirstName", conn);
                    var dt = new DataTable();
                    da.Fill(dt);

                    try { _suppressSelectionChange = true; dgvDoctors.DataSource = dt; }
                    finally { _suppressSelectionChange = false; }
                }
                if (dgvDoctors.Columns["DoctorID"] != null)
                    dgvDoctors.Columns["DoctorID"].Visible = false;

                ResetToNewMode();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load doctors. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }

        // ── Edit mode ─────────────────────────────────────────────────────────

        private void DgvDoctors_SelectionChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionChange || dgvDoctors.CurrentRow == null)
            {
                if (!_suppressSelectionChange) ResetToNewMode();
                return;
            }

            var row = dgvDoctors.CurrentRow;
            _editDoctorId     = Convert.ToInt32(row.Cells["DoctorID"].Value);
            txtFirstName.Text = row.Cells["FirstName"].Value?.ToString() ?? "";
            txtLastName.Text  = row.Cells["LastName"].Value?.ToString()  ?? "";
            txtEmail.Text     = row.Cells["Email"].Value?.ToString()     ?? "";
            txtSpecialty.Text = row.Cells["Specialty"].Value?.ToString() ?? "";

            lblActionTitle.Text = "Edit Doctor";
            btnAdd.Text         = "💾  Update Doctor";
            Theme.Primary(btnAdd);
        }

        private void ResetToNewMode()
        {
            _editDoctorId = 0;
            txtFirstName.Clear(); txtLastName.Clear();
            txtEmail.Clear();     txtSpecialty.Clear();
            lblActionTitle.Text = "Doctor Registration";
            btnAdd.Text         = "Register Doctor";
            Theme.SuccessBtn(btnAdd);
        }

        // ── Add / Update ──────────────────────────────────────────────────────

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName  = txtLastName.Text.Trim();
            string email     = txtEmail.Text.Trim();
            string specialty = txtSpecialty.Text.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(email)     || string.IsNullOrEmpty(specialty))
            {
                MessageBox.Show("All fields are required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidationHelper.IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_editDoctorId > 0)
                UpdateDoctor(firstName, lastName, email, specialty);
            else
                InsertDoctor(firstName, lastName, email, specialty);
        }

        private void InsertDoctor(string firstName, string lastName, string email, string specialty)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string tempPassword = GeneratePassword();

                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmdChk = new SqlCommand("SELECT COUNT(*) FROM Doctors WHERE Email=@E", conn))
                    {
                        cmdChk.Parameters.AddWithValue("@E", email);
                        if ((int)cmdChk.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("A doctor with that email already exists.", "Duplicate",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (var cmd = new SqlCommand(@"
                        INSERT INTO Doctors
                        (FirstName, LastName, Email, Specialty, Password, PhoneNumber, HospitalAffiliation, YearsOfExperience, IsAvailable)
                        VALUES (@FN, @LN, @E, @S, @Pass, '000', 'General', 1, 1)", conn))
                    {
                        cmd.Parameters.AddWithValue("@FN",   firstName);
                        cmd.Parameters.AddWithValue("@LN",   lastName);
                        cmd.Parameters.AddWithValue("@E",    email);
                        cmd.Parameters.AddWithValue("@S",    specialty);
                        cmd.Parameters.AddWithValue("@Pass", PasswordHelper.Hash(tempPassword));
                        cmd.ExecuteNonQuery();
                    }
                }

                Clipboard.SetText(tempPassword);
                MessageBox.Show(
                    $"Doctor registered!\n\nTemporary password:  {tempPassword}\n\n(Copied to clipboard — share with the doctor.)",
                    "Registration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDoctors(null, null);
            }
            catch (Exception)
            {
                MessageBox.Show("Registration failed. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }

        private void UpdateDoctor(string firstName, string lastName, string email, string specialty)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // Check if email is taken by a DIFFERENT doctor
                    using (var cmdChk = new SqlCommand(
                        "SELECT COUNT(*) FROM Doctors WHERE Email=@E AND DoctorID <> @ID", conn))
                    {
                        cmdChk.Parameters.AddWithValue("@E",  email);
                        cmdChk.Parameters.AddWithValue("@ID", _editDoctorId);
                        if ((int)cmdChk.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Another doctor already uses that email.", "Duplicate",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (var cmd = new SqlCommand(
                        "UPDATE Doctors SET FirstName=@FN, LastName=@LN, Email=@E, Specialty=@S WHERE DoctorID=@ID",
                        conn))
                    {
                        cmd.Parameters.AddWithValue("@FN", firstName);
                        cmd.Parameters.AddWithValue("@LN", lastName);
                        cmd.Parameters.AddWithValue("@E",  email);
                        cmd.Parameters.AddWithValue("@S",  specialty);
                        cmd.Parameters.AddWithValue("@ID", _editDoctorId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Doctor updated successfully.", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDoctors(null, null);
            }
            catch (Exception)
            {
                MessageBox.Show("Update failed. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }

        // ── Delete ────────────────────────────────────────────────────────────

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDoctors.CurrentRow == null)
            {
                MessageBox.Show("Please select a doctor to remove.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string name = $"{dgvDoctors.CurrentRow.Cells["FirstName"].Value} {dgvDoctors.CurrentRow.Cells["LastName"].Value}".Trim();
            if (MessageBox.Show($"Remove Dr. {name}?\nThis will also remove their appointment history.",
                    "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            int id = Convert.ToInt32(dgvDoctors.CurrentRow.Cells["DoctorID"].Value);
            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("DELETE FROM Doctors WHERE DoctorID = @ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadDoctors(null, null);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot remove this doctor — they may have active appointments.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private static string GeneratePassword(int length = 12)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789!@#";
            var buf = new byte[length];
            using (var rng = new RNGCryptoServiceProvider()) rng.GetBytes(buf);
            var sb = new StringBuilder();
            foreach (byte b in buf) sb.Append(chars[b % chars.Length]);
            return sb.ToString();
        }

        private void Header_MouseDown(object sender, MouseEventArgs e) => WindowHelper.StartDrag(this.Handle);
        private void btnMaximize_Click(object sender, EventArgs e) => WindowHelper.ToggleMaximize(this, btnMaximize);
        private void btnClose_Click(object sender, EventArgs e) => this.Close();
        private void btnLogout_Click(object sender, EventArgs e) { new LoginForm().Show(); this.Close(); }
    }
}
