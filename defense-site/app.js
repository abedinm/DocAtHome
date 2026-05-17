/* ═══════════════════════════════════════════════════════════
   DocAtHome Defense Portal — App Logic
═══════════════════════════════════════════════════════════ */

'use strict';

/* ── DATA ──────────────────────────────────────────────────── */

const ROLES = [
  {
    id: 'admin', icon: '🛡️', name: 'Admin', color: '#ef4444',
    tagline: 'Full system oversight and control',
    pill: 'Super User',
    steps: [
      'Log in → routed to AdminDashboard automatically',
      'View KPI cards: Doctors, Patients, Nurses, Revenue',
      'Inspect spline area chart of booking activity',
      'Navigate sidebar: Manage Doctors / Nurses / Patients',
      'Search and register new staff with full CRUD',
      'Create bookings on behalf of patients',
      'View all appointments and nurse bookings system-wide',
    ],
    features: [
      { icon: '📊', text: 'Live KPI dashboard with revenue in BDT' },
      { icon: '📋', text: 'Full CRUD for Doctors, Nurses, Patients' },
      { icon: '📅', text: 'Create and view all appointments' },
      { icon: '🔍', text: 'Search + detail panel on Manage Patients' },
      { icon: '🗂️', text: 'Manage all nurse bookings + service types' },
    ],
  },
  {
    id: 'patient', icon: '🧑‍⚕️', name: 'Patient', color: '#3b82f6',
    tagline: 'Book visits, pay, track history',
    pill: 'End User',
    steps: [
      'Log in → MainForm (patient dashboard)',
      'View 3 stat cards: Upcoming, Pending, Unpaid Bills',
      'See notification banner for latest appointment status',
      'Click Book Doctor → availability check + conflict guard',
      'Click Book Nurse → pick service type + see price',
      'Select Accepted row → Pay Selected → choose Cash/Card/Bkash',
      'Payment recorded, stat cards update in real time',
    ],
    features: [
      { icon: '🔔', text: 'Notification banner for status changes' },
      { icon: '🚫', text: 'Conflict detection prevents double-booking' },
      { icon: '💳', text: 'Card payment with Luhn validation + auto-format' },
      { icon: '❌', text: 'Self-cancel Pending appointments only' },
      { icon: '📜', text: 'Full appointment history with status badges' },
    ],
  },
  {
    id: 'doctor', icon: '👨‍⚕️', name: 'Doctor', color: '#10b981',
    tagline: 'Manage queue and availability',
    pill: 'Provider',
    steps: [
      'Log in → DoctorDashboard',
      'Toggle availability checkbox in header',
      'View all incoming appointment requests',
      'See patient name, date, time, and reason per row',
      'Click ✓ Accept Visit → status updates to Accepted',
      'Click ✕ Reject Visit → status updates to Rejected',
      'Patient is notified on their dashboard immediately',
    ],
    features: [
      { icon: '🔄', text: 'Availability toggle removes from booking dropdowns' },
      { icon: '✅', text: 'Accept or Reject each incoming request' },
      { icon: '👁️', text: 'Full patient context visible before accepting' },
      { icon: '🔃', text: 'Refresh live — no page reload needed' },
    ],
  },
  {
    id: 'nurse', icon: '👩‍⚕️', name: 'Nurse', color: '#8b5cf6',
    tagline: 'Service bookings and availability',
    pill: 'Provider',
    steps: [
      'Log in → NurseDashboard',
      'Toggle availability in header',
      'View incoming service booking requests',
      'Service type and date visible directly in grid',
      'Accept or Reject each booking',
      'Accepted bookings are payable by the patient',
    ],
    features: [
      { icon: '💉', text: '5 service types: Injection, Wound Care, IV Drip, Elderly Care, Physical Therapy' },
      { icon: '🔄', text: 'Availability toggle hides from patient booking form' },
      { icon: '📋', text: 'Service type and booking date in grid' },
      { icon: '✅', text: 'Accept or Reject queue management' },
    ],
  },
];

const SCHEMA_TABLES = [
  {
    name: 'Admins', color: '#ef4444', gridArea: '1 / 1 / 2 / 2',
    cols: [
      { name: 'AdminID',   type: 'INT',          key: 'PK' },
      { name: 'Email',     type: 'VARCHAR(150)',  key: '' },
      { name: 'Password',  type: 'VARCHAR(255)',  key: '' },
    ],
    desc: 'Stores administrator accounts. Admins have full read/write access to all entities.',
    relations: [],
  },
  {
    name: 'Patients', color: '#3b82f6', gridArea: '1 / 2 / 2 / 3',
    cols: [
      { name: 'PatientID',     type: 'INT',         key: 'PK' },
      { name: 'FirstName',     type: 'VARCHAR(50)',  key: '' },
      { name: 'LastName',      type: 'VARCHAR(50)',  key: '' },
      { name: 'Email',         type: 'VARCHAR(150)', key: '' },
      { name: 'Password',      type: 'VARCHAR(255)', key: '' },
      { name: 'PhoneNumber',   type: 'VARCHAR(20)',  key: '' },
      { name: 'DateOfBirth',   type: 'DATE',         key: '' },
      { name: 'MedicalHistory',type: 'TEXT',         key: '' },
    ],
    desc: 'Registered patients. PatientID is FK in Appointments, NurseBookings, and Payments.',
    relations: ['Appointments', 'NurseBookings', 'Payments'],
  },
  {
    name: 'Doctors', color: '#10b981', gridArea: '1 / 3 / 2 / 4',
    cols: [
      { name: 'DoctorID',   type: 'INT',          key: 'PK' },
      { name: 'FirstName',  type: 'VARCHAR(50)',   key: '' },
      { name: 'LastName',   type: 'VARCHAR(50)',   key: '' },
      { name: 'Email',      type: 'VARCHAR(150)',  key: '' },
      { name: 'Password',   type: 'VARCHAR(255)',  key: '' },
      { name: 'Specialty',  type: 'VARCHAR(100)',  key: '' },
      { name: 'IsAvailable',type: 'BIT',           key: '' },
    ],
    desc: 'Doctor accounts. IsAvailable controls visibility in the patient booking dropdown.',
    relations: ['Appointments'],
  },
  {
    name: 'Nurses', color: '#8b5cf6', gridArea: '1 / 4 / 2 / 5',
    cols: [
      { name: 'NurseID',       type: 'INT',          key: 'PK' },
      { name: 'FirstName',     type: 'VARCHAR(50)',   key: '' },
      { name: 'LastName',      type: 'VARCHAR(50)',   key: '' },
      { name: 'Email',         type: 'VARCHAR(150)',  key: '' },
      { name: 'Password',      type: 'VARCHAR(255)',  key: '' },
      { name: 'LicenseNumber', type: 'VARCHAR(50)',   key: '' },
      { name: 'IsAvailable',   type: 'BIT',           key: '' },
    ],
    desc: 'Nurse accounts with license tracking. IsAvailable controls booking visibility.',
    relations: ['NurseBookings'],
  },
  {
    name: 'Appointments', color: '#f59e0b', gridArea: '2 / 1 / 3 / 3',
    cols: [
      { name: 'AppointmentID',  type: 'INT',          key: 'PK' },
      { name: 'PatientID',      type: 'INT',          key: 'FK → Patients' },
      { name: 'DoctorID',       type: 'INT',          key: 'FK → Doctors' },
      { name: 'AppointmentDate',type: 'DATE',          key: '' },
      { name: 'AppointmentTime',type: 'TIME',          key: '' },
      { name: 'Description',    type: 'TEXT',          key: '' },
      { name: 'Status',         type: 'VARCHAR(20)',   key: '' },
    ],
    desc: 'Links Patients to Doctors. Status: Pending → Accepted/Rejected/Cancelled. Conflict detection runs before every INSERT.',
    relations: ['Payments'],
  },
  {
    name: 'NurseBookings', color: '#64748b', gridArea: '2 / 3 / 3 / 5',
    cols: [
      { name: 'BookingID',  type: 'INT',          key: 'PK' },
      { name: 'PatientID',  type: 'INT',          key: 'FK → Patients' },
      { name: 'NurseID',    type: 'INT',          key: 'FK → Nurses' },
      { name: 'BookingDate',type: 'DATE',          key: '' },
      { name: 'ServiceType',type: 'VARCHAR(100)',  key: '' },
      { name: 'Status',     type: 'VARCHAR(20)',   key: '' },
    ],
    desc: 'Tracks nurse service bookings. ServiceType determines pricing via ServicePricing static class.',
    relations: ['Payments'],
  },
  {
    name: 'Payments', color: '#06b6d4', gridArea: '3 / 2 / 4 / 4',
    cols: [
      { name: 'PaymentID',   type: 'INT',           key: 'PK' },
      { name: 'PatientID',   type: 'INT',           key: 'FK → Patients' },
      { name: 'RelatedType', type: 'VARCHAR(20)',    key: 'Appointment | NurseBooking' },
      { name: 'RelatedID',   type: 'INT',            key: 'Polymorphic FK' },
      { name: 'Amount',      type: 'DECIMAL(10,2)',  key: '' },
      { name: 'Method',      type: 'VARCHAR(20)',    key: 'Cash | Card | Bkash' },
      { name: 'Status',      type: 'VARCHAR(20)',    key: '' },
      { name: 'PaidAt',      type: 'DATETIME',       key: '' },
      { name: 'CreatedAt',   type: 'DATETIME',       key: '' },
    ],
    desc: 'Polymorphic payments table. RelatedType + RelatedID handle both Appointment and NurseBooking payments in one table, avoiding duplication.',
    relations: [],
  },
];

