using System;
using System.Text.RegularExpressions;

namespace costa_rica_dotnet
{
    public static class IBANUtils
    {
        const string LOCAL_COUNTRY = "CR";
        const int IBAN_VALID_LENGTH = 22;
        const string IBAN_VALID_FORMAT = @"/^CR\d\d0(1|[3-8])\d{16}$/";
        public static string getCountryPrefixFromIBAN(string IBAN)
        {
            return IBAN.Substring(0, 2);
        } 
        public static bool verifyIBANLocalCountryPrefix(string IBAN)
        {
            return getCountryPrefixFromIBAN(IBAN).Equals(LOCAL_COUNTRY);
        }
        public static bool verifyIBANLength(string IBAN)
        {
            return !string.IsNullOrWhiteSpace(IBAN) && IBAN.Length.Equals(IBAN_VALID_LENGTH);
        }
        public static bool verifyIBANFormat(string IBAN)
        {
            if (string.IsNullOrWhiteSpace(IBAN))
            {
                return false;
            }
            var match = Regex.Match(IBAN, IBAN_VALID_FORMAT, RegexOptions.IgnoreCase);
            return match.Success;

        }

    }
}
