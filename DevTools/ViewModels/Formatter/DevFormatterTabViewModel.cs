using AngleSharp.Html;
using AngleSharp.Html.Parser;
using ControlzEx.Theming;
using DevTools.Core.Attributes;
using DevTools.Core.BaseClasses;
using DevTools.Core.Extender;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Media;
using System.Xml.Linq;

namespace DevTools.ViewModels.Formatter
{
    public class DevFormatterTabViewModel : ViewModelBase
    {
        public DevFormatterTabViewModel()
        {
            ThemeManager.Current.ThemeChanged +=
                (sender, e) =>
                {
                    OnPropertyChanged(nameof(Foreground));
                };
        }

        public string TextToFormat
        {
            get => Get<string>();
            set => Set(value);
        }

        public string ConvertedText
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool HasError
        {
            get => Get<bool>();
            set => Set(value);
        }

        public eFormatType FormatType
        {
            get => Get<eFormatType>();
            set => Set(value);
        }

        public Array FormatTypes => Enum.GetValues(typeof(eFormatType));

        [DevDependsUpon(nameof(HasError))]
        public Brush Foreground
        {
            get
            {
                if (HasError)
                    return Brushes.Red;
                return MainWindowViewModel.IsDarkMode ? Brushes.White : Brushes.Black;
            }
        }

        [DevDependsUpon(nameof(TextToFormat))]
        [DevDependsUpon(nameof(FormatType))]
        public void FormatText()
        {
            try
            {
                HasError = false;
                ConvertedText = __FormatTextByType();
            }
            catch (System.Exception ex)
            {
                HasError = false;
                HasError = true;
                ConvertedText = (ex.InnerException ?? ex).ToStringValue();
            }
        }

        private string __FormatTextByType()
        {
            switch (FormatType)
            {
                case eFormatType.HTML:
                    var sw = new StringWriter();
                    new HtmlParser().ParseDocument(TextToFormat).ToHtml(sw, new PrettyMarkupFormatter());
                    return sw.ToString();
                case eFormatType.XML:
                    return TextToFormat.IsNullOrEmpty() ? string.Empty : XDocument.Parse(TextToFormat).ToString();
                case eFormatType.JSON:
                    if (TextToFormat.IsNullOrEmpty())
                        return string.Empty;
                    else
                    {
                        using (var stringReader = new StringReader(TextToFormat))
                        using (var stringWriter = new StringWriter())
                        {
                            var jsonReader = new JsonTextReader(stringReader);
                            var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                            jsonWriter.WriteToken(jsonReader);
                            return stringWriter.ToString();
                        }
                    }
            }
            return "";
        }
    }

    public enum eFormatType
    {
        HTML,
        XML,
        JSON,
        //RTF,
    }

}