const QUESTIONS = [
  /* ═══ SECTION A — Concept ═══ */
  { id:1, cat:'concept', q:'What problem does DocAtHome solve?', hint:'Think: Bangladeshi healthcare market, no central booking system.',
    a:'In Bangladesh and similar markets, accessing a doctor or nurse for a home visit is unorganized — done through phone calls and word of mouth. DocAtHome centralizes the whole workflow: patients book online, doctors and nurses manage their own availability and queues, admins oversee operations, and payments are recorded digitally. No paperwork, no phone tag.',
    options:null },
  { id:2, cat:'concept', q:'Why was a desktop app chosen over web or mobile?', hint:'Think: focus on business logic, stable runtime, clinic workstations.',
    a:'Desktop was chosen deliberately — it allowed full focus on business logic, database design, and multi-role architecture without splitting effort across front-end frameworks, REST APIs, and deployment infrastructure. WinForms on .NET Framework 4.8 is mature, stable, and runs on any Windows machine without a browser or internet. A web front-end would be the natural next step.',
    options:null },
  { id:3, cat:'concept', q:'Who are the four user roles in DocAtHome?', hint:'Think: who books, who visits, who treats, who manages.',
    a:'Admin, Patient, Doctor, and Nurse. Patients book visits and pay. Doctors manage their appointment queue. Nurses manage service bookings. Admins have full read/write access to every entity and can perform all operations on behalf of any user.',
    options:['Admin, Patient, Doctor, Nurse','Admin, User, Provider, Manager','Owner, Client, Worker, Supervisor','SuperUser, Booker, Healer, Caretaker'] },
  { id:4, cat:'concept', q:'What makes DocAtHome more than a simple CRUD app?', hint:'List the features that go beyond basic insert/update/delete.',
    a:'Role-based authentication routing each user to their own UI; async database calls keeping the UI responsive; a real payment flow with service pricing; appointment conflict detection preventing double-booking; brute-force lockout protecting accounts; OTP-based password recovery; and a consistent theme system — not default WinForms chrome.',
    options:null },

  /* ═══ SECTION B — Database ═══ */
  { id:5, cat:'database', q:'How many tables are in the DocAtHome database?', hint:'Count the actor tables + relationship tables.',
    a:'7 tables: Patients, Doctors, Nurses, Admins (actor tables), Appointments, NurseBookings (relationship tables), and Payments (polymorphic).',
    options:['7','5','8','6'] },
  { id:6, cat:'database', q:'Why use a polymorphic Payments table instead of two separate tables?', hint:'One table handles both payment types — why?',
    a:'Because the payment logic is identical for both a doctor visit and a nurse service. One table means one query, one form, one insert pattern. Splitting it would duplicate code with no benefit. The trade-off is that RelatedID is not a foreign key with a constraint — a known limitation addressable with a check constraint or junction table in production.',
    options:null },
  { id:7, cat:'database', q:'Is the DocAtHome database normalized?', hint:'Third Normal Form — what does that mean?',
    a:'Yes, to Third Normal Form. Each table represents one entity. Patient details are not repeated in Appointments — only the PatientID FK is stored. Doctor specialty lives in Doctors, not copied into each appointment. The only intentional denormalization noted is a historical PatientEmail in some earlier designs, which was corrected to use PatientID.',
    options:['Yes, to Third Normal Form (3NF)','No, it has significant denormalization','Yes, to Boyce-Codd Normal Form','Partially — only first normal form'] },
  { id:8, cat:'database', q:'What is the appointment status lifecycle?', hint:'Start at Pending — what transitions are possible?',
    a:'Pending → Accepted or Rejected (by the doctor). From Pending, the patient can also Cancel it. Accepted → can be paid (triggers PaymentForm). This is enforced at the application layer through BookingStatus constants, not database constraints.',
    options:null },
  { id:9, cat:'database', q:'Which two columns make the Payments table "polymorphic"?', hint:'One identifies the type, one identifies the row.',
    a:'RelatedType (varchar: "Appointment" or "NurseBooking") and RelatedID (int holding the corresponding primary key). Together they let one Payments table serve both payment scenarios without duplication.',
    options:['RelatedType and RelatedID','PaymentType and SourceID','BookingType and BookingID','Category and ReferenceID'] },
  { id:10, cat:'database', q:'What indexes would you add in production?', hint:'Think about which columns are used in WHERE clauses most often.',
    a:'Indexes on Appointments(PatientID), Appointments(DoctorID), NurseBookings(PatientID), NurseBookings(NurseID), and Payments(RelatedID, RelatedType). The primary keys are auto-indexed. For this prototype at small data volumes it was not necessary, but the placement is clear.',
    options:null },

  /* ═══ SECTION C — Technical ═══ */
  { id:11, cat:'technical', q:'How does the login system determine which dashboard to open?', hint:'Think: sequential table checks, role detection.',
    a:'The login checks credentials against four tables in order — Admins, Patients, Doctors, Nurses — using a CheckUser helper. Whichever table returns a match determines which dashboard to open. Before any query runs, LoginAttemptTracker checks if the account is currently locked out.',
    options:null },
  { id:12, cat:'technical', q:'What is Theme.cs and why does it exist?', hint:'One change, every button updates.',
    a:'Theme.cs is a static class that centralizes all visual styling. Instead of setting BackColor, ForeColor, Font, FlatStyle on every button across 18 forms, you call Theme.Primary(button) or Theme.Outlined(button). If the brand color ever changes, it\'s one line in one file — not 40 scattered changes.',
    options:['A static styling class for centralized visual theming','A CSS stylesheet embedded in the project','A database configuration helper','A form inheritance base class'] },
  { id:13, cat:'technical', q:'How does async/await keep the UI responsive?', hint:'Task.Run + Task.WhenAll — what does this prevent?',
    a:'Database calls are wrapped in await Task.Run(() => { ... }) and awaited from async event handlers. The UI thread is never blocked during a SQL query — the form stays responsive and the user can scroll or move the window while data loads. The patient dashboard fires three queries simultaneously using Task.WhenAll.',
    options:null },
  { id:14, cat:'technical', q:'What is WindowHelper.cs and why is it needed?', hint:'FormBorderStyle.None removes what?',
    a:'Because forms use FormBorderStyle.None (no OS chrome) for the custom design, the default drag-to-move and maximize behavior is lost. WindowHelper re-implements it: StartDrag sends the WM_NCLBUTTONDOWN Windows message to trick the OS into treating a client-area mouse-down as a title-bar drag.',
    options:null },
  { id:15, cat:'technical', q:'How does appointment conflict detection work?', hint:'Which three columns are checked? What status values are excluded?',
    a:'Before inserting a new appointment, BookDoctorForm queries Appointments for any existing record where DoctorID, AppointmentDate, and AppointmentTime all match AND Status is not Rejected or Cancelled. If count > 0, the booking is blocked and the user is told to choose a different slot.',
    options:['Checks DoctorID + Date + Time, excludes Rejected/Cancelled','Checks PatientID + Date only','Checks DoctorID + Date, excludes all non-Pending','No conflict detection exists'] },
  { id:16, cat:'technical', q:'Why are SQL queries inline strings and not stored procedures?', hint:'Safe from injection? How? What would stored procs add in production?',
    a:'For a prototype, inline parameterized queries are practical and safe from SQL injection because all user input goes through SqlParameter. In production, stored procedures would be better: they are precompiled (faster), DBAs can tune them independently, and they add security by limiting what operations the app\'s DB user can perform.',
    options:null },
  { id:17, cat:'technical', q:'How is password security handled? Be honest about the limitation.', hint:'PasswordHelper.Hash — what does it actually do?',
    a:'PasswordHelper.Hash is currently a pass-through stub — it returns the password unchanged. This was an intentional prototype simplification. Replacing it with BCrypt is a two-line change. Because Hash and Verify are fully abstracted behind a static class, the rest of the codebase needs zero changes for that migration.',
    options:null },

  /* ═══ SECTION D — Features ═══ */
  { id:18, cat:'features', q:'How does OTP-based password recovery work?', hint:'Walk through: email entry → code → new password.',
    a:'The user enters their email. OtpService generates a random six-digit code, stores it in memory with a short expiry, and sends it via SMTP. The user enters the code — if it matches and has not expired, they can set a new password which is updated in whichever table (Patients, Doctors, or Nurses) the email belongs to.',
    options:null },
  { id:19, cat:'features', q:'What service types do nurses offer and what are the prices?', hint:'Five types, prices from 200 to 400 BDT.',
    a:'Injection — 200 BDT, Wound Care — 250 BDT, Physical Therapy — 300 BDT, IV Drip — 350 BDT, Elderly Care — 400 BDT. All prices are defined in the ServicePricing static class — one place to update if fees change.',
    options:['Injection 200, WoundCare 250, PhysTherapy 300, IVDrip 350, ElderlyCare 400','All services cost 300 BDT flat','Injection 150, WoundCare 200, PhysTherapy 250, IVDrip 300, ElderlyCare 350','Prices are entered manually per booking'] },
  { id:20, cat:'features', q:'What happens if a patient tries to cancel an Accepted appointment?', hint:'The cancel button checks Status before acting.',
    a:'The cancel button checks Status == "Pending" before doing anything. If the status is Accepted, Completed, or anything else, it shows an informational message: "Only Pending entries can be cancelled." The database UPDATE never runs.',
    options:['Shows message: Only Pending entries can be cancelled — no UPDATE runs','The appointment is cancelled regardless','An admin must cancel accepted appointments','The button is hidden for non-Pending appointments'] },
  { id:21, cat:'features', q:'How do the stat cards on the Patient dashboard stay current?', hint:'Task.WhenAll + refresh after every action.',
    a:'Loaded asynchronously on form load using Task.WhenAll(LoadHistoryAsync(), LoadStatsAsync(), LoadNotificationAsync()). Each count is a separate parameterized SQL scalar query. They also refresh after every action — after booking, paying, or cancelling — so the numbers are always current without restarting the form.',
    options:null },
  { id:22, cat:'features', q:'Can a doctor see which patient booked them?', hint:'What columns are visible in the DoctorDashboard grid?',
    a:'Yes. The DoctorDashboard grid shows the patient name, appointment date, time, reason/description, and status. The doctor has full context before choosing to accept or reject.',
    options:['Yes — patient name, date, time, reason, and status','No — only the date and time','Yes — but only after accepting','Only the PatientID is shown'] },

  /* ═══ SECTION E — Future/Improvements ═══ */
  { id:23, cat:'future', q:'What three things would you prioritize if you had more time?', hint:'Security, notifications, and platform.',
    a:'1. BCrypt passwords — two lines of code, massive security improvement. 2. Email notifications — when a doctor accepts or rejects, automatically email the patient. The OtpService SMTP infrastructure is already there. 3. Web front-end — expose the same SQL Server database through an ASP.NET Core API so patients can book from their phone. The database schema needs zero changes.',
    options:null },
  { id:24, cat:'future', q:'How would you add a new role, say a Pharmacist?', hint:'Four steps — DB, form, login check, admin CRUD.',
    a:'1. Create a Pharmacists table in the DB with the same column pattern as Doctors/Nurses. 2. Add a PharmacistDashboard form. 3. Add one else if (CheckUser(conn, "Pharmacists", email, password)) line in LoginForm. 4. Add CRUD to AdminDashboard. The role-based architecture makes this extension trivial.',
    options:null },
  { id:25, cat:'future', q:'How would you scale DocAtHome to 10,000 users?', hint:'Six steps — security, connections, indexes, paging, architecture.',
    a:'1. BCrypt passwords. 2. Move connection string to environment variables. 3. Configure connection pooling in DBConfig. 4. Add indexes on foreign key columns. 5. Add paging to DataGridView queries instead of loading all rows. 6. Move to a web architecture with horizontal scaling and an ASP.NET Core API.',
    options:null },

  /* ═══ SECTION F — Live Changes ═══ */
  { id:26, cat:'live', q:'Panel asks: "Change the button color." Which file do you open?', hint:'One file, every button updates.',
    a:'Open Theme.cs. Change the BackColor in Theme.Primary(): button.BackColor = Color.FromArgb(16, 185, 129); — any RGB value. Every primary button in every form updates automatically. Takes about 10 seconds.',
    options:['Theme.cs','MainForm.Designer.cs','UIHelper.cs','App.config'] },
  { id:27, cat:'live', q:'Panel asks: "Make grid rows alternating colors." One line — where?', hint:'UIHelper.cs has a StyleGrid method.',
    a:'Open UIHelper.cs, in the StyleGrid method, add: dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252); — one line, 15 seconds.',
    options:['UIHelper.cs → StyleGrid method','MainForm.Designer.cs','DBConfig.cs','Theme.cs'] },
  { id:28, cat:'live', q:'Panel asks: "Show doctor\'s specialty in appointment history." Where?', hint:'Look at the LoadHistoryAsync SQL SELECT statement.',
    a:'Open MainForm.cs, find LoadHistoryAsync, add D.Specialty AS [Specialty] to the SELECT with a JOIN to Doctors. The DataGridView uses AutoSizeColumnsMode = Fill so the new column appears automatically. About 30 seconds.',
    options:['MainForm.cs → LoadHistoryAsync SQL query','DoctorDashboard.cs','ManageDoctorsForm.cs','ViewAppointmentsForm.cs'] },
  { id:29, cat:'live', q:'Panel asks: "Add export to CSV button." Describe the approach.', hint:'StringBuilder, loop over DataGridView rows.',
    a:'Add a button click handler with: var sb = new StringBuilder(); foreach each DataGridViewRow, sb.AppendLine(string.Join(",", row.Cells.Cast<DataGridViewCell>().Select(c => c.Value))); File.WriteAllText("export.csv", sb.ToString()); — about 3 minutes.',
    options:null },
  { id:30, cat:'live', q:'Panel asks: "Sort the grid by clicking a column header." Is this hard?', hint:'DataGridView already supports it natively.',
    a:'DataGridView supports this natively. Just set dgv.AllowUserToOrderColumns = true and set SortMode to Automatic on each column in the Designer. About 30 seconds — no code logic needed.',
    options:['Already built-in — set AllowUserToOrderColumns + SortMode = Automatic','Requires a custom sort algorithm','Need to handle ColumnHeaderMouseClick event manually','Not possible in WinForms DataGridView'] },
  { id:31, cat:'live', q:'Panel asks: "Add a date filter to the appointments grid." Approach?', hint:'DateTimePicker + WHERE clause addition.',
    a:'Open ViewAppointmentsForm.cs. Add a DateTimePicker to panelToolbar in the Designer, then add AND AppointmentDate >= @Start AND AppointmentDate <= @End to the WHERE clause in LoadAppointments. Wire the picker to re-call LoadAppointments. About 3 minutes.',
    options:null },

  /* ═══ SECTION G — Worst Case ═══ */
  { id:32, cat:'worstcase', q:'Panel says: "Your passwords are stored as plain text — defend that."', hint:'Acknowledge honestly, explain the design decision, show BCrypt readiness.',
    a:'"You\'re absolutely right, and I want to address that directly. PasswordHelper is deliberately implemented as a stub for the prototype — the method signature, abstraction, and interface are all correct. Replacing it with BCrypt is a two-line change. I made the conscious decision to ship a complete feature set and flag security hardening as a known next step. In production, this ships with BCrypt."',
    options:null },
  { id:33, cat:'worstcase', q:'Panel asks: "What happens if the database goes down?"', hint:'try-catch wrapping, user message, no crash.',
    a:'"Every database call is wrapped in a try-catch. If the connection fails, the user sees a clear error message — Unable to connect to the server — and the app stays open. No crash, no data corruption. In production I would add a retry policy and a connection health check on startup."',
    options:null },
  { id:34, cat:'worstcase', q:'Panel asks: "Can two admins use the system simultaneously?"', hint:'SQL Server handles this natively.',
    a:'"SQL Server handles concurrent connections natively — each user gets their own SqlConnection object. The application doesn\'t use in-memory shared state, so multiple simultaneous users are safe. For high-concurrency scenarios I would add row-level locking and optimistic concurrency checks on status updates."',
    options:['Yes — SQL Server handles concurrent connections natively','No — only one user at a time','Yes — but only if using the same PC','Only two admins maximum'] },
  { id:35, cat:'worstcase', q:'Panel asks: "There is no server-side validation — what if someone bypasses the UI?"', hint:'Application layer validates. DB constraints are the production answer.',
    a:'"The application layer validates all inputs before every INSERT and UPDATE. For a truly adversarial environment, I would add CHECK constraints and stored procedures at the database layer, so even a direct SQL connection cannot insert invalid data. That\'s on the production hardening roadmap."',
    options:null },
  { id:36, cat:'worstcase', q:'Panel: "Your app only runs on Windows — is that a limitation?"', hint:'Deliberate scope decision + migration path.',
    a:'"It\'s a deliberate scope decision. WinForms targets Windows, where admin workstations in clinics typically run. For cross-platform reach, the next iteration would expose the same SQL Server database through an ASP.NET Core API — Angular, React, or mobile can then consume it. The database schema requires zero changes for that migration."',
    options:null },
  { id:37, cat:'worstcase', q:'Panel: "What if a nurse marks herself unavailable after being booked?"', hint:'Existing bookings vs new bookings.',
    a:'"Existing bookings are not affected — they stay in NurseBookings with their current status. The IsAvailable flag only affects the BookNurseForm dropdown — unavailable nurses simply don\'t appear for new bookings. The admin can still manually assign an unavailable nurse through AdminBookNurseForm if needed."',
    options:null },

  /* ═══ SECTION H — Quick Fire ═══ */
  { id:38, cat:'quickfire', q:'What language is DocAtHome written in?', hint:'', a:'C#', options:['C#','Java','Python','VB.NET'] },
  { id:39, cat:'quickfire', q:'What is the UI technology?', hint:'', a:'Windows Forms (WinForms)', options:['Windows Forms (WinForms)','WPF','MAUI','ASP.NET'] },
  { id:40, cat:'quickfire', q:'What version of .NET Framework?', hint:'', a:'4.8', options:['4.8','4.5','6.0','3.5'] },
  { id:41, cat:'quickfire', q:'What database is used?', hint:'', a:'Microsoft SQL Server', options:['Microsoft SQL Server','MySQL','SQLite','PostgreSQL'] },
  { id:42, cat:'quickfire', q:'What is the DB access layer?', hint:'', a:'ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter)', options:['ADO.NET','Entity Framework','Dapper','NHibernate'] },
  { id:43, cat:'quickfire', q:'How many forms are in the project?', hint:'', a:'18', options:['18','12','22','15'] },
  { id:44, cat:'quickfire', q:'How many database tables?', hint:'', a:'7', options:['7','5','9','6'] },
  { id:45, cat:'quickfire', q:'What is the brute-force lockout policy?', hint:'', a:'5 failed attempts → 15 minute lockout', options:['5 attempts → 15 min lockout','3 attempts → 30 min lockout','10 attempts → 5 min lockout','5 attempts → permanent lock'] },
  { id:46, cat:'quickfire', q:'What is the doctor appointment price?', hint:'', a:'500 BDT', options:['500 BDT','300 BDT','750 BDT','1000 BDT'] },
  { id:47, cat:'quickfire', q:'Where is the connection string stored?', hint:'', a:'App.config (read via ConfigurationManager)', options:['App.config','Web.config','appsettings.json','hardcoded in DBConfig'] },
  { id:48, cat:'quickfire', q:'How is OTP delivered?', hint:'', a:'Email via SMTP (OtpService.cs)', options:['Email via SMTP','SMS via Twilio','Push notification','Shown on screen'] },
  { id:49, cat:'quickfire', q:'What async pattern is used?', hint:'', a:'async/await + Task.Run', options:['async/await + Task.Run','Thread + BackgroundWorker','Parallel.For','Fire and forget'] },
  { id:50, cat:'quickfire', q:'Which class centralizes all visual styles?', hint:'', a:'Theme.cs', options:['Theme.cs','UIHelper.cs','WindowHelper.cs','StyleManager.cs'] },
];

