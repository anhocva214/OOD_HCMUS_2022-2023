
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace financial_management_service.Extensions
{
    public static class StringExt
    {
        public static bool EqualIgnoreCase(this string val1, string val2)
        {
            try
            {
                return string.Equals(val1, val2, StringComparison.CurrentCultureIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNullOrEmpty(this string? dto)
        {
            try
            {
                return string.IsNullOrWhiteSpace(dto);
            }
            catch
            {
                return false;
            }
        }

        public static decimal ToDecimal(this string input, decimal defaultVal)
        {
            try
            {
                return decimal.Parse(input);
            }
            catch
            {
                return defaultVal;
            }
        }

        public static T ToObject<T>(this string? input)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(input);
            }
            catch
            {
                return default;
            }
        }

        public static Guid ConvertToGuid(this string? input)
        {
            try
            {
                return Guid.Parse(input);
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public static bool IsGuid(this string? input) => ConvertToGuid(input) != Guid.Empty;

        public static bool IsStringNumber(this string input)
        {
            try
            {
                var numberRegex = new Regex(@"^\d+$");
                return numberRegex.IsMatch(input);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsSpecialString(this string input)
        {
            try
            {
                var specialRegex = new Regex("^[a-zA-Z0-9;]");
                return specialRegex.IsMatch(input);
            }
            catch
            {
                return false;
            }
        }


    }
}
