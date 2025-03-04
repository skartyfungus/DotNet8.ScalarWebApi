[Route("api/transactions")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly BankDbContext _context;

    public TransactionsController(BankDbContext context)
    {
        _context = context;
    }

    // Transfer Money
    [HttpPost]
    public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
    {
        var fromUser = await _context.Users.FindAsync(request.FromUserId);
        var toUser = await _context.Users.FindAsync(request.ToUserId);

        if (fromUser == null || toUser == null)
            return NotFound("User(s) not found.");

        if (fromUser.Balance < request.Amount)
            return BadRequest("Insufficient balance.");

        fromUser.Balance -= request.Amount;
        toUser.Balance += request.Amount;

        var transaction = new Transaction
        {
            FromUserId = request.FromUserId,
            ToUserId = request.ToUserId,
            Amount = request.Amount,
            Date = DateTime.UtcNow
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return Ok(new { fromUser.Balance, toUser.Balance });
    }
}

public class TransferRequest
{
    public int FromUserId { get; set; }
    public int ToUserId { get; set; }
    public decimal Amount { get; set; }
}
