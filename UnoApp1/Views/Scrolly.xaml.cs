using Microsoft.UI.Xaml.Markup;

namespace UnoApp1.Views;

[ContentProperty(Name = nameof(Children))]
public sealed partial class Scrolly : UserControl
{
    public UIElementCollection Children => PART_Panel.Children;

    public Scrolly()
    {
        InitializeComponent();

        NavigationService.Current.AttachScrollViewer(MainScrollViewer);
    }
}
