using DevTools.Core.BaseClasses;
using DevTools.ViewModels.Converter;
using DevTools.ViewModels.Converter.FileToBase64;

namespace DevTools.ViewModels
{
	public class DevConverterTabViewModel : ViewModelBase
	{
		public DevConverterTabViewModel()
		{
			__InitVms();
		}


		#region JSON/XML
		public string JSON_XMLTabName
		{
			get => Get<string>();
			set => Set(value);
		}

		public DevJson_XmlConverterViewModel JSONXMLConverterViewModel
		{
			get => Get<DevJson_XmlConverterViewModel>();
			set => Set(value);
		}
		#endregion

		#region File/Base64
		public string File_Base64TabName
		{
			get => Get<string>();
			set => Set(value);
		}

		public DevFileToBase64ConverterViewModel File_Base64ConverterViewModel
		{
			get => Get<DevFileToBase64ConverterViewModel>();
			set => Set(value);
		}
		#endregion

		private void __InitVms()
		{
			JSONXMLConverterViewModel = new DevJson_XmlConverterViewModel();
			JSONXMLConverterViewModel.PropertyChanged += __JSONXMLConverterViewModel_PropertyChanged;
			JSON_XMLTabName = "XML => JSON";

			File_Base64ConverterViewModel = new DevFileToBase64ConverterViewModel();
			File_Base64ConverterViewModel.PropertyChanged += __File_Base64ConverterViewModel_PropertyChanged;
			File_Base64TabName = "File => Base64";
		}

		private void __JSONXMLConverterViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(DevJson_XmlConverterViewModel.IsLeftSideXML))
			{
				if (JSONXMLConverterViewModel.IsLeftSideXML)
					JSON_XMLTabName = "XML => JSON";
				else
					JSON_XMLTabName = "JSON => XML";
			}
		}

		private void __File_Base64ConverterViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(DevFileToBase64ConverterViewModel.IsLeftSideFile))
			{
				if (File_Base64ConverterViewModel.IsLeftSideFile)
					File_Base64TabName = "File => Base64";
				else
					File_Base64TabName = "Base64 => File";
			}
		}

	}
}
