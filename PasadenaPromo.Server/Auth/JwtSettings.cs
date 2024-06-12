namespace PasadenaPromo.Auth
{
    public class JwtSettings
    {

        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int MinutesToExpiration { get; set; }

        public TimeSpan Expire => TimeSpan.FromMinutes(MinutesToExpiration);
    }
}
