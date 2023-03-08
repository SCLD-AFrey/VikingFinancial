using Microsoft.Extensions.Logging;
using TransactionClient.Models;

namespace TransactionClient.ViewModels.Controls;

public class DetailsViewModel : ViewModelBase
{
    private readonly ILogger<DetailsViewModel> m_logger;

    public DetailsViewModel(ILogger<DetailsViewModel> p_logger,
        DetailsModel p_model)
    {
        m_logger = p_logger;
        m_logger.LogDebug("Instantiating DetailsViewModel");
        Model = p_model;
    }
    private DetailsModel Model { get; }
}