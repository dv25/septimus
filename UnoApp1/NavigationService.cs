using System.Runtime.InteropServices.JavaScript;
using Microsoft.UI.Xaml.Media.Animation;
using Uno.Foundation;
using Windows.UI.Core;

namespace UnoApp1;

public class NavigationService
{
    private Frame? _frame;
    private ContentPresenter? _header;
    private ScrollViewer? MainScrollViewer;

    public static NavigationService Current { get => field ??= new NavigationService(); }

    public void RefreshHeaderWith<T>() where T : UIElement, new()
    {
        (_header as ContentPresenter)?.Content = new T();
    }

    public void AttachFrame(Frame frame)
    {
        _frame = frame;
        _frame.NavigationFailed += _frame_NavigationFailed;

#if __WASM__
        SystemNavigationManager.GetForCurrentView().BackRequested += NavigationService_BackRequested;
        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
#endif
    }

    private void NavigationService_BackRequested(object? sender, BackRequestedEventArgs e)
    {
        if (_frame == null) return;

        if (_frame.CanGoBack)
        {
            e.Handled = true;
            NavigateBack();
        }
    }

    public void AttachHeader(ContentPresenter header) => _header = header;

    public void AttachScrollViewer(ScrollViewer scrollViewer)
    {
        MainScrollViewer?.ViewChanged -= OnScrollChanged;
        MainScrollViewer = scrollViewer;
        MainScrollViewer.ViewChanged += OnScrollChanged;

        _lastOffset = 0;
        o1 = false;
        o2 = false;

        if (_header != null)
        {
            //AnimateNavbar(_header, 0);
            AnimateStart();
        }
    }

    private double _lastOffset = 0;

    private bool o1 = false;
    private bool o2 = false;

    private void OnScrollChanged(object? sender, ScrollViewerViewChangedEventArgs e)
    {
        if (_header is null || MainScrollViewer is null) return;

        double offset = MainScrollViewer.VerticalOffset;

        if (offset < 60)
        {
            o1 = false;
            o2 = false;
            AnimateNavbar(_header, 0);
        }
        else if ((offset > _lastOffset) && o1 == false)
        {
            o1 = true;
            o2 = false;
            // Scrolling down → hide navbar
            AnimateNavbar(_header, -_header.ActualSize.Y);
        }
        else if ((offset < _lastOffset) && o2 == false)
        {
            o1 = false;
            o2 = true;
            // Scrolling up → show navbar
            AnimateNavbar(_header, 0);
        }

        _lastOffset = offset;
    }

    private async void AnimateStart()
    {
        if (_header != null)
        {
            await Task.Delay(100);
            AnimateNavbar(_header, 0);
        }
    }

    private void AnimateNavbar(UIElement element, double targetY)
    {
        // Create animation
        var animation = new DoubleAnimation
        {
            To = targetY,
            Duration = new Duration(TimeSpan.FromMilliseconds(300)),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        // Apply to TranslateTransform.Y
        Storyboard.SetTarget(animation, _header);
        Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(TranslateTransform.Y)");
        //Storyboard.SetTargetProperty(animation, "Opacity");

        // Ensure NavBar has a RenderTransform
        if (element.RenderTransform is not TranslateTransform)
            element.RenderTransform = new TranslateTransform();

        // Run storyboard
        var sb = new Storyboard();
        sb.Children.Add(animation);
        sb.Begin();
    }

    public void NavigateBack()
    {
        _frame?.GoBack();
    }

    public void NavigateForward()
    {
        _frame?.GoForward();
    }

    public void NavigateTo<T>() where T : Page
    {
        _frame?.Navigate(typeof(T));
    }

    public void RefreshPage()
    {
        _frame?.Navigate(_frame.SourcePageType);
    }

    public void SetLanguageTo(string language)
    {
        Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = language;

        _frame?.Navigate(_frame.SourcePageType);
    }

    private void _frame_NavigationFailed(object sender, NavigationFailedEventArgs e)
    {
        throw new InvalidOperationException($"Failed to load {e.SourcePageType.FullName}: {e.Exception}");
    }
}
