# DocAtHome — Defense Demo Script
### Estimated Time: 10–12 minutes | Practice this at least 3 times before the day

---

## Before You Walk In (checklist)
- [ ] Run `SEED_DATA.sql` on a fresh DB — verify the SELECT at the bottom shows correct row counts
- [ ] Build the solution in **Release** mode — no compiler warnings visible
- [ ] Open the app once, log in as Admin, confirm the chart loads
- [ ] Set your screen resolution to 1920×1080 or 1280×720
- [ ] Close all other windows — no distractions in the background
- [ ] Have the login credentials card ready (from SEED_DATA.sql comments)
- [ ] Keep `DIAGRAMS.md` open in a second tab for reference if asked

---

## Opening Line (say this while app is loading)

> *"DocAtHome is a role-based home healthcare management system built in C# WinForms on SQL Server.
> It connects patients with doctors and nurses for home visits, manages bookings, tracks appointment
> status in real time, and handles payments — all from a single desktop application with four distinct
> user roles: Admin, Patient, Doctor, and Nurse."*

---

## Step 1 — Login Screen (30 seconds)

**Show:** LoginForm

**Say:**
> *"The application opens to a secure login screen. It supports four roles — the system automatically
> detects which role the credentials belong to and routes to the correct dashboard.
> It also has brute-force protection: five failed attempts locks the account for fifteen minutes."*

**Do:** Type `admin@docathome.com` / `Admin@1234` → click Login

> *Credentials card: Admin → admin@docathome.com / Admin@1234*

---

## Step 2 — Admin Dashboard (90 seconds)

**Show:** AdminDashboard with KPI cards and chart

**Say:**
> *"The Admin Dashboard gives a live overview of the system. These four cards pull real counts from
> the database — total doctors, patients, nurses, and total revenue collected in BDT.
> The chart below shows booking activity over time as a spline area graph."*

**Point to each card as you say it.**

**Then navigate the sidebar:**
> *"From the sidebar the admin can manage all four entities. Let me show you the doctor management."*

**Click → Manage Doctors**

**Show:** ManageDoctorsForm

**Say:**
> *"The admin can register a new doctor here — name, email, specialty, password — or remove a selected
> one. The same pattern applies to nurses and patients."*

**Click → Manage Patients**

**Say:**
> *"Patient management has a live search and a detail panel on the right that shows the selected
> patient's contact info before any destructive action."*

---

## Step 3 — Doctor Bookings (Admin side) (60 seconds)

**Click → Doctor Bookings (ViewAppointmentsForm)**

**Say:**
> *"This is the admin's full view of all doctor appointments across every patient. They can search,
> filter by status — Pending, Accepted, Rejected — and refresh live."*

**Click → Create New Booking button**

**Show:** AdminBookDoctorForm

**Say:**
> *"The admin can also create a booking on behalf of a patient — useful for walk-in or phone
> bookings. They pick the patient, doctor, date, time, and reason. It inserts directly into
> the Appointments table with status Pending."*

**Close the modal. Click → Nurse Bookings.**

**Say:**
> *"Same pattern for nurse bookings — the admin can assign a nurse and a specific service type,
> such as IV Drip, Wound Care, or Physical Therapy."*

---

## Step 4 — Patient Dashboard (2 minutes)

**Logout → Login as** `john.doe@example.com` / `Patient@1234`

**Show:** MainForm

**Say:**
> *"The patient dashboard is the most feature-rich screen. At the top we have three live stat cards —
> upcoming confirmed visits, appointments awaiting acceptance, and unpaid bills.
> Below that, a notification banner shows the latest status change on any appointment.
> The main grid shows the full appointment history."*

**Point to the grid.**

**Click → Book Doctor**

**Show:** BookDoctorForm

**Say:**
> *"The booking form only shows available doctors — if a doctor has marked themselves unavailable,
> they won't appear here. There's also a conflict check — if that doctor already has a booking at
> the chosen date and time, the system blocks the request."*

**Cancel. Click → Book Nurse.**

**Show:** BookNurseForm

**Say:**
> *"Nurse bookings work the same way, with a service type dropdown — Injection, Wound Care,
> IV Drip, Elderly Care, Physical Therapy. Each service has a fixed price defined in the system."*

**Cancel. Select the Accepted row (sarah.wilson, 2026-05-14, AppointmentID 1020). Click → Pay Selected.**

**Show:** PaymentForm

**Say:**
> *"Once an appointment is accepted, the patient can pay. Cash is straightforward. If they select
> Card, the form expands to reveal a card number field — the layout adjusts dynamically.
> On confirmation, a payment record is inserted and the unpaid bill counter updates."*

**Confirm payment. Show updated stat card.**

---

## Step 5 — Doctor Dashboard (60 seconds)

**Logout → Login as** `sarah.wilson@docathome.com` / `Doctor@1234`

**Show:** DoctorDashboard

**Say:**
> *"The doctor sees all incoming appointment requests. They can toggle their availability using this
> checkbox in the header — when marked unavailable, they disappear from the patient's booking dropdown.
> They can accept or reject each request. The patient is notified on their dashboard immediately."*

**Accept the pending appointment from john.doe@example.com (AppointmentID 2022, 2026-05-20).**

---

## Step 6 — Nurse Dashboard (45 seconds)

**Logout → Login as** `nurse.maria@docathome.com` / `Nurse@1234`

**Show:** NurseDashboard

**Say:**
> *"The nurse workspace follows the same pattern — availability toggle, request queue, accept or reject.
> The service type and booking date are visible directly in the grid so the nurse knows exactly
> what to prepare for."*

---

## Step 7 — Password Reset / Security (30 seconds)

**Logout → Click 'Forgot Password' on LoginForm**

**Say:**
> *"Password recovery uses a one-time PIN sent to the user's registered email via OTP service.
> The user enters the OTP, sets a new password, and is redirected back to login.
> The login system also enforces brute-force lockout — five failures, fifteen-minute timeout."*

---

## Closing Statement

> *"In summary — DocAtHome covers the complete lifecycle of a home healthcare interaction:
> a patient books a visit, the doctor or nurse accepts it, the patient pays, and the admin
> has full oversight of everything in real time. The codebase is structured with a consistent
> theme system, async database calls to keep the UI responsive, and clear separation between
> the four user roles. Thank you."*

---

## Timing Summary

| Section | Time |
|---------|------|
| Opening | 0:30 |
| Admin Dashboard + CRUD | 1:30 |
| Doctor Bookings (Admin) | 1:00 |
| Patient Dashboard | 2:00 |
| Doctor Dashboard | 1:00 |
| Nurse Dashboard | 0:45 |
| Password Reset | 0:30 |
| Closing | 0:30 |
| **Total** | **~8 min** |

Leaves 2–4 minutes buffer for the panel to ask you to show something again.
