using DevTools.Core.Extender;
using DevTools.ViewModels.Formatter;
using System.Windows.Controls;

namespace DevTools.Views.Formatter
{
	/// <summary>
	/// Interaction logic for DevJsonFormatterTabView.xaml
	/// </summary>
	public partial class DevJsonFormatterTabView : UserControl
	{
		public DevJsonFormatterTabView()
		{
			InitializeComponent();
			DataContext = new DevJsonFormatterViewModel();
		}

		private void __TextChanged(object sender, TextChangedEventArgs e)
		{
			if (e.Source is TextBox tb && DataContext is DevFormatterViewModelBase vm)
				vm.TextToFormat = tb.Text.ToStringValue();
		}

	}
}
