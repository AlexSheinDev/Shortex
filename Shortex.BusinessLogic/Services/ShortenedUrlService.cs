using AutoMapper;
using Microsoft.Extensions.Logging;
using Shortex.BusinessLogic.Services.IServices;
using Shortex.Common.Models;
using Shortex.Common.Models.DTO;
using Shortex.DataAccess.Repositories.IRepositories;

namespace Shortex.BusinessLogic.Services
{
    public class ShortenedUrlService : IShortenedUrlService
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public ShortenedUrlService(
            IUnitOfWork uof,
            ILogger logger,
            IMapper mapper)
        {
            _uof = uof;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<ShortenedUrlDTO> GetAll()
        {
            var shortenedUrlsFromDb = _uof.ShortenedUrls.GetAll();

            _logger.LogInformation($"Retrieved {shortenedUrlsFromDb?.Count()} shortened Urls.");

            return _mapper.Map<IEnumerable<ShortenedUrl>, IEnumerable<ShortenedUrlDTO>>(shortenedUrlsFromDb);
        }

        public async Task<ShortenedUrlDTO> GetLastUrlByTimeAsync()
        {
            try
            {
                var result = await _uof.ShortenedUrls.GetLastUrlByTimeAsync();
                return _mapper.Map<ShortenedUrl, ShortenedUrlDTO>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"The error occurred while retrieving last shortened Url: {ex.ToString()}.");
                return new ShortenedUrlDTO();
            }
        }

        public async Task CreateAsync(ShortenedUrlDTO entity)
        {
            // Logic for shortening Urls.

            _uof.ShortenedUrls.Create(_mapper.Map<ShortenedUrlDTO, ShortenedUrl>(entity));
            var result = await _uof.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError("An entity wasn`t created.");
            }

            _logger.LogInformation("The entity has been created successfully.");
        }

        public async Task<int> ClearHistoryAsync()
        {
            await _uof.ShortenedUrls.ClearShortenedUrlsAsync();
            var result = await _uof.SaveChangesAsync();

            _logger.LogInformation($"The history was cleared: {result} entities were affected.");

            return result;
        }
    }
}
