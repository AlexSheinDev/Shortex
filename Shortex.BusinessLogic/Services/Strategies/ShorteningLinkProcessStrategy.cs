using Shortex.BusinessLogic.Services.IServices;

namespace Shortex.BusinessLogic.Services.Strategies
{
    public class ShorteningLinkProcessStrategy : ILinkProcessStrategy
    {
        private readonly ShortUrlService _service;

        public ShorteningLinkProcessStrategy(ShortUrlService service)
        {
            _service = service;
        }

        public async Task<IDictionary<int, string>> ProcessLinkAsync(string url)
        {
            if (await _service.LongLinkExistsAsync(url))
            {
                return new Dictionary<int, string> { { 302, "Link already exists." } };
            }

            if (await _service.ShortLinkExistsAsync(url))
            {
                return new Dictionary<int, string> { { 403, "The same shortened link already exists." } };
            }

            if (!IsValidUrlProtocol(url))
            {
                return new Dictionary<int, string> { { 400, "The URL must include protocol." } };
            }

            await _service.CreateAsync(url);
            return new Dictionary<int, string> { { 200, "Link was shortened successfully." } };
        }

        // TODO: Possibility to improve logic of checking / assigning protocols in the future
        private static bool IsValidUrlProtocol(string url)
            => url.StartsWith("http://") || url.StartsWith("https://");
    }
}
