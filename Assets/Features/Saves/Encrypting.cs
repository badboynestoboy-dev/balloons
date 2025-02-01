namespace Balloons.Features.Saves
{
    /// <summary>
    /// Статичный касс для шифрования данных
    /// </summary>
    public static class Encrypting
    {
        private const string CODE_WORD = "nightcrawler";

        public static string CryptoXOR(string text, string codeWord = CODE_WORD)
        {
            string result = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                result += (char)(text[i] ^ codeWord[i % codeWord.Length]);
            }

            return result;
        }
    }
}