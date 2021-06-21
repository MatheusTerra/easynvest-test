namespace Easynvest.Investment.Portfolio.API.Models
{
    public class HealthCheckModel
    {
        public HealthCheckModel()
        {
            
        }
        
        public HealthCheckModel(string service, bool online)
        {
            Service = service;
            Online = online;
        }

        public HealthCheckModel(string service, bool online, string version)
        {
            Service = service;
            Online = online;
            Version = version;
        }

        public string Service { get; set; }
        public bool Online { get; set; }
        public string Version { get; set; }
    }
}