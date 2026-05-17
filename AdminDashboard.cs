using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
            Theme.SidebarNav(btnManageDoctors);
            Theme.SidebarNav(btnManageNurses);
            Theme.SidebarNav(btnManagePatients);
            Theme.SidebarNav(btnViewAppointments);
            Theme.SidebarNav(btnViewNurseBookings);
            Theme.SidebarLogout(btnLogout);
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadDashboardStats();
            panelMain.SizeChanged += (s, ev) => PositionCards();
            PositionCards();
        }

        private void PositionCards()
        {
            int w       = panelMain.ClientSize.Width;
            int margin  = 20;
            int gap     = 10;
            int cardH   = 130;
            int cardY   = 20;
            int cardW   = Math.Max(160, (w - margin * 2 - gap * 3) / 4);

            int x = margin;
            card1.SetBounds(x, cardY, cardW, cardH); x += cardW + gap;
            card2.SetBounds(x, cardY, cardW, cardH); x += cardW + gap;
            card3.SetBounds(x, cardY, cardW, cardH); x += cardW + gap;
            card4.SetBounds(x, cardY, cardW, cardH);

            int iconLeft = Math.Max(10, cardW - 68);
            labelIcon1.Left = iconLeft;
            labelIcon2.Left = iconLeft;
            labelIcon3.Left = iconLeft;
            labelIcon4.Left = iconLeft;

            chartActivity.SetBounds(margin, 170,
                w - margin * 2,
                Math.Max(100, panelMain.ClientSize.Height - 190));
        }
        private void Header_MouseDown(object sender, MouseEventArgs e) => WindowHelper.StartDrag(this.Handle);
        private void btnMaximize_Click(object sender, EventArgs e) => WindowHelper.ToggleMaximize(this, btnMaximize);
        private void btnClose_Click(object sender, EventArgs e) => Application.Exit();
        private void btnLogout_Click(object sender, EventArgs e) { new LoginForm().Show(); this.Close(); }

        public void LoadDashboardStats()
        {
            try
            {
                using (SqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    lblDocCount.Text     = new SqlCommand("SELECT COUNT(*) FROM Doctors",  conn).ExecuteScalar().ToString();
                    lblPatientCount.Text = new SqlCommand("SELECT COUNT(*) FROM Patients", conn).ExecuteScalar().ToString();
                    lblNurseCount.Text   = new SqlCommand("SELECT COUNT(*) FROM Nurses",   conn).ExecuteScalar().ToString();

                    object rev = new SqlCommand(
                        "SELECT ISNULL(SUM(Amount), 0) FROM Payments WHERE Status='Paid'", conn)
                        .ExecuteScalar();
                    decimal revenue = rev != null ? Convert.ToDecimal(rev) : 0m;
                    lblRevenue.Text = revenue >= 1000
                        ? $"{revenue / 1000:0.#}K"
                        : revenue.ToString("0");

                    LoadChartData(conn);
                }
            }
            catch (Exception) { /* stats are non-critical; chart stays empty */ }
        }

        private void LoadChartData(SqlConnection conn)
        {
            try
            {
                string sql = @"
                    SELECT TOP 14
                        CAST(AppointmentDate AS DATE) AS [Day],
                        COUNT(*) AS Total
                    FROM Appointments
                    GROUP BY CAST(AppointmentDate AS DATE)
                    ORDER BY [Day] ASC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    chartActivity.Series["Bookings"].Points.Clear();
                    while (reader.Read())
                        chartActivity.Series["Bookings"].Points.AddXY(
                            Convert.ToDateTime(reader[0]).ToString("dd MMM"), reader[1]);
                }

                var area = chartActivity.ChartAreas[0];
                area.BackColor              = Color.FromArgb(248, 250, 252);
                area.AxisX.LineColor        = Color.FromArgb(226, 232, 240);
                area.AxisY.LineColor        = Color.FromArgb(226, 232, 240);
                area.AxisX.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
                area.AxisY.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
                area.AxisX.LabelStyle.Font  = new Font("Segoe UI", 8.5F);
                area.AxisY.LabelStyle.Font  = new Font("Segoe UI", 8.5F);
                chartActivity.BackColor     = Color.FromArgb(248, 250, 252);
            }
            catch (Exception) { /* chart is non-critical */ }
        }

        private void btnManageDoctors_Click(object sender, EventArgs e)    { new ManageDoctorsForm().ShowDialog();    LoadDashboardStats(); }
        private void btnManageNurses_Click(object sender, EventArgs e)     { new ManageNursesForm().ShowDialog();     LoadDashboardStats(); }
        private void btnManagePatients_Click(object sender, EventArgs e)   { new ManagePatientsForm().ShowDialog();   LoadDashboardStats(); }
        private void btnViewAppointments_Click(object sender, EventArgs e) { new ViewAppointmentsForm().ShowDialog(); }
        private void btnViewNurseBookings_Click(object sender, EventArgs e){ new ViewNurseBookingsForm().ShowDialog();}
    }
}
