# DocAtHome — Complete Q&A Prep Sheet
### For University Defense | Every angle covered — concept, technical, live changes, worst-case

---

## SECTION A — Project Concept & Motivation

**Q: What problem does DocAtHome solve?**
> In Bangladesh and similar markets, accessing a doctor or nurse for a home visit is unorganized —
> it's done through phone calls, word of mouth, or fragmented apps. DocAtHome centralizes the entire
> workflow: patients book online, doctors and nurses manage their own availability and queues,
> admins oversee the whole operation, and payments are recorded digitally. No paperwork, no phone tag.

**Q: Why a desktop application and not a web or mobile app?**
> Desktop was chosen deliberately for this scope — it allowed full focus on business logic, database
> design, and multi-role architecture without splitting effort across front-end frameworks, REST APIs,
> and deployment infrastructure. WinForms on .NET Framework 4.8 is mature, stable, and runs on any
> Windows machine without a browser or internet connection — ideal for a clinic admin workstation.
> A web or mobile front-end would be the natural next evolution; the database schema and business
> logic are completely independent of the UI layer.

**Q: Who are the users of this system?**
> Four roles: **Patients** book visits and pay, **Doctors** manage their appointment queues,
> **Nurses** manage their service bookings, and **Admins** oversee all data, register staff, and
> have full read/write access to every entity.

**Q: What makes this different from a simple CRUD app?**
> Several things: role-based authentication routes each user to their own UI;
> asynchronous database calls keep the interface responsive; there's a real payment flow with
> service pricing; appointment conflict detection prevents double-booking;
> brute-force lockout protects accounts; and OTP-based password recovery adds a real-world
> security layer. The UI is also built with a consistent theme system — not default WinForms chrome.

---

## SECTION B — Database Design

**Q: Walk me through your database schema.**
> Seven tables: **Patients, Doctors, Nurses, Admins** are the actor tables. **Appointments** links
> Patients to Doctors with a date, time, description, and status. **NurseBookings** links Patients
> to Nurses with a date and service type. **Payments** is a polymorphic table — its `RelatedType`
> column is either "Appointment" or "NurseBooking" and `RelatedID` holds the corresponding primary key,
> so one payments table handles both payment scenarios.

**Q: Why did you use a polymorphic Payments table instead of two separate tables?**
> Because the payment logic is identical regardless of whether you're paying for a doctor visit or
> a nurse service. One table means one query, one form, one insert pattern. Splitting it would
> duplicate code with no benefit. The trade-off is that `RelatedID` is not a foreign key with a
> constraint — that's a known limitation I'd address in production with a check constraint or
> a junction table.

**Q: Is your database normalized?**
> Yes, to Third Normal Form. Each table represents one entity. Patient details are not repeated in
> Appointments — only the PatientID foreign key is stored. Doctor specialty lives in Doctors, not
> copied into each appointment. The only intentional denormalization is `PatientEmail` in Payments,
> kept for display convenience — in a fully normalized design it would just be PatientID.

**Q: What indexes do you have?**
> The primary keys are auto-indexed by SQL Server. Ideally I would add indexes on
> `Appointments(PatientID)`, `Appointments(DoctorID)`, `NurseBookings(PatientID)`,
> and `Payments(RelatedID, RelatedType)` for query performance at scale.
> For this prototype at small data volumes it wasn't necessary, but I'm aware of where they'd go.

**Q: What is the Status lifecycle for an appointment?**
> Pending → Accepted or Rejected (by the doctor).
> From Pending, the patient can also Cancelled it themselves.
> Accepted → Completed (after the visit) or can be paid.
> This is enforced at the application layer through BookingStatus constants.

---

## SECTION C — Technical & Code

**Q: Why C# and .NET Framework 4.8?**
> C# is strongly-typed, has excellent WinForms tooling in Visual Studio, and .NET Framework 4.8
> is the current long-term-supported desktop runtime for Windows. It's the industry standard for
> enterprise desktop applications in this region. .NET 6+ would work equally well — the only
> difference in this codebase would be that `PlaceholderText` on TextBox becomes available
> (we removed it to maintain .NET Framework compatibility).

