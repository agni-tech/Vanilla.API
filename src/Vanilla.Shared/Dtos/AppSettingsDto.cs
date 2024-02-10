namespace Vanilla.Shared.Dtos
{
    public class AppSettingsDto
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public SecretsDto Secrets { get; set; }
        public string Locale { get; set; }
        public EmailSettingsDto EmailSettings { get; set; }
        public Links Links { get; set; }

        public AppSettingsDto()
        {
            ConnectionStrings = new ConnectionStrings();
            EmailSettings = new EmailSettingsDto();
            Secrets = new SecretsDto();

            Links = new Links();
        }
    }

    public class SecretsDto
    {
        public string TokenSecurityKey { get; set; }
        public string RedemetApiKey { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }

    public class Links
    {
        public string Site { get; set; }
        public string ZipCodeSearch { get; set; }
        public string Redemet { get; set; }
    }
}
