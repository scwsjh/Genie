namespace Web.Controllers
{
    [Route("Captcha/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly ICaptchaService _captcha;

        public CaptchaController(ICaptchaService captcha)
        {
            _captcha = captcha;
        }

        [HttpGet]
        public async Task<FileContentResult> CaptchaAsync(string key)
        {
            var code = await _captcha.GenerateRandomCaptchaAsync(key);
            var result = await _captcha.GenerateCaptchaImageAsync(code);

            return File(result.CaptchaMemoryStream.ToArray(), "image/png");
        }
    }
}