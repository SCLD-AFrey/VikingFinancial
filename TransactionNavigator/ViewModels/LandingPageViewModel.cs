using Microsoft.Extensions.Logging;
using TransactionNavigator.Models;

namespace TransactionNavigator.ViewModels;

public class LandingPageViewModel
{
    
    private readonly ILogger<LandingPageViewModel> m_logger;
    
    public LandingPageViewModel(ILogger<LandingPageViewModel> p_logger,
        LandingPageModel p_model)
    {
        m_logger = p_logger;
        m_logger.LogDebug("Instantiating WelcomePageViewModel");
        Model = p_model;
    }

    private LandingPageModel Model { get; }
    public void ClickNavigationButton(object p_parameter)
    {
        if ( p_parameter is not string stringParameter ) return;

        m_logger.LogDebug("Clicked Welcome Page navigation button '{NavigationTarget:l}'", stringParameter);
        
        Model.SetNavigation(stringParameter);
    }
}