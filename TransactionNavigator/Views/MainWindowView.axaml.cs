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

#pragma warning disable CS8618
    public MainWindowView() { }
    #pragma warning restore CS8618
        
    public MainWindowView(MainWindowViewModel p_viewModel, ILogger<MainWindowView> p_logger)
    {
        m_logger = p_logger;
            
        m_logger.LogDebug("Creating MainWindowView");
            
        InitializeComponent();

        DataContext              = p_viewModel;

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
        Environment.Exit(0);
    }
}