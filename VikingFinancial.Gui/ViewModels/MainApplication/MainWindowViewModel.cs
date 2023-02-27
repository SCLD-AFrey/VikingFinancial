using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Collections;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;
using TextCopy;
using VikingFinancial.Gui.Models.BackingModels.MainApplication;
using VikingFinancial.Gui.Models.DataStructures.Logging;
using VikingFinancial.Gui.Models.Services;
using VikingFinancial.Gui.Views.MainApplication.MainWindow;

namespace VikingFinancial.Gui.ViewModels.MainApplication
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ILogger<MainWindowViewModel> m_logger;
        
        public MainWindowViewModel(ILogger<MainWindowViewModel> p_logger,
            MainWindowModel p_model,
            LandingPageView p_welcomePageView,
            MainWorkspaceView p_mainWorkspaceView,
            VersionInfo p_versionInfoService)
        {
            m_logger = p_logger;

            m_logger.LogDebug("Instantiating MainWindowViewModel");

            Model = p_model;
        
            //WindowTitle = $"ConfigOS MPO Forge v{p_versionInfoService.FileVersionString}";
            WindowTitle = $"Viking Financial";

            MainWorkspaceView = p_mainWorkspaceView;

            CollectionSink.SetCollection(Messages);
        }
        private MainWindowModel Model { get; }
        [Reactive] public string WindowTitle { get; set; }
        public LandingPageView WelcomePageView { get; set; }
        public MainWorkspaceView MainWorkspaceView { get; set; }
        [Reactive] public AvaloniaList<ConsoleLogMessage> Messages         { get; set; } = new ();
        [Reactive] public AvaloniaList<ConsoleLogMessage> SelectedMessages { get; set; } = new ();
        [Reactive] public int SelectedPageIndex { get; set; } = 0;

        public void CopyMessages()
        {
            var selectedText = new StringBuilder();

            foreach ( var message in SelectedMessages )
            {
                selectedText.AppendLine(message.Text);
            }
            
            
            ClipboardService.SetText(selectedText.ToString());
        }
    }
}
