using System.Text;

namespace Shortex.BusinessLogic.Helpers
{
    public static class ShortUrlGenerator
    {
        private static readonly string _availableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";
        private static readonly int _codeLength = 8;
        private static readonly Random _random = new();
        private static readonly StringBuilder _builder = new();

        public static string GenerateShortUrlCode()
        {
            for (int i = 0; i <= _codeLength; i++)
            {
                _builder.Append(_availableChars[_random.Next(_availableChars.Length)]);
            }
            return _builder.ToString();
        }
    }
}
