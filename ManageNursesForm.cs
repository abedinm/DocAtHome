using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class ManageNursesForm : Form
    {
        private int  _editNurseId             = 0;
        private bool _suppressSelectionChange = false;

        public ManageNursesForm()
        {
            InitializeComponent();
            UIHelper.StyleGrid(dgvNurses);
            Theme.SuccessBtn(btnAdd);
            Theme.Outlined(btnDelete, Theme.Danger);
            Theme.Input(txtFirstName);
            Theme.Input(txtLastName);
            Theme.Input(txtEmail);
            Theme.Input(txtLicense);

            lbl1.ForeColor = Theme.TextSec;
            lbl2.ForeColor = Theme.TextSec;
            lbl3.ForeColor = Theme.TextSec;
            lbl4.ForeColor = Theme.TextSec;

            dgvNurses.SelectionChanged += DgvNurses_SelectionChanged;
        }

        // ── Grid data ─────────────────────────────────────────────────────────

        private void LoadNurses(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    var da = new SqlDataAdapter(
                        "SELECT NurseID, FirstName, LastName, Email, LicenseNumber FROM Nurses ORDER BY FirstName", conn);
                    var dt = new DataTable();
                    da.Fill(dt);

                    try { _suppressSelectionChange = true; dgvNurses.DataSource = dt; }
                    finally { _suppressSelectionChange = false; }
                }
                if (dgvNurses.Columns["NurseID"] != null)
                    dgvNurses.Columns["NurseID"].Visible = false;

                ResetToNewMode();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load nurses. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }

        // ── Edit mode ─────────────────────────────────────────────────────────

        private void DgvNurses_SelectionChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionChange || dgvNurses.CurrentRow == null)
            {
                if (!_suppressSelectionChange) ResetToNewMode();
                return;
            }

            var row = dgvNurses.CurrentRow;
            _editNurseId      = Convert.ToInt32(row.Cells["NurseID"].Value);
            txtFirstName.Text = row.Cells["FirstName"].Value?.ToString()    ?? "";
            txtLastName.Text  = row.Cells["LastName"].Value?.ToString()     ?? "";
            txtEmail.Text     = row.Cells["Email"].Value?.ToString()        ?? "";
            txtLicense.Text   = row.Cells["LicenseNumber"].Value?.ToString() ?? "";

            lblActionTitle.Text = "Edit Nurse";
            btnAdd.Text         = "💾  Update Nurse";
            Theme.Primary(btnAdd);
        }

        private void ResetToNewMode()
        {
            _editNurseId = 0;
            txtFirstName.Clear(); txtLastName.Clear();
            txtEmail.Clear();     txtLicense.Clear();
            lblActionTitle.Text = "Nurse Registration";
            btnAdd.Text         = "Register Nurse";
            Theme.SuccessBtn(btnAdd);
        }

        // ── Add / Update ──────────────────────────────────────────────────────

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName  = txtLastName.Text.Trim();
            string email     = txtEmail.Text.Trim();
            string license   = txtLicense.Text.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(email)     || string.IsNullOrEmpty(license))
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

            if (_editNurseId > 0)
                UpdateNurse(firstName, lastName, email, license);
            else
                InsertNurse(firstName, lastName, email, license);
        }

        private void InsertNurse(string firstName, string lastName, string email, string license)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string tempPassword = GeneratePassword();

                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmdChk = new SqlCommand("SELECT COUNT(*) FROM Nurses WHERE Email=@E", conn))
                    {
                        cmdChk.Parameters.AddWithValue("@E", email);
                        if ((int)cmdChk.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("A nurse with that email already exists.", "Duplicate",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (var cmd = new SqlCommand(@"
                        INSERT INTO Nurses
                        (FirstName, LastName, Email, LicenseNumber, Password, PhoneNumber, YearsOfExperience, IsAvailable)
                        VALUES (@FN, @LN, @E, @L, @Pass, '000', 1, 1)", conn))
                    {
                        cmd.Parameters.AddWithValue("@FN",   firstName);
                        cmd.Parameters.AddWithValue("@LN",   lastName);
                        cmd.Parameters.AddWithValue("@E",    email);
                        cmd.Parameters.AddWithValue("@L",    license);
                        cmd.Parameters.AddWithValue("@Pass", PasswordHelper.Hash(tempPassword));
                        cmd.ExecuteNonQuery();
                    }
                }

                Clipboard.SetText(tempPassword);
                MessageBox.Show(
                    $"Nurse registered!\n\nTemporary password:  {tempPassword}\n\n(Copied to clipboard — share with the nurse.)",
                    "Registration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNurses(null, null);
            }
            catch (Exception)
            {
                MessageBox.Show("Registration failed. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }

        private void UpdateNurse(string firstName, string lastName, string email, string license)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmdChk = new SqlCommand(
                        "SELECT COUNT(*) FROM Nurses WHERE Email=@E AND NurseID <> @ID", conn))
                    {
                        cmdChk.Parameters.AddWithValue("@E",  email);
                        cmdChk.Parameters.AddWithValue("@ID", _editNurseId);
                        if ((int)cmdChk.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Another nurse already uses that email.", "Duplicate",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (var cmd = new SqlCommand(
                        "UPDATE Nurses SET FirstName=@FN, LastName=@LN, Email=@E, LicenseNumber=@L WHERE NurseID=@ID",
                        conn))
                    {
                        cmd.Parameters.AddWithValue("@FN", firstName);
                        cmd.Parameters.AddWithValue("@LN", lastName);
                        cmd.Parameters.AddWithValue("@E",  email);
                        cmd.Parameters.AddWithValue("@L",  license);
                        cmd.Parameters.AddWithValue("@ID", _editNurseId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Nurse updated successfully.", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNurses(null, null);
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
            if (dgvNurses.CurrentRow == null)
            {
                MessageBox.Show("Please select a nurse to remove.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string name = $"{dgvNurses.CurrentRow.Cells["FirstName"].Value} {dgvNurses.CurrentRow.Cells["LastName"].Value}".Trim();
            if (MessageBox.Show($"Remove nurse {name}?",
                    "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            int id = Convert.ToInt32(dgvNurses.CurrentRow.Cells["NurseID"].Value);
            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("DELETE FROM Nurses WHERE NurseID = @ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadNurses(null, null);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot remove this nurse — they may have active bookings.", "Error",
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
