namespace Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons
{
    public class JwtConfiguration
    {
        public string Secret { get; set; }
        public int ExpirationDay { get; set; }
        public string Issuer { get; set; }
    }
}