const CATEGORIES = [
  { id:'concept',   letter:'A', name:'Concept & Motivation',   color:'#3b82f6', emoji:'💡' },
  { id:'database',  letter:'B', name:'Database Design',        color:'#10b981', emoji:'🗄️' },
  { id:'technical', letter:'C', name:'Technical & Code',       color:'#8b5cf6', emoji:'⚙️' },
  { id:'features',  letter:'D', name:'Features & Functionality',color:'#f59e0b', emoji:'✨' },
  { id:'future',    letter:'E', name:'"What Would You Add?"',  color:'#06b6d4', emoji:'🚀' },
  { id:'live',      letter:'F', name:'Live Change Requests',   color:'#ec4899', emoji:'🔧' },
  { id:'worstcase', letter:'G', name:'Worst-Case Scenarios',   color:'#ef4444', emoji:'⚠️' },
  { id:'quickfire', letter:'H', name:'Quick-Fire Facts',       color:'#64748b', emoji:'⚡' },
];

const BADGES = [
  { id:'first',      icon:'🎯', name:'First Blood',   desc:'Answer your first question',        cond: s => s.totalAnswered >= 1 },
  { id:'streak3',    icon:'🔥', name:'On Fire',        desc:'Get a 3-question streak',           cond: s => s.streak >= 3 },
  { id:'streak5',    icon:'💥', name:'Unstoppable',    desc:'Get a 5-question streak',           cond: s => s.streak >= 5 },
  { id:'concept',    icon:'💡', name:'Concept Master', desc:'Complete Concept category',         cond: s => s.catDone.includes('concept') },
  { id:'database',   icon:'🗄️', name:'DB Architect',  desc:'Complete Database category',        cond: s => s.catDone.includes('database') },
  { id:'technical',  icon:'⚙️', name:'Tech Wizard',   desc:'Complete Technical category',       cond: s => s.catDone.includes('technical') },
  { id:'quickfire',  icon:'⚡', name:'Speed Demon',    desc:'Complete Quick-Fire category',      cond: s => s.catDone.includes('quickfire') },
  { id:'worstcase',  icon:'🛡️', name:'Bulletproof',   desc:'Complete Worst-Case category',      cond: s => s.catDone.includes('worstcase') },
  { id:'allcats',    icon:'🏆', name:'Defense Ready',  desc:'Complete all 8 categories',         cond: s => s.catDone.length >= 8 },
  { id:'xp500',      icon:'💎', name:'XP Master',      desc:'Earn 500+ XP',                     cond: s => s.xp >= 500 },
  { id:'senior',     icon:'🎓', name:'Senior Dev',     desc:'Reach Senior level',               cond: s => s.level >= 3 },
];

