using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI.Fody.Helpers;

namespace TransactionClientGui.Controls.Global;

public partial class PageHeader : UserControl
{
    public PageHeader()
    {
        InitializeComponent();
        this.DataContext = this;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    [Reactive] public string PageTitle { get; set; } = string.Empty;
}