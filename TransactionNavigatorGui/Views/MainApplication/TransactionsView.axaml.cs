using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TransactionNavigatorGui.ViewModels.MainApplication;

namespace TransactionNavigatorGui.Views.MainApplication;

public partial class TransactionsView : UserControl
{
    public TransactionsView()
    {
        InitializeComponent();
        DataContext = new TransactionsViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}