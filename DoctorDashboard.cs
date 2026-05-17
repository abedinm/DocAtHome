using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class DoctorDashboard : Form
    {
        private string _doctorEmail;
        private int    _doctorId;

        // Dynamic controls
        private Label _lblWelcome;
        private Panel _panelStats;
        private Label _lblStatPending, _lblStatToday, _lblStatTotal;

        public DoctorDashboard(string doctorEmail)
        {
            InitializeComponent();
            _doctorEmail = doctorEmail;
            SetupLayout();
            Theme.SidebarNav(btnRefresh);
            Theme.SidebarLogout(btnLogout);
            Theme.SuccessBtn(btnAccept);
            Theme.Outlined(btnReject, Theme.Danger);
        }

        // ── Layout ────────────────────────────────────────────────────────────

        private void SetupLayout()
        {
            // Remove Designer anchors — RepositionMainControls drives all placement
            panelGridCard.Anchor = AnchorStyles.None;
            btnAccept.Anchor     = AnchorStyles.None;
            btnReject.Anchor     = AnchorStyles.None;

            // Welcome label
            _lblWelcome = new Label
            {
                Text      = "Welcome, Doctor",
                Font      = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location  = new Point(35, 18),
                AutoSize  = true,
                Anchor    = AnchorStyles.None
            };
            panelMain.Controls.Add(_lblWelcome);

            // Stat cards strip
            _panelStats = new Panel
            {
                Location  = new Point(40, 68),
                Size      = new Size(940, 85),
                BackColor = Color.Transparent,
                Anchor    = AnchorStyles.None
            };
            panelMain.Controls.Add(_panelStats);

            _lblStatPending = BuildStatCard(0,   "Pending Requests",     Color.FromArgb(245, 158, 11));
            _lblStatToday   = BuildStatCard(316, "Today's Appointments",  Color.FromArgb(59, 130, 246));
            _lblStatTotal   = BuildStatCard(632, "Total Appointments",    Color.FromArgb(16, 185, 129));

            panelMain.SizeChanged += RepositionMainControls;
        }

        private void RepositionMainControls(object sender, EventArgs e)
        {
            int w     = panelMain.ClientSize.Width;
            int h     = panelMain.ClientSize.Height;
            int mar   = 40;
            int gridW = Math.Max(200, w - mar * 2);
            int btnY  = h - 58;
            int gridH = Math.Max(80, btnY - 165);

            _panelStats.SetBounds(mar, 68, gridW, 85);
            panelGridCard.SetBounds(mar, 163, gridW, gridH);

            btnAccept.SetBounds(w - 210, btnY, 185, 45);
            btnReject.SetBounds(w - 410, btnY, 185, 45);
        }

        private Label BuildStatCard(int x, string title, Color accent)
        {
            var card = new Panel { Location = new Point(x, 0), Size = new Size(298, 82), BackColor = Color.White };
            card.Controls.Add(new Panel { Location = new Point(0, 0), Size = new Size(5, 82), BackColor = accent });
            card.Controls.Add(new Label { Text = title, Font = new Font("Segoe UI", 9F), ForeColor = Color.FromArgb(100, 116, 139), Location = new Point(18, 12), AutoSize = true });
            var lbl = new Label { Text = "—", Font = new Font("Segoe UI", 24F, FontStyle.Bold), ForeColor = Color.FromArgb(15, 23, 42), Location = new Point(16, 32), AutoSize = true };
            card.Controls.Add(lbl);
            _panelStats.Controls.Add(card);
            return lbl;
        }

        // ── Load sequence ─────────────────────────────────────────────────────

        private async void DoctorDashboard_Load(object sender, EventArgs e)
        {
            RepositionMainControls(null, EventArgs.Empty);
            UIHelper.StyleGrid(dgvAppointments);
            await LoadDoctorInfoAsync();
            await Task.WhenAll(LoadAppointmentsAsync(), LoadStatsAsync());
        }

        private async Task LoadDoctorInfoAsync()
        {
            try
            {
                var (id, firstName, available) = await Task.Run(() =>
                {
                    using (var conn = DBConfig.GetConnection())
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand(
                            "SELECT DoctorID, FirstName, IsAvailable FROM Doctors WHERE Email=@E", conn))
                        {
                            cmd.Parameters.AddWithValue("@E", _doctorEmail);
                            using (var r = cmd.ExecuteReader())
                            {
                                if (r.Read()) return (r.GetInt32(0), r.GetString(1), r.GetBoolean(2));
                                return (0, "", false);
                            }
                        }
                    }
                });

                _doctorId = id;
                if (!string.IsNullOrEmpty(firstName))
                    _lblWelcome.Text = $"Welcome, Dr. {firstName}";
                chkAvailable.Checked = available;
            }
            catch (Exception) { /* non-critical */ }
        }

        // ── Appointments ──────────────────────────────────────────────────────

        private async Task LoadAppointmentsAsync()
        {
            if (_doctorId == 0) return;
            try
            {
                var dt = await Task.Run(() =>
                {
                    using (var conn = DBConfig.GetConnection())
                    {
                        conn.Open();
                        const string sql = @"
                            SELECT
                                A.AppointmentID,
                                P.FirstName + ' ' + P.LastName AS Patient,
                                A.AppointmentDate AS [Date],
                                A.AppointmentTime AS [Time],
                                A.Status,
                                CASE
                                    WHEN EXISTS (
                                        SELECT 1 FROM Payments Pay
                                        WHERE Pay.PatientID  = A.PatientID
                                          AND Pay.RelatedType = 'Appointment'
                                          AND Pay.RelatedID   = A.AppointmentID
                                          AND Pay.Status      = 'Paid'
                                    ) THEN 'Paid'
                                    ELSE 'Unpaid'
                                END AS PaymentStatus
                            FROM Appointments A
                            JOIN Patients P ON A.PatientID = P.PatientID
                            WHERE A.DoctorID = @DID
                            ORDER BY A.AppointmentDate DESC";

                        using (var da = new SqlDataAdapter(sql, conn))
                        {
                            da.SelectCommand.Parameters.AddWithValue("@DID", _doctorId);
                            var table = new DataTable();
                            da.Fill(table);
                            return table;
                        }
                    }
                });

                dgvAppointments.DataSource = dt;
                if (dgvAppointments.Columns["AppointmentID"] != null)
                    dgvAppointments.Columns["AppointmentID"].Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load appointments. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Stats ─────────────────────────────────────────────────────────────

        private async Task LoadStatsAsync()
        {
            if (_doctorId == 0) return;
            try
            {
                var (pending, today, total) = await Task.Run(() =>
                {
                    using (var conn = DBConfig.GetConnection())
                    {
                        conn.Open();
                        int p = Scalar(conn,
                            "SELECT COUNT(*) FROM Appointments WHERE DoctorID=@D AND Status='Pending'");
                        int t = Scalar(conn,
                            "SELECT COUNT(*) FROM Appointments WHERE DoctorID=@D AND AppointmentDate=CAST(GETDATE() AS DATE) AND Status NOT IN ('Rejected','Cancelled')");
                        int all = Scalar(conn,
                            "SELECT COUNT(*) FROM Appointments WHERE DoctorID=@D");
                        return (p, t, all);
                    }
                });

                _lblStatPending.Text = pending.ToString();
                _lblStatToday.Text   = today.ToString();
                _lblStatTotal.Text   = total.ToString();
            }
            catch { /* stats are non-critical */ }
        }

        private int Scalar(SqlConnection conn, string sql)
        {
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@D", _doctorId);
                return (int)(cmd.ExecuteScalar() ?? 0);
            }
        }

        // ── Accept / Reject ───────────────────────────────────────────────────

        private async Task UpdateStatusAsync(string status)
        {
            if (dgvAppointments.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvAppointments.CurrentRow.Cells["AppointmentID"].Value);
            try
            {
                await Task.Run(() =>
                {
                    using (var conn = DBConfig.GetConnection())
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand(
                            "UPDATE Appointments SET Status=@Status WHERE AppointmentID=@ID", conn))
                        {
                            cmd.Parameters.AddWithValue("@Status", status);
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                });
                await Task.WhenAll(LoadAppointmentsAsync(), LoadStatsAsync());
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to update appointment status. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Cell formatting ───────────────────────────────────────────────────

        private void dgvAppointments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            UIHelper.FormatStatusCell(e, dgvAppointments, "Status", "PaymentStatus");
            UIHelper.FormatTimeCell(e, dgvAppointments);
        }

        // ── Availability toggle ───────────────────────────────────────────────

        private void chkAvailable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(
                        "UPDATE Doctors SET IsAvailable=@Status WHERE DoctorID=@ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", chkAvailable.Checked);
                        cmd.Parameters.AddWithValue("@ID", _doctorId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception) { /* best-effort */ }
        }

        // ── Misc handlers ─────────────────────────────────────────────────────

        private async void btnRefresh_Click(object sender, EventArgs e) =>
            await Task.WhenAll(LoadAppointmentsAsync(), LoadStatsAsync());

        private async void btnAccept_Click(object sender, EventArgs e) =>
            await UpdateStatusAsync(BookingStatus.Accepted);

        private async void btnReject_Click(object sender, EventArgs e) =>
            await UpdateStatusAsync(BookingStatus.Rejected);

        private void Header_MouseDown(object sender, MouseEventArgs e) =>
            WindowHelper.StartDrag(this.Handle);

        private void btnMaximize_Click(object sender, EventArgs e) =>
            WindowHelper.ToggleMaximize(this, btnMaximize);

        private void btnClose_Click(object sender, EventArgs e) => Application.Exit();

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Close();
        }
    }
}
