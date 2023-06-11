using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows;

namespace DevTools.Controls
{
	public class DevNumericUpAndDown : NumericUpDown
	{

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			var leftColumn = Template.FindName("PART_LeftColumn", this);
			(leftColumn as ColumnDefinition).Width = GridLength.Auto;
			var numericDownPart = Template.FindName("PART_NumericDown", this);
			Grid.SetColumn(numericDownPart as UIElement, 0);

			var middleColumn= Template.FindName("PART_MiddleColumn", this);
			(middleColumn as ColumnDefinition).Width = new GridLength(0, GridUnitType.Star);
			var numericTextBoxPart = Template.FindName("PART_TextBox", this);
			Grid.SetColumn(numericTextBoxPart as UIElement, 1);

			var rightColumn = Template.FindName("PART_RightColumn", this);
			(rightColumn as ColumnDefinition).Width = GridLength.Auto;
			var numericUpPart = Template.FindName("PART_NumericUp", this);
			Grid.SetColumn(numericUpPart as UIElement, 2);

		}

	}
}
