using DevTools.Core.BaseClasses;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DevTools.ViewModels.Converter.FileToBase64
{
  public class DevFileToBase64ConverterViewModel : ViewModelBase
  {
    public DevFileToBase64ConverterViewModel()
    {
      __Init();
    }


    public ObservableCollection<DevConvertedBase64ItemViewModel> ItemsToConvert
    {
      get => Get<ObservableCollection<DevConvertedBase64ItemViewModel>>();
      set => Set(value);
    }

    public void Execute_SelectFiles()
    {
      try
      {
        var dlg = new OpenFileDialog();
        dlg.Multiselect = true;
        if (dlg.ShowDialog() == true)
        {
          var files = dlg.FileNames.Distinct().ToArray();
          __InitSelectedFiles(files);
        }
      }
      catch (Exception ex)
      {
        ShowErrorMessage(ex);
      }
    }

    public void Execute_Clear()
    {
      try
      {
        ItemsToConvert.Clear();
      }
      catch (Exception ex)
      {
        ShowErrorMessage(ex);
      }
    }

    public void RemoveItem(DevConvertedBase64ItemViewModel item)
    {
      ItemsToConvert.Remove(item);
    }

    private void __InitSelectedFiles(string[] files)
    {
      if (ItemsToConvert == null)
        ItemsToConvert = new ObservableCollection<DevConvertedBase64ItemViewModel>();
      Parallel.ForEach(files, file =>
      {
        var vm = __NewVM(file);
        Application.Current.Dispatcher.BeginInvoke(() => { ItemsToConvert.Add(vm); });
      });
    }

    private DevConvertedBase64ItemViewModel __NewVM(string fileLocation)
    {
      var vm = new DevConvertedBase64ItemViewModel(fileLocation, this);
      //Attach events and so on
      return vm;
    }

    private void __Init()
    {
      ItemsToConvert = new ObservableCollection<DevConvertedBase64ItemViewModel>();
    }

  }
}
