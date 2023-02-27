using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace VikingFinancial.Gui.Views.FirstRunWizard.Pages;

public partial class WelcomePageView : UserControl
{
    public WelcomePageView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}