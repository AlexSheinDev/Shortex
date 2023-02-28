namespace Shortex.Common.Models.DTO
{
    public class ShortUrlDTO
    {
        public Guid Id { get; set; }
        public string LongUrl { get; set; }
        public string? Code { get; set; }
        public string? ShortenedUrl { get; set; }
    }
}
