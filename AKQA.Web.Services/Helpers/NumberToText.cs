using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AKQA.Web.Services.Helpers
{
    public static class NumberToText
    {
        private static string[] _ones =
        {
            "zero",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        };

        private static string[] _teens =
        {
            "ten",
            "eleven",
            "twelve",
            "thirteen",
            "fourteen",
            "fifteen",
            "sixteen",
            "seventeen",
            "eighteen",
            "nineteen"
        };

        private static string[] _tens =
        {
            "",
            "ten",
            "twenty",
            "thirty",
            "forty",
            "fifty",
            "sixty",
            "seventy",
            "eighty",
            "ninety"
        };

        // US Nnumbering:
        private static string[] _thousands =
        {
            "",
            "thousand",
            "million",
            "billion",
            "trillion",
            "quadrillion"
        };


        public static string Convert(decimal value)
        {
            string digits;
            string isNegative = string.Empty;
            string strNumDec = string.Empty;
            string decima_numnber = string.Empty;

            // Use StringBuilder to build result
            StringBuilder builder = new StringBuilder();

            /* set the decimal number upto 2 places */
            value = Math.Round(value, 2);

            // Convert integer portion of value to string
            digits = value.ToString();

            /* check if the number is in Minus */
            if (digits.Contains("-"))
            {
                // append the Minus string 
                builder.Insert(0, "Minus ");

                // remove Minus (-) from the input 
                digits = digits.Substring(1, digits.Length - 1);
            }

            /* check if number have decimals */

            if (digits.IndexOf(".") + 1 != 0)
            {
                strNumDec = digits.Substring(digits.IndexOf(".") + 2 - 1);
                strNumDec = strNumDec.Trim('0');
                digits = digits.Substring(0, digits.IndexOf(".") + 0);


            }

          

            /* convert the number to words */
            string number = NumberToWords(digits);
            // Append number reuslt
            builder.AppendFormat("{0} ", number);


            /* convert the decimal points to words */
            if (!String.IsNullOrEmpty(strNumDec)) { 

                decima_numnber = NumberToWords(strNumDec);
                // Append fractional portion/cents
                builder.AppendFormat("and {0} cents", decima_numnber);
            }

            // Capitalize first letter
            return String.Format("{0}{1}",
                Char.ToUpper(builder[0]),
                builder.ToString(1, builder.Length - 1));
        }


        private static string NumberToWords(string digits)
        {
            string temp;
            bool showThousands = false;
            bool allZeros = true;

            // Use StringBuilder to build result
            StringBuilder builder = new StringBuilder();

            // Traverse characters in reverse order
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int ndigit = (int)(digits[i] - '0');
                int column = (digits.Length - (i + 1));



                // Determine if ones, tens, or hundreds column
                switch (column % 3)
                {
                    case 0:        // Ones position
                        showThousands = true;
                        if (i == 0)
                        {
                            // First digit in number (last in loop)
                            temp = String.Format("{0} ", _ones[ndigit]);
                        }
                        else if (digits[i - 1] == '1')
                        {
                            // This digit is part of "teen" value
                            temp = String.Format("{0} ", _teens[ndigit]);
                            // Skip tens position
                            i--;
                        }
                        else if (ndigit != 0)
                        {
                            // Any non-zero digit
                            temp = String.Format("{0} ", _ones[ndigit]);
                        }
                        else
                        {
                            // This digit is zero. If digit in tens and hundreds
                            // column are also zero, don't show "thousands"
                            temp = String.Empty;
                            // Test for non-zero digit in this grouping
                            if (digits[i - 1] != '0' || (i > 1 && digits[i - 2] != '0'))
                                showThousands = true;
                            else
                                showThousands = false;
                        }

                        // Show "thousands" if non-zero in grouping
                        if (showThousands)
                        {
                            if (column > 0)
                            {
                                temp = String.Format("{0}{1}{2}",
                                    temp,
                                    _thousands[column / 3],
                                    allZeros ? " " : " and ");
                            }
                            // Indicate non-zero digit encountered
                            allZeros = false;
                        }
                        builder.Insert(0, temp);
                        break;

                    case 1:        // Tens column
                        if (ndigit > 0)
                        {
                            temp = String.Format("{0}{1}",
                                _tens[ndigit],
                                (digits[i + 1] != '0') ? "-" : " ");
                            builder.Insert(0, temp);
                        }
                        break;

                    case 2:        // Hundreds column
                        if (ndigit > 0)
                        {
                            temp = String.Format("{0} hundred ", _ones[ndigit]);
                            builder.Insert(0, temp);
                        }
                        break;
                }
            }

            // Capitalize first letter
            return String.Format("{0}{1}",
                Char.ToUpper(builder[0]),
                builder.ToString(1, builder.Length - 1));


        }
    }
}
