public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }  // Required modifier for non-nullable properties
    public required string PasswordHash { get; set; } // Required modifier for non-nullable properties
    public decimal Balance { get; set; }
}