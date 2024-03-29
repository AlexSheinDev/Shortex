﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using Shortex.BusinessLogic.Helpers;
using Shortex.BusinessLogic.Services.IServices;
using Shortex.BusinessLogic.Services.Strategies;
using Shortex.Common;
using Shortex.Common.Models;
using Shortex.Common.Models.DTO;
using Shortex.DataAccess.Repositories.IRepositories;

namespace Shortex.BusinessLogic.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public ShortUrlService(
            IUnitOfWork uof,
            ILogger<ShortUrlService> logger,
            IMapper mapper)
        {
            _uof = uof;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ShortUrlDTO> GetOriginalUrl(string shortUrl)
        {
            var shortUrlFromDb = await _uof.ShortUrls.GetOriginalUrlByShortUrl(shortUrl);
            return _mapper.Map<ShortUrl, ShortUrlDTO>(shortUrlFromDb);
        }

        public IEnumerable<ShortUrlDTO> GetAll()
        {
            var shortUrlsFromDb = _uof.ShortUrls.GetAll();

            _logger.LogInformation($"Retrieved {shortUrlsFromDb?.Count()} shortened Urls.");

            return _mapper.Map<IEnumerable<ShortUrl>, IEnumerable<ShortUrlDTO>>(shortUrlsFromDb);
        }

        public async Task<ShortUrlDTO> GetLastUrlByTimeAsync()
        {
            try
            {
                var result = await _uof.ShortUrls.GetLastUrlByTimeAsync();
                return _mapper.Map<ShortUrl, ShortUrlDTO>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"The error occurred while retrieving last shortened Url: {ex.ToString()}.");
                return new ShortUrlDTO();
            }
        }

        public async Task<IDictionary<int, string>> ProcessLinkAsync(string url, LinkProcessType processType)
        {
            _logger.LogInformation("The link processing started.");

            ILinkProcessStrategy strategy = processType switch
            {
                LinkProcessType.Forwarding => new ForwardingLinkProcessStrategy(this),
                LinkProcessType.Shortening => new ShorteningLinkProcessStrategy(this),
                _ => throw new ArgumentException("Invalid process type", nameof(processType)),
            };

            return await strategy.ProcessLinkAsync(url);
        }

        public async Task CreateAsync(string url)
        {
            var newShortUrl = new ShortUrlDTO
            {
                LongUrl = url
            };

            GenerateShortUrl(newShortUrl);

            _uof.ShortUrls.Create(_mapper.Map<ShortUrlDTO, ShortUrl>(newShortUrl));
            var result = await _uof.SaveChangesAsync();

            if (result == 0)
            {
                _logger.LogError("An entity wasn`t created.");
            }

            _logger.LogInformation("The entity has been created successfully.");
        }

        public async Task<int> ClearHistoryAsync()
        {
            await _uof.ShortUrls.ClearShortUrlsAsync();
            var result = await _uof.SaveChangesAsync();

            _logger.LogInformation($"The history was cleared: {result} entities were affected.");

            return result;
        }

        internal async Task<bool> ShortLinkExistsAsync(string code) => await _uof.ShortUrls.ShortLinkExistsAsync(code);

        internal async Task<bool> LongLinkExistsAsync(string link) => await _uof.ShortUrls.LongLinkExistsAsync(link);

        internal void GenerateShortUrl(ShortUrlDTO entity)
        {
            string generatedCode = ShortUrlGenerator.GenerateShortUrlCode();
            entity.Code = generatedCode;
            entity.ShortenedUrl = SD.BaseAddress + generatedCode;

            _logger.LogInformation($"Short Url was generated: '{entity.ShortenedUrl}'.");
        }
    }
}
