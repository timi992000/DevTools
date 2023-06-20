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
	}
}
