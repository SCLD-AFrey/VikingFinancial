using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Xpo;
using Google.Protobuf.WellKnownTypes;
using ValkyrieFinancial.Protos;
using VikingFinancial.Data.Transaction;

namespace TransactionNavigatorGui.Services.Grpc;

public class TransactionService
{
    private readonly ClientProvisioner m_clientProvisionerService;
    public List<G_Transaction> GetTransactionRange(DateTime p_startDate, DateTime p_endDate)
    {
        var client = m_clientProvisionerService.ProvisionTransactionClient();
        var reply = client.GetTransactionRangeAsync(new G_GetTransactionRangeRequest()
        {
            StartDate = p_startDate.ToUniversalTime().ToTimestamp(),
            EndDate = p_endDate.ToUniversalTime().ToTimestamp()
        });
        return reply.ResponseAsync.Result.TransactionRange.Transactions.ToList();
    } 

    
}