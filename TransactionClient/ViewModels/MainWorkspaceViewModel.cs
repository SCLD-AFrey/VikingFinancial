using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Metadata;
using Avalonia.Threading;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TransactionClient.Models;


namespace TransactionClient.ViewModels;

public class MainWorkspaceViewModel : ViewModelBase
{
    
    private readonly ILogger<MainWorkspaceViewModel> m_logger;
    private readonly IServiceProvider                m_serviceProvider;

    public MainWorkspaceViewModel(MainWorkspaceModel p_model,
        ILogger<MainWorkspaceViewModel> p_logger,
        IServiceProvider p_serviceProvider)
    {
        m_logger          = p_logger;
        m_serviceProvider = p_serviceProvider;
        Model                             = p_model;
    }
    private MainWorkspaceModel Model       { get; }
}