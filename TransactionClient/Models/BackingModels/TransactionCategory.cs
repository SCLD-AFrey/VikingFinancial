namespace TransactionClient.Models.BackingModels;

public class TransactionCategory
{
    public string Title { get; set; }
    public bool IsLocked { get; set; } = false;
    public bool IsCredit { get; set; } = false;
    public bool IsDebit { get; set; } = false;
}