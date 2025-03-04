namespace BankApi.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}