**Q: Explain your async/await usage.**
> Database calls are wrapped in `await Task.Run(() => { ... })` and awaited from async event handlers.
> This means the UI thread is never blocked during a SQL query — the form stays responsive and
> the user can still scroll or move the window while data loads. This is especially important for
> the patient dashboard which fires three queries simultaneously on load using `Task.WhenAll`.

**Q: What is Theme.cs and why does it exist?**
> Theme.cs is a static class that centralises all visual styling. Instead of setting BackColor,
> ForeColor, Font, FlatStyle, and BorderSize on every button individually throughout the codebase,
> I call `Theme.Primary(button)` or `Theme.Outlined(button)`. This means if the client ever wants
> to change the brand color from blue to green, it's one line changed in one file — not 40 lines
> across 18 forms.

**Q: How does your login system work?**
> The login checks the entered credentials against four tables in order — Admins, Patients, Doctors,
> Nurses — using a `CheckUser` helper method. Whichever table returns a match determines which
> dashboard to open. Before the query runs, `LoginAttemptTracker` checks if the account is locked.
> After each failure it increments a counter; at five failures it records a lockout timestamp and
> blocks further attempts for fifteen minutes.

**Q: How is password security handled?**
> Currently `PasswordHelper.Hash` is a pass-through stub — it returns the password unchanged.
> This was an intentional simplification for the prototype phase. In production I would replace
> the single line `return password` with `return BCrypt.HashPassword(password, workFactor: 12)`
> and update `Verify` to use `BCrypt.Verify`. Because the Hash and Verify methods are fully
> abstracted behind the static class, this is literally a two-line change with no impact on
> the rest of the codebase.

**Q: How does appointment conflict detection work?**
> Before inserting a new appointment, BookDoctorForm queries the Appointments table for any
> existing record where DoctorID, AppointmentDate, and AppointmentTime all match, and where
> Status is not Rejected or Cancelled. If count > 0, the booking is blocked and the user is
> told to choose a different slot.

**Q: What is WindowHelper.cs?**
> Because the forms use `FormBorderStyle.None` (no OS chrome) for the custom design, the default
> drag-to-move and maximize behavior is lost. WindowHelper re-implements it:
> `StartDrag` sends the `WM_NCLBUTTONDOWN` Windows message to trick the OS into treating a
> client-area mouse-down as a title-bar drag. `ToggleMaximize` swaps between `Maximized`
> and `Normal` window state and updates the maximize button text.

**Q: Why are your SQL queries inline strings and not stored procedures?**
> For a prototype of this scale, inline parameterized queries are practical and safe from SQL
> injection because all user input goes through `SqlParameter`. In a production system I would
> move queries to stored procedures for three reasons: they're precompiled (faster), DBAs can
> tune them independently, and they add an extra security layer by limiting what operations
> the app's DB user can perform.

---

## SECTION D — Features & Functionality

**Q: How does the OTP / password reset work?**
> The user enters their email. `OtpService` generates a random six-digit code, stores it
> in memory with a short expiry, and sends it to the email using SMTP. The user enters the code
> — if it matches and hasn't expired, they can set a new password which is updated in the
> correct table (Patients, Doctors, or Nurses — whichever email matches).

**Q: How do the stat cards update?**
> They're loaded asynchronously on form load using `Task.WhenAll(LoadHistoryAsync(), LoadStatsAsync(), LoadNotificationAsync())`.
> Each count is a separate parameterized SQL scalar query. They also refresh after every action
> — after booking, paying, or cancelling, the stat cards re-query so the numbers are always current.

**Q: What happens if a patient tries to cancel an accepted appointment?**
> The cancel button checks `Status == "Pending"` before doing anything. If the status is Accepted,
> Completed, or anything else, it shows an informational message: "Only Pending entries can be
> cancelled." The database UPDATE never runs.

**Q: What service types do nurses offer and what are the prices?**
> Injection — 200 BDT, Wound Care — 250 BDT, Physical Therapy — 300 BDT,
> IV Drip — 350 BDT, Elderly Care — 400 BDT, Doctor Appointment — 500 BDT.
> All prices are defined in the `ServicePricing` static class — one place to update if fees change.

