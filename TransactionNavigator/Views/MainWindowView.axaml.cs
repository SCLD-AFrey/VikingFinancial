using System;
using System.ComponentModel;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Models;
using Microsoft.Extensions.Logging;
using TransactionNavigator.ViewModels;

namespace TransactionNavigator.Views;

public partial class MainWindowView : Window
{
    private readonly ILogger<MainWindowView> m_logger;
    private readonly MainWorkspaceViewModel  m_mainWorkspaceViewModel;

#pragma warning disable CS8618
    public MainWindowView() { }
    #pragma warning restore CS8618
        
    public MainWindowView(MainWindowViewModel p_viewModel, ILogger<MainWindowView> p_logger, MainWorkspaceViewModel p_mainWorkspaceViewModel)
    {
        m_logger = p_logger;
            
        m_logger.LogDebug("Creating MainWindowView");
            
        InitializeComponent();

        DataContext              = p_viewModel;
        m_mainWorkspaceViewModel = p_mainWorkspaceViewModel;

        #if DEBUG
        this.AttachDevTools();
        #endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    // ReSharper disable once UnusedParameter.Local
    private async void MainWindow_OnClosing(object? p_sender, CancelEventArgs p_e)
    {
        m_logger.LogDebug("Closing application was triggered");
        p_e.Cancel = true;

        if (m_mainWorkspaceViewModel.UserIsEditing)
        {
            var assemblyName = Assembly.GetEntryAssembly()!.GetName().Name;
            var assets       = AvaloniaLocator.Current.GetService<IAssetLoader>();
            var messageBox = MessageBoxManager.GetMessageBoxCustomWindow(new MessageBoxCustomParams
                                                                         {
                                                                             ContentTitle = "Confirm Closing",
                                                                             ContentMessage =
                                                                                 @"You have unsaved changes, do you want to exit? ",
                                                                             ButtonDefinitions = new[]
                                                                                 {
                                                                                     new ButtonDefinition
                                                                                     {
                                                                                         Name      = "Yes",
                                                                                         IsDefault = false
                                                                                     },
                                                                                     new ButtonDefinition
                                                                                     {
                                                                                         Name     = "No",
                                                                                         IsDefault = true,
                                                                                         IsCancel = true
                                                                                     }
                                                                                 },
                                                                             Icon = MessageBox.Avalonia.Enums.Icon.Question,
                                                                             WindowStartupLocation =
                                                                                 WindowStartupLocation.CenterScreen,
                                                                             CanResize         = false,
                                                                             Topmost           = true,
                                                                             ShowInCenter      = true,
                                                                             SystemDecorations = SystemDecorations.Full
                                                                         });
            
            var response = await messageBox.Show();

            if ( response is not "Yes" )
            {
                return;
            }
        }

        p_e.Cancel = false;
        Environment.Exit(0);
    }
}