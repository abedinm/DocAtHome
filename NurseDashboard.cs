using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class NurseDashboard : Form
    {
        private readonly string _nurseEmail;
        private int _nurseId;

        // Dynamic controls
        private Label _lblWelcome;
        private Panel _panelStats;
        private Label _lblStatPending, _lblStatToday, _lblStatTotal;

        public NurseDashboard(string nurseEmail)
        {
            InitializeComponent();
            _nurseEmail = nurseEmail;
            SetupLayout();
            Theme.SidebarNav(btnNavRequests);
            Theme.SidebarLogout(btnLogout);
            Theme.SuccessBtn(btnAccept);
            Theme.Outlined(btnReject, Theme.Danger);
        }

        // ── Layout ────────────────────────────────────────────────────────────

        private void SetupLayout()
        {
            // Remove Designer anchors — RepositionMainControls drives all placement
            panelTableContainer.Anchor = AnchorStyles.None;
            btnAccept.Anchor           = AnchorStyles.None;
            btnReject.Anchor           = AnchorStyles.None;

            // Welcome label
            _lblWelcome = new Label
            {
                Text      = "Welcome, Nurse",
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

            _lblStatPending = BuildStatCard(0,   "Pending Requests",  Color.FromArgb(245, 158, 11));
            _lblStatToday   = BuildStatCard(316, "Today's Visits",    Color.FromArgb(59, 130, 246));
            _lblStatTotal   = BuildStatCard(632, "Total Bookings",    Color.FromArgb(16, 185, 129));

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
            panelTableContainer.SetBounds(mar, 163, gridW, gridH);

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

        private async void NurseDashboard_Load(object sender, EventArgs e)
        {
            RepositionMainControls(null, EventArgs.Empty);
            UIHelper.StyleGrid(dgvNurseRequests);
            dgvNurseRequests.CellFormatting += dgvNurseRequests_CellFormatting;
            await LoadNurseInfoAsync();
            await Task.WhenAll(LoadRequestsAsync(), LoadStatsAsync());
        }

        private async Task LoadNurseInfoAsync()
        {
            try
            {
                var (id, firstName, available) = await Task.Run(() =>
                {
                    using (var conn = DBConfig.GetConnection())
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand(
                            "SELECT NurseID, FirstName, IsAvailable FROM Nurses WHERE Email=@E", conn))
                        {
                            cmd.Parameters.AddWithValue("@E", _nurseEmail);
                            using (var r = cmd.ExecuteReader())
                            {
                                if (r.Read()) return (r.GetInt32(0), r.GetString(1), r.GetBoolean(2));
                                return (0, "", false);
                            }
                        }
                    }
                });

                _nurseId = id;
                if (!string.IsNullOrEmpty(firstName))
                    _lblWelcome.Text = $"Welcome, {firstName}";
                chkAvailable.Checked = available;
            }
            catch (Exception) { /* non-critical */ }
        }

        // ── Requests ──────────────────────────────────────────────────────────

        private async Task LoadRequestsAsync()
        {
            if (_nurseId == 0) return;
            try
            {
                var dt = await Task.Run(() =>
                {
                    using (var conn = DBConfig.GetConnection())
                    {
                        conn.Open();
                        const string sql = @"
                            SELECT
                                B.BookingID,
                                P.FirstName + ' ' + P.LastName AS Patient,
                                B.BookingDate AS [Date],
                                B.ServiceType,
                                B.Status,
                                CASE
                                    WHEN EXISTS (
                                        SELECT 1 FROM Payments Pay
                                        WHERE Pay.PatientID  = B.PatientID
                                          AND Pay.RelatedType = 'NurseBooking'
                                          AND Pay.RelatedID   = B.BookingID
                                          AND Pay.Status      = 'Paid'
                                    ) THEN 'Paid'
                                    ELSE 'Unpaid'
                                END AS PaymentStatus
                            FROM NurseBookings B
                            JOIN Patients P ON B.PatientID = P.PatientID
                            WHERE B.NurseID = @NurseID
                            ORDER BY B.BookingDate DESC";

                        using (var da = new SqlDataAdapter(sql, conn))
                        {
                            da.SelectCommand.Parameters.AddWithValue("@NurseID", _nurseId);
                            var table = new DataTable();
                            da.Fill(table);
                            return table;
                        }
                    }
                });

                dgvNurseRequests.DataSource = dt;
                if (dgvNurseRequests.Columns["BookingID"] != null)
                    dgvNurseRequests.Columns["BookingID"].Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load requests. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Stats ─────────────────────────────────────────────────────────────

        private async Task LoadStatsAsync()
        {
            if (_nurseId == 0) return;
            try
            {
                var (pending, today, total) = await Task.Run(() =>
                {
                    using (var conn = DBConfig.GetConnection())
                    {
                        conn.Open();
                        int p = Scalar(conn,
                            "SELECT COUNT(*) FROM NurseBookings WHERE NurseID=@D AND Status='Pending'");
                        int t = Scalar(conn,
                            "SELECT COUNT(*) FROM NurseBookings WHERE NurseID=@D AND BookingDate=CAST(GETDATE() AS DATE) AND Status NOT IN ('Rejected','Cancelled')");
                        int all = Scalar(conn,
                            "SELECT COUNT(*) FROM NurseBookings WHERE NurseID=@D");
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
                cmd.Parameters.AddWithValue("@D", _nurseId);
                return (int)(cmd.ExecuteScalar() ?? 0);
            }
        }

        // ── Cell formatting ───────────────────────────────────────────────────

        private void dgvNurseRequests_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            UIHelper.FormatStatusCell(e, dgvNurseRequests, "Status", "PaymentStatus");
            UIHelper.FormatTimeCell(e, dgvNurseRequests);
        }

        // ── Accept / Reject ───────────────────────────────────────────────────

        private async Task UpdateStatusAsync(string status)
        {
            if (dgvNurseRequests.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvNurseRequests.CurrentRow.Cells["BookingID"].Value);
            try
            {
                await Task.Run(() =>
                {
                    using (var conn = DBConfig.GetConnection())
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand(
                            "UPDATE NurseBookings SET Status=@Status WHERE BookingID=@ID", conn))
                        {
                            cmd.Parameters.AddWithValue("@Status", status);
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                });
                await Task.WhenAll(LoadRequestsAsync(), LoadStatsAsync());
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to update status. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        "UPDATE Nurses SET IsAvailable=@A WHERE NurseID=@ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@A", chkAvailable.Checked);
                        cmd.Parameters.AddWithValue("@ID", _nurseId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception) { /* best-effort */ }
        }

        // ── Misc handlers ─────────────────────────────────────────────────────

        private async void btnNavRequests_Click(object sender, EventArgs e) =>
            await Task.WhenAll(LoadRequestsAsync(), LoadStatsAsync());

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
