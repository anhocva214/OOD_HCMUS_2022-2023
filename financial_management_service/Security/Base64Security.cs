using System.Text;
using financial_management_service.Extensions;

namespace financial_management_service.Security
{
    public class Base64Security
    {
        protected Base64Security() { }

        public static string Encode(string plainText) => System.Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

        public static string Decode(string base64Encoded) => Encoding.UTF8.GetString(ConvertFromBase64String(base64Encoded));

        private static byte[] ConvertFromBase64String(string input)
        {
            if (input.IsNullOrEmpty()) return Array.Empty<byte>();
            try
            {
                string working = input.Replace('-', '+').Replace('_', '/');

                StringBuilder bld = new(working);
                while (bld.ToString().Length % 4 != 0)
                    bld.Append('=');

                string str = bld.ToString();

                return Convert.FromBase64String(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Array.Empty<byte>();
            }
        }
    }
}

