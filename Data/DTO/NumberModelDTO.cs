namespace excercise.NumberToWords.Data.DTO
{
    public class NumberModelDTO
    {
        public decimal Amount { get; set; } = 0;
        public string StringAmount { get; set; } = string.Empty;
        public string InputAmount { get; set; } = string.Empty;
        public bool InputCondition { get; set; } = true;
    }
}
