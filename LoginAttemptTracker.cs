using System;
using System.Collections.Generic;

namespace DocAtHome
{
    internal static class LoginAttemptTracker
    {
        private static readonly Dictionary<string, (int Count, DateTime LockedUntil)> _state
            = new Dictionary<string, (int, DateTime)>(StringComparer.OrdinalIgnoreCase);

        private const int MaxAttempts = 5;
        private static readonly TimeSpan LockDuration = TimeSpan.FromMinutes(15);

        public static bool IsLocked(string email)
        {
            if (!_state.TryGetValue(email, out var s)) return false;
            if (s.LockedUntil > DateTime.Now) return true;
            _state.Remove(email);
            return false;
        }

        public static TimeSpan LockRemaining(string email)
        {
            if (!_state.TryGetValue(email, out var s)) return TimeSpan.Zero;
            var r = s.LockedUntil - DateTime.Now;
            return r > TimeSpan.Zero ? r : TimeSpan.Zero;
        }

        public static void RecordFailure(string email)
        {
            _state.TryGetValue(email, out var s);
            int count = s.Count + 1;
            var lockUntil = count >= MaxAttempts ? DateTime.Now.Add(LockDuration) : DateTime.MinValue;
            _state[email] = (count, lockUntil);
        }

        public static void RecordSuccess(string email) => _state.Remove(email);

        public static int AttemptsRemaining(string email)
        {
            _state.TryGetValue(email, out var s);
            return Math.Max(0, MaxAttempts - s.Count);
        }
    }
}
