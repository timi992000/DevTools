using DevTools.Core.Attributes;
using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DevTools.ViewModels.Converter.FileToBase64
{
	public class DevFileToBase64ConverterViewModel : ViewModelBase
	{
		public DevFileToBase64ConverterViewModel()
		{
			IsLeftSideFile = true;
			__Init();
		}


		public ObservableCollection<DevFileToBase64ItemViewModel> FileItemsToConvert
		{
			get => Get<ObservableCollection<DevFileToBase64ItemViewModel>>();
			set => Set(value);
		}

		public ObservableCollection<DevBase64ToFileItemViewModel> Base64ItemsToConvert
		{
			get => Get<ObservableCollection<DevBase64ToFileItemViewModel>>();
			set => Set(value);
		}

		[DevDependsUpon(nameof(IsLeftSideFile))]
		public Visibility Base64ButtonsVisibility => IsLeftSideFile ? Visibility.Visible : Visibility.Collapsed;
		[DevDependsUpon(nameof(IsLeftSideFile))]
		public Visibility NotBase64ButtonsVisibility => Base64ButtonsVisibility != Visibility.Visible ? Visibility.Visible : Visibility.Collapsed;

		public bool IsLeftSideFile
		{
			get => Get<bool>();
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

		public void Execute_ClearFileItems()
		{
			try
			{
				FileItemsToConvert.Clear();
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex);
			}
		}

		public void Execute_ClearBase64Items()
		{
			try
			{
				Base64ItemsToConvert.Clear();
				__AddEmptyBase64ToFileItemIfNeeded();
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex);
			}
		}

		public void RemoveFileItem(DevFileToBase64ItemViewModel item)
		{
			FileItemsToConvert.Remove(item);
		}

		public void RemoveBase64Item(DevBase64ToFileItemViewModel item)
		{
			Base64ItemsToConvert.Remove(item);
			__AddEmptyBase64ToFileItemIfNeeded();
		}

		public void Swapped()
		{
			IsLeftSideFile = !IsLeftSideFile;
		}

		private void __InitSelectedFiles(string[] files)
		{
			if (FileItemsToConvert == null)
				FileItemsToConvert = new ObservableCollection<DevFileToBase64ItemViewModel>();
			Parallel.ForEach(files, file =>
			{
				var vm = __NewFileToBase64VM(file);
				Application.Current.Dispatcher.BeginInvoke(() => { FileItemsToConvert.Add(vm); });
			});
		}

		private DevFileToBase64ItemViewModel __NewFileToBase64VM(string fileLocation)
		{
			var vm = new DevFileToBase64ItemViewModel(fileLocation, this);
			//Attach events and so on
			return vm;
		}

		private DevBase64ToFileItemViewModel __NewBase64ToFileVM(string encodedBase64String)
		{
			var vm = new DevBase64ToFileItemViewModel(encodedBase64String, this);
			vm.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == nameof(DevBase64ToFileItemViewModel.EncodedBase64String))
					__AddEmptyBase64ToFileItemIfNeeded();
			};
			return vm;
		}

		private void __Init()
		{
			FileItemsToConvert = new ObservableCollection<DevFileToBase64ItemViewModel>();
			Base64ItemsToConvert = new ObservableCollection<DevBase64ToFileItemViewModel>();
			__AddEmptyBase64ToFileItemIfNeeded();
		}


		private void __AddEmptyBase64ToFileItemIfNeeded()
		{
			try
			{
				if (Base64ItemsToConvert.Count == 0 || Base64ItemsToConvert.All(i => i.EncodedBase64String.IsNotNullOrEmpty()))
				{
					Base64ItemsToConvert.Add(__NewBase64ToFileVM(""));
				}
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex);
			}
		}

		private Dictionary<string, string> _FileExtensions = new Dictionary<string, string>();
		internal void __AddFileExtension(DevFileToBase64ItemViewModel vm)
		{
			if (vm.Converted.IsNullOrEmpty())
				return;
			var data = vm.Converted.Substring(0, 5).ToUpper();
			var tryGetFileExtension = vm.Converted.GetFileExtensionFromBase64String();

			if (!_FileExtensions.ContainsKey(data))
			{
				if (tryGetFileExtension.IsNotNullOrEmpty())
					_FileExtensions[data] = tryGetFileExtension;
				else
					_FileExtensions[data] = Path.GetExtension(vm.FileLocation).Replace(".", "");
			}
		}
	}
}
