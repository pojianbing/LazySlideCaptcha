using Lazy.SlideCaptcha.Core;
using Lazy.SlideCaptcha.Core.Validator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lazy.SlideCaptcha.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly ICaptcha _captcha;

        public CaptchaController(ICaptcha captcha)
        {
            _captcha = captcha;
        }

        /// <summary>
        /// id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("gen")]
        [HttpGet]
        public CaptchaData Generate()
        {
            return _captcha.Generate();
        }

        /// <summary>
        /// id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("check")]
        [HttpPost]
        public ValidateResult Validate([FromQuery]string id, SlideTrack track)
        {
            return _captcha.Validate(id, track);
        }
    }
}
