using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TransactionClient.Views.Controls;

public partial class TransactionListView : UserControl
{
    public TransactionListView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}