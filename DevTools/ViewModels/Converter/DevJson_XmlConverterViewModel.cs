using DevTools.Core.Attributes;
using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml;

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
				if (XMLText.IsNullOrEmpty())
				{
					ExecuteWithoutDependendObjects(() => { JSONText = string.Empty; });
					return;
				}
				var doc = new XmlDocument();
				doc.LoadXml(XMLText);
				var jsonString = JsonConvert.SerializeXmlNode(doc);
				var formatJson = true;
				if (formatJson)
					jsonString = __FormatJson(jsonString);
				ExecuteWithoutDependendObjects(() => { JSONText = jsonString; });
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
				if (JSONText.IsNullOrEmpty())
				{
					ExecuteWithoutDependendObjects(() => { XMLText = string.Empty; });
					return;
				}
				var doc = JsonConvert.DeserializeXNode(JSONText);
				if (doc == null)
					return;
				ExecuteWithoutDependendObjects(() => { XMLText = doc.ToString(); });
			}
			catch (Exception ex)
			{
				ExecuteWithoutDependendObjects(() => { XMLText = ex.Message ?? ex.ToStringValue(); });
			}
		}

		private string __FormatJson(string jsonString)
		{
			try
			{
				using (var stringReader = new StringReader(jsonString))
				using (var stringWriter = new StringWriter())
				{
					var jsonReader = new JsonTextReader(stringReader);
					var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Newtonsoft.Json.Formatting.Indented };
					jsonWriter.WriteToken(jsonReader);
					return stringWriter.ToString();
				}
			}
			catch (Exception)
			{
				return "";
			}
		}

	}
}
