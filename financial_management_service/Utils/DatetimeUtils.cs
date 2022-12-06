using System.Globalization;

namespace financial_management_service.Extensions
{
	public static class DatetimeUtils
	{
        public static DateTime? ToDate(string dto,string format)
        {
            try
            {
                return DateTime.ParseExact(dto, format, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime ToEndOfDay(DateTime dto) => new DateTime(dto.Year, dto.Month, dto.Day, 23, 59, 59);

        public static DateTime ToDatetimeWithoutTimezone(this DateTime dt) => DateTime.SpecifyKind(dt, DateTimeKind.Utc);
    }
}
