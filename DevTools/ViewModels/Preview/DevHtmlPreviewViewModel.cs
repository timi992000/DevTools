using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.ViewModels.Preview
{
	public class DevHtmlPreviewViewModel : DevPreviewViewModelBase
	{
		public DevHtmlPreviewViewModel() 
			: base("html", "", true)
		{
		}

		public WebView2 BrowserControl
		{
			get => Get<WebView2>();
			set => Set(value);
		}

		public async void NavigateToString(string htmlContent)
		{
			if (BrowserControl == null)
			{
				Execute_Open();
				return;
			}
			await BrowserControl.EnsureCoreWebView2Async();
			BrowserControl.NavigateToString(htmlContent);
		}

	}
}
