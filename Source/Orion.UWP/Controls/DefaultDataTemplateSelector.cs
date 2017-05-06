using System.Linq;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WinRTXamlToolkit.Controls.Extensions;

namespace Orion.UWP.Controls
{
    public class DefaultDataTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var rootParent = container.GetAncestorsOfType<ListViewBase>().First();
            return (DataTemplate) rootParent.Resources[item.GetType().Name];
        }
    }
}