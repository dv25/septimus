using UnoApp1.Pages;

namespace UnoApp1.Views;

public sealed partial class StartPage_Section06 : UserControl
{
    public Visibility HeaderVisibility { get => (Visibility)GetValue(HeaderVisibilityProperty); set => SetValue(HeaderVisibilityProperty, value); }
    public static readonly DependencyProperty HeaderVisibilityProperty = DependencyProperty.Register(nameof(HeaderVisibility), typeof(Visibility), typeof(StartPage_Section05), new PropertyMetadata(Visibility.Visible));

    public StartPage_Section06()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Current.NavigateTo<VacantAssignmentsPage>();
    }
}
