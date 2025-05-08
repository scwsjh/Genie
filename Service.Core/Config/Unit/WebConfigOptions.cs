using Furion.ConfigurableOptions;
using Microsoft.Extensions.Configuration;

namespace Service.Core
{
    public class WebConfigOptions : IConfigurableOptionsListener<WebConfigOptions>
    {
        public string WebName { get; set; }
        public string WebTitle { get; set; }
        public string KeyWord { get; set; }
        public string Description { get; set; }
        public string TimeName { get; set; }
        public string webUrl { get; set; }

        public void OnListener(WebConfigOptions options, IConfiguration configuration)
        {
            WebName = options.WebName;
            WebTitle = options.WebTitle;  // 实时的最新值
            KeyWord = options.KeyWord;  // 实时的最新值
            Description = options.Description;
            TimeName = options.TimeName;
        }

        public void PostConfigure(WebConfigOptions options, IConfiguration configuration)
        {
        }
    }
}