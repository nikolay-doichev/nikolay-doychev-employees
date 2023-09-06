using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace SirmaWebApp.Helpers
{
    public class FlexibleDateTimeConverter : ITypeConverter
    {
        private readonly string[] dateFormats = new[]
        {
            "yyyy-MM-dd",
            "MM/dd/yyyy",
            "dd-MM-yyyy",
        };
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text) || text.Trim().Equals("NULL", StringComparison.OrdinalIgnoreCase))
            {
                // Handle "NULL" strings as null for nullable DateTime
                return DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            foreach (string format in dateFormats)
            {
                if (DateTime.TryParseExact(text.Trim(), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                {
                    return result;
                }
            }

            // If none of the formats match, try a final attempt with a default format
            if (DateTime.TryParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var defaultResult))
            {
                return defaultResult;
            }

            // If parsing fails, you may want to handle this differently, like returning a default value or throwing an exception.
            throw new CsvTypeConverterException("Invalid date format");
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null)
            {
                return null;
            }

            if (value is DateTime dateTime)
            {
                return dateTime.ToString("yyyy-MM-dd");
            }
            return null;
        }

        public class CsvTypeConverterException : Exception 
        {
            public CsvTypeConverterException()
            {
            }

            public CsvTypeConverterException(string message)
                : base(message)
            {
            }

            public CsvTypeConverterException(string message, Exception innerException)
                : base(message, innerException)
            {
            }
        }
    }
}
