using System.Text;

namespace Shortex.BusinessLogic.Helpers
{
    public static class ShortUrlGenerator
    {
        private static readonly string _availableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";
        private static readonly int _codeLength = 8;
        private static readonly Random _random = new();

        public static string GenerateShortUrlCode()
        {
            StringBuilder builder = new();

            for (int i = 0; i <= _codeLength; i++)
            {
                builder.Append(_availableChars[_random.Next(_availableChars.Length)]);
            }
            return builder.ToString();
        }
    }
}
