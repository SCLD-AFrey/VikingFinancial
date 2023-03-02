using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TransactionNavigator.Views.Controls;

public partial class PageHeader : UserControl
{
    public PageHeader()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
            
    public static readonly StyledProperty<string> HeaderTextProperty =
        AvaloniaProperty.Register<PageHeader, string>(nameof(HeaderText));

    public string HeaderText
    {
        get => GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }
            
    public static readonly StyledProperty<object> CustomContentProperty =
        AvaloniaProperty.Register<PageHeader, object>(nameof(CustomContent));

    public object CustomContent
    {
        get => GetValue(CustomContentProperty);
        set => SetValue(CustomContentProperty, value);
    }
}