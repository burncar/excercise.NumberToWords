using Microsoft.AspNetCore.Routing.Template;
using System.Text;

namespace excercise.NumberToWords.Data.SD
{
    public class GetNumberConverter
    {
        //static string[] units = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        //static string[] teens = { "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        //static string[] tens = { "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        //static string[] highTeens = {"Million", "Billion", "Trillion" };
        //static string[] highTenTeens = {"Ten Million", "Ten Billion", "Ten Trillion" };
        //static string[] highHundredTeens = {"Hundred Million", "Hundred Billion", "Hundred Trillion" };
        static Dictionary<string, decimal> numberMap = new Dictionary<string, decimal>
        {
            {"over", (decimal)0.01},{ "overhundred", (decimal)0.01},{"over one hundred", (decimal)0.01},
            {"centavos", (decimal)0.01},{"cents", (decimal)0.01},{"and",0 },
            {"zero", 0}, {"one", 1}, {"two", 2}, {"three", 3}, {"four", 4},
            {"five", 5}, {"six", 6}, {"seven", 7}, {"eight", 8}, {"nine", 9},
            {"ten", 10}, {"eleven", 11}, {"twelve", 12}, {"thirteen", 13},
            {"fourteen", 14}, {"fifteen", 15}, {"sixteen", 16}, {"seventeen", 17},
            {"eighteen", 18}, {"nineteen", 19}, {"twenty", 20}, {"thirty", 30},
            {"forty", 40}, {"fifty", 50}, {"sixty", 60}, {"seventy", 70},
            {"eighty", 80}, {"ninety", 90}, {"hundred", 100}, {"thousand", 1000},
            {"million", 1000000 }, {"billion", 1000000000},{"trillion", 1000000000000},

        };

        public static decimal StringToNumbersConvert(string words)
        {
            decimal result = 0;
            decimal temp = 0;
            List<string> arr = words.Split(" ").ToList();
            int ctr = 0;
            for(int i=0; i < arr.Count(); i++) {
                if (numberMap.ContainsKey(arr[i].ToLower()))
                {
                    if(numberMap[arr[i].ToLower()] == 100 
                        || numberMap[arr[i].ToLower()] == 1000 
                        || numberMap[arr[i].ToLower()] == 1000000
                        || numberMap[arr[i].ToLower()] == 1000000000
                        || numberMap[arr[i].ToLower()] == 1000000000000
                        )
                    {

                       
                        temp = temp * numberMap[arr[i].ToLower()];

                        if (temp >= 1000000000000)
                        {
                            result = result + temp;
                            temp = 0;
                        }
                        else if (temp >= 1000000000)
                        {
                            result = result + temp;
                            temp = 0;
                        }
                        else if (temp >= 1000000)
                        {
                            result = result + temp;
                            temp = 0;
                        }else if(temp >= 1000)
                        {
                            result = result + temp;
                            temp = 0;
                        }
                       
                    }
                    else if (arr[i] == "and")
                    {
                        result += temp;
                        temp = 0;
                    }
                    else if (numberMap[arr[i].ToLower()] == (decimal)0.01)
                    {

                        result += temp;
                        break;
                    }
                    else
                    {

                        if (i + 1 == arr.Count()-1)
                        {
                            if (numberMap[arr[i + 1].ToLower()] == (decimal)0.01)
                            {
                                temp = (numberMap[arr[i].ToLower()] * numberMap[arr[i + 1].ToLower()]) + temp;
                            }
                            else
                            {
                                temp = temp + numberMap[arr[i].ToLower()];
                            }
                        }
                        else if (i + 2 == arr.Count() - 1)
                        {
                            if (numberMap[arr[i + 2].ToLower()] == (decimal)0.01)
                            {
                                temp = (numberMap[arr[i].ToLower()] * numberMap[arr[i + 2].ToLower()]) + temp;
                            }
                            else
                            {

                                temp = temp + numberMap[arr[i].ToLower()];
                            }
                        }
                        else
                        {
                            temp = temp + numberMap[arr[i].ToLower()];
                        }
                        
                    }


                    //if (ctr + 1 == arr.Count())
                    //{
                    //    result += temp;
                    //}
                    //ctr++;
                    
                       
                }

            }


            return result;
        }

    }
}
