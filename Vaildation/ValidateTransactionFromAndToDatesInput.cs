using System.Globalization;

namespace E_Wallet_Server_Side.Vaildation
{
    public static class ValidateTransactionFromAndToDatesInput
    {
        public static bool ValidateDateFormats(string date, out DateTime validDate)
        {
            string[] allowedFormats = new[]
            {
                "yyyy-MM-dd",      // ISO 8601 format
                "dd/MM/yyyy",      // European format with slashes
                "MM/dd/yyyy",      // US format with slashes
                "dd-MM-yyyy",      // European format with hyphens
                "MM-dd-yyyy",      // US format with hyphens
                "dd.MM.yyyy",      // Dotted format
                "MMMM dd, yyyy",   // Full month name
                "dd MMM yyyy",     // Short month name
                "yyyy/MM/dd",      // ISO format with slashes
                "dd/MM/yy",        // European short year format
                "MM/dd/yy",        // US short year format
                "yyyy-MM-ddTHH:mm:ss", // ISO 8601 with time
            };

            // Attempt to parse the date with allowed formats
            return DateTime.TryParseExact(date, allowedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out validDate);
        }

    }
}
