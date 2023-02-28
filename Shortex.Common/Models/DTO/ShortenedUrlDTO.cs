namespace Shortex.Common.Models.DTO
{
    public class ShortenedUrlDTO
    {
        public Guid Id { get; set; }
        public string LongUrl { get; set; }
        public string? Code { get; set; }
        public string? ShortUrl { get; set; }
    }
}
