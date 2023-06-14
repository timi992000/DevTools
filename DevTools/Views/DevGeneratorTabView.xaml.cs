using DevTools.ViewModels;
using System.Windows.Controls;

namespace DevTools.Views
{
  /// <summary>
  /// Interaction logic for DevGeneratorTabView.xaml
  /// </summary>
  public partial class DevGeneratorTabView : UserControl
  {
    public DevGeneratorTabView()
    {
      InitializeComponent();
      DataContext = new DevGeneratorTabViewModel();
    }
  }
}
