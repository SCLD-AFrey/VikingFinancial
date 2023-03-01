using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using TransactionClient.ViewModels;

namespace TransactionClient.Views;

public partial class MainWorkspaceView : UserControl
{
    public MainWorkspaceView() { }
    
    public MainWorkspaceView(MainWorkspaceViewModel p_viewModel, ILogger<MainWorkspaceView> p_logger)
    {
        p_logger.LogDebug("Creating MainWorkspaceView");

        InitializeComponent();

        DataContext = p_viewModel;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}