const LEVELS = [
  { name: 'Intern',     xp: 0   },
  { name: 'Resident',   xp: 100 },
  { name: 'Senior Dev', xp: 250 },
  { name: 'Architect',  xp: 500 },
  { name: 'Attending',  xp: 800 },
];

/* ── GAME STATE ─────────────────────────────────────────────── */
const KEY = 'docathome_defense_v2';

function loadState() {
  try {
    const s = JSON.parse(localStorage.getItem(KEY) || '{}');
    return {
      xp: s.xp || 0,
      level: s.level || 0,
      streak: s.streak || 0,
      totalAnswered: s.totalAnswered || 0,
      totalCorrect: s.totalCorrect || 0,
      catDone: s.catDone || [],
      catAnswered: s.catAnswered || {},
      catCorrect: s.catCorrect || {},
      badges: s.badges || [],
    };
  } catch { return { xp:0, level:0, streak:0, totalAnswered:0, totalCorrect:0, catDone:[], catAnswered:{}, catCorrect:{}, badges:[] }; }
}

function saveState() { localStorage.setItem(KEY, JSON.stringify(GS)); }

const GS = loadState();

/* game panel state */
let currentCat   = null;
let currentQs    = [];
let currentIdx   = 0;
let currentMode  = 'study';
let cardFlipped  = false;
let quizAnswered = false;
let speedTimer   = null;
let speedLeft    = 20;

