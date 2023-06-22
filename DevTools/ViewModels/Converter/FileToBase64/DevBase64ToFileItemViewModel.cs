using ControlzEx.Theming;
using DevTools.Core.Attributes;
using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Media;

namespace DevTools.ViewModels.Converter.FileToBase64
{
	public class DevBase64ToFileItemViewModel : ViewModelBase
	{
		private readonly DevFileToBase64ConverterViewModel _ParentVM;
		private byte[] _Bytes;
		public DevBase64ToFileItemViewModel(string encodedBase64String, DevFileToBase64ConverterViewModel devFileToBase64ConverterViewModel)
		{
			EncodedBase64String = encodedBase64String;
			_ParentVM = devFileToBase64ConverterViewModel;
			ThemeManager.Current.ThemeChanged += (sender, e) => { OnPropertyChanged(nameof(ButtonSaveAsFill)); };
		}

		public Brush ButtonSaveAsFill => MainWindowViewModel.IsDarkMode ? Brushes.White : Brushes.Black;

		public string EncodedBase64String
		{
			get => Get<string>();
			set => Set(value);
		}

		public string FileName
		{
			get => Get<string>();
			set => Set(value);
		}

		public void Execute_Remove()
		{
			_ParentVM.RemoveBase64Item(this);
		}

		public bool CanExecute_SaveFileAs()
		{
			return _Bytes != null && _Bytes.Length > 0;
		}
		public void Execute_SaveFileAs()
		{
			try
			{
				if (_Bytes == null || _Bytes.Length == 0)
					return;
				var dlg = new SaveFileDialog();
				dlg.FileName = FileName;
				if (dlg.ShowDialog() == true)
				{
					FileName = dlg.FileName;
					File.WriteAllBytes(FileName, _Bytes);
				}
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex);
			}
		}

		[DevDependsUpon(nameof(EncodedBase64String))]
		private void __Convert()
		{
			try
			{
				if (EncodedBase64String.IsNullOrEmpty())
				{
					__ClearCurrentBase64ToFile();
					return;
				}
				var ext = EncodedBase64String.GetFileExtensionFromBase64String();
				_Bytes = Convert.FromBase64String(EncodedBase64String);
				FileName = $"Your{ext}File.{ext}";
				OnCanExecuteChanged(nameof(Execute_SaveFileAs));
			}
			catch (Exception ex)
			{
				__ClearCurrentBase64ToFile();
			}
		}

		private void __ClearCurrentBase64ToFile()
		{
			FileName = "";
			_Bytes = null;
			OnCanExecuteChanged(nameof(Execute_SaveFileAs));
		}
	}
}
