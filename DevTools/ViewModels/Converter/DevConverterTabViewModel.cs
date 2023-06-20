using DevTools.Core.BaseClasses;
using DevTools.ViewModels.Converter;

namespace DevTools.ViewModels
{
	public class DevConverterTabViewModel : ViewModelBase
	{
		public DevConverterTabViewModel()
		{
			__InitVms();
		}

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

		private void __InitVms()
		{
			JSONXMLConverterViewModel = new DevJson_XmlConverterViewModel();
			JSONXMLConverterViewModel.PropertyChanged += __JSONXMLConverterViewModel_PropertyChanged;
			JSON_XMLTabName = "XML => JSON";
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

	}
}
