namespace Questao5.Domain.Entities
{
    public class AccountBalance
    {
        public long? AccountNumber { get; set; }
        public string? AccountHolder { get; set; }
        public DateTime? Date { get; set; }
        public double? Balance { get; set; }
        public AccountBalance()
        {
        }
    }
}
