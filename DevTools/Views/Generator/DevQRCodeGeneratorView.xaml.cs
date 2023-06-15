using DevTools.ViewModels.Generator;
using System.Windows.Controls;

namespace DevTools.Views.Generator
{
	/// <summary>
	/// Interaction logic for DevQRCodeGeneratorView.xaml
	/// </summary>
	public partial class DevQRCodeGeneratorView : UserControl
	{
		public DevQRCodeGeneratorView()
		{
			InitializeComponent();
			DataContext = new DevQRCodeGeneratorViewModel();
		}

		private void __TextForGenerationChanged(object sender, TextChangedEventArgs e)
		{
			if (sender is TextBox tb && DataContext is DevQRCodeGeneratorViewModel vm)
				vm.TextForGeneration = tb.Text;
		}
	}
}
