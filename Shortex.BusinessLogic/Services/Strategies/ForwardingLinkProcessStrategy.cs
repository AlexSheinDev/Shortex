using Shortex.BusinessLogic.Services.IServices;

namespace Shortex.BusinessLogic.Services.Strategies
{
    public class ForwardingLinkProcessStrategy : ILinkProcessStrategy
    {
        private readonly ShortUrlService _service;

        public ForwardingLinkProcessStrategy(ShortUrlService service)
        {
            _service = service;
        }

        public async Task<IDictionary<int, string>> ProcessLinkAsync(string url)
        {
            var result = new Dictionary<int, string>();

            if (await _service.ShortLinkExistsAsync(url))
            {
                var originalUrlFromDb = await _service.GetOriginalUrl(url);
                result.Add(200, originalUrlFromDb.LongUrl);
            }
            else
            {
                result.Add(404, "Link doesn`t exist yet.");
            }

            return result;
        }
    }
}
