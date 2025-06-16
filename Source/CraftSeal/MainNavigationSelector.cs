using CraftSeal.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CraftSeal;

internal partial class MainNavigationSelector : DataTemplateSelector
{
    public DataTemplate SessionTemplate { get; set; } = null!;

    public DataTemplate OtherTemplate { get; set; } = null!;

    protected override DataTemplate SelectTemplateCore(object item)
    {
        return item switch
        {
            SessionVM => SessionTemplate,
            _ => OtherTemplate,
        };
    }

    protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) => SelectTemplate(item);
}
