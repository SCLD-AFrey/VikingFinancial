using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using TransactionNavigatorGui.ViewModels.MainApplication;

namespace TransactionNavigatorGui.Views.MainApplication;

public partial class MainApplicationView : UserControl
{
   
    public MainApplicationView()
    {
        InitializeComponent();
        DataContext = new MainApplicationViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}