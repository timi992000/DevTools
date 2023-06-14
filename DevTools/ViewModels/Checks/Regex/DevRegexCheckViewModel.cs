using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DevTools.ViewModels.Checks.Regex
{
  public class DevRegexCheckViewModel : ViewModelBase
  {
    public DevRegexCheckViewModel()
    {
      __Init();
    }

    public ObservableCollection<DevRegexToCheckItemViewModel> ItemsToCheck
    {
      get => Get<ObservableCollection<DevRegexToCheckItemViewModel>>();
      set => Set(value);
    }

    public void Execute_ImportClipBoardItem()
    {
      if (Clipboard.ContainsText())
      {
        ItemsToCheck = new ObservableCollection<DevRegexToCheckItemViewModel>(Clipboard.GetText().Split("\r")
          .Select(u => u.Trim()).Where(s => s.IsNotNullOrEmpty() && !s.IsEquals("\n"))
          .Distinct().Select(n => __NewVM(n)));
      }
    }

    private DevRegexToCheckItemViewModel __NewVM(string stringToCheck)
    {
      var vm = new DevRegexToCheckItemViewModel();
      vm.TextToCheck = stringToCheck;
      return vm;
    }

    private void __Init()
    {
      ItemsToCheck = new ObservableCollection<DevRegexToCheckItemViewModel>
      {
        new DevRegexToCheckItemViewModel { TextToCheck = "POPO" }
      };
    }
  }
}
