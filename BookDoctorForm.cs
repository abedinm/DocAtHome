using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class BookDoctorForm : Form
    {
        private string _patientEmail;

        public BookDoctorForm(string patientEmail)
        {
            InitializeComponent();
            _patientEmail = patientEmail;
            Theme.Primary(btnSave);
            Theme.Outlined(btnCancel, Theme.TextSec);
            this.AcceptButton = btnSave;
        }

        private void Header_MouseDown(object sender, MouseEventArgs e) =>
            WindowHelper.StartDrag(this.Handle);

        private void BookDoctorForm_Load(object sender, EventArgs e)
        {
            LoadDoctors();
            dtpDate.Value = DateTime.Today;
            dtpTime.Value = DateTime.Today.AddHours(9);
        }

        private void LoadDoctors()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var da = new SqlDataAdapter(
                        "SELECT DoctorID, FirstName + ' ' + LastName + '  —  ' + Specialty AS DoctorName FROM Doctors WHERE IsAvailable=1 ORDER BY FirstName",
                        conn))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        cmbDoctors.DataSource    = dt;
                        cmbDoctors.DisplayMember = "DoctorName";
                        cmbDoctors.ValueMember   = "DoctorID";
                        cmbDoctors.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load doctors. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbDoctors.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a doctor.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime date     = dtpDate.Value.Date;
            TimeSpan time     = dtpTime.Value.TimeOfDay;
            string   desc     = txtDescription.Text.Trim();
            int      doctorId = Convert.ToInt32(cmbDoctors.SelectedValue);

            if (date < DateTime.Today)
            {
                MessageBox.Show("Appointment date cannot be in the past.", "Invalid Date",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

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

                    int patientId;
                    using (var cmdPid = new SqlCommand("SELECT PatientID FROM Patients WHERE Email=@E", conn))
                    {
                        cmdPid.Parameters.AddWithValue("@E", _patientEmail);
                        var r = cmdPid.ExecuteScalar();
                        if (r == null) { MessageBox.Show("Patient account not found.", "Error"); return; }
                        patientId = (int)r;
                    }

                    using (var cmdIns = new SqlCommand(@"
                        INSERT INTO Appointments
                            (PatientID, DoctorID, NurseID, AppointmentDate, AppointmentTime, Description, Status)
                        VALUES (@PID, @DID, NULL, @D, @T, @Desc, 'Pending')", conn))
                    {
                        cmdIns.Parameters.AddWithValue("@PID",  patientId);
                        cmdIns.Parameters.AddWithValue("@DID",  doctorId);
                        cmdIns.Parameters.AddWithValue("@D",    date);
                        cmdIns.Parameters.AddWithValue("@T",    time);
                        cmdIns.Parameters.AddWithValue("@Desc", desc);
                        cmdIns.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Appointment request sent!\nYou will be notified once the doctor confirms.",
                    "Booked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to book appointment. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }
    }
}
