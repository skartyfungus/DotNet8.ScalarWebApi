using Microsoft.EntityFrameworkCore;

public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<TransactionHistory> TransactionHistories { get; set; }

}
