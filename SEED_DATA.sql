-- ============================================================
--  DocAtHome — Demo Seed Data
--  Run this ONCE on a clean database before your defense.
--  Passwords are plain text (matches PasswordHelper.Hash).
-- ============================================================

-- ── 0. Clean slate (safe re-run) ────────────────────────────
DELETE FROM Payments;
DELETE FROM NurseBookings;
DELETE FROM Appointments;
DELETE FROM Patients;
DELETE FROM Nurses;
DELETE FROM Doctors;
DELETE FROM Admins;

-- Reset identity seeds
DBCC CHECKIDENT ('Payments',      RESEED, 0);
DBCC CHECKIDENT ('NurseBookings', RESEED, 0);
DBCC CHECKIDENT ('Appointments',  RESEED, 0);
DBCC CHECKIDENT ('Patients',      RESEED, 0);
DBCC CHECKIDENT ('Nurses',        RESEED, 0);
DBCC CHECKIDENT ('Doctors',       RESEED, 0);
DBCC CHECKIDENT ('Admins',        RESEED, 0);

-- ── 1. Admin ─────────────────────────────────────────────────
INSERT INTO Admins (Email, Password) VALUES
('admin@docathome.com', 'Admin@1234');

-- ── 2. Doctors ───────────────────────────────────────────────
INSERT INTO Doctors (FirstName, LastName, Email, Password, Specialty, IsAvailable) VALUES
('Rahim',    'Chowdhury',  'rahim@docathome.com',   'Doctor@1234', 'Cardiology',        1),
('Nadia',    'Islam',      'nadia@docathome.com',    'Doctor@1234', 'General Medicine',  1),
('Tariq',    'Hassan',     'tariq@docathome.com',    'Doctor@1234', 'Neurology',         1),
('Farzana',  'Begum',      'farzana@docathome.com',  'Doctor@1234', 'Pediatrics',        0),
('Imran',    'Siddiqui',   'imran@docathome.com',    'Doctor@1234', 'Orthopedics',       1);

-- ── 3. Nurses ────────────────────────────────────────────────
INSERT INTO Nurses (FirstName, LastName, Email, Password, LicenseNumber, IsAvailable) VALUES
('Sadia',    'Akter',      'sadia@docathome.com',    'Nurse@1234',  'LIC-2021-001',      1),
('Kamrul',   'Hossain',    'kamrul@docathome.com',   'Nurse@1234',  'LIC-2020-045',      1),
('Roksana',  'Parvin',     'roksana@docathome.com',  'Nurse@1234',  'LIC-2022-012',      0),
('Mehedi',   'Hasan',      'mehedi@docathome.com',   'Nurse@1234',  'LIC-2019-088',      1);

-- ── 4. Patients ──────────────────────────────────────────────
INSERT INTO Patients
    (FirstName, LastName, Email, Password, PhoneNumber, Address, City, State, ZipCode, DateOfBirth, MedicalHistory)
VALUES
('Arif',     'Ahmed',     'arif@gmail.com',    'Patient@1234', '01711-000001', '12 Mirpur Road',   'Dhaka',      'Dhaka',      '1216', '1995-03-14', 'Hypertension'),
('Sumaiya',  'Rahman',    'sumaiya@gmail.com', 'Patient@1234', '01812-000002', '45 Dhanmondi',     'Dhaka',      'Dhaka',      '1209', '1998-07-22', 'Asthma'),
('Kabir',    'Hossain',   'kabir@gmail.com',   'Patient@1234', '01912-000003', '8 Uttara Sector 7','Dhaka',      'Dhaka',      '1230', '1988-11-05', 'Diabetes Type 2'),
('Tania',    'Begum',     'tania@gmail.com',   'Patient@1234', '01611-000004', '22 Agrabad',       'Chattogram', 'Chittagong', '4100', '2000-01-30', 'None'),
('Rakibul',  'Islam',     'rakib@gmail.com',   'Patient@1234', '01511-000005', '67 GEC Circle',    'Chattogram', 'Chittagong', '4000', '1992-09-18', 'Kidney Stone');

-- PatientIDs after seed: Arif=1, Sumaiya=2, Kabir=3, Tania=4, Rakibul=5
-- DoctorIDs  after seed: Rahim=1, Nadia=2, Tariq=3, Farzana=4, Imran=5
-- NurseIDs   after seed: Sadia=1, Kamrul=2, Roksana=3, Mehedi=4

-- ── 5. Appointments ──────────────────────────────────────────
INSERT INTO Appointments (PatientID, DoctorID, NurseID, AppointmentDate, AppointmentTime, Description, Status) VALUES
-- Arif — one Pending (future), one Accepted (can pay), one Completed (history)
(1, 1, NULL, DATEADD(day,  2, CAST(GETDATE() AS DATE)), '10:00:00', 'Chest pain and shortness of breath',  'Pending'),
(1, 2, NULL, DATEADD(day, -5, CAST(GETDATE() AS DATE)), '09:30:00', 'Routine check-up',                    'Accepted'),
(1, 3, NULL, DATEADD(day,-10, CAST(GETDATE() AS DATE)), '11:00:00', 'Recurring headaches',                 'Completed'),

