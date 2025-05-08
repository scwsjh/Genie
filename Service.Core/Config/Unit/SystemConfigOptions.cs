using Furion.ConfigurableOptions;

namespace Service.Core
{
    public class SystemConfigOptions : IConfigurableOptions
    {
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public string LoginUrl { get; set; }
        public string RegUrl { get; set; }
        public string HomeUrl { get; set; }
        public string MyHomeUrl { get; set; }
        public string BbsUrl { get; set; }
        public string PlateUrl { get; set; }
        public string OutUrl { get; set; }
    }
}