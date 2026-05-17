using System;
using System.Drawing;
using System.Linq;
using System.Net.Mail;

namespace DocAtHome
{
    internal static class ValidationHelper
    {
        // ── Email ─────────────────────────────────────────────────────────────
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                var addr = new MailAddress(email.Trim());
                return addr.Address == email.Trim();
            }
            catch { return false; }
        }

        // ── Password strength ─────────────────────────────────────────────────
        // Returns 0–4:
        //   0 = empty    1 = too short (<6)   2 = weak
        //   3 = fair     4 = strong
        public static int PasswordStrength(string password)
        {
            if (string.IsNullOrEmpty(password)) return 0;
            if (password.Length < 6) return 1;

            int score = 1; // baseline — meets minimum length
            if (password.Length >= 10) score++;
            if (password.Any(char.IsUpper) && password.Any(char.IsLower)) score++;
            if (password.Any(char.IsDigit)) score++;
            if (password.Any(c => !char.IsLetterOrDigit(c))) score++;

            return Math.Min(score, 4);
        }

        // Minimum "fair" quality required for account operations
        public static bool IsStrongEnough(string password) =>
            PasswordStrength(password) >= 2;

        public static string PasswordStrengthLabel(int score)
        {
            switch (score)
            {
                case 1: return "Too short  (min. 6 characters)";
                case 2: return "Weak — try adding numbers or uppercase";
                case 3: return "Fair";
                case 4: return "Strong";
                default: return "";
            }
        }

        public static Color PasswordStrengthColor(int score)
        {
            switch (score)
            {
                case 1: return Color.FromArgb(239, 68,  68);   // red
                case 2: return Color.FromArgb(245, 158, 11);   // amber
                case 3: return Color.FromArgb(59,  130, 246);  // blue
                case 4: return Color.FromArgb(16,  185, 129);  // green
                default: return Color.FromArgb(148, 163, 184); // muted
            }
        }

        // ── Luhn algorithm (credit card validation) ───────────────────────────
        public static bool IsValidLuhn(string digits)
        {
            if (string.IsNullOrEmpty(digits) || !digits.All(char.IsDigit)) return false;
            int sum = 0;
            bool alternate = false;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int n = digits[i] - '0';
                if (alternate)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }
                sum += n;
                alternate = !alternate;
            }
            return sum % 10 == 0;
        }
    }
}
