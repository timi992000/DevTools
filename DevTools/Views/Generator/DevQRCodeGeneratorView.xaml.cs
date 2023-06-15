using DevTools.ViewModels.Generator;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

    private void __MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
    {
      if(Keyboard.Modifiers == ModifierKeys.Control && DataContext is DevQRCodeGeneratorViewModel vm)
      {
        vm.ChangeScalingFactor(e.Delta);
      }
    }

    private void __KeyDown(object sender, KeyEventArgs e)
    {
      if (DataContext is DevQRCodeGeneratorViewModel vm)
      {
        vm.KeyDown(e);
      }
    }
  }
}
