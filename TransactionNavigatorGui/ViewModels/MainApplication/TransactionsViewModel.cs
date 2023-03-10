using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TransactionNavigatorGui.Services.Grpc;
using ValkyrieFinancial.Protos;
using VikingFinancial.Data.Transaction;

namespace TransactionNavigatorGui.ViewModels.MainApplication;

public class TransactionsViewModel : ViewModelBase
{
    private readonly ClientProvisioner         m_clientProvisionerService;
    private readonly TransactionService        m_transactionService;

    public TransactionsViewModel()
    {
        m_clientProvisionerService = new ClientProvisioner();
        Transactions = m_transactionService.GetTransactionRange(StartDate, EndDate);
    }
    
    [Reactive] public string PageHeaderText { get; set; } = "Transactions";
    [Reactive] public List<Transaction> Transactions { get; set; } = new();
    [Reactive] public DateTime StartDate { get; set; } = DateTime.UtcNow - TimeSpan.FromDays(DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month));
    [Reactive] public DateTime EndDate { get; set; } = DateTime.UtcNow;


}