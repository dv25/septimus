using UnoApp1.Pages;

namespace UnoApp1.Views;

public sealed partial class FooterView : UserControl
{
    public FooterView()
    {
        InitializeComponent();
    }

    private void HomeButton_Click(object sender, RoutedEventArgs e) => NavigateTo<StartPage>();
    private void AboutButton_Click(object sender, RoutedEventArgs e) => NavigateTo<AboutPage>();
    private void WhatWeDoButton_Click(object sender, RoutedEventArgs e) => NavigateTo<WhatWeDoPage>();
    private void VacantAssignmentsButton_Click(object sender, RoutedEventArgs e) => NavigateTo<VacantAssignmentsPage>();
    private void FaqButton_Click(object sender, RoutedEventArgs e) => NavigateTo<FaqPage>();
    private void ContactUsButton_Click(object sender, RoutedEventArgs e) => NavigateTo<ContactUsPage>();

    private void NavigateTo<T>() where T : Page
    {
        NavigationService.Current.NavigateTo<T>();
    }
}
