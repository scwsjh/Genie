using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace Web.TagHelpers
{
    [HtmlTargetElement("ResImg")]
    public class ResImg : TagHelper
    {
        private readonly IOptions<ResUrlOptions> ResConfig;

        public ResImg(IOptions<ResUrlOptions> _ResConfig)
        {
            ResConfig = _ResConfig;
        }

        public string src { get; set; }
        public string alt { get; set; }
        public string imgClass { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            string html = string.Empty;
            string url = string.Format("{0}{1}", ResConfig.Value.resUrl, src);
            output.Attributes.SetAttribute("src", url);
            output.Attributes.SetAttribute("alt", alt);
            output.Attributes.SetAttribute("class", imgClass);
            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
    }

    [HtmlTargetElement("LocImg")]
    public class LocImg : TagHelper
    {
        private readonly IOptions<ResUrlOptions> ResConfig;

        public LocImg(IOptions<ResUrlOptions> _ResConfig)
        {
            ResConfig = _ResConfig;
        }

        public string src { get; set; }
        public string alt { get; set; }
        public string imgClass { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            string html = string.Empty;
            string url = string.Format("{0}{1}", ResConfig.Value.local, src);
            output.Attributes.SetAttribute("src", url);
            output.Attributes.SetAttribute("alt", alt);
            output.Attributes.SetAttribute("class", imgClass);
            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
    }

    [HtmlTargetElement("VideoBar")]
    public class VideoBar : TagHelper
    {
        private readonly IOptions<ResUrlOptions> ResConfig;

        public VideoBar(IOptions<ResUrlOptions> _ResConfig)
        {
            ResConfig = _ResConfig;
        }

        public string src { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "video";
            string url = string.Format("{0}{1}", ResConfig.Value.videoCDN, src);
            output.Attributes.SetAttribute("style", "background-color:black;width:100%;background-repeat:no-repeat;background-size:cover; webkit-playsinline=");
            output.Attributes.SetAttribute("controls", "controls");
            //output.Attributes.SetAttribute("poster", string.Format("{0}{1}", ResConfig.Value.local, "/images/site/video.jpg"));
            string html = string.Format("<source src='{0}' type='video/mp4'>  ", url);
            output.Content.AppendHtml(html);
            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
    }

    [HtmlTargetElement("MusicBar")]
    public class MusicBar : TagHelper
    {
        private readonly IOptions<ResUrlOptions> ResConfig;

        public MusicBar(IOptions<ResUrlOptions> _ResConfig)
        {
            ResConfig = _ResConfig;
        }

        public string src { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "audio";
            string url = string.Format("{0}{1}", ResConfig.Value.videoCDN, src);
            output.Attributes.SetAttribute("controls", "controls");
            string html = string.Format("<source src='{0}' type='audio/mp3'>  ", url);
            output.Content.AppendHtml(html);
            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
    }
}