-- Sumaiya
(2, 2, NULL, DATEADD(day,  3, CAST(GETDATE() AS DATE)), '14:00:00', 'Breathing difficulty and wheezing',   'Accepted'),
(2, 5, NULL, DATEADD(day, -3, CAST(GETDATE() AS DATE)), '10:00:00', 'Knee pain after fall',                'Rejected'),

-- Kabir
(3, 1, NULL, DATEADD(day,  1, CAST(GETDATE() AS DATE)), '16:00:00', 'Blood pressure follow-up',            'Pending'),
(3, 2, NULL, DATEADD(day, -7, CAST(GETDATE() AS DATE)), '08:30:00', 'Blood sugar management',              'Accepted'),

-- Tania
(4, 4, NULL, DATEADD(day, -2, CAST(GETDATE() AS DATE)), '13:00:00', 'Child fever consultation',            'Accepted'),

-- Rakibul
(5, 3, NULL, DATEADD(day,  5, CAST(GETDATE() AS DATE)), '15:00:00', 'Severe lower back pain',              'Pending'),
(5, 2, NULL, DATEADD(day,-15, CAST(GETDATE() AS DATE)), '09:00:00', 'Annual health screening',             'Completed');

-- ── 6. Nurse Bookings ────────────────────────────────────────
INSERT INTO NurseBookings (PatientID, NurseID, BookingDate, ServiceType, Status) VALUES
(1, 1, DATEADD(day,  1, CAST(GETDATE() AS DATE)), 'IV Drip',          'Pending'),
(1, 2, DATEADD(day, -4, CAST(GETDATE() AS DATE)), 'Wound Care',       'Accepted'),
(2, 1, DATEADD(day,  4, CAST(GETDATE() AS DATE)), 'Injection',        'Pending'),
(3, 4, DATEADD(day,  2, CAST(GETDATE() AS DATE)), 'Elderly Care',     'Accepted'),
(4, 2, DATEADD(day, -6, CAST(GETDATE() AS DATE)), 'Physical Therapy', 'Rejected'),
(5, 1, DATEADD(day, -1, CAST(GETDATE() AS DATE)), 'Wound Care',       'Pending');

-- ── 7. Payments ──────────────────────────────────────────────
-- PatientID 1=Arif, 2=Sumaiya, 3=Kabir, 4=Tania
-- Accepted Appointment IDs after seed: 2(Arif), 4(Sumaiya), 7(Kabir), 8(Tania)
-- Accepted NurseBooking IDs:           2(Arif-WoundCare), 4(Kabir-ElderlyCare)
INSERT INTO Payments (PatientID, RelatedType, RelatedID, Amount, Method, Status, PaidAt, CreatedAt) VALUES
(1, 'Appointment',  2,  500.00, 'Cash',  'Paid', DATEADD(day, -4, GETDATE()), DATEADD(day, -4, GETDATE())),
(2, 'Appointment',  4,  500.00, 'Card',  'Paid', DATEADD(day, -2, GETDATE()), DATEADD(day, -2, GETDATE())),
(3, 'Appointment',  7,  500.00, 'Cash',  'Paid', DATEADD(day, -6, GETDATE()), DATEADD(day, -6, GETDATE())),
(4, 'Appointment',  8,  500.00, 'Bkash', 'Paid', DATEADD(day, -1, GETDATE()), DATEADD(day, -1, GETDATE())),
(1, 'NurseBooking', 2,  250.00, 'Cash',  'Paid', DATEADD(day, -3, GETDATE()), DATEADD(day, -3, GETDATE())),
(3, 'NurseBooking', 4,  400.00, 'Card',  'Paid', DATEADD(day,  0, GETDATE()), DATEADD(day,  0, GETDATE()));

-- ── 8. Verify ────────────────────────────────────────────────
SELECT 'Admins'        AS [Table], COUNT(*) AS [Rows] FROM Admins
UNION ALL SELECT 'Doctors',       COUNT(*) FROM Doctors
UNION ALL SELECT 'Nurses',        COUNT(*) FROM Nurses
UNION ALL SELECT 'Patients',      COUNT(*) FROM Patients
UNION ALL SELECT 'Appointments',  COUNT(*) FROM Appointments
UNION ALL SELECT 'NurseBookings', COUNT(*) FROM NurseBookings
UNION ALL SELECT 'Payments',      COUNT(*) FROM Payments;

-- ── Login Quick-Reference ─────────────────────────────────────
/*
  ROLE      EMAIL                     PASSWORD       NOTES
  ────────  ────────────────────────  ────────────   ──────────────────────────────────
  Admin     admin@docathome.com       Admin@1234
  Doctor    rahim@docathome.com       Doctor@1234    Has Pending appointment from Arif
  Doctor    nadia@docathome.com       Doctor@1234
  Nurse     sadia@docathome.com       Nurse@1234     Has Pending bookings from Arif & Rakibul
  Patient   arif@gmail.com            Patient@1234   Has Accepted appt (payable) + Pending
  Patient   sumaiya@gmail.com         Patient@1234
*/
