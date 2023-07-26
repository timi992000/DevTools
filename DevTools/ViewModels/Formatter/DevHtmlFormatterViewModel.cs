using AngleSharp.Html;
using AngleSharp.Html.Parser;
using DevTools.Core.Extender;
using System;
using System.IO;

namespace DevTools.ViewModels.Formatter
{
    public class DevHtmlFormatterViewModel : DevFormatterViewModelBase
    {
        public override void FormatText()
        {
            try
            {
                var sw = new StringWriter();
                new HtmlParser().ParseDocument(TextToFormat).ToHtml(sw, new PrettyMarkupFormatter());
                ConvertedText = sw.ToString();
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
