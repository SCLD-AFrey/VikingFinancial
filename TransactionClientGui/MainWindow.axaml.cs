using System.Reactive;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace TransactionClientGui
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            m_carousel = this.FindControl<Carousel>("MainCarousel")!;
            DoNavigation = ReactiveCommand.Create<string>(Navigate);
        }

        public ReactiveCommand<string, Unit> DoNavigation { get; }
        [Reactive] private Carousel m_carousel { get; set; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Navigate(string p_page)
        {
            m_carousel.SelectedIndex = p_page switch
            {
                "welcome" => 0,
                "transactions" => 1,
                "balances" => 2,
                "settings" => 3,
                _ => m_carousel.SelectedIndex
            };
        }
    }
}