/* ── INIT ──────────────────────────────────────────────────── */
document.addEventListener('DOMContentLoaded', () => {
  initNavbar();
  initHero();
  initRoles();
  initSchema();
  initArena();
  initDefenseKit();
  updateHUD();
  renderBadges();
});

/* ── NAVBAR ─────────────────────────────────────────────────── */
function initNavbar() {
  window.addEventListener('scroll', () => {
    document.getElementById('navbar').classList.toggle('scrolled', scrollY > 20);
    highlightNavLink();
  });
  document.querySelectorAll('.nav-link').forEach(a => {
    a.addEventListener('click', e => {
      e.preventDefault();
      const target = document.querySelector(a.getAttribute('href'));
      if (target) target.scrollIntoView({ behavior: 'smooth' });
    });
  });
}

function highlightNavLink() {
  const sections = ['hero','roles','schema','arena','defensekit'];
  let current = 'hero';
  sections.forEach(id => {
    const el = document.getElementById(id);
    if (el && el.getBoundingClientRect().top < 120) current = id;
  });
  document.querySelectorAll('.nav-link').forEach(a => {
    a.classList.toggle('active', a.getAttribute('href') === '#' + current);
  });
}

/* ── HERO: animated counters ────────────────────────────────── */
function initHero() {
  const obs = new IntersectionObserver(entries => {
    entries.forEach(e => {
      if (e.isIntersecting) { animateCounters(); obs.disconnect(); }
    });
  }, { threshold: 0.3 });
  obs.observe(document.getElementById('hero-stats'));
}

function animateCounters() {
  document.querySelectorAll('.stat-num').forEach(el => {
    const target = +el.dataset.count;
    let cur = 0;
    const step = target / 40;
    const interval = setInterval(() => {
      cur = Math.min(cur + step, target);
      el.textContent = Math.floor(cur);
      if (cur >= target) clearInterval(interval);
    }, 30);
  });
}

/* ── ROLES ─────────────────────────────────────────────────── */
let activeRole = null;
function initRoles() {
  const grid = document.getElementById('roles-grid');
  ROLES.forEach(role => {
    const card = document.createElement('div');
    card.className = 'role-card';
    card.style.setProperty('--role-color', role.color);
    card.innerHTML = `
      <div class="role-icon">${role.icon}</div>
      <div class="role-name">${role.name}</div>
      <div class="role-tagline">${role.tagline}</div>
      <span class="role-pill" style="color:${role.color}">${role.pill}</span>
    `;
    card.addEventListener('click', () => openRole(role, card));
    grid.appendChild(card);
  });
}

function openRole(role, card) {
  document.querySelectorAll('.role-card').forEach(c => c.classList.remove('active'));
  card.classList.add('active');
  activeRole = role.id;

  const detail = document.getElementById('role-detail');
  detail.classList.remove('hidden');
  detail.style.setProperty('--role-color', role.color);

  const stepsHTML = role.steps.map((s,i) => `
    <li class="rd-step">
      <div class="step-num" style="background:${role.color}">${i+1}</div>
      <div class="step-text">${s}</div>
    </li>`).join('');

  const featHTML = role.features.map(f => `
    <div class="rd-feature">
      <span class="rf-icon">${f.icon}</span>
      <span>${f.text}</span>
    </div>`).join('');

  detail.innerHTML = `
    <div class="rd-header">
      <div class="rd-icon">${role.icon}</div>
      <div>
        <div class="rd-title" style="color:${role.color}">${role.name}</div>
        <div class="rd-sub">${role.tagline}</div>
      </div>
    </div>
    <div class="rd-body">
      <div class="rd-col">
        <h4>User Journey</h4>
        <ul class="rd-steps">${stepsHTML}</ul>
      </div>
      <div class="rd-col">
        <h4>Key Features</h4>
        <div class="rd-features">${featHTML}</div>
      </div>
    </div>
  `;

  detail.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
}

/* ── SCHEMA ─────────────────────────────────────────────────── */
let activeTable = null;
function initSchema() {
  const diag = document.getElementById('schema-diagram');
  diag.style.gridTemplateColumns = 'repeat(4,1fr)';
  diag.style.gridTemplateRows = 'repeat(3, auto)';

  SCHEMA_TABLES.forEach((tbl, idx) => {
    const card = document.createElement('div');
    card.className = 'tbl-card';
    card.id = 'tbl-' + tbl.name;
    card.style.setProperty('--tbl-color', tbl.color);
    card.style.gridArea = tbl.gridArea;

    const colsHTML = tbl.cols.slice(0,4).map(c => `
      <div class="tbl-col">
        ${c.key ? `<span class="col-${c.key.includes('PK')?'pk':'fk'}">${c.key.includes('PK')?'PK':'FK'}</span>` : '<span style="width:22px"></span>'}
        <span>${c.name}</span>
      </div>`).join('');

    card.innerHTML = `
      <div class="tbl-head">
        <div class="tbl-dot"></div>
        ${tbl.name}
      </div>
      <div class="tbl-cols">
        ${colsHTML}
        ${tbl.cols.length > 4 ? `<div class="tbl-col" style="color:var(--slate)">+${tbl.cols.length-4} more…</div>` : ''}
      </div>
    `;
    card.addEventListener('click', () => openTable(tbl, card));
    diag.appendChild(card);
  });

  setTimeout(drawSchemaLines, 300);
  window.addEventListener('resize', debounce(drawSchemaLines, 200));
}

function openTable(tbl, card) {
  document.querySelectorAll('.tbl-card').forEach(c => c.classList.remove('active'));
  card.classList.add('active');
  activeTable = tbl.name;

  const detail = document.getElementById('schema-detail');
  detail.classList.remove('hidden');

  const colsHTML = tbl.cols.map(c => `
    <div class="sd-col-row">
      <div class="sd-col-name">${c.name}</div>
      <div class="sd-col-type">${c.type}</div>
      ${c.key ? `<div class="sd-col-note">${c.key}</div>` : ''}
    </div>`).join('');

  const relHTML = tbl.relations.length
    ? `<p style="margin-top:16px;font-size:.8rem;color:var(--muted)">References: <strong style="color:var(--blue)">${tbl.relations.join(', ')}</strong></p>`
    : '';

  detail.innerHTML = `
    <div class="sd-title">
      <span style="color:${tbl.color}">⬟</span> ${tbl.name}
    </div>
    <p style="font-size:.875rem;color:var(--muted);margin-bottom:16px">${tbl.desc}</p>
    <div class="sd-cols">${colsHTML}</div>
    ${relHTML}
  `;
  detail.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
}

function drawSchemaLines() {
  const svg = document.getElementById('schema-svg');
  const wrap = document.querySelector('.schema-wrap');
  if (!svg || !wrap) return;
  svg.innerHTML = '';
  const wRect = wrap.getBoundingClientRect();

  const connections = [
    ['Patients','Appointments'], ['Doctors','Appointments'],
    ['Patients','NurseBookings'], ['Nurses','NurseBookings'],
    ['Appointments','Payments'], ['NurseBookings','Payments'],
    ['Patients','Payments'],
  ];

  connections.forEach(([from, to]) => {
    const a = document.getElementById('tbl-'+from);
    const b = document.getElementById('tbl-'+to);
    if (!a || !b) return;
    const ar = a.getBoundingClientRect();
    const br = b.getBoundingClientRect();
    const x1 = ar.left - wRect.left + ar.width/2;
    const y1 = ar.top  - wRect.top  + ar.height/2;
    const x2 = br.left - wRect.left + br.width/2;
    const y2 = br.top  - wRect.top  + br.height/2;
    const line = document.createElementNS('http://www.w3.org/2000/svg','line');
    line.setAttribute('x1', x1); line.setAttribute('y1', y1);
    line.setAttribute('x2', x2); line.setAttribute('y2', y2);
    line.setAttribute('stroke', 'rgba(148,163,184,0.2)');
    line.setAttribute('stroke-width', '1.5');
    line.setAttribute('stroke-dasharray', '4 4');
    svg.appendChild(line);
  });
}

