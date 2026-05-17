using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class BookNurseForm : Form
    {
        private string _patientEmail;
        private Label  _lblPrice;

        public BookNurseForm(string patientEmail)
        {
            InitializeComponent();
            _patientEmail = patientEmail;
            Theme.Primary(btnSave);
            Theme.Outlined(btnCancel, Theme.TextSec);
            this.AcceptButton = btnSave;

            // Dynamic pricing label — sits between service combo and buttons
            _lblPrice = new Label
            {
                Font      = new Font("Segoe UI Semibold", 10F),
                ForeColor = Theme.Success,
                Location  = new Point(40, 289),
                Size      = new Size(370, 22),
                Text      = ""
            };
            this.Controls.Add(_lblPrice);
            _lblPrice.BringToFront();

            cmbService.SelectedIndexChanged += CmbService_SelectedIndexChanged;
        }

        private void BookNurseForm_Load(object sender, EventArgs e)
        {
            LoadNurses();
            LoadServices();
            dtpDate.Value = DateTime.Today;
        }

        private void Header_MouseDown(object sender, MouseEventArgs e) =>
            WindowHelper.StartDrag(this.Handle);

        private void LoadNurses()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    var da = new SqlDataAdapter(
                        "SELECT NurseID, FirstName + ' ' + LastName AS Name FROM Nurses WHERE IsAvailable=1 ORDER BY FirstName", conn);
                    var dt = new DataTable();
                    da.Fill(dt);
                    cmbNurses.DataSource    = dt;
                    cmbNurses.DisplayMember = "Name";
                    cmbNurses.ValueMember   = "NurseID";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load nurses. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }

        private void LoadServices()
        {
            cmbService.Items.AddRange(new string[]
            {
                "Injection", "Wound Care", "IV Drip", "Elderly Care", "Physical Therapy"
            });
            cmbService.SelectedIndex = 0;
        }

        private void CmbService_SelectedIndexChanged(object sender, EventArgs e)
        {
            string service = cmbService.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(service)) { _lblPrice.Text = ""; return; }
            decimal price = ServicePricing.ForNurseService(service);
            _lblPrice.Text = $"Service fee:  BDT {price:0.00}";
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbNurses.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a nurse.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Visit date cannot be in the past.", "Invalid Date",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string serviceType = cmbService.SelectedItem?.ToString() ?? "General";

            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    int pid;
                    using (var cmdPid = new SqlCommand("SELECT PatientID FROM Patients WHERE Email=@E", conn))
                    {
                        cmdPid.Parameters.AddWithValue("@E", _patientEmail);
                        var r = cmdPid.ExecuteScalar();
                        if (r == null) { MessageBox.Show("Patient account not found.", "Error"); return; }
                        pid = (int)r;
                    }

                    using (var cmd = new SqlCommand(@"
                        INSERT INTO NurseBookings (PatientID, NurseID, BookingDate, ServiceType, Status)
                        VALUES (@PID, @NID, @D, @S, 'Pending')", conn))
                    {
                        cmd.Parameters.AddWithValue("@PID", pid);
                        cmd.Parameters.AddWithValue("@NID", cmbNurses.SelectedValue);
                        cmd.Parameters.AddWithValue("@D",   dtpDate.Value.Date);
                        cmd.Parameters.AddWithValue("@S",   serviceType);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Nurse service requested!\nYou will be notified once a nurse confirms.",
                    "Request Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to request nurse service. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }
    }
}
