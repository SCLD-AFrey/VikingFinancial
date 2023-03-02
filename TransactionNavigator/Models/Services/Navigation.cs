using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TransactionNavigator.ViewModels;

namespace TransactionNavigator.Models.Services;

public class Navigation
{
    private readonly ILogger<Navigation> m_logger;
    private readonly IServiceProvider m_serviceProvider;

    public Navigation(ILogger<Navigation> p_logger, IServiceProvider p_serviceProvider)
    {
        m_logger = p_logger;
        m_serviceProvider = p_serviceProvider;

        m_logger.LogDebug("Initializing Navigation service");
    }

    public void SetNavigation(string p_stringParameter)
    {
        m_logger.LogDebug("Navigating to '{NavigationTarget:l}'", p_stringParameter);

        switch (p_stringParameter.ToUpper())
        {
            case "WELCOME":
                NavigateToWelcomeScreen();
                break;
            case "BUDGET":
                NavigateToBudgetSetupScreen();
                break;
            case "TRANSACTIONS":
                NavigateToTransactionsScreen();
                break;
            case "SETTINGS":
                NavigateToSettingsScreen();
                break;
        }
    }

    private void NavigateToWelcomeScreen()
    {
        var mainWindowViewModel = m_serviceProvider.GetRequiredService<MainWindowViewModel>();
        mainWindowViewModel.SelectedPageIndex = 0;
    }

    private void NavigateToBudgetSetupScreen()
    {
        var mainWindowViewModel = m_serviceProvider.GetRequiredService<MainWindowViewModel>();
        mainWindowViewModel.SelectedPageIndex = 1;
    }

    private void NavigateToTransactionsScreen()
    {
        var mainWindowViewModel = m_serviceProvider.GetRequiredService<MainWindowViewModel>();
        mainWindowViewModel.SelectedPageIndex = 2;
    }

    private void NavigateToSettingsScreen()
    {
        var mainWindowViewModel = m_serviceProvider.GetRequiredService<MainWindowViewModel>();
        mainWindowViewModel.SelectedPageIndex = 3;
    }
}