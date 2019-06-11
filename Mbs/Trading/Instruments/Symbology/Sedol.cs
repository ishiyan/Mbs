using System;
using System.Globalization;

namespace Mbs.Trading.Instruments
{
    /// <summary>
    /// SEDOL (Stock Exchange Daily Official List) utilities.
    /// </summary>
    /// <remarks>
    /// <para>
    /// SEDOL stands for Stock Exchange Daily Official List, a list of security identifiers used in the United
    /// Kingdom and Ireland for clearing purposes. The numbers are assigned by the London Stock Exchange, on
    /// request by the security issuer. SEDOL codes serve as the NSIN for all securities issued in the United
    /// Kingdom and are therefore part of the security's ISIN as well.
    /// </para>
    /// <para>
    /// SEDOL codes are seven characters in length, consisting of two parts: a six-place alphanumeric code and
    /// a trailing check digit. There are three types of SEDOL codes.
    /// </para>
    /// <para>
    /// ➊ Old style SEDOL codes issued prior to 2004 were composed only of digits. They cannot begin with the
    /// leading digit 9.
    /// </para>
    /// <para>
    /// ➋ New style SEDOL codes issued after 2004, were changed to be alpha-numeric and are issued
    /// sequentially, beginning with B000009. They begin with a leading letter followed by five alphanumeric
    /// characters and the trailing check digit. Vowels 'AEUIO' are never used.
    /// </para>
    /// <para>
    /// ➌ User defined SEDOL codes begin with a leaing digit 9 followed by five alphanumeric characters and
    /// the trailing check digit. The alphanumeric characters may be vowels. There will be no codes issued with
    /// 9 as the lead character. This allows the 9-series to be reserved for end user allocation.
    /// </para>
    /// <para>
    /// The check digit for SEDOL codes is chosen to make the total weighted sum of all seven characters a
    /// multiple of 10. The check digit is computed using a weighted sum of the first six characters. Letters
    /// are converted to numbers by adding their ordinal position in the alphabet to 9, such that B = 11 and
    /// Z = 35. The resulting string of 7 numbers is then multiplied by the weighting factors [1, 3, 1, 7, 3,
    /// 9, 1]. The check digit is chosen to make the total sum, including the check digit, a multiple of 10,
    /// which can be calculated from the weighted sum of the first six characters as
    /// <c>(10 - (weighted sum modulo 10)) modulo 10</c>.
    /// </para>
    /// <para>
    /// For British and Irish securities, SEDOL codes are converted to ISINs by padding the front with two
    /// zeros, then adding the country code on the front and the ISIN check digit at the end.
    /// </para>
    /// <para>
    /// See
    /// http://www.londonstockexchange.com/products-and-services/reference-data/sedol-master-file/sedol-master-file.htm,
    /// http://www.londonstockexchange.com/products-and-services/reference-data/sedol-master-file/documentation/sedol-technical-specification.pdf,
    /// https://en.wikipedia.org/wiki/SEDOL.
    /// </para>
    /// </remarks>
    public static class Sedol
    {
        private const int Length = 7;
        private const int ChecksumDigitIndex = 6;
        private const char UserDefinedChar = '9';

        /// <summary>
        /// Validates if a given SEDOL code is valid.
        /// </summary>
        /// <param name="sedol">The SEDOL code to validate.</param>
        /// <returns>The <see cref="SedolValidationError"/> validation result.</returns>
        public static SedolValidationError SedolValidate(this string sedol)
        {
            if (string.IsNullOrWhiteSpace(sedol) || sedol.Length != Length)
                return SedolValidationError.InvalidLength;

            char[] input = sedol.ToCharArray();
            bool isUserDefined = false;
            bool isOldStyle = false;
            int sum = 0;
            for (int i = 0; i < ChecksumDigitIndex; ++i)
            {
                int c = char.ToUpper(input[i], CultureInfo.InvariantCulture);
                bool isDigit = c >= '0' && c <= '9';
                bool isAlpha = c >= 'A' && c <= 'Z';
                if (!isAlpha && !isDigit)
                    return SedolValidationError.HasNonAlphaNumerics;

                // Now the character is alphanumeric.
                if (i == 0)
                {
                    isUserDefined = c == UserDefinedChar;
                    isOldStyle = !isUserDefined && isDigit;
                }

                if (isOldStyle && !isDigit)
                    return SedolValidationError.OldStyleHasAlpha;

                if (!isUserDefined && "AEUIO".IndexOf((char)c, StringComparison.Ordinal) >= 0)
                    return SedolValidationError.HasVowels;

                if (isDigit)
                    c -= '0';
                else
                    c += 10 - 'A';
                sum += ApplyWeights(i, c);
            }

            sum = (10 - sum % 10) % 10;
            int checkDigit = input[ChecksumDigitIndex];
            if (checkDigit < '0' || checkDigit > '9')
                return SedolValidationError.InvalidCheckDigit;
            checkDigit -= '0';
            if (sum != checkDigit)
                return SedolValidationError.InvalidCheckDigit;
            return SedolValidationError.None;
        }

        /// <summary>
        /// Validates if a given SEDOL code is valid.
        /// </summary>
        /// <param name="sedol">The SEDOL code to validate.</param>
        /// <returns>True if SEDOL is valid.</returns>
        public static bool SedolIsValid(this string sedol)
        {
            return sedol.SedolValidate() == SedolValidationError.None;
        }

        /// <summary>
        /// Calculates the trailing check digit for a SEDOL code.
        /// The input SEDOL code will be not validated.
        /// </summary>
        /// <param name="sedol">The SEDOL code to calculate a trailing check digit from. This code will not be validated.</param>
        /// <returns>The calculated trailing check digit.</returns>
        public static char SedolCalculateCheckDigit(this string sedol)
        {
            if (string.IsNullOrWhiteSpace(sedol) || sedol.Length < ChecksumDigitIndex)
                return '0';
            int sum = 0;
            for (int i = 0; i < ChecksumDigitIndex; ++i)
            {
                int c = char.ToUpper(sedol[i], CultureInfo.InvariantCulture);
                if (c >= '0' && c <= '9')
                    c -= '0';
                else
                    c += 10 - 'A';
                sum += ApplyWeights(i, c);
            }

            sum = (10 - sum % 10) % 10;
            return (char)('0' + sum);
        }

        private static int ApplyWeights(int i, int c)
        {
            switch (i)
            {
                case 1:
                case 4:
                    c *= 3; break;
                case 3:
                    c *= 7; break;
                case 5:
                    c *= 9; break;
            }

            return c;
        }
    }
 }
