using DevTools.Core.Commands;
using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevTools.Controls
{
	/// <summary>
	/// Interaction logic for SwappableContentPanel.xaml
	/// </summary>
	public partial class SwappableContentPanel : UserControl
	{
		private RelayCommand _SwapCommand;
		private eSwapMode _SwapMode;
		public event EventHandler Swapped;
		public SwappableContentPanel()
		{
			InitializeComponent();
		}


		#region ContentPanel1
		public static object GetContentPanel1(DependencyObject obj)
		{
			return (object)obj.GetValue(ContentPanel1Property);
		}

		public static void SetContentPanel1(DependencyObject obj, object value)
		{
			obj.SetValue(ContentPanel1Property, value);
		}

		// Using a DependencyProperty as the backing store for ContentPanel1.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ContentPanel1Property =
				DependencyProperty.RegisterAttached("ContentPanel1", typeof(object), typeof(SwappableContentPanel), new PropertyMetadata(null, __ContentPanel1Changed));

		private static void __ContentPanel1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{

		}
		#endregion

		#region ContentPanel2
		public static object GetContentPanel2(DependencyObject obj)
		{
			return (object)obj.GetValue(ContentPanel2Property);
		}

		public static void SetContentPanel2(DependencyObject obj, object value)
		{
			obj.SetValue(ContentPanel2Property, value);
		}

		// Using a DependencyProperty as the backing store for ContentPanel2.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ContentPanel2Property =
				DependencyProperty.RegisterAttached("ContentPanel2", typeof(object), typeof(SwappableContentPanel), new PropertyMetadata(null, __ContentPanel2Changed));

		private static void __ContentPanel2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{

		}
		#endregion

		#region SwapMode
		public static eSwapMode GetSwapMode(DependencyObject obj)
		{
			return (eSwapMode)obj.GetValue(SwapModeProperty);
		}

		public static void SetSwapMode(DependencyObject obj, eSwapMode value)
		{
			obj.SetValue(SwapModeProperty, value);
		}

		// Using a DependencyProperty as the backing store for SwapMode.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SwapModeProperty =
				DependencyProperty.Register("SwapMode", typeof(eSwapMode), typeof(SwappableContentPanel), new PropertyMetadata(eSwapMode.Horizontal, __SwapModeChanged));

		public RelayCommand SwapCommand
		{
			get
			{
				if (_SwapCommand == null)
				{
					_SwapCommand = new RelayCommand(p => __SwapPanels());
				}
				return _SwapCommand;
			}
		}

		private static void __SwapModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{

		}
		#endregion

		private void __SwapPanels()
		{
			if (GetSwapMode(this as SwappableContentPanel) == eSwapMode.Horizontal)
			{
				var leftSide = this.FindChild<Border>("ContentBorderHorizontalLeft");
				var rightSide = this.FindChild<Border>("ContentBorderHorizontalRight");

				if (leftSide != null && rightSide != null)
				{
					var leftColumn = Grid.GetColumn(leftSide);
					if (leftColumn == 0)
					{
						Grid.SetColumn(leftSide, 2);
						Grid.SetColumn(rightSide, 0);
					}
					else
					{
						Grid.SetColumn(leftSide, 0);
						Grid.SetColumn(rightSide, 2);
					}
				}
			}
			else
			{
				var topSide = this.FindChild<Border>("ContentBorderVerticalTop");
				var bottomSide = this.FindChild<Border>("ContentBorderVerticalBottom");

				if (topSide != null && bottomSide != null)
				{
					var leftColumn = Grid.GetRow(topSide);
					if (leftColumn == 0)
					{
						Grid.SetRow(topSide, 2);
						Grid.SetRow(bottomSide, 0);
					}
					else
					{
						Grid.SetRow(topSide, 0);
						Grid.SetRow(bottomSide, 2);
					}
				}
			}
			Swapped?.Invoke(this, EventArgs.Empty);
		}
	}

	public enum eSwapMode
	{
		Horizontal,
		Vertical,
	}

}
