using System;
using System.Reactive;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TransactionNavigatorGui.Services.Grpc;
using ValkyrieFinancial.Protos;

namespace TransactionNavigatorGui.ViewModels.MainApplication;

public class MainApplicationViewModel : ViewModelBase
{
    private readonly ClientProvisioner         m_clientProvisionerService;
    public MainApplicationViewModel()
    {
        DoNavigation = ReactiveCommand.Create<string>(Navigate);
        m_clientProvisionerService = new ClientProvisioner();
        CheckConnection();
    }
    
    public void Navigate(string p_indexName)
    {
        SelectedPageIndex = p_indexName.ToLower() switch
        {
            "welcomeview" => 0,
            "transactionsview" => 1,
            "balanceview" => 2,
            "settingsview" => 3,
            _ => 0
        };
    }
    
    public ReactiveCommand<string, Unit> DoNavigation { get; }
    
    [Reactive] public bool IsOnline { get; set; } = false;
    [Reactive] public int SelectedPageIndex { get; set; } = 0;

    private async Task CheckConnection()
    {
        var client = m_clientProvisionerService.ProvisionConnectivityClient();
        try
        {
            var response = await client.CheckServerConnectionAsync(new Empty());
            switch (response.ResponseCase)
            {
                case G_ConnectCheckResponse.ResponseOneofCase.Success:
                    IsOnline = true;
                    break;
                case G_ConnectCheckResponse.ResponseOneofCase.Failure:
                    IsOnline = false;
                    break;
            }
        }
        catch (Exception e)
        {
            IsOnline = false;
        }
    }


}