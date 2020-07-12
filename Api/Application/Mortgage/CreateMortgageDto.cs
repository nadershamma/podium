namespace Api.Application.Mortgage
{
    public class CreateMortgageDto
    {
        public string Lender { get; set; }
        public int Rate { get; set; }
        public string Type { get; set; }
        public int LoanToValue { get; set; }
    }
}