namespace Api.Core.Mortgage
{
    public class Mortgage
    {
        public long Id { get; set; }
        public string Lender { get; set; }
        public int Rate { get; set; }
        public string Type { get; set; }
        public int LoanToValue { get; set; }
    }
}