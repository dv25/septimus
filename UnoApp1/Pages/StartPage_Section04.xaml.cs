using UnoApp1.Pages;

namespace UnoApp1.Views;

public sealed partial class StartPage_Section04 : UserControl
{
    public StartPage_Section04()
    {
        InitializeComponent();
    }

    private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Current.NavigateTo<AboutPage>();
    }
}
