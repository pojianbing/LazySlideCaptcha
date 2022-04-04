using Lazy.SlideCaptcha.Core.Resources;
using Lazy.SlideCaptcha.Core.Resources.Handler;

namespace Lazy.SlideCaptcha.Demo.Extensions
{
    public class UrlResourceHandler : IResourceHandler
    {
        public const string Type = "url";

        public bool CanHandle(string handlerType)
        {
            return handlerType == Type;
        }

        /// <summary>
        /// 这里仅演示，仍然从本地读取。实际需要通过Http读取
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public byte[] Handle(Resource resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            return File.ReadAllBytes(resource.Data);
        }
    }
}
