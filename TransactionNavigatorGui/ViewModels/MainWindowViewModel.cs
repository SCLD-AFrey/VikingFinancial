using ReactiveUI.Fody.Helpers;

namespace TransactionNavigatorGui.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    [Reactive] public string WindowTitle { get; set; } = "Welcome to Transaction Navigator";

    public MainWindowViewModel()
    {
        
    }
}