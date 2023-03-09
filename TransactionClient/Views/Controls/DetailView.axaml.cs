using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TransactionClient.Views.Controls;

public partial class DetailView : UserControl
{
    public DetailView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}