using Lazy.SlideCaptcha.Core.Resources;
using Lazy.SlideCaptcha.Core.Resources.Provider;

namespace Lazy.SlideCaptcha.Demo.Extensions
{
    public class CustomResourceProvider : IResourceProvider
    {
        public List<Resource> Backgrounds()
        {
            return Enumerable.Range(1, 10)
                .ToList()
                .Select(e => new Resource(Core.Resources.Handler.FileResourceHandler.TYPE, $"wwwroot/images/background/{e}.jpg"))
                .ToList();
        }

        public List<TemplatePair> Templates()
        {
            return new List<TemplatePair>();
        }
    }
}
