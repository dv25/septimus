using UnoApp1.Pages;

namespace UnoApp1.Views;

public sealed partial class RootView : UserControl
{
    public RootView()
    {
        this.InitializeComponent();

        NavigationService.Current.AttachFrame(Frame);
        NavigationService.Current.AttachHeader(Header);

        NavigationService.Current.NavigateTo<StartPage>();
    }
}
