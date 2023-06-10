using DevTools.Core.BaseClasses;
using System.Diagnostics;
using System.Windows;
using System;
using System.IO;
using MahApps.Metro.Controls.Dialogs;
using DevTools.Core.Extender;
using System.ComponentModel;

namespace DevTools.ViewModels.Preview
{
	public class DevRtfPreviewViewModel : DevPreviewViewModelBase
	{
		public DevRtfPreviewViewModel()
			: base("rtf", @"{\rtf")
		{
		}

	}
}
