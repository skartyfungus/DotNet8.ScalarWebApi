
namespace BankApi.Models
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public User User { get; set; }
    }
}