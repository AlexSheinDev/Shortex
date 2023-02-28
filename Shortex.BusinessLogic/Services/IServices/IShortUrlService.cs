using Shortex.Common;
using Shortex.Common.Models.DTO;

namespace Shortex.BusinessLogic.Services.IServices
{
    public interface IShortUrlService
    {
        Task<ShortUrlDTO> GetOriginalUrl(string shortUrl);
        IEnumerable<ShortUrlDTO> GetAll();
        Task<ShortUrlDTO> GetLastUrlByTimeAsync();
        Task CreateAsync(string url);
        Task<int> ClearHistoryAsync();
        Task<IDictionary<int, string>> ProcessLinkAsync(string url, LinkProcessType processType);
    }
}
