namespace DocAtHome
{
    public static class PasswordHelper
    {
        public static string Hash(string password) => password;

        public static bool Verify(string password, string stored) => password == stored;
    }
}
