using DevTools.ViewModels.Generator;
using System.Windows.Controls;

namespace DevTools.Views.Generator
{
	/// <summary>
	/// Interaction logic for DevLoremIpsumGeneratorView.xaml
	/// </summary>
	public partial class DevLoremIpsumGeneratorView : UserControl
	{
		public DevLoremIpsumGeneratorView()
		{
			InitializeComponent();
			DataContext = new DevLoremIpsumGeneratorViewModel();
		}
	}
}
