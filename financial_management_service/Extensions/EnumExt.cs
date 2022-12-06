using System.ComponentModel;
using System.Reflection;
using financial_management_service.Core.Exceptions;

namespace financial_management_service.Extensions
{
	public static class EnumExt
	{
        public static T ToEnum<T>(this string? value)
        {
            if (value == null) throw new UnhandledException();

            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string GetDescription(this Enum value)
        {
            try
            {
                var fi = value.GetType().GetField(value.ToString());
                if (fi == null) return string.Empty;

                var attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                        typeof(DescriptionAttribute),
                        false);

                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Description;

                return value.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static TEnum GetEnumByDescription<TEnum>(this string description)
           where TEnum : Enum
        {
            foreach (var item in typeof(TEnum).GetFields())
                if (Attribute.GetCustomAttribute(item, typeof(DescriptionAttribute)) is
                    DescriptionAttribute attribute && string.Equals(attribute.Description, description, StringComparison.OrdinalIgnoreCase))
                    return (TEnum)item.GetValue(null)!;

            throw new ArgumentException($"Enum item with description \"{description}\" could not be found",
                nameof(description));
        }
    }
}

