using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class ViewNurseBookingsForm : Form
    {
        public ViewNurseBookingsForm()
        {
            InitializeComponent();
            ApplyStyling();
            InitFilters();

            // Call LoadBookings manually on startup
            LoadBookings(null, null);
        }

        private void ApplyStyling()
        {
            UIHelper.StyleGrid(dgvBookings);
        }

        private void InitFilters()
        {
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.AddRange(new string[] { "All", "Pending", "Accepted", "Rejected", "Completed" });
            cmbStatusFilter.SelectedIndex = 0;
            txtSearch.Text = "";
        }

        private void dgvBookings_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            UIHelper.FormatStatusCell(e, dgvBookings, "Status", "PaymentStatus");
            UIHelper.FormatTimeCell(e, dgvBookings);
        }

        // FIXED: Added (object sender, EventArgs e) to match delegate 'EventHandler'
        private void LoadBookings(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            string status = (cmbStatusFilter.SelectedItem != null) ? cmbStatusFilter.SelectedItem.ToString() : "All";

            try
            {
                using (SqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            B.BookingID,
                            P.FirstName + ' ' + P.LastName AS Patient,
                            N.FirstName + ' ' + N.LastName AS Nurse,
                            B.BookingDate AS [Date],
                            B.ServiceType,
                            B.Status,
                            CASE 
                                WHEN EXISTS (
                                    SELECT 1 FROM Payments Pay
                                    WHERE Pay.RelatedType = 'NurseBooking'
                                      AND Pay.RelatedID = B.BookingID
                                      AND Pay.Status = 'Paid'
                                ) THEN 'Paid'
                                ELSE 'Unpaid'
                            END AS PaymentStatus
                        FROM NurseBookings B
                        JOIN Patients P ON B.PatientID = P.PatientID
                        JOIN Nurses  N ON B.NurseID  = N.NurseID
                        WHERE (P.FirstName LIKE @K OR N.FirstName LIKE @K OR B.ServiceType LIKE @K)";

                    if (status != "All") sql += " AND B.Status = @Status";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@K", "%" + keyword + "%");
                        if (status != "All") da.SelectCommand.Parameters.AddWithValue("@Status", status);

                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvBookings.DataSource = dt;

                        if (dgvBookings.Columns["BookingID"] != null)
                            dgvBookings.Columns["BookingID"].Visible = false;
                    }
                }
            }
            catch (Exception) { MessageBox.Show("Unable to load nurse bookings. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void panelHeader_SizeChanged(object sender, EventArgs e)
        {
            btnClose.Location    = new System.Drawing.Point(panelHeader.Width - 36, 25);
            btnMaximize.Location = new System.Drawing.Point(panelHeader.Width - 72, 25);
        }

        private void Header_MouseDown(object sender, MouseEventArgs e) => WindowHelper.StartDrag(this.Handle);
        private void btnMaximize_Click(object sender, EventArgs e) => WindowHelper.ToggleMaximize(this, btnMaximize);
        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadBookings(null, null);
        }

        private void btnSearch_Click(object sender, EventArgs e) => LoadBookings(null, null);
        private void btnAdminBookNurse_Click(object sender, EventArgs e)
        {
            // Ensure you have created AdminBookNurseForm.cs first
            AdminBookNurseForm f = new AdminBookNurseForm();
            f.ShowDialog();
            LoadBookings(null, null); // Refresh list after booking
        }


        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e) => LoadBookings(null, null);
    }
}
