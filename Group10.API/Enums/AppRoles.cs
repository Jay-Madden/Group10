namespace Group10.Data.Enums
{
    public readonly struct AppRoles
    {
        private readonly string _val;
        private AppRoles(string val) => _val = val;
        
        public static AppRoles Admin => new AppRoles("admin");
        public static AppRoles Driver => new AppRoles("driver");
        public static AppRoles Sponsor => new AppRoles("sponsor");
        
        public static implicit operator string(AppRoles role) => role._val;
    }
}
