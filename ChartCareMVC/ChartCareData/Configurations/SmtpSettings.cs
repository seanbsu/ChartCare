namespace ChartCareData.Configurations
{
    public class SmtpSettings
    {
        public required string SiteEmail { get; set; }
        public required string SmtpUsername { get; set; }
        public required string SmtpPassword { get; set; }
        public required string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
    }

}
