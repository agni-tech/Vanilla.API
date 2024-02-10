using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Vanilla.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + string.Join("", input.Skip(1));
        }

        public static string FormatCurrency(this float value)
        {
            return value.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));
        }

        public static string FormatCurrency(this int value)
        {
            return value.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));
        }
        public static string MakeHash(this string value)
        {
            var salt = "#g$";
            var encryptedPassword = CreateSHA256(value + salt);
            return encryptedPassword.ToString();
        }
        private static string CreateSHA256(string strData)
        {
            var message = Encoding.UTF8.GetBytes(strData);
            using (var alg = SHA256.Create())
            {
                string hex = "";

                var hashValue = alg.ComputeHash(message);
                foreach (byte x in hashValue)
                    hex += string.Format("{0:x2}", x);

                return hex;
            }
        }

    }
}