/* ── ARENA ─────────────────────────────────────────────────── */
function initArena() {
  renderCategories();
  document.getElementById('btn-back').addEventListener('click', closeGamePanel);
  document.getElementById('btn-next').addEventListener('click', nextQuestion);
  document.getElementById('btn-prev').addEventListener('click', prevQuestion);
  document.querySelectorAll('.mode-tab').forEach(btn => {
    btn.addEventListener('click', () => {
      document.querySelectorAll('.mode-tab').forEach(b => b.classList.remove('active'));
      btn.classList.add('active');
      currentMode = btn.dataset.mode;
      currentIdx = 0;
      renderQuestion();
    });
  });
}

function renderCategories() {
  const grid = document.getElementById('cat-grid');
  grid.innerHTML = '';
  CATEGORIES.forEach(cat => {
    const qs = QUESTIONS.filter(q => q.cat === cat.id);
    const done = GS.catAnswered[cat.id] || 0;
    const correct = GS.catCorrect[cat.id] || 0;
    const pct = qs.length > 0 ? Math.round((done / qs.length) * 100) : 0;
    const completed = GS.catDone.includes(cat.id);

    const card = document.createElement('div');
    card.className = 'cat-card' + (completed ? ' completed' : '');
    card.style.setProperty('--cat-color', cat.color);
    card.innerHTML = `
      <div class="cat-card-top">
        <div class="cat-letter">${cat.emoji}</div>
        <div class="cat-score">${correct}/${qs.length} correct</div>
      </div>
      <div class="cat-name">${cat.name}</div>
      <div class="cat-count">${cat.letter} · ${qs.length} questions</div>
      <div class="cat-prog-bar"><div class="cat-prog-fill" style="width:${pct}%"></div></div>
      <div class="cat-badge">✅</div>
    `;
    card.addEventListener('click', () => openCategory(cat));
    grid.appendChild(card);
  });
}

function openCategory(cat) {
  currentCat = cat;
  currentQs  = shuffle(QUESTIONS.filter(q => q.cat === cat.id));
  currentIdx = 0;
  currentMode = 'study';
  cardFlipped = false;
  clearSpeedTimer();

  document.querySelector('.mode-tab[data-mode="study"]').click();
  document.getElementById('gp-cat-label').textContent = cat.emoji + ' ' + cat.name;
  document.getElementById('gp-cat-label').style.color = cat.color;

  document.getElementById('cat-grid').style.display = 'none';
  document.querySelector('.badges-wrap').style.display = 'none';
  const panel = document.getElementById('game-panel');
  panel.classList.remove('hidden');
  panel.scrollIntoView({ behavior: 'smooth', block: 'start' });

  renderQuestion();
}

function closeGamePanel() {
  clearSpeedTimer();
  document.getElementById('game-panel').classList.add('hidden');
  document.getElementById('cat-grid').style.display = '';
  document.querySelector('.badges-wrap').style.display = '';
  renderCategories();
}

function renderQuestion() {
  if (!currentQs.length) return;
  const q = currentQs[currentIdx];
  updateProgressBar();
  renderDots();

  if (currentMode === 'study') renderStudy(q);
  else if (currentMode === 'quiz') renderQuiz(q);
  else renderSpeed(q);

  document.getElementById('btn-prev').disabled = currentIdx === 0;
  document.getElementById('btn-next').textContent = currentIdx === currentQs.length - 1 ? 'Finish ✓' : 'Next →';
}

function updateProgressBar() {
  const pct = ((currentIdx + 1) / currentQs.length) * 100;
  document.getElementById('gp-prog-fill').style.width = pct + '%';
  document.getElementById('gp-progress').textContent = `${currentIdx+1} / ${currentQs.length}`;
}

function renderDots() {
  const container = document.getElementById('q-dots');
  container.innerHTML = '';
  currentQs.forEach((q, i) => {
    const dot = document.createElement('div');
    dot.className = 'q-dot' + (i === currentIdx ? ' active' : '');
    container.appendChild(dot);
  });
}

/* STUDY — flashcard */
function renderStudy(q) {
  cardFlipped = false;
  document.getElementById('q-area').innerHTML = `
    <div class="flashcard" id="flashcard">
      <div class="flashcard-inner">
        <div class="flashcard-front">
          <div class="fc-label">Question ${currentIdx+1} of ${currentQs.length}</div>
          <div class="fc-question">${q.q}</div>
          ${q.hint ? `<div class="fc-hint">💡 ${q.hint}</div>` : ''}
          <div class="fc-flip-hint">👆 Click card to reveal answer</div>
        </div>
        <div class="flashcard-back">
          <div class="fc-label">Answer</div>
          <div class="fc-answer">${q.a}</div>
        </div>
      </div>
    </div>
    <div style="text-align:center;margin-top:20px;display:flex;gap:12px;justify-content:center">
      <button class="btn btn-ghost" onclick="markStudy(false)">👎 Review Again</button>
      <button class="btn btn-primary" onclick="markStudy(true)">👍 Got It!</button>
    </div>
  `;
  document.getElementById('flashcard').addEventListener('click', () => {
    document.getElementById('flashcard').classList.toggle('flipped');
    cardFlipped = !cardFlipped;
  });
}

function markStudy(correct) {
  if (!cardFlipped) {
    document.getElementById('flashcard').classList.add('flipped');
    cardFlipped = true;
    return;
  }
  recordAnswer(correct);
}

/* QUIZ — multiple choice */
function renderQuiz(q) {
  quizAnswered = false;
  const opts = q.options
    ? shuffleOpts(q.options, q.a)
    : generateOptions(q.a, QUESTIONS.filter(x => x.cat === q.cat && x.id !== q.id));

  const optsHTML = opts.map((opt, i) => `
    <button class="quiz-option" data-answer="${opt}" onclick="checkQuiz(this,'${escQ(q.a)}')">
      <span class="opt-letter">${'ABCD'[i]}</span>
      <span>${opt}</span>
    </button>`).join('');

  document.getElementById('q-area').innerHTML = `
    <div class="quiz-wrap">
      <div class="quiz-question">${q.q}</div>
      <div class="quiz-options">${optsHTML}</div>
      <div id="quiz-feedback"></div>
    </div>
  `;
}

function checkQuiz(btn, correctAns) {
  if (quizAnswered) return;
  quizAnswered = true;
  const chosen = btn.dataset.answer;
  const correct = chosen === correctAns ||
    chosen.toLowerCase().includes(correctAns.toLowerCase().slice(0,20));

  document.querySelectorAll('.quiz-option').forEach(b => {
    b.disabled = true;
    if (b.dataset.answer === correctAns || b.dataset.answer.toLowerCase().includes(correctAns.toLowerCase().slice(0,20))) {
      b.classList.add('correct');
    } else if (b === btn) {
      b.classList.add('wrong');
    }
  });

  const fb = document.getElementById('quiz-feedback');
  if (correct) {
    fb.className = 'quiz-feedback ok';
    fb.innerHTML = `✅ <strong>Correct!</strong> +${xpFor(true)} XP`;
  } else {
    fb.className = 'quiz-feedback bad';
    fb.innerHTML = `❌ <strong>Wrong.</strong> Correct: <em>${correctAns}</em>`;
  }
  recordAnswer(correct);
}

function generateOptions(correct, pool) {
  const wrongs = shuffle(pool.map(q => q.a.slice(0, 60))).slice(0, 3);
  return shuffle([correct, ...wrongs]);
}

function shuffleOpts(opts, correct) {
  return shuffle([...opts]);
}

/* SPEED mode */
function renderSpeed(q) {
  clearSpeedTimer();
  speedLeft = 20;
  document.getElementById('q-area').innerHTML = `
    <div class="speed-wrap">
      <div class="speed-timer-row">
        <div class="speed-timer" id="speed-timer">20</div>
        <div style="color:var(--muted);font-size:.8rem">seconds</div>
      </div>
      <div class="speed-q">${q.q}</div>
      <div style="font-size:.8rem;color:var(--muted);margin-bottom:20px;font-style:italic">${q.hint || 'Think fast!'}</div>
      <div class="speed-actions">
        <button class="btn btn-know" onclick="speedAnswer(true)">✅ I Know It</button>
        <button class="btn btn-skip" onclick="speedAnswer(false)">⏭ Skip</button>
      </div>
    </div>
  `;
  speedTimer = setInterval(() => {
    speedLeft--;
    const el = document.getElementById('speed-timer');
    if (el) {
      el.textContent = speedLeft;
      el.classList.toggle('urgent', speedLeft <= 5);
    }
    if (speedLeft <= 0) { clearSpeedTimer(); speedAnswer(false); }
  }, 1000);
}

