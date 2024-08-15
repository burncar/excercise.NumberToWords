namespace excercise.NumberToWords.Data
{
    public interface IConvertNumToWords
    {
        Task<string> ConvertNumber(decimal num);
    }
}
