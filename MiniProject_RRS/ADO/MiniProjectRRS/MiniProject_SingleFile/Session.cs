namespace MiniProjectRRS
{
    public static class Session
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }
        public static string Role { get; set; }

        public static void Clear()
        {
            UserId = 0;
            Username = string.Empty;
            Role = string.Empty;
        }

        public static bool IsLoggedIn()
        {
            return UserId > 0 && !string.IsNullOrEmpty(Role);
        }
    }
}