function speedAnswer(knew) {
  clearSpeedTimer();
  recordAnswer(knew);
  const bonus = Math.max(0, speedLeft * 2);
  if (knew && bonus > 0) {
    addXP(bonus);
    toast(`⚡ Speed bonus: +${bonus} XP`, 'xp');
  }
  setTimeout(() => {
    if (currentIdx < currentQs.length - 1) nextQuestion();
    else finishCategory();
  }, 500);
}

function clearSpeedTimer() { if (speedTimer) { clearInterval(speedTimer); speedTimer = null; } }

/* ── NAVIGATION ────────────────────────────────────────────── */
function nextQuestion() {
  clearSpeedTimer();
  if (currentIdx < currentQs.length - 1) {
    currentIdx++;
    cardFlipped = false;
    renderQuestion();
  } else {
    finishCategory();
  }
}

function prevQuestion() {
  clearSpeedTimer();
  if (currentIdx > 0) { currentIdx--; cardFlipped = false; renderQuestion(); }
}

function finishCategory() {
  clearSpeedTimer();
  const total = currentQs.length;
  const correct = GS.catCorrect[currentCat.id] || 0;
  const pct = Math.round((correct / total) * 100);

  if (!GS.catDone.includes(currentCat.id)) {
    GS.catDone.push(currentCat.id);
    addXP(50);
    toast(`🏅 Category complete! +50 XP`, 'level');
    saveState();
    checkBadges();
  }

  document.getElementById('q-area').innerHTML = `
    <div class="speed-result">
      <div style="font-size:3rem;margin-bottom:8px">${pct >= 80 ? '🏆' : pct >= 50 ? '🎯' : '📖'}</div>
      <div class="speed-score">${pct}%</div>
      <div class="speed-msg">${correct} / ${total} correct in <strong style="color:var(--blue)">${currentCat.name}</strong></div>
      <div style="margin-top:24px;display:flex;gap:12px;justify-content:center;flex-wrap:wrap">
        <button class="btn btn-ghost" onclick="retryCategory()">🔄 Retry</button>
        <button class="btn btn-primary" onclick="closeGamePanel()">← Back to Categories</button>
      </div>
    </div>
  `;
  if (pct === 100) confetti();
}

function retryCategory() {
  currentQs = shuffle(QUESTIONS.filter(q => q.cat === currentCat.id));
  currentIdx = 0; cardFlipped = false;
  renderQuestion();
}

/* ── SCORING ────────────────────────────────────────────────── */
function recordAnswer(correct) {
  GS.totalAnswered++;
  if (correct) {
    GS.streak++;
    GS.totalCorrect++;
    GS.catCorrect[currentCat.id] = (GS.catCorrect[currentCat.id] || 0) + 1;
    const xpGain = xpFor(true);
    addXP(xpGain);
    spawnXPBurst('+' + xpGain + ' XP');
    if (GS.streak === 3) { spawnFire(); toast('🔥 3-streak! Keep going!', 'xp'); }
    if (GS.streak === 5) { spawnFire(); toast('💥 5-streak! You\'re unstoppable!', 'xp'); }
  } else {
    GS.streak = 0;
  }
  GS.catAnswered[currentCat.id] = (GS.catAnswered[currentCat.id] || 0) + 1;
  saveState();
  updateHUD();
  checkBadges();
}

function xpFor(correct) {
  if (!correct) return 0;
  const multiplier = GS.streak >= 5 ? 3 : GS.streak >= 3 ? 2 : 1;
  return 10 * multiplier;
}

function addXP(amount) {
  GS.xp += amount;
  const newLevel = getLevelIdx(GS.xp);
  if (newLevel > GS.level) {
    GS.level = newLevel;
    toast(`🎓 Level up! You are now: ${LEVELS[GS.level].name}`, 'level');
    confetti();
  }
  saveState();
  updateHUD();
}

function getLevelIdx(xp) {
  let idx = 0;
  LEVELS.forEach((l, i) => { if (xp >= l.xp) idx = i; });
  return idx;
}

/* ── HUD ─────────────────────────────────────────────────────── */
function updateHUD() {
  const levelName = LEVELS[GS.level]?.name || 'Intern';
  const nextLvl   = LEVELS[GS.level + 1];
  const xpCurrent = GS.xp - (LEVELS[GS.level]?.xp || 0);
  const xpNeeded  = nextLvl ? nextLvl.xp - (LEVELS[GS.level]?.xp || 0) : 9999;
  const pct       = nextLvl ? Math.min(100, (xpCurrent / xpNeeded) * 100) : 100;

  document.getElementById('hud-level').textContent    = levelName;
  document.getElementById('hud-xp').textContent       = GS.xp;
  document.getElementById('hud-next').textContent     = nextLvl ? nextLvl.xp : '∞';
  document.getElementById('hud-streak').textContent   = GS.streak;
  document.getElementById('hud-correct').textContent  = `${GS.totalCorrect}/${GS.totalAnswered}`;
  document.getElementById('xp-fill').style.width      = pct + '%';

  document.getElementById('nav-level').textContent = levelName;
  document.getElementById('nav-xp').textContent    = GS.xp;
}

/* ── BADGES ─────────────────────────────────────────────────── */
function checkBadges() {
  BADGES.forEach(badge => {
    if (!GS.badges.includes(badge.id) && badge.cond(GS)) {
      GS.badges.push(badge.id);
      saveState();
      toast(`${badge.icon} Badge unlocked: ${badge.name}!`, 'badge');
      confetti();
      renderBadges();
    }
  });
}

function renderBadges() {
  const grid = document.getElementById('badges-grid');
  grid.innerHTML = '';
  BADGES.forEach(b => {
    const el = document.createElement('div');
    el.className = 'badge-item' + (GS.badges.includes(b.id) ? ' earned' : '');
    el.innerHTML = `
      <div class="badge-icon">${b.icon}</div>
      <div class="badge-info">
        <div class="badge-name">${b.name}</div>
        <div class="badge-desc">${b.desc}</div>
      </div>
    `;
    grid.appendChild(el);
  });
}

/* ── DEFENSE KIT ────────────────────────────────────────────── */
function initDefenseKit() {
  renderCreds();
  renderPricing();
  renderTiming();
  renderFlow();
  renderQuickfire();
  renderConfidence();
}

const CREDENTIALS = [
  { role:'Admin',   color:'#ef4444', email:'admin@docathome.com',          pass:'Admin@1234'   },
  { role:'Doctor',  color:'#10b981', email:'sarah.wilson@docathome.com',   pass:'Doctor@1234'  },
  { role:'Doctor',  color:'#10b981', email:'michael.brown@docathome.com',  pass:'Doctor@1234'  },
  { role:'Nurse',   color:'#8b5cf6', email:'nurse.maria@docathome.com',    pass:'Nurse@1234'   },
  { role:'Nurse',   color:'#8b5cf6', email:'nurse.sara@docathome.com',     pass:'Nurse@1234'   },
  { role:'Patient', color:'#3b82f6', email:'john.doe@example.com',         pass:'Patient@1234' },
  { role:'Patient', color:'#3b82f6', email:'jane.smith@example.com',       pass:'Patient@1234' },
];

function renderCreds() {
  document.getElementById('creds-body').innerHTML = CREDENTIALS.map(c => `
    <div class="cred-row">
      <span class="cred-role" style="background:${c.color}20;color:${c.color}">${c.role}</span>
      <span class="cred-email">${c.email}</span>
      <span class="cred-pass" style="color:var(--amber)">${c.pass}</span>
    </div>`).join('');
}

function copyCreds() {
  const text = CREDENTIALS.map(c => `${c.role.padEnd(8)} | ${c.email.padEnd(35)} | ${c.pass}`).join('\n');
  navigator.clipboard.writeText(text).then(() => toast('✅ Credentials copied!', 'ok'));
}
window.copyCreds = copyCreds;

function renderPricing() {
  const items = [
    { name:'Doctor Appointment', price:'500 BDT', color:'#3b82f6' },
    { name:'Elderly Care',       price:'400 BDT', color:'#8b5cf6' },
    { name:'IV Drip',            price:'350 BDT', color:'#06b6d4' },
    { name:'Physical Therapy',   price:'300 BDT', color:'#10b981' },
    { name:'Wound Care',         price:'250 BDT', color:'#f59e0b' },
    { name:'Injection',          price:'200 BDT', color:'#ef4444' },
  ];
  document.getElementById('pricing-body').innerHTML = items.map(i => `
    <div class="price-row">
      <span>${i.name}</span>
      <span class="price-amount" style="color:${i.color}">${i.price}</span>
    </div>`).join('');
}

