using DevTools.Core.Extender;
using System;
using System.Xml.Linq;

namespace DevTools.ViewModels.Formatter
{
	public class DevXMLFormatterViewModel : DevFormatterViewModelBase
	{
		public override void FormatText()
		{
			try
			{
				if (TextToFormat.IsNullOrEmpty())
					ConvertedText = string.Empty;
				else
				{
					XDocument doc = XDocument.Parse(TextToFormat);
					ConvertedText = doc.ToString();
				}
				HasError = false;
			}
			catch (Exception ex)
			{
				HasError = false;
				HasError = true;
				ConvertedText = (ex.InnerException ?? ex).ToStringValue();
			}
		}
	}
}
