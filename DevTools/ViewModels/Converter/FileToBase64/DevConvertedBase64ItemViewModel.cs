using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using QRCoder;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DevTools.ViewModels.Converter.FileToBase64
{
	public class DevConvertedBase64ItemViewModel : ViewModelBase
	{
		private readonly DevFileToBase64ConverterViewModel _ParentVM;
		public DevConvertedBase64ItemViewModel(string fileLocation, DevFileToBase64ConverterViewModel devFileToBase64ConverterViewModel)
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
			_ParentVM.RemoveItem(this);
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
					Converted = Convert.ToBase64String(bytes);
				}
				catch (Exception ex)
				{
					ShowErrorMessage(ex);
				}
			});
		}
	}
}
