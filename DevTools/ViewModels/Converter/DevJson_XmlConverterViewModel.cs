using DevTools.Core.Attributes;
using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using System;

namespace DevTools.ViewModels.Converter
{
	public class DevJson_XmlConverterViewModel : ViewModelBase
	{
		public DevJson_XmlConverterViewModel()
		{
			IsLeftSideXML = true;
		}

		public bool IsLeftSideXML
		{
			get => Get<bool>();
			set => Set(value);
		}

		#region [XML]
		public string XMLText
		{
			get => Get<string>();
			set => Set(value);
		}

		[DevDependsUpon(nameof(IsLeftSideXML))]
		public bool IsXMLReadOnly => !IsLeftSideXML;

		#endregion

		#region [JSON]
		public string JSONText
		{
			get => Get<string>();
			set => Set(value);
		}

		[DevDependsUpon(nameof(IsLeftSideXML))]
		public bool IsJSONReadOnly => IsLeftSideXML;

		#endregion

		public void Swapped()
		{
			IsLeftSideXML = !IsLeftSideXML;
			ExecuteWithoutDependendObjects(() => { JSONText = string.Empty; });
			ExecuteWithoutDependendObjects(() => { XMLText = string.Empty; });
		}

		[DevDependsUpon(nameof(XMLText))]
		public void XMLToJSON()
		{
			try
			{
				ExecuteWithoutDependendObjects(() => { JSONText = XMLText.XMLToJsonFormattedString(); });
			}
			catch (Exception ex)
			{
				ExecuteWithoutDependendObjects(() => { JSONText = ex.Message ?? ex.ToStringValue(); });
			}
		}


		[DevDependsUpon(nameof(JSONText))]
		public void JSONToXML()
		{
			try
			{
				ExecuteWithoutDependendObjects(() => { XMLText = JSONText.JsonToXMLFormattedString(); });
			}
			catch (Exception ex)
			{
				ExecuteWithoutDependendObjects(() => { XMLText = ex.Message ?? ex.ToStringValue(); });
			}
		}



	}
}
