using DevTools.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.ViewModels.Checks.Regex
{
  public class DevRegexToCheckItemViewModel : ViewModelBase
  {
    public DevRegexToCheckItemViewModel()
    {
    }

    public string TextToCheck
    {
      get => Get<string>();
      set => Set(value);
    }

    public bool IsRegexMatch
    {
      get =>  Get<bool>();
      set => Set(value);
    }

    public void Execute_Remove()
    {

    }


  }
}
