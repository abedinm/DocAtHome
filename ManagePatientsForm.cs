using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class ManagePatientsForm : Form
    {
        private const string SearchPlaceholder = "Search by name or email...";

        public ManagePatientsForm()
        {
            InitializeComponent();

            Theme.SidebarLogout(btnLogout);
            Theme.Primary(btnSearch);
            Theme.Outlined(btnRefresh, Theme.TextSec);
            Theme.Outlined(btnDelete, Theme.Danger);
            Theme.Input(txtSearch);

            txtSearch.Text      = SearchPlaceholder;
            txtSearch.ForeColor = Theme.TextMuted;
            txtSearch.Enter    += TxtSearch_Enter;
            txtSearch.Leave    += TxtSearch_Leave;

            UIHelper.StyleGrid(dgvPatients);
            dgvPatients.SelectionChanged += DgvPatients_SelectionChanged;

            this.Load += (s, e) => LoadPatients();
        }

        private void LoadPatients(string keyword = null)
        {
            try
            {
                string sql = string.IsNullOrWhiteSpace(keyword)
                    ? "SELECT PatientID, FirstName, LastName, Email, PhoneNumber FROM Patients ORDER BY FirstName"
                    : @"SELECT PatientID, FirstName, LastName, Email, PhoneNumber FROM Patients
                        WHERE FirstName + ' ' + LastName LIKE @K OR Email LIKE @K
                        ORDER BY FirstName";

                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var da = new SqlDataAdapter(sql, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                            da.SelectCommand.Parameters.AddWithValue("@K", "%" + keyword + "%");

                        var dt = new DataTable();
                        da.Fill(dt);
                        dgvPatients.DataSource = dt;
                    }
                }

                if (dgvPatients.Columns["PatientID"] != null)
                    dgvPatients.Columns["PatientID"].Visible = false;

                // Update stat count
                lblPatientCount.Text = dgvPatients.Rows.Count.ToString();

                // Reset selected info
                SetSelectedInfo(null);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load patients. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvPatients_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPatients.CurrentRow == null) { SetSelectedInfo(null); return; }
            var row = dgvPatients.CurrentRow;
            string first = row.Cells["FirstName"].Value?.ToString() ?? "";
            string last  = row.Cells["LastName"].Value?.ToString()  ?? "";
            string email = row.Cells["Email"].Value?.ToString()     ?? "—";
            string phone = row.Cells["PhoneNumber"].Value?.ToString() ?? "—";
            lblSelectedName.Text  = (first + " " + last).Trim();
            lblSelectedEmail.Text = email;
            lblSelectedPhone.Text = "📞 " + phone;
        }

        private void SetSelectedInfo(object _ )
        {
            lblSelectedName.Text  = "—";
            lblSelectedEmail.Text = "—";
            lblSelectedPhone.Text = "—";
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == SearchPlaceholder)
            {
                txtSearch.Text      = "";
                txtSearch.ForeColor = Theme.TextPri;
            }
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text      = SearchPlaceholder;
                txtSearch.ForeColor = Theme.TextMuted;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string kw = txtSearch.Text.Trim();
            if (kw == SearchPlaceholder) kw = "";
            LoadPatients(kw);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text      = SearchPlaceholder;
            txtSearch.ForeColor = Theme.TextMuted;
            LoadPatients();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPatients.CurrentRow == null)
            {
                MessageBox.Show("Please select a patient to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string name = lblSelectedName.Text;
            var confirm = MessageBox.Show(
                $"Delete patient \"{name}\" and all related records?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            int id = Convert.ToInt32(dgvPatients.CurrentRow.Cells["PatientID"].Value);
            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var tx = conn.BeginTransaction())
                    {
                        try
                        {
                            // Remove dependent records before the patient row
                            foreach (string sql in new[]
                            {
                                "DELETE FROM Payments      WHERE PatientID = @ID",
                                "DELETE FROM NurseBookings WHERE PatientID = @ID",
                                "DELETE FROM Appointments  WHERE PatientID = @ID",
                                "DELETE FROM Patients      WHERE PatientID = @ID"
                            })
                            {
                                using (var cmd = new SqlCommand(sql, conn, tx))
                                {
                                    cmd.Parameters.AddWithValue("@ID", id);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            tx.Commit();
                        }
                        catch { tx.Rollback(); throw; }
                    }
                }
                LoadPatients();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to delete this patient. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }

        private void Header_MouseDown(object sender, MouseEventArgs e) =>
            WindowHelper.StartDrag(this.Handle);

        private void btnMaximize_Click(object sender, EventArgs e) => WindowHelper.ToggleMaximize(this, btnMaximize);
        private void btnClose_Click(object sender, EventArgs e)  => this.Close();
        private void btnLogout_Click(object sender, EventArgs e) { new LoginForm().Show(); this.Close(); }
    }
}
