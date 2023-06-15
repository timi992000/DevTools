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
			DataContext = new DevFileToBase64ConverterViewModel();
		}
	}
}
