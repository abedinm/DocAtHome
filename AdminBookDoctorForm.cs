using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class AdminBookDoctorForm : Form
    {
        public AdminBookDoctorForm()
        {
            InitializeComponent();
            Theme.Primary(btnConfirm);
            Theme.Outlined(btnCancel, Theme.TextSec);
            Theme.Input(txtTime);
            Theme.Input(txtDescription);
        }

        private void AdminBookDoctorForm_Load(object sender, EventArgs e)
        {
            LoadData();
            dtpDate.Value    = DateTime.Today;
            txtTime.Text     = "09:00";
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

                    var daD = new SqlDataAdapter(
                        "SELECT DoctorID, FirstName + ' ' + LastName + '  —  ' + Specialty AS Name FROM Doctors WHERE IsAvailable=1 ORDER BY FirstName", conn);
                    var dtD = new DataTable(); daD.Fill(dtD);
                    cmbDoctors.DataSource    = dtD;
                    cmbDoctors.DisplayMember = "Name";
                    cmbDoctors.ValueMember   = "DoctorID";
                }
            }
            catch (Exception) { MessageBox.Show("Unable to load data. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { Cursor = Cursors.Default; }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbPatients.SelectedValue == null || cmbDoctors.SelectedValue == null)
            {
                MessageBox.Show("Please select both a patient and a doctor.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TimeSpan.TryParse(txtTime.Text.Trim(), out TimeSpan time))
            {
                MessageBox.Show("Please enter a valid time in HH:MM format (e.g. 14:30).", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime date     = dtpDate.Value.Date;
            int      patientId = Convert.ToInt32(cmbPatients.SelectedValue);
            int      doctorId  = Convert.ToInt32(cmbDoctors.SelectedValue);

            if (date < DateTime.Today)
            {
                MessageBox.Show("Appointment date cannot be in the past.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // Double-booking check
                    using (var cmdChk = new SqlCommand(@"
                        SELECT COUNT(*) FROM Appointments
                        WHERE DoctorID=@DID AND AppointmentDate=@D AND AppointmentTime=@T
                          AND Status NOT IN ('Rejected','Cancelled')", conn))
                    {
                        cmdChk.Parameters.AddWithValue("@DID", doctorId);
                        cmdChk.Parameters.AddWithValue("@D",   date);
                        cmdChk.Parameters.AddWithValue("@T",   time);
                        if ((int)cmdChk.ExecuteScalar() > 0)
                        {
                            MessageBox.Show(
                                "This doctor already has an appointment at that date and time.\nPlease choose a different slot.",
                                "Slot Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (var cmd = new SqlCommand(@"
                        INSERT INTO Appointments
                        (PatientID, DoctorID, AppointmentDate, AppointmentTime, Description, Status)
                        VALUES (@PID, @DID, @Date, @Time, @Desc, 'Accepted')", conn))
                    {
                        cmd.Parameters.AddWithValue("@PID",  patientId);
                        cmd.Parameters.AddWithValue("@DID",  doctorId);
                        cmd.Parameters.AddWithValue("@Date", date);
                        cmd.Parameters.AddWithValue("@Time", time);
                        cmd.Parameters.AddWithValue("@Desc", txtDescription.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Appointment created and confirmed.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to create appointment. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }

        private void Header_MouseDown(object sender, MouseEventArgs e) => WindowHelper.StartDrag(this.Handle);
        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}
