using Avalonia.Collections;

namespace TransactionClient.Models.BackingModels;

public class UserProfile
{
    public string Username { get; set; }
    public string Password { get; set; }
    public byte[] Salt { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsLocked { get; set; }
    public AvaloniaList<Transaction> Transactions {get;set;} = new();
}