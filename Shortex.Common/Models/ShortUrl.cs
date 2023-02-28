namespace Shortex.Common.Models
{
    public class ShortUrl
    {
        public Guid Id { get; set; }
        public string LongUrl { get; set; }
        public string Code { get; set; }
        public string ShortenedUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
