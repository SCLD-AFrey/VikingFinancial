using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;


namespace TransactionNavigator.Views.Controls;

public partial class HamburgerButton : UserControl
{
    public HamburgerButton()
    {
        InitializeComponent();
        TextColor = Brushes.Black;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
        
    public static readonly StyledProperty<string> ButtonTextProperty =
        AvaloniaProperty.Register<HamburgerButton, string>(nameof(ButtonText));

    public string ButtonText
    {
        get => GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }
        
    public static readonly StyledProperty<ICommand> ClickCommandProperty =
        AvaloniaProperty.Register<HamburgerButton, ICommand>(nameof(ClickCommand));

    public ICommand ClickCommand
    {
        get => GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
    }
        
    public static readonly StyledProperty<object> CommandParameterProperty =
        AvaloniaProperty.Register<HamburgerButton, object>(nameof(CommandParameter));

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
        
    public static readonly StyledProperty<IImage> ImageSourceProperty =
        AvaloniaProperty.Register<HamburgerButton, IImage>(nameof(ImageSource));

    public IImage ImageSource
    {
        get => GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
        
    public static readonly StyledProperty<IBrush> TextColorProperty =
        AvaloniaProperty.Register<HamburgerButton, IBrush>(nameof(TextColor));

    public IBrush TextColor
    {
        get => GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
}