**Q: Can a doctor see which patient booked them?**
> Yes — the DoctorDashboard grid shows the patient name, appointment date, time, reason,
> and status. The doctor has full context before accepting or rejecting.

---

## SECTION E — "What Would You Add?" Questions

**Q: What would you improve if you had more time?**
> Three things I'd prioritize:
> 1. **BCrypt passwords** — two lines of code, massive security improvement.
> 2. **Email notifications** — when a doctor accepts or rejects, send an email to the patient
>    automatically. The OtpService SMTP infrastructure is already there.
> 3. **Web front-end** — expose the same SQL Server database through an ASP.NET Core API so
>    patients can book from their phone browser. The database schema needs zero changes.

**Q: How would you scale this to 10,000 users?**
> First: BCrypt passwords, second: move the connection string to environment variables,
> third: add a connection pool configuration in DBConfig, fourth: add indexes on foreign key columns,
> fifth: add paging to the DataGridView queries instead of loading all rows at once,
> sixth: consider moving to a web architecture with horizontal scaling.

**Q: How would you add a new role, say a Pharmacist?**
> 1. Create a `Pharmacists` table in the DB with the same column pattern as Doctors/Nurses.
> 2. Add a `PharmacistDashboard` form.
> 3. Add one `else if (CheckUser(conn, "Pharmacists", email, password))` line in LoginForm.
> 4. Add CRUD to AdminDashboard.
> The role-based architecture was designed to make exactly this kind of extension trivial.

---

## SECTION F — Live Change Requests (What The Panel Might Ask You To Do)

> **Strategy:** Stay calm. Say "Sure, that's straightforward — let me show you exactly where."
> Then open the file, make the change, rebuild. Don't guess — be precise.

---

**"Can you add a date filter to the appointments grid?"**
> Open `ViewAppointmentsForm.cs`. In the `LoadAppointments` method, add a `DateTimePicker`
> to `panelToolbar` in the Designer, then add `AND AppointmentDate >= @Start AND AppointmentDate <= @End`
> to the WHERE clause. Takes about 3 minutes.

**"Can you change the button color?"**
> Open `Theme.cs`. Change the `Primary` color:
> `button.BackColor = Color.FromArgb(16, 185, 129);` (green) or any RGB value.
> Every primary button in every form updates automatically. 10 seconds.

**"Can you show the doctor's specialty in the appointment history grid?"**
> Open `MainForm.cs`, find `LoadHistoryAsync`, add `D.Specialty AS [Specialty]` to the SELECT.
> The DataGridView uses `AutoSizeColumnsMode = Fill` so it appears automatically. 30 seconds.

**"Can you add a phone number field to the booking form?"**
> Open `BookDoctorForm.Designer.cs`, add a `Label` and `TextBox` for phone,
> then in `BookDoctorForm.cs` `btnSave_Click`, add it as a parameter to the INSERT
> (requires adding a PhoneNumber column to Appointments). 5 minutes.

**"Can you make the grid rows alternating colors?"**
> Open `UIHelper.cs`, in `StyleGrid`:
> `dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);`
> One line. 15 seconds.

**"Can you add a total count label above the grid?"**
> Open the relevant form's `.cs` file. After `dgv.DataSource = dt;` add:
> `lblCount.Text = $"{dt.Rows.Count} records found";`
> Add a label to the Designer for it. 2 minutes.

**"Can you sort the grid by clicking a column header?"**
> DataGridView already supports this. Just set:
> `dgv.AllowUserToOrderColumns = true;` and `dgv.SortMode` on each column to `Automatic`.
> 30 seconds in the Designer.

**"Can you add an export to CSV button?"**
> Open the form's `.cs`. Add a button click handler:
> ```csharp
> var sb = new StringBuilder();
> foreach (DataGridViewRow row in dgv.Rows)
>     sb.AppendLine(string.Join(",", row.Cells.Cast<DataGridViewCell>().Select(c => c.Value)));
> File.WriteAllText("export.csv", sb.ToString());
> ```
> 3 minutes.

**"Can you show a message if the grid is empty?"**
> After loading data: `lblEmpty.Visible = dt.Rows.Count == 0;`
> This pattern already exists in MainForm — copy it. 1 minute.

