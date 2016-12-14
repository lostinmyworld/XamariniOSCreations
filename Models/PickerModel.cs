using System;
using System.Collections.Generic;
using UIKit;
namespace Mobile.iOS
{
	public class PickerModel : UIPickerViewModel
	{
		public List<string> PickerValues;
		int selectedIndex = 0;
		public event EventHandler<EventArgs> ValueChanged;

		public string GetSelectedItem ()
		{
			return PickerValues [selectedIndex];
		}

		public PickerModel (List<string> actions)
		{
			PickerValues = actions;
		}

		public override nint GetComponentCount (UIPickerView pickerView)
		{
			return 1;
		}

		public override nint GetRowsInComponent (UIPickerView pickerView, nint component)
		{
			return PickerValues.Count;
		}

		public override string GetTitle (UIPickerView pickerView, nint row, nint component)
		{
			return PickerValues [(int)row];
		}

		public override nfloat GetRowHeight (UIPickerView pickerView, nint component)
		{
			return 40f;
		}

		public override UIView GetView (UIPickerView pickerView, nint row, nint component, UIView view)
		{
			UILabel label = new UILabel ();
			label.Text = PickerValues [(int)row];
			label.AdjustsFontSizeToFitWidth = true;

			return label;
		}

		public override void Selected (UIPickerView pickerView, nint row, nint component)
		{
			selectedIndex = (int)row;
			if (ValueChanged != null) {
				ValueChanged (this, new EventArgs ());
			}
		}
	}
}

