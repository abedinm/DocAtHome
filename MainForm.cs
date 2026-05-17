using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class MainForm : Form
    {
        private readonly string _patientEmail;
        private int  _patientId;
        private bool _viewingNurse = false;

        // Dynamically-built controls
        private Label  _lblNotification;
        private Panel  _panelStats;
        private Button _btnCancelAppointment;
        private Button _btnProfile;
        private Button _btnNavNurseBookings;
        private Label  _lblEmpty;
        private Label  _lblViewTitle;
        private Label  _lblStatUpcoming, _lblStatPending, _lblStatUnpaid;

        public MainForm(string patientEmail)
        {
            InitializeComponent();
            _patientEmail = patientEmail;
            SetupLayout();
            Theme.SidebarNav(btnNavAppointments);
            Theme.SidebarLogout(btnLogout);
        }

        // ── Layout setup (runs once in constructor) ───────────────────────────

        private void SetupLayout()
        {
            // Remove hardcoded anchors — RepositionMainControls handles all placement
            panelGridCard.Anchor = AnchorStyles.None;
            btnPay.Anchor        = AnchorStyles.None;
            btnBookDoctor.Anchor = AnchorStyles.None;
            btnBookNurse.Anchor  = AnchorStyles.None;

            lblWelcomePatient.Location = new Point(35, 20);
            lblClock.Location          = new Point(40, 58);

            // ── Notification strip ──────────────────────────────────────────
            _lblNotification = new Label
            {
                Location  = new Point(40, 83),
                Size      = new Size(940, 26),
                Font      = new Font("Segoe UI Semibold", 9.5F),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding   = new Padding(10, 0, 0, 0),
                Visible   = false,
                Anchor    = AnchorStyles.None
            };
            panelMain.Controls.Add(_lblNotification);

            // ── Stat cards ──────────────────────────────────────────────────
            _panelStats = new Panel
            {
                Location  = new Point(40, 113),
                Size      = new Size(940, 88),
                BackColor = Color.Transparent,
                Anchor    = AnchorStyles.None
            };
            panelMain.Controls.Add(_panelStats);

            _lblStatUpcoming = BuildStatCard(0,   "Upcoming Visits",     Color.FromArgb(59, 130, 246));
            _lblStatPending  = BuildStatCard(310, "Awaiting Acceptance",  Color.FromArgb(245, 158, 11));
            _lblStatUnpaid   = BuildStatCard(620, "Unpaid Bills",         Color.FromArgb(239, 68, 68));

            // ── View title label ────────────────────────────────────────────
            _lblViewTitle = new Label
            {
                Location  = new Point(40, 210),
                AutoSize  = true,
                Font      = new Font("Segoe UI Semibold", 8.5F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Text      = "APPOINTMENT HISTORY",
                Anchor    = AnchorStyles.None
            };
            panelMain.Controls.Add(_lblViewTitle);

            // ── Cancel button ───────────────────────────────────────────────
            _btnCancelAppointment = new Button
            {
                Size      = new Size(145, 38),
                Text      = "✕  Cancel",
                Font      = new Font("Segoe UI Semibold", 9F),
                ForeColor = Color.FromArgb(239, 68, 68),
                FlatStyle = FlatStyle.Flat,
                Cursor    = Cursors.Hand,
                Anchor    = AnchorStyles.None
            };
            _btnCancelAppointment.FlatAppearance.BorderColor = Color.FromArgb(239, 68, 68);
            _btnCancelAppointment.Click += BtnCancelAppointment_Click;
            panelMain.Controls.Add(_btnCancelAppointment);

            // ── Profile button in sidebar ───────────────────────────────────
            _btnProfile = new Button
            {
                Location  = new Point(0, 175),
                Size      = new Size(260, 55),
                Text      = "   👤  My Profile",
                Font      = new Font("Segoe UI Semibold", 11F),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor    = Cursors.Hand,
                BackColor = Color.Transparent
            };
            _btnProfile.FlatAppearance.BorderSize = 0;
            _btnProfile.Click += BtnProfile_Click;
            panelSidebar.Controls.Add(_btnProfile);

            // ── Nurse bookings nav in sidebar ───────────────────────────────
            _btnNavNurseBookings = new Button
            {
                Location  = new Point(0, 230),
                Size      = new Size(260, 55),
                Text      = "   🏥  Nurse Bookings",
                Font      = new Font("Segoe UI Semibold", 11F),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor    = Cursors.Hand,
                BackColor = Color.Transparent
            };
            _btnNavNurseBookings.FlatAppearance.BorderSize = 0;
            _btnNavNurseBookings.Click += BtnNavNurseBookings_Click;
            panelSidebar.Controls.Add(_btnNavNurseBookings);

            // ── Empty state overlay ─────────────────────────────────────────
            _lblEmpty = new Label
            {
                Text      = "No appointments yet.\nClick \"Book Doctor\" or \"Book Nurse\" to get started.",
                Font      = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(148, 163, 184),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock      = DockStyle.Fill,
                Visible   = false
            };
            panelGridCard.Controls.Add(_lblEmpty);
            _lblEmpty.BringToFront();

            // ── Grid ────────────────────────────────────────────────────────
            UIHelper.StyleGrid(dgvPatientHistory);
            dgvPatientHistory.CellFormatting += DgvHistory_CellFormatting;

            // ── Responsive resize ───────────────────────────────────────────
            panelMain.SizeChanged += RepositionMainControls;
        }

        private void RepositionMainControls(object sender, EventArgs e)
        {
            int w      = panelMain.ClientSize.Width;
            int h      = panelMain.ClientSize.Height;
            int margin = 40;
            int gridW  = Math.Max(200, w - margin * 2);
            int btnY   = h - 56;
            int gridH  = Math.Max(80, btnY - 230);

            _lblNotification.SetBounds(margin, 83, gridW, 26);
            _panelStats.SetBounds(margin, 113, gridW, 88);
            _lblViewTitle.Location = new Point(margin, 210);
            panelGridCard.SetBounds(margin, 225, gridW, gridH);

            btnPay.SetBounds(margin, btnY + 4, 145, 38);
            _btnCancelAppointment.SetBounds(margin + 155, btnY + 4, 145, 38);
            btnBookNurse.SetBounds(w - 400, btnY, 185, 45);
            btnBookDoctor.SetBounds(w - 205, btnY, 185, 45);
        }

        private Label BuildStatCard(int x, string title, Color accent)
        {
            var card = new Panel { Location = new Point(x, 0), Size = new Size(300, 85), BackColor = Color.White };
            card.Controls.Add(new Panel { Location = new Point(0, 0), Size = new Size(5, 85), BackColor = accent });
            card.Controls.Add(new Label { Text = title, Font = new Font("Segoe UI", 9F), ForeColor = Color.FromArgb(100, 116, 139), Location = new Point(18, 14), AutoSize = true });
            var lbl = new Label { Text = "—", Font = new Font("Segoe UI", 26F, FontStyle.Bold), ForeColor = Color.FromArgb(15, 23, 42), Location = new Point(16, 34), AutoSize = true };
            card.Controls.Add(lbl);
            _panelStats.Controls.Add(card);
            return lbl;
        }

        // ── Load sequence ─────────────────────────────────────────────────────

        private async void MainForm_Load(object sender, EventArgs e)
        {
            RepositionMainControls(null, EventArgs.Empty);
            await LoadPatientIdAsync();
            LoadPatientName();
            await Task.WhenAll(LoadHistoryAsync(), LoadStatsAsync(), LoadNotificationAsync());
        }

        private async Task LoadPatientIdAsync()
        {
            _patientId = await Task.Run(() =>
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT PatientID FROM Patients WHERE Email=@E", conn))
                    {
                        cmd.Parameters.AddWithValue("@E", _patientEmail);
                        var r = cmd.ExecuteScalar();
                        return r != null ? (int)r : 0;
                    }
                }
            });
        }

        private void LoadPatientName()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT FirstName FROM Patients WHERE Email=@E", conn))
                    {
                        cmd.Parameters.AddWithValue("@E", _patientEmail);
                        var res = cmd.ExecuteScalar();
                        lblWelcomePatient.Text = res != null ? $"Welcome back, {res}!" : "Welcome back!";
                    }
                }
            }
            catch { lblWelcomePatient.Text = "Welcome back!"; }
        }

        // ── Appointment history (async) ───────────────────────────────────────

        private async Task LoadHistoryAsync()
        {
            if (_patientId == 0) return;
            try
            {
                var dt = await Task.Run(() =>
                {
                    using (var conn = DBConfig.GetConnection())
                    {
                        conn.Open();
                        const string sql = @"
                            SELECT A.AppointmentID,
                                   A.AppointmentDate AS [Date],
                                   A.AppointmentTime AS [Time],
                                   D.FirstName + ' ' + D.LastName AS [Doctor],
                                   A.Description AS [Reason],
                                   A.Status
                            FROM   Appointments A
                            JOIN   Doctors D ON A.DoctorID = D.DoctorID
                            WHERE  A.PatientID = @PID
                            ORDER  BY A.AppointmentDate DESC, A.AppointmentTime DESC";

                        using (var da = new SqlDataAdapter(sql, conn))
                        {
                            da.SelectCommand.Parameters.AddWithValue("@PID", _patientId);
                            var table = new DataTable();
                            da.Fill(table);
                            return table;
                        }
                    }
                });

                dgvPatientHistory.DataSource = dt;
                if (dgvPatientHistory.Columns["AppointmentID"] != null)
                    dgvPatientHistory.Columns["AppointmentID"].Visible = false;

                bool empty = dt.Rows.Count == 0;
                _lblEmpty.Text    = "No appointments yet.\nClick \"Book Doctor\" to get started.";
                _lblEmpty.Visible = empty;
                dgvPatientHistory.Visible = !empty;
            }
            catch (Exception) { MessageBox.Show("Unable to load appointment history.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // ── Nurse booking history (async) ─────────────────────────────────────

        private async Task LoadNurseHistoryAsync()
        {
            if (_patientId == 0) return;
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
                                B.BookingDate AS [Date],
                                N.FirstName + ' ' + N.LastName AS [Nurse],
                                B.ServiceType AS [Service],
                                B.Status,
                                CASE WHEN EXISTS (
                                    SELECT 1 FROM Payments Py
                                    WHERE Py.RelatedID   = B.BookingID
                                      AND Py.RelatedType = 'NurseBooking'
                                      AND Py.Status      = 'Paid'
                                ) THEN 'Paid' ELSE 'Unpaid' END AS [PaymentStatus]
                            FROM NurseBookings B
                            JOIN Nurses N ON B.NurseID = N.NurseID
                            WHERE B.PatientID = @PID
                            ORDER BY B.BookingDate DESC";

                        using (var da = new SqlDataAdapter(sql, conn))
                        {
                            da.SelectCommand.Parameters.AddWithValue("@PID", _patientId);
                            var table = new DataTable();
                            da.Fill(table);
                            return table;
                        }
                    }
                });

                dgvPatientHistory.DataSource = dt;
                if (dgvPatientHistory.Columns["BookingID"] != null)
                    dgvPatientHistory.Columns["BookingID"].Visible = false;

                bool empty = dt.Rows.Count == 0;
                _lblEmpty.Text    = "No nurse bookings yet.\nClick \"Book Nurse\" to get started.";
                _lblEmpty.Visible = empty;
                dgvPatientHistory.Visible = !empty;
            }
            catch (Exception) { MessageBox.Show("Unable to load nurse bookings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // ── Stat cards (async) ────────────────────────────────────────────────

        private async Task LoadStatsAsync()
        {
            if (_patientId == 0) return;
            try
            {
                var (upcoming, pending, unpaid) = await Task.Run(() =>
                {
                    using (var conn = DBConfig.GetConnection())
                    {
                        conn.Open();
                        int u = Scalar(conn,
                            "SELECT COUNT(*) FROM Appointments WHERE PatientID=@P AND Status='Accepted' AND AppointmentDate >= CAST(GETDATE() AS DATE)",
                            _patientId);
                        int p = Scalar(conn,
                            "SELECT COUNT(*) FROM Appointments WHERE PatientID=@P AND Status='Pending'",
                            _patientId);
                        int apptUnpaid = Scalar(conn,
                            @"SELECT COUNT(*) FROM Appointments A
                              WHERE A.PatientID=@P AND A.Status='Accepted'
                              AND NOT EXISTS (
                                  SELECT 1 FROM Payments Py
                                  WHERE Py.RelatedID=A.AppointmentID
                                    AND Py.RelatedType='Appointment'
                                    AND Py.Status='Paid')",
                            _patientId);
                        int nurseUnpaid = Scalar(conn,
                            @"SELECT COUNT(*) FROM NurseBookings B
                              WHERE B.PatientID=@P AND B.Status='Accepted'
                              AND NOT EXISTS (
                                  SELECT 1 FROM Payments Py
                                  WHERE Py.RelatedID=B.BookingID
                                    AND Py.RelatedType='NurseBooking'
                                    AND Py.Status='Paid')",
                            _patientId);
                        return (u, p, apptUnpaid + nurseUnpaid);
                    }
                });

                _lblStatUpcoming.Text = upcoming.ToString();
                _lblStatPending.Text  = pending.ToString();
                _lblStatUnpaid.Text   = unpaid.ToString();
            }
            catch { /* stats are non-critical */ }
        }

        private static int Scalar(SqlConnection conn, string sql, int patientId)
        {
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@P", patientId);
                return (int)(cmd.ExecuteScalar() ?? 0);
            }
        }

        // ── Notification strip (async) ────────────────────────────────────────

        private async Task LoadNotificationAsync()
        {
            if (_patientId == 0) return;
            try
            {
                string text  = null;
                Color  color = Color.Transparent;

                await Task.Run(() =>
                {
                    using (var conn = DBConfig.GetConnection())
                    {
                        conn.Open();
                        const string sql = @"
                            SELECT TOP 1
                                D.FirstName + ' ' + D.LastName,
                                A.Status,
                                A.AppointmentDate
                            FROM  Appointments A
                            JOIN  Doctors D ON A.DoctorID = D.DoctorID
                            WHERE A.PatientID = @P
                              AND A.Status IN ('Accepted','Rejected')
                              AND A.AppointmentDate >= CAST(GETDATE() AS DATE)
                            ORDER BY A.AppointmentDate ASC";

                        using (var cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@P", _patientId);
                            using (var r = cmd.ExecuteReader())
                            {
                                if (!r.Read()) return;
                                string doctor = r.GetString(0);
                                string status = r.GetString(1);
                                string date   = r.GetDateTime(2).ToString("MMM dd");

                                if (status == "Accepted")
                                {
                                    text  = $"✓  Your appointment with {doctor} on {date} is confirmed.";
                                    color = Color.FromArgb(16, 185, 129);
                                }
                                else
                                {
                                    text  = $"✕  Your appointment with {doctor} on {date} was declined — please rebook.";
                                    color = Color.FromArgb(239, 68, 68);
                                }
                            }
                        }
                    }
                });

                if (text != null)
                {
                    _lblNotification.Text      = text;
                    _lblNotification.BackColor = color;
                    _lblNotification.Visible   = true;
                }
            }
            catch { /* notifications are non-critical */ }
        }

        // ── Nav: Appointments ─────────────────────────────────────────────────

        private async void btnViewAppointments_Click(object sender, EventArgs e)
        {
            _viewingNurse = false;
            _lblViewTitle.Text = "APPOINTMENT HISTORY";
            _btnCancelAppointment.Text = "✕  Cancel";
            await LoadHistoryAsync();
        }

        // ── Nav: Nurse Bookings ───────────────────────────────────────────────

        private async void BtnNavNurseBookings_Click(object sender, EventArgs e)
        {
            _viewingNurse = true;
            _lblViewTitle.Text = "NURSE BOOKING HISTORY";
            _btnCancelAppointment.Text = "✕  Cancel Booking";
            await LoadNurseHistoryAsync();
        }

        // ── Cancel (appointment or nurse booking) ─────────────────────────────

        private async void BtnCancelAppointment_Click(object sender, EventArgs e)
        {
            if (dgvPatientHistory.CurrentRow == null) return;

            string status = dgvPatientHistory.CurrentRow.Cells["Status"].Value?.ToString();
            if (status != "Pending")
            {
                MessageBox.Show("Only Pending entries can be cancelled.", "Cannot Cancel",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string entity = _viewingNurse ? "nurse booking" : "appointment";
            if (MessageBox.Show($"Cancel this {entity}?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                if (_viewingNurse)
                {
                    if (!int.TryParse(dgvPatientHistory.CurrentRow.Cells["BookingID"].Value?.ToString(), out int bid)) return;
                    await Task.Run(() =>
                    {
                        using (var conn = DBConfig.GetConnection())
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("UPDATE NurseBookings SET Status='Cancelled' WHERE BookingID=@ID", conn))
                            {
                                cmd.Parameters.AddWithValue("@ID", bid);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    });
                    await Task.WhenAll(LoadNurseHistoryAsync(), LoadStatsAsync());
                }
                else
                {
                    if (!int.TryParse(dgvPatientHistory.CurrentRow.Cells["AppointmentID"].Value?.ToString(), out int aid)) return;
                    await Task.Run(() =>
                    {
                        using (var conn = DBConfig.GetConnection())
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("UPDATE Appointments SET Status='Cancelled' WHERE AppointmentID=@ID", conn))
                            {
                                cmd.Parameters.AddWithValue("@ID", aid);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    });
                    await Task.WhenAll(LoadHistoryAsync(), LoadStatsAsync(), LoadNotificationAsync());
                }
            }
            catch (Exception) { MessageBox.Show("Unable to cancel. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // ── Pay (appointment or nurse booking) ───────────────────────────────

        private async void btnPay_Click(object sender, EventArgs e)
        {
            if (dgvPatientHistory.CurrentRow == null)
            {
                MessageBox.Show("Select a row to pay for.", "No Selection");
                return;
            }

            if (_viewingNurse)
            {
                if (!int.TryParse(dgvPatientHistory.CurrentRow.Cells["BookingID"].Value?.ToString(), out int bid)) return;
                string service = dgvPatientHistory.CurrentRow.Cells["Service"].Value?.ToString() ?? "";
                decimal price  = ServicePricing.ForNurseService(service);

                var pf = new PaymentForm(_patientEmail, PaymentType.NurseBooking, bid, price);
                if (pf.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Payment recorded.");
                    await Task.WhenAll(LoadNurseHistoryAsync(), LoadStatsAsync());
                }
            }
            else
            {
                if (!int.TryParse(dgvPatientHistory.CurrentRow.Cells["AppointmentID"].Value?.ToString(), out int aid)) return;
                var pf = new PaymentForm(_patientEmail, PaymentType.Appointment, aid, ServicePricing.DoctorAppointment);
                if (pf.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Payment recorded.");
                    await LoadStatsAsync();
                }
            }
        }

        // ── Profile ───────────────────────────────────────────────────────────

        private void BtnProfile_Click(object sender, EventArgs e)
        {
            using (var f = new PatientProfileForm(_patientEmail))
            {
                f.ShowDialog();
                LoadPatientName();
            }
        }

        // ── Book buttons ──────────────────────────────────────────────────────

        private async void btnBookDoctor_Click(object sender, EventArgs e)
        {
            new BookDoctorForm(_patientEmail).ShowDialog();
            _viewingNurse = false;
            _lblViewTitle.Text = "APPOINTMENT HISTORY";
            _btnCancelAppointment.Text = "✕  Cancel";
            await Task.WhenAll(LoadHistoryAsync(), LoadStatsAsync(), LoadNotificationAsync());
        }

        private async void btnBookNurse_Click(object sender, EventArgs e)
        {
            new BookNurseForm(_patientEmail).ShowDialog();
            _viewingNurse = true;
            _lblViewTitle.Text = "NURSE BOOKING HISTORY";
            _btnCancelAppointment.Text = "✕  Cancel Booking";
            await Task.WhenAll(LoadNurseHistoryAsync(), LoadStatsAsync());
        }

        // ── Misc handlers ─────────────────────────────────────────────────────

        private void btnLogout_Click(object sender, EventArgs e) { new LoginForm().Show(); Close(); }
        private void btnClose_Click(object sender, EventArgs e)  => Application.Exit();
        private void btnMaximize_Click(object sender, EventArgs e) => WindowHelper.ToggleMaximize(this, btnMaximize);
        private void Header_MouseDown(object sender, MouseEventArgs e) => WindowHelper.StartDrag(Handle);

        private void timerClock_Tick(object sender, EventArgs e) =>
            lblClock.Text = DateTime.Now.ToString("dddd, MMM dd, yyyy | hh:mm tt");

        private void DgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_viewingNurse)
                UIHelper.FormatStatusCell(e, dgvPatientHistory, "Status", "PaymentStatus");
            else
            {
                UIHelper.FormatStatusCell(e, dgvPatientHistory, "Status");
                UIHelper.FormatTimeCell(e, dgvPatientHistory, "Time");
            }
        }
    }
}
