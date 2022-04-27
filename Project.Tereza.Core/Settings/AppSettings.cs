namespace Project.Tereza.Core.Settings
{
    public interface IAppSettings
    {
        string? Secret { get; init; }
        string? Issuer { get; set; }
        string? Audience { get; set; }
    }

    public record AppSettings : IAppSettings
    {
        public const string SectionName = "AppSettings";

        public string? Secret { get; init; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}