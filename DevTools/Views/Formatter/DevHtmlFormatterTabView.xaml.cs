using DevTools.Core.Extender;
using DevTools.ViewModels.Formatter;
using System.Windows.Controls;

namespace DevTools.Views.Formatter
{
    /// <summary>
    /// Interaction logic for DevHtmlFormatterTabView.xaml
    /// </summary>
    public partial class DevHtmlFormatterTabView : UserControl
    {
        public DevHtmlFormatterTabView()
        {
            InitializeComponent();
            DataContext = new DevHtmlFormatterViewModel();
        }

        private void __TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.Source is TextBox tb && DataContext is DevFormatterViewModelBase vm)
                vm.TextToFormat = tb.Text.ToStringValue();
        }
    }
}
