using Lazy.SlideCaptcha.Core;
using Lazy.SlideCaptcha.Core.Validator;

namespace Lazy.SlideCaptcha.Demo.Extensions
{
    public class CustomValidator : BaseValidator
    {
        public override bool ValidateCore(SlideTrack slideTrack, CaptchaValidateData captchaValidateData)
        {
            // BaseValidator已做了基本滑块与凹槽的对齐验证，这里做其他验证

            return true;
        }
    }
}
