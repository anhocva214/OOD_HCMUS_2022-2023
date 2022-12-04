using System.ComponentModel;

namespace financial_management_service.Utils
{
    public class EnumUltis
    {
        protected EnumUltis() { }

        public static Dictionary<string, string> ConvertEnumToDictionary<T>() 
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
                return new Dictionary<string, string>();

            return Enum.GetValues(enumType).Cast<Enum>().ToDictionary(e => e.ToString(), e => GetEnumDescription(e));
        }


        public static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
