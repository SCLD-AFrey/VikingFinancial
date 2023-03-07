using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using TransactionClient.ViewModels;

namespace TransactionClient.Views;
public partial class MainWindow : Window
{
    private readonly ILogger<MainWindow> m_logger;

    // ReSharper disable once EmptyConstructor
    public MainWindow() { }
        
    public MainWindow(MainWindowViewModel p_viewModel, ILogger<MainWindow> p_logger)
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

    private async void MainWindow_OnClosing(object? p_sender, CancelEventArgs p_e)
    {
        m_logger.LogDebug("Closing application was triggered");
        Environment.Exit(0);
    }
}