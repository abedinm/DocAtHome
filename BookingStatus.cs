namespace DocAtHome
{
    internal static class BookingStatus
    {
        public const string Pending   = "Pending";
        public const string Accepted  = "Accepted";
        public const string Rejected  = "Rejected";
        public const string Cancelled = "Cancelled";
        public const string Paid      = "Paid";
        public const string Completed = "Completed";
    }

    internal static class PaymentType
    {
        public const string Appointment  = "Appointment";
        public const string NurseBooking = "NurseBooking";
    }

    internal static class ServicePricing
    {
        public const decimal DoctorAppointment = 500.00m;
        public const decimal Injection         = 200.00m;
        public const decimal WoundCare         = 250.00m;
        public const decimal IVDrip            = 350.00m;
        public const decimal ElderlyCare       = 400.00m;
        public const decimal PhysicalTherapy   = 300.00m;

        public static decimal ForNurseService(string serviceType)
        {
            switch (serviceType)
            {
                case "Injection":        return Injection;
                case "Wound Care":       return WoundCare;
                case "IV Drip":          return IVDrip;
                case "Elderly Care":     return ElderlyCare;
                case "Physical Therapy": return PhysicalTherapy;
                default:                 return 300.00m;
            }
        }
    }
}
