using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class AdminBookNurseForm : Form
    {
        public AdminBookNurseForm()
        {
            InitializeComponent();
            Theme.Primary(btnConfirm);
            Theme.Outlined(btnCancel, Theme.TextSec);
        }

        private void AdminBookNurseForm_Load(object sender, EventArgs e)
        {
            LoadData();
            cmbService.Items.AddRange(new string[] { "Injection", "Wound Care", "IV Drip", "Elderly Care", "Physical Therapy" });
            cmbService.SelectedIndex = 0;
            dtpDate.Value = DateTime.Today;
        }

        private void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    var daP = new SqlDataAdapter(
                        "SELECT PatientID, FirstName + ' ' + LastName AS Name FROM Patients ORDER BY FirstName", conn);
                    var dtP = new DataTable(); daP.Fill(dtP);
                    cmbPatients.DataSource    = dtP;
                    cmbPatients.DisplayMember = "Name";
                    cmbPatients.ValueMember   = "PatientID";

                    var daN = new SqlDataAdapter(
                        "SELECT NurseID, FirstName + ' ' + LastName AS Name FROM Nurses WHERE IsAvailable=1 ORDER BY FirstName", conn);
                    var dtN = new DataTable(); daN.Fill(dtN);
                    cmbNurses.DataSource    = dtN;
                    cmbNurses.DisplayMember = "Name";
                    cmbNurses.ValueMember   = "NurseID";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load data. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbPatients.SelectedValue == null || cmbNurses.SelectedValue == null)
            {
                MessageBox.Show("Please select both a patient and a nurse.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Visit date cannot be in the past.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(@"
                        INSERT INTO NurseBookings (PatientID, NurseID, BookingDate, ServiceType, Status)
                        VALUES (@PID, @NID, @Date, @Service, 'Accepted')", conn))
                    {
                        cmd.Parameters.AddWithValue("@PID",     cmbPatients.SelectedValue);
                        cmd.Parameters.AddWithValue("@NID",     cmbNurses.SelectedValue);
                        cmd.Parameters.AddWithValue("@Date",    dtpDate.Value.Date);
                        cmd.Parameters.AddWithValue("@Service", cmbService.SelectedItem.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Nurse assigned successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to assign nurse. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }

        private void Header_MouseDown(object sender, MouseEventArgs e) => WindowHelper.StartDrag(this.Handle);
        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}
