namespace Web.Api.Authorization
{
    public class JwtConfiguration
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int DurationInDays { get; set; }
        public string Key { get; set; } = string.Empty;
    }
}
