using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Xml;

namespace DevTools.Core.Extender
{
	public static class StringExtender
	{
		public static string ToStringValue(this object rawValue)
		{
			if (rawValue == null)
				return string.Empty;
			else if (rawValue is string)
				return (string)rawValue;
			else
				return rawValue.ToString();
		}

		public static bool IsNotNullOrEmpty(this string value)
				=> !string.IsNullOrEmpty(value);
		public static bool IsNullOrEmpty(this string value)
				=> string.IsNullOrEmpty(value);

		public static byte[] ToByteArray(this string value)
				=> Encoding.UTF8.GetBytes(value);

		public static string ToUTF8String(this string value)
				=> Encoding.Unicode.GetString(Encoding.UTF8.GetBytes(value));

		public static bool IsEquals(this string value, string valueToCompare)
		{
			if (value == null && valueToCompare == null)
				return true;
			if ((value == null && valueToCompare != null) || value != null && valueToCompare == null)
				return false;

			return value.Equals(valueToCompare, StringComparison.InvariantCultureIgnoreCase);
		}

		public static string JsonToXMLFormattedString(this string jsonString)
		{
			try
			{
				if (jsonString.IsNullOrEmpty())
					return string.Empty;
				var doc = JsonConvert.DeserializeXNode(jsonString).ToString();
				if (doc == null)
					return string.Empty;
				jsonString = doc.ToString();
				return jsonString;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		public static string XMLToJsonFormattedString(this string xmlString)
		{
			try
			{
				if (xmlString.IsNullOrEmpty())
					return string.Empty;
				var doc = new XmlDocument();
				doc.LoadXml(xmlString);
				var jsonString = JsonConvert.SerializeXmlNode(doc);
				var formatJson = true;
				if (formatJson)
					jsonString = __FormatJson(jsonString);
				return jsonString;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static string GetRtfUnicodeEscapedString(this string value)
		{
			var sb = new StringBuilder();
			foreach (var c in value)
			{
				if (c == '\\' || c == '{' || c == '}')
					sb.Append(@"\" + c);
				else if (c <= 0x7f)
					sb.Append(c);
				else
					sb.Append("\\u" + Convert.ToUInt32(c) + "?");
			}
			return sb.ToString();
		}

		private static string __FormatJson(string jsonString)
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

		public static string GetFileExtensionFromBase64String(this string base64String)
		{
			var data = base64String.Substring(0, 5);

			switch (data.ToUpper())
			{
				case "IVBOR":
					return "png";
				case "/9J/4":
					return "jpg";
				case "AAAAF":
				case "AAAAG":
					return "mp4";
				case "JVBER":
					return "pdf";
				case "AAABA":
					return "ico";
				case "UMFYI":
					return "rar";
				case "E1XYD":
					return "rtf";
				case "MQOWM":
				case "77U/M":
					return "srt";
				case "UESDB":
					return "zip";
				case "0M8R4":
					return "msi";
				case "TVQQA":
				case "TVPQA":
					return "exe";
				case "C2VWP":
					return "csv";
				case "TELTL":
				case "U1PKC":
				case "QNVKZ":
				case "R3JVD":
				case "U29TB":
					return "txt";
				case "PGRPD":
					return "";
				case "77U/D":
					return "cs";
				case "UKLGR":
					return "wav";
				case "QKVHS":
					return "ics";
				case "A9MIM":
					return "kdbx";
				case "U1FMA":
					return "db";
				case "PFVZZ":
					return "vssettings";
				case "LY8GR":
					return "eos";
				case "TAAAA":
					return "lnk";
				case "Y2QGQ":
				case "QGVJA":
				case "DQONC":
					return "bat";
				case "ZM9YI":
					return "cmd";
				case "GKXFO":
					return "mkv";
				case "CMVKA":
					return "rdp";
				default:
					return string.Empty;
			}
		}


	}
}
