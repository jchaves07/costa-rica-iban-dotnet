using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using costa_rica_dotnet.Models;

namespace costa_rica_dotnet
{
    public static class IBANUtils
    {
        const string LOCAL_COUNTRY = "CR";
        const int IBAN_VALID_LENGTH = 22;
        const string IBAN_VALID_FORMAT = @"^CR\d\d0(1|[3-8])\d{16}$";

        /// <summary>
        /// Lista de operadores (entidades).
        /// </summary>
        public static List<IBANOperator> IBANOperatorList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IBANOperator>>(File.ReadAllText("bankCollection.json"));
        /// <summary>
        /// Obtiene el codigo de pais de la cuenta IBAN
        /// </summary>
        /// <param name="IBAN">Numero de cuenta IBAN</param>
        /// <returns></returns>
        public static string getCountryPrefixFromIBAN(string IBAN)
        {
            if (string.IsNullOrWhiteSpace(IBAN))
            {
                return string.Empty;
            }
            return IBAN.Substring(0, 2);
        }
        /// <summary>
        /// Valida que sea una cuenta local (de Costa Rica).
        /// </summary>
        /// <param name="IBAN">Numero de cuenta IBAN</param>
        /// <returns></returns>
        public static bool verifyIBANLocalCountryPrefix(string IBAN)
        {
            if (string.IsNullOrWhiteSpace(IBAN))
            {
                return false;
            }
            return getCountryPrefixFromIBAN(IBAN).Equals(LOCAL_COUNTRY);
        }
        /// <summary>
        /// Verifica que sea la longitud correcta de la cuenta IBAN (22).
        /// </summary>
        /// <param name="IBAN">Numero de cuenta IBAN</param>
        /// <returns></returns>
        public static bool verifyIBANLength(string IBAN)
        {
            return !string.IsNullOrWhiteSpace(IBAN) && IBAN.Length.Equals(IBAN_VALID_LENGTH);
        }
        /// <summary>
        /// Valida el formato correcto de la cuenta IBAN
        /// </summary>
        /// <param name="IBAN">Numero de cuenta IBAN</param>
        /// <returns></returns>
        public static bool verifyIBANFormat(string IBAN)
        {
            if (string.IsNullOrWhiteSpace(IBAN))
            {
                return false;
            }
            var match = Regex.Match(IBAN, IBAN_VALID_FORMAT, RegexOptions.IgnoreCase);
            return match.Success && (IBANOperatorList.Where(q=> q.code == IBAN.Substring(5,3)).Count() > 0);
        }
        /// <summary>
        /// Obtiene el codigo de la entidad de la cuenta IBAN consultada.
        /// </summary>
        /// <param name="IBAN">Numero de cuenta IBAN</param>
        /// <returns></returns>
        public static string getBankCodeFromIBAN(string IBAN)
        {
            if (string.IsNullOrWhiteSpace(IBAN))
            {
                return string.Empty;
            }
            return IBAN.Substring(5, 3);
        }
        /// <summary>
        /// Obtiene el objeto de la entidad (operador) asociado a la cuenta consultada
        /// </summary>
        /// <param name="IBAN">Numero de cuenta IBAN</param>
        /// <returns></returns>
        public static IBANOperator getBankObjectFromIBAN(string IBAN)
        {
            var BankCode = getBankCodeFromIBAN(IBAN);
            return IBANOperatorList.Where(q => q.code == BankCode).FirstOrDefault();
        }
        /// <summary>
        /// Obtiene el nombre de la entidad (operador) de la cuenta IBAN consultada
        /// </summary>
        /// <param name="IBAN">Numero de cuenta IBAN</param>
        /// <returns></returns>
        public static string getBankNameFromIBAN(string IBAN)
        {
            return getBankNameFromIBAN(IBAN, false);
        }
        /// <summary>
        /// Obtiene el nombre de la entidad (operador) de la cuenta IBAN consultada
        /// </summary>
        /// <param name="IBAN">Numero de cuenta IBAN</param>
        /// <param name="returnRepresentative">Indica si se quiere obtener el nombre de la entidad que representa a la entidad de esa cuenta</param>
        /// <returns></returns>
        public static string getBankNameFromIBAN(string IBAN, bool returnRepresentative)
        {
            var BankObject = getBankObjectFromIBAN(IBAN);
            if (returnRepresentative)
            {
                return BankObject.representative;
            }
            return BankObject.entity;
        }

    }
}
