using UnoApp1.Pages;
using Windows.Globalization;

namespace UnoApp1.Views;

public sealed partial class HeaderView : UserControl
{
    public HeaderView()
    {
        InitializeComponent();
    }

    private void HomeButton_Click(object sender, RoutedEventArgs e) => NavigateTo<StartPage>();
    private void BackButton_Click(object sender, RoutedEventArgs e) => NavigationService.Current.NavigateBack();
    private void ForwardButton_Click(object sender, RoutedEventArgs e) => NavigationService.Current.NavigateForward();
    private void AboutButton_Click(object sender, RoutedEventArgs e) => NavigateTo<AboutPage>();
    private void WhatWeDoButton_Click(object sender, RoutedEventArgs e) => NavigateTo<WhatWeDoPage>();
    private void VacantAssignmentsButton_Click(object sender, RoutedEventArgs e) => NavigateTo<VacantAssignmentsPage>();
    private void FaqButton_Click(object sender, RoutedEventArgs e) => NavigateTo<FaqPage>();
    private void ContactUsButton_Click(object sender, RoutedEventArgs e) => NavigateTo<ContactUsPage>();

    private void LanguageButton_Click(object sender, RoutedEventArgs e)
    {
        if (ApplicationLanguages.PrimaryLanguageOverride is null) return;

        if (ApplicationLanguages.PrimaryLanguageOverride.StartsWith("en"))
        {
            NavigationService.Current.SetLanguageTo("sv-SE");
        }
        else
        {
            NavigationService.Current.SetLanguageTo("en-US");
        }

        NavigationService.Current.RefreshHeaderWith<HeaderView>();
    }

    private void NavigateTo<T>() where T : Page
    {
        NavigationService.Current.NavigateTo<T>();

        MenuButton.Flyout?.Hide();
    }
}
