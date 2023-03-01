using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using TransactionClient.ViewModels;

namespace TransactionClient.Views;

public partial class UserLoginView : Window
{
#pragma warning disable CS8618
    public UserLoginView() { }
#pragma warning restore CS8618

    public UserLoginView(UserLoginViewModel p_viewModel, ILogger<UserLoginView> p_logger)
    {
        p_logger.LogDebug("Creating UserLoginView");

        InitializeComponent();

        DataContext = p_viewModel;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}