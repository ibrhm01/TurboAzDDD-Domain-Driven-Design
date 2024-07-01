namespace Application.Configuration
{
	public class JwtConfig
	{
		public string SecretKey { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;

    }
}

