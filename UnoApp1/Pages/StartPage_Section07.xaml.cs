using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using UnoApp1.Pages;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UnoApp1.Views
{
	public sealed partial class StartPage_Section07 : UserControl
	{
		public StartPage_Section07()
		{
			this.InitializeComponent();
		}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Current.NavigateTo<ContactUsPage>();
        }
    }
}
