using Shortex.Common.Models.DTO;

namespace Shortex.BusinessLogic.Services.IServices
{
    public interface IShortenedUrlService
    {
        IEnumerable<ShortenedUrlDTO> GetAll();
        Task<ShortenedUrlDTO> GetLastUrlByTimeAsync();
        Task CreateAsync(ShortenedUrlDTO entity);
        Task<int> ClearHistoryAsync();
    }
}
