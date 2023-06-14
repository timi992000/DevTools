using DevTools.ViewModels.Checks.Regex;
using System.Windows.Controls;

namespace DevTools.Views.Checks
{
  /// <summary>
  /// Interaction logic for DevRegexCheckView.xaml
  /// </summary>
  public partial class DevRegexCheckView : UserControl
  {
    public DevRegexCheckView()
    {
      InitializeComponent();
      DataContext = new DevRegexCheckViewModel();
    }
  }
}