function renderTiming() {
  const items = [
    { sec:'Opening', time:'0:30' },
    { sec:'Admin Dashboard + CRUD', time:'1:30' },
    { sec:'Doctor Bookings (Admin)', time:'1:00' },
    { sec:'Patient Dashboard', time:'2:00' },
    { sec:'Doctor Dashboard', time:'1:00' },
    { sec:'Nurse Dashboard', time:'0:45' },
    { sec:'Password Reset / Security', time:'0:30' },
    { sec:'Closing Statement', time:'0:30' },
    { sec:'TOTAL', time:'~8 min', total:true },
  ];
  document.getElementById('timing-body').innerHTML = items.map(i => `
    <div class="timing-row ${i.total?'total':''}">
      <span class="timing-section">${i.sec}</span>
      <span class="timing-time">${i.time}</span>
    </div>`).join('');
}

function renderFlow() {
  document.getElementById('flow-body').innerHTML = `
    <div style="display:flex;flex-direction:column;gap:12px;padding:8px 0">
      <div style="display:flex;align-items:center;gap:8px;flex-wrap:wrap">
        <span class="status-node" style="background:rgba(59,130,246,0.15);color:#93c5fd;border:1px solid rgba(59,130,246,0.3)">Pending</span>
        <span class="status-arrow">→</span>
        <span class="status-node" style="background:rgba(16,185,129,0.15);color:#6ee7b7;border:1px solid rgba(16,185,129,0.3)">Accepted</span>
        <span class="status-arrow">→</span>
        <span class="status-node" style="background:rgba(6,182,212,0.15);color:#67e8f9;border:1px solid rgba(6,182,212,0.3)">Paid</span>
      </div>
      <div style="display:flex;align-items:center;gap:8px;flex-wrap:wrap">
        <span class="status-node" style="background:rgba(59,130,246,0.15);color:#93c5fd;border:1px solid rgba(59,130,246,0.3)">Pending</span>
        <span class="status-arrow">→</span>
        <span class="status-node" style="background:rgba(239,68,68,0.15);color:#fca5a5;border:1px solid rgba(239,68,68,0.3)">Rejected</span>
      </div>
      <div style="display:flex;align-items:center;gap:8px;flex-wrap:wrap">
        <span class="status-node" style="background:rgba(59,130,246,0.15);color:#93c5fd;border:1px solid rgba(59,130,246,0.3)">Pending</span>
        <span class="status-arrow">→</span>
        <span class="status-node" style="background:rgba(100,116,139,0.15);color:#94a3b8;border:1px solid rgba(100,116,139,0.3)">Cancelled</span>
        <span style="font-size:.75rem;color:var(--muted)">(patient only)</span>
      </div>
      <p style="font-size:.75rem;color:var(--muted);margin-top:4px">Enforced at application layer via BookingStatus constants. Only Pending appointments can be cancelled by the patient.</p>
    </div>
  `;
}

function renderQuickfire() {
  const facts = [
    { q:'Language',         a:'C#' },
    { q:'Framework',        a:'.NET Framework 4.8' },
    { q:'UI Technology',    a:'WinForms' },
    { q:'Database',         a:'SQL Server' },
    { q:'DB Access',        a:'ADO.NET' },
    { q:'Async Pattern',    a:'async/await + Task.Run' },
    { q:'Form Count',       a:'18 Forms' },
    { q:'Table Count',      a:'7 Tables' },
    { q:'User Roles',       a:'4 Roles' },
    { q:'Doctor Appt Price',a:'500 BDT' },
    { q:'Nurse Services',   a:'5 types (200–400 BDT)' },
    { q:'Lockout Policy',   a:'5 attempts → 15 min' },
    { q:'Password Hashing', a:'Stub (BCrypt-ready)' },
    { q:'Connection String',a:'App.config' },
    { q:'OTP Delivery',     a:'Email via SMTP' },
    { q:'Theme System',     a:'Theme.cs static class' },
  ];
  document.getElementById('qf-grid').innerHTML = facts.map(f => `
    <div class="qf-item">
      <div class="qf-q">${f.q}</div>
      <div class="qf-a">${f.a}</div>
    </div>`).join('');
}

function renderConfidence() {
  const anchors = [
    { num:'1', text:'You built 18 forms with consistent design, real DB integration, four user roles, async calls, payment flow, OTP recovery, and brute-force protection. That is not a simple project.' },
    { num:'2', text:'You know this codebase line by line. You fixed every off-screen button, every missing cursor, every broken anchor. If they point at anything on screen, you know exactly which file and which line it lives in.' },
    { num:'3', text:'Every weakness has a prepared answer. Plain text passwords → BCrypt stub, ready to swap. No stored procedures → parameterized queries, SQL injection safe. No server-side validation → application layer + DB constraints roadmap. You are not hiding weaknesses. You are demonstrating engineering judgment.' },
  ];
  document.getElementById('conf-grid').innerHTML = anchors.map(a => `
    <div class="conf-item">
      <div class="conf-num">#${a.num}</div>
      <div class="conf-text">${a.text}</div>
    </div>`).join('');
}

/* ── TOAST ─────────────────────────────────────────────────── */
function toast(msg, type = 'ok') {
  const el = document.createElement('div');
  el.className = `toast ${type}`;
  el.textContent = msg;
  document.getElementById('toast-wrap').appendChild(el);
  setTimeout(() => el.remove(), 3500);
}

/* ── XP BURST ───────────────────────────────────────────────── */
function spawnXPBurst(text) {
  const el = document.createElement('div');
  el.className = 'xp-burst';
  el.textContent = text;
  el.style.left = (Math.random() * 60 + 20) + 'vw';
  el.style.top  = '60vh';
  document.body.appendChild(el);
  setTimeout(() => el.remove(), 1300);
}

function spawnFire() {
  const el = document.createElement('div');
  el.className = 'streak-fire';
  el.textContent = '🔥';
  el.style.left = (Math.random() * 60 + 20) + 'vw';
  el.style.top  = '50vh';
  document.body.appendChild(el);
  setTimeout(() => el.remove(), 1600);
}

/* ── CONFETTI ───────────────────────────────────────────────── */
function confetti() {
  const canvas = document.getElementById('confetti-canvas');
  const ctx = canvas.getContext('2d');
  canvas.width = innerWidth; canvas.height = innerHeight;
  const pieces = Array.from({length:120}, () => ({
    x: Math.random() * innerWidth,
    y: -20,
    r: Math.random() * 8 + 4,
    d: Math.random() * 120 + 40,
    color: ['#3b82f6','#10b981','#f59e0b','#ef4444','#8b5cf6','#06b6d4'][Math.floor(Math.random()*6)],
    tilt: Math.random() * 10 - 10,
    tiltAngle: 0,
    tiltSpeed: Math.random() * 0.1 + 0.05,
    vx: Math.random() * 2 - 1,
    vy: Math.random() * 3 + 2,
  }));
  let frame = 0;
  function draw() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    pieces.forEach(p => {
      ctx.beginPath();
      ctx.fillStyle = p.color;
      ctx.save();
      ctx.translate(p.x, p.y);
      ctx.rotate(p.tiltAngle);
      ctx.fillRect(-p.r/2, -p.r/2, p.r, p.r);
      ctx.restore();
      p.x += p.vx; p.y += p.vy;
      p.tiltAngle += p.tiltSpeed;
      if (p.y > canvas.height) { p.y = -10; p.x = Math.random() * canvas.width; }
    });
    frame++;
    if (frame < 120) requestAnimationFrame(draw);
    else ctx.clearRect(0, 0, canvas.width, canvas.height);
  }
  draw();
}

/* ── UTILS ──────────────────────────────────────────────────── */
function shuffle(arr) {
  const a = [...arr];
  for (let i = a.length-1; i > 0; i--) {
    const j = Math.floor(Math.random() * (i+1));
    [a[i], a[j]] = [a[j], a[i]];
  }
  return a;
}

function escQ(s) { return s.replace(/'/g, "\\'").replace(/"/g, '&quot;').slice(0, 60); }

function debounce(fn, ms) {
  let t; return (...args) => { clearTimeout(t); t = setTimeout(() => fn(...args), ms); };
}
