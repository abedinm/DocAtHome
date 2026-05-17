using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class ViewAppointmentsForm : Form
    {
        public ViewAppointmentsForm()
        {
            InitializeComponent();
            ApplyStyling();
            InitFilters();
            LoadAppointments();
        }

        private void ApplyStyling()
        {
            UIHelper.StyleGrid(dgvAppointments);
        }

        private void InitFilters()
        {
            cmbStatusFilter.Items.AddRange(new string[] { "All", "Pending", "Accepted", "Rejected", "Completed" });
            cmbStatusFilter.SelectedIndex = 0;
        }

        private void LoadAppointments()
        {
            string keyword = txtSearch.Text.Trim();
            string status  = cmbStatusFilter.SelectedItem?.ToString() ?? "All";

            try
            {
                using (SqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT A.AppointmentID, P.FirstName + ' ' + P.LastName AS Patient,
                                 D.FirstName + ' ' + D.LastName AS Doctor, A.AppointmentDate as [Date],
                                 A.AppointmentTime as [Time], A.Status,
                                 CASE WHEN EXISTS (SELECT 1 FROM Payments Pay WHERE Pay.RelatedID = A.AppointmentID AND Pay.RelatedType = 'Appointment')
                                 THEN 'Paid' ELSE 'Unpaid' END AS PaymentStatus
                                 FROM Appointments A JOIN Patients P ON A.PatientID = P.PatientID
                                 JOIN Doctors D ON A.DoctorID = D.DoctorID
                                 WHERE (P.FirstName LIKE @K OR D.FirstName LIKE @K OR A.Status LIKE @K)";

                    if (status != "All") sql += " AND A.Status = @Status";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@K", "%" + keyword + "%");
                    if (status != "All") da.SelectCommand.Parameters.AddWithValue("@Status", status);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvAppointments.DataSource = dt;

                    if (dgvAppointments.Columns["AppointmentID"] != null)
                        dgvAppointments.Columns["AppointmentID"].Visible = false;
                }
            }
            catch (Exception) { MessageBox.Show("Unable to load appointments. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvAppointments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            UIHelper.FormatStatusCell(e, dgvAppointments, "Status", "PaymentStatus");
            UIHelper.FormatTimeCell(e, dgvAppointments);
        }

        private void btnAdminBook_Click(object sender, EventArgs e)
        {
            AdminBookDoctorForm f = new AdminBookDoctorForm();
            f.ShowDialog();
            LoadAppointments(); // Refresh the grid to show the new booking
        }

        private void panelHeader_SizeChanged(object sender, EventArgs e)
        {
            btnClose.Location    = new System.Drawing.Point(panelHeader.Width - 36, 25);
            btnMaximize.Location = new System.Drawing.Point(panelHeader.Width - 72, 25);
        }

        private void Header_MouseDown(object sender, MouseEventArgs e) => WindowHelper.StartDrag(this.Handle);
        private void btnMaximize_Click(object sender, EventArgs e) => WindowHelper.ToggleMaximize(this, btnMaximize);
        private void btnClose_Click(object sender, EventArgs e) => this.Close();
        private void btnRefresh_Click(object sender, EventArgs e) => LoadAppointments();
        private void btnSearch_Click(object sender, EventArgs e) => LoadAppointments();
        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e) => LoadAppointments();
    }
}
