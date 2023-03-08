using Microsoft.Extensions.Logging;

namespace TransactionClient.Models;

public class DetailsModel
{
    private readonly ILogger<DetailsModel> m_logger;
    
    public DetailsModel(ILogger<DetailsModel> p_logger)
    {
        m_logger = p_logger;
    }
}