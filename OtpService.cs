using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace DocAtHome
{
    internal static class OtpService
    {
        private static readonly Dictionary<string, (string Code, DateTime Expiry)> _pending
            = new Dictionary<string, (string, DateTime)>(StringComparer.OrdinalIgnoreCase);

        // Generates a cryptographically random 6-digit code valid for 10 minutes.
        public static string Generate(string email)
        {
            byte[] buf = new byte[4];
            using (var rng = new RNGCryptoServiceProvider()) rng.GetBytes(buf);
            int code = (int)(BitConverter.ToUInt32(buf, 0) % 900_000) + 100_000;
            string codeStr = code.ToString();
            _pending[email] = (codeStr, DateTime.Now.AddMinutes(10));
            return codeStr;
        }

        public static bool Verify(string email, string code)
        {
            if (!_pending.TryGetValue(email, out var entry)) return false;
            if (DateTime.Now > entry.Expiry) { _pending.Remove(email); return false; }
            bool valid = string.Equals(entry.Code.Trim(), code.Trim(), StringComparison.Ordinal);
            if (valid) _pending.Remove(email);
            return valid;
        }

        public static bool HasPending(string email)
            => _pending.TryGetValue(email, out var e) && DateTime.Now <= e.Expiry;
    }
}
