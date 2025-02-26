namespace excercise.NumberToWords.Data.SD
{
    public static class GetStringConverter
    {
        static string[] units = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        static string[] teens = { "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        static string[] tens = { "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public static string NumberToWordsConvert(long number)
        {
            if (number == 0)
                return units[0];

            if (number < 0)
                return "Negative " + NumberToWordsConvert(Math.Abs(number));

            string words = "";
          

             if ((number / 1000000000000) > 0)
            {
                words += NumberToWordsConvert(number / 1000000000000) + " Trillion ";
                number %= 1000000000000;
            }


            if ((number / 1000000000) > 0)
            {
                words += NumberToWordsConvert(number / 1000000000) + " Billion ";
                number %= 1000000000;
            }


            if ((number / 1000000) > 0)
            {
                words += NumberToWordsConvert(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWordsConvert(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWordsConvert(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += " ";

                if (number < 10)
                    words += units[number];
                else if (number < 20)
                    words += teens[number - 11];
                else
                {
                    words += tens[number / 10 - 1];
                    if ((number % 10) > 0)
                        words += "-" + units[number % 10];
                }
            }

            return words;
        }
    }
}
