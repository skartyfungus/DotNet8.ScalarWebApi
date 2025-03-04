using Microsoft.EntityFrameworkCore;

namespace BankApi
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
    }
}

