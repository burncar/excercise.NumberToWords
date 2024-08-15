
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;
using System.ComponentModel;
using excercise.NumberToWords.Data.SD;

namespace excercise.NumberToWords.Data
{
    public class IConvertNumToWordsImpl : IConvertNumToWords
    {
        
        public async Task<string> ConvertNumber(decimal num)
        {
            long wholeNumber = (long)num;
            decimal decimalPart = num - wholeNumber; 
            string decimalPartStr = (decimalPart * 100).ToString("00")+ "/100 dollars" ; 

            string amount = GetStringConverter.NumberToWordsConvert(wholeNumber);
            return amount+" and "+ decimalPartStr;
        }
    }
}
