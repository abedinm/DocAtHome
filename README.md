# DocAtHome

A role-based home healthcare management system - book doctors and nurses for at-home visits, with dedicated dashboards per role and a full booking and payment flow. Built as a Windows desktop application in C# WinForms with a SQL Server backend.

## Features

- Role-based access for Admin, Doctor, Nurse, and Patient, each with its own dashboard
- Book home visits from doctors or nurses
- Admin management of doctors, nurses, and bookings
- Login with failed-attempt tracking and a forgot-password flow
- Booking status tracking and a payment flow
- 18 Windows Forms covering the end-to-end workflow

## Tech stack

C# (.NET Framework, WinForms), SQL Server.

## Getting started

Requirements: Visual Studio with the ".NET desktop development" workload, and SQL Server (Express edition is fine).

1. Clone the repo and open `DocAtHome.csproj` in Visual Studio.
2. Create the SQL Server database and load the seed data (see `DocAtHome_Seed_Data.pdf`).
3. Update the connection string in `App.config` to point to your SQL Server instance.
4. Build and run (F5).

## Documentation

- `DIAGRAMS.md` - system and data diagrams
- `DEFENSE_DEMO_SCRIPT.md` and `DocAtHome_Defense_Demo_Script.pdf` - guided walkthrough
- `DocAtHome_QA_Prep_Sheet.pdf` - Q&A preparation

## License

See the repository for license details.
