using System.Globalization;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ValkyrieFinancial.Protos;
using VikingFinancial.Data.Transaction;

namespace TransactionService.Services;

public class UserService : ValkyrieFinancial.Protos.UserService.UserServiceBase
{
    private readonly ILogger<UserService> m_logger;

    public UserService(ILogger<UserService> p_logger)
    {
        m_logger = p_logger;
    }
}