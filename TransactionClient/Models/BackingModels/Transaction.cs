using System;

namespace TransactionClient.Models.BackingModels;

public abstract class Transaction
{
    public double Amount { get; set; }
    public string Note { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateTransaction { get; set; }
    public UserProfile AddedByUser { get; set; }
    public TransactionType Type { get; set; }
}