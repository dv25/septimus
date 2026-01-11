using UnoApp1.Pages;

namespace UnoApp1.Views;

public sealed partial class StartPage_Section05 : UserControl
{
    public ImageSource ImageSource { get => (ImageSource)GetValue(ImageSourceProperty); set => SetValue(ImageSourceProperty, value); }
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(StartPage_Section05), new PropertyMetadata(null));

    public StartPage_Section05()
    {
        InitializeComponent();
    }

    private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Current.NavigateTo<WhatWeDoPage>();
    }
}
