using Microsoft.AspNetCore.Mvc;
[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly BankDbContext _context;

    public UsersController(BankDbContext context)
    {
        _context = context;
    }

    // Register User
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (_context.Users.Any(u => u.Username == user.Username))
            return BadRequest("Username already taken.");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        user.Balance = 0;  // Set initial balance to 0

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }

    // Get Balance
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBalance(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        return Ok(new { user.Username, user.Balance });
    }

    // Deposit Money
    [HttpPost("{id}/deposit")]
    public async Task<IActionResult> Deposit(int id, [FromBody] decimal amount)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        user.Balance += amount;
        await _context.SaveChangesAsync();

        // Log deposit transaction
        var transaction = new Transaction
        {
            FromUserId = id,
            ToUserId = id,
            Amount = amount,
            Date = DateTime.UtcNow
        };
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return Ok(user);
    }
}