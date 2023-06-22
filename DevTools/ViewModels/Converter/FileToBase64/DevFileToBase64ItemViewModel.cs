using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace DevTools.ViewModels.Converter.FileToBase64
{
	public class DevFileToBase64ItemViewModel : ViewModelBase
	{
		private readonly DevFileToBase64ConverterViewModel _ParentVM;
		public DevFileToBase64ItemViewModel(string fileLocation, DevFileToBase64ConverterViewModel devFileToBase64ConverterViewModel)
		{
			FileLocation = fileLocation;
			_ParentVM = devFileToBase64ConverterViewModel;
			__Convert();
		}

		public string FileLocation
		{
			get => Get<string>();
			set => Set(value);
		}

		public string Converted
		{
			get => Get<string>();
			set => Set(value);
		}

		public void Execute_Remove()
		{
			_ParentVM.RemoveFileItem(this);
		}

		public void Execute_Copy()
		{
			if (Converted.IsNotNullOrEmpty())
				Clipboard.SetText(Converted);
		}

		private void __Convert()
		{
			var fakt = new TaskFactory();
			fakt.StartNew(() =>
			{
				try
				{
					if (!File.Exists(FileLocation))
						return;
					byte[] bytes = File.ReadAllBytes(FileLocation);
					Application.Current.Dispatcher.Invoke(() => {
						Converted = Convert.ToBase64String(bytes);
						_ParentVM.__AddFileExtension(this);
					});
				}
				catch (Exception ex)
				{
					//ShowErrorMessage(ex);
				}
			});
		}
	}
}
