using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;
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


        var carousel = this.Get<Carousel>("Carousel");
        carousel.SelectedIndex = p_viewModel.SelectedIndex;

        var btnStartPage = this.Get<Button>("BtnStartPage");
        btnStartPage.Click += (p_sender, p_args) => p_viewModel.SelectedIndex = 0;
        
        var btnTransactions = this.Get<Button>("BtnTransactions");
        btnTransactions.Click += (p_sender, p_args) => p_viewModel.SelectedIndex = 1;

        var btnDetails = this.Get<Button>("BtnShowDetail");
        btnDetails.Click += (p_sender, p_args) => p_viewModel.SelectedIndex = 2;

        var btnSettings = this.Get<Button>("BtnSettings");
        btnSettings.Click += (p_sender, p_args) => p_viewModel.SelectedIndex = 3;
        
        
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