**"Can you change the sidebar width?"**
> Open the form's Designer.cs. Change `panelSidebar.Size = new Size(300, 720);`
> and all controls using DockStyle.Fill/Top auto-adjust. 10 seconds.

---

## SECTION G — Worst-Case Questions

**Q: Your password storage is not secure — plain text. How do you defend that?**
> *"You're absolutely right, and I want to address that directly. PasswordHelper is deliberately
> implemented as a stub for the prototype — the method signature, the abstraction, and the
> interface are all correct. Replacing it with BCrypt is a two-line change.
> I made the conscious decision to ship a working, complete feature set first and flag
> security hardening as a known next step, rather than partially implement features.
> In production, this ships with BCrypt. I have the implementation ready."*

**Q: What happens if the database goes down?**
> *"Every database call is wrapped in a try-catch. If the connection fails, the user sees
> a clear error message — 'Unable to connect to the server' — and the app stays open.
> No crashes, no data corruption. In production I would add a retry policy and a connection
> health check on startup."*

**Q: Can two admins use the system simultaneously?**
> *"SQL Server handles concurrent connections natively — each user gets their own SqlConnection
> object. The application doesn't use any in-memory shared state, so multiple simultaneous
> users are safe. For high-concurrency scenarios I would add row-level locking and optimistic
> concurrency checks on status updates."*

**Q: There's no server-side validation — what if someone bypasses the UI?**
> *"That's a valid production concern. The application layer validates inputs before every
> INSERT and UPDATE. For a truly adversarial environment, I'd add CHECK constraints and
> stored procedures at the database layer, so even a direct SQL connection cannot insert
> invalid data. That's on the production hardening roadmap."*

**Q: Your app only runs on Windows — is that a limitation?**
> *"It's a deliberate scope decision. WinForms targets Windows, where admin workstations
> in clinics typically run. For cross-platform reach, the next iteration would expose
> the same SQL Server database through an ASP.NET Core API — Angular, React, or a mobile
> app can then consume it. The database schema requires zero changes for that migration."*

**Q: What if a nurse is booked but then marks herself unavailable?**
> *"Existing bookings are not affected — they stay in the NurseBookings table with their
> current status. The IsAvailable flag only affects the dropdown in BookNurseForm —
> unavailable nurses simply don't appear for new bookings. The admin can still manually
> assign an unavailable nurse through AdminBookNurseForm if needed."*

**Q: How do you handle time zones?**
> *"Currently the system uses the local machine time via DateTime.Now and DateTime.Today,
> which is correct for a single-location clinic operating in one time zone.
> For a multi-city deployment, I would store all times in UTC in the database and
> convert to the user's local time zone on display using TimeZoneInfo."*

---

## SECTION H — Quick-Fire Technical Facts (memorize these)

| Question | Answer |
|----------|--------|
| Language | C# |
| Framework | .NET Framework 4.8 |
| UI Technology | Windows Forms (WinForms) |
| Database | Microsoft SQL Server |
| DB Access | ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter) |
| Async pattern | async/await + Task.Run |
| Form count | 18 forms |
| DB table count | 7 tables |
| User roles | 4 (Admin, Patient, Doctor, Nurse) |
| Doctor appt price | 500 BDT |
| Nurse services | 5 types (200–400 BDT) |
| Lockout policy | 5 attempts → 15 minute lockout |
| Password hashing | Stub (BCrypt-ready interface) |
| Connection string | app.config (ConfigurationManager) |
| OTP delivery | Email via SMTP (OtpService.cs) |

---

## SECTION I — Your Confidence Anchors

> When you feel nervous, return to these three facts:

1. **You built 18 forms** with consistent design, real DB integration, four user roles, async calls,
   payment flow, OTP recovery, and brute-force protection. That is not a simple project.

2. **You know this codebase line by line.** You fixed every off-screen button, every missing cursor,
   every broken anchor. If they point at anything on screen, you know exactly which file and which
   line it lives in.

3. **Every weakness has a prepared answer.** Plain text passwords → BCrypt stub, ready to swap.
   No stored procedures → parameterized queries, SQL injection safe. No server-side validation →
   application layer + DB constraints roadmap. You're not hiding weaknesses. You're demonstrating
   engineering judgment.
