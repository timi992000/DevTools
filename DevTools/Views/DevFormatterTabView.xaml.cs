using DevTools.Core.Extender;
using DevTools.ViewModels.Formatter;
using System.Windows.Controls;

namespace DevTools.Views
{
	/// <summary>
	/// Interaction logic for DevFormatterTabView.xaml
	/// </summary>
	public partial class DevFormatterTabView : UserControl
	{
		public DevFormatterTabView()
		{
			InitializeComponent();
			DataContext = new DevFormatterTabViewModel();
		}

        private void __TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.Source is TextBox tb && DataContext is DevFormatterTabViewModel vm)
                vm.TextToFormat = tb.Text.ToStringValue();
        }

    }
}
