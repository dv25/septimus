namespace UnoApp1.Views;

public sealed partial class StartPage_Section01 : UserControl
{
    public string Header { get => (string)GetValue(HeaderProperty); set => SetValue(HeaderProperty, value); }
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(string), typeof(StartPage_Section01), new PropertyMetadata(null));

    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(StartPage_Section01), new PropertyMetadata(null));

    public StartPage_Section01()
    {
        InitializeComponent();
    }
}
