using DevTools.Core.Extender;
using DevTools.ViewModels.Converter;
using System.Windows.Controls;

namespace DevTools.Views.Converter
{
	/// <summary>
	/// Interaction logic for DevJson_XmlConverterView.xaml
	/// </summary>
	public partial class DevJson_XmlConverterView : UserControl
	{
		public DevJson_XmlConverterView()
		{
			InitializeComponent();
		}

		private void __Swapped(object sender, System.EventArgs e)
		{
			if (DataContext is DevJson_XmlConverterViewModel vm)
			{
				vm.Swapped();
			}
		}

		private void __XMLTextChanged(object sender, TextChangedEventArgs e)
		{
			if (e.Source is TextBox tb && DataContext is DevJson_XmlConverterViewModel vm)
				vm.XMLText = tb.Text.ToStringValue();
		}

		private void __JsonTextChanged(object sender, TextChangedEventArgs e)
		{
			if (e.Source is TextBox tb && DataContext is DevJson_XmlConverterViewModel vm)
				vm.JSONText = tb.Text.ToStringValue();
		}
	}
}
