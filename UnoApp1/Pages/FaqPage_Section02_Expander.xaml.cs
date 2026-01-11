namespace UnoApp1.Views;

public sealed partial class FaqPage_Section02_Expander : UserControl
{
    public string Header { get => (string)GetValue(HeaderProperty); set => SetValue(HeaderProperty, value); }
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(string), typeof(FaqPage_Section02_Expander), new PropertyMetadata(null));

    public string Details { get => (string)GetValue(DetailsProperty); set => SetValue(DetailsProperty, value); }
    public static readonly DependencyProperty DetailsProperty = DependencyProperty.Register(nameof(Details), typeof(string), typeof(FaqPage_Section02_Expander), new PropertyMetadata(null));

    public Visibility DetailsVisibility { get => (Visibility)GetValue(DetailsVisibilityProperty); set => SetValue(DetailsVisibilityProperty, value); }
    public static readonly DependencyProperty DetailsVisibilityProperty = DependencyProperty.Register(nameof(DetailsVisibility), typeof(Visibility), typeof(FaqPage_Section02_Expander), new PropertyMetadata(Visibility.Collapsed));

    public FaqPage_Section02_Expander()
    {
        this.InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (DetailsVisibility == Visibility.Visible)
        {
            DetailsVisibility = Visibility.Collapsed;
            VisualStateManager.GoToState(this, "Collapsed", true);
        }
        else
        {
            DetailsVisibility = Visibility.Visible;
            VisualStateManager.GoToState(this, "Visible", true);
        }
    }
}
