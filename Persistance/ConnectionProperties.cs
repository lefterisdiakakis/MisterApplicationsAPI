namespace Persistance
{
    public record ConnectionProperties
    {
        public string MisterApplicationsApiBaseUrl { get; set; }
        public string MisterApplicationsConnectionString { get; set; }
        public int MisterApplicationsConnectionTimeOut { get; set; }
        public string MisterApplicationsDataBaseVersion { get; set; }
    }
}
