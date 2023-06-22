using DevTools.ViewModels.Converter.FileToBase64;
using System.Windows.Controls;

namespace DevTools.Views.Converter
{
	/// <summary>
	/// Interaction logic for DevFileToBase64ConverterView.xaml
	/// </summary>
	public partial class DevFileToBase64ConverterView : UserControl
	{
		public DevFileToBase64ConverterView()
		{
			InitializeComponent();
		}

		private void __Swapped(object sender, System.EventArgs e)
		{
			if (DataContext is DevFileToBase64ConverterViewModel vm)
			{
				vm.Swapped();
			}
		}

		private void __Base64StringChanged(object sender, TextChangedEventArgs e)
		{
			if (sender is TextBox tb && tb.DataContext is DevBase64ToFileItemViewModel vm)
				vm.EncodedBase64String = tb.Text;
		}
	}
}
