using System;
using UIKit;

namespace Mobile.iOS
{
	public sealed class InfoMessage
	{
		/// <summary>
		/// Shows a simple message.
		/// </summary>
		/// <param name="Title">Title.</param>
		/// <param name="Description">Description.</param>
		/// <param name="controller">Controller to show the info message.</param>
		public static void SimpleMessage (string Title, string Description, UIViewController controller)
		{

			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var okAlertController = UIAlertController.Create (Title, Description, UIAlertControllerStyle.Alert);
				okAlertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
				controller.PresentViewController (okAlertController, true, null);

			} else {
				new UIAlertView (Title, Description, null, "OK", null).Show ();
			}
		}

		public static void SimpleMessage (string Title, string Description, UIViewController controller, Action action)
		{

			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var okAlertController = UIAlertController.Create (Title, Description, UIAlertControllerStyle.Alert);
				okAlertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default,
					alert => {
						action ();
					}));
				controller.PresentViewController (okAlertController, true, null);

			} else {
				UIAlertView alert = new UIAlertView {
					Title = Title,
					Message = Description
				};
				alert.AddButton ("OK");
				alert.Show ();
				alert.Clicked += (s, ev) => {
					if (ev.ButtonIndex == 0) {
						action ();
					}
				};
			}
		}

		/// <summary>
		/// Shows a simple error message with title ('Error').
		/// </summary>
		/// <param name="Description">Description.</param>
		/// <param name="controller">Controller to show the info message.</param>
		public static void SimpleErrorMessage (string Description, UIViewController controller)
		{
			string Title = Localize.GetString ("error_title");
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var okAlertController = UIAlertController.Create (Title, Description, UIAlertControllerStyle.Alert);
				okAlertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
				controller.PresentViewController (okAlertController, true, null);

			} else {
				new UIAlertView (Title, Description, null, "OK", null).Show ();
			}
		}

		public static void SimpleErrorMessage (string Description, UIViewController controller, Action action)
		{
			string Title = Localize.GetString ("error_title");
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var okAlertController = UIAlertController.Create (Title, Description, UIAlertControllerStyle.Alert);
				okAlertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default,
				   	alert => {
						   action ();
					   }));
				controller.PresentViewController (okAlertController, true, null);

			} else {
				UIAlertView alert = new UIAlertView {
					Title = Title,
					Message = Description
				};
				alert.AddButton ("OK");
				alert.Show ();
				alert.Clicked += (s, ev) => {
					if (ev.ButtonIndex == 0) {
						action ();
					}
				};
			}
		}

		/// <summary>
		/// Shows OK/Cancel message. An action to be executed on OK must be passed.
		/// </summary>
		/// <param name="Title">Title.</param>
		/// <param name="Description">Description.</param>
		/// <param name="OkAction">Action that will be executed when OK is pressed.</param>
		/// <param name="controller">Controller to show the info message.</param>
		public static void OKCancelView (string Title, string Description, Action OkAction, UIViewController controller)
		{

			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var okAlertController = UIAlertController.Create (Title, Description,
											UIAlertControllerStyle.Alert);
				okAlertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default,
					alert => {
						OkAction ();
					}));
				okAlertController.AddAction (UIAlertAction.Create (Localize.GetString ("cancel"), UIAlertActionStyle.Cancel, null));
				controller.PresentViewController (okAlertController, true, null);
			} else {
				UIAlertView alert = new UIAlertView {
					Title = Title,
					Message = Description
				};
				alert.AddButton ("OK");
				alert.AddButton (Localize.GetString ("cancel"));
				alert.Show ();
				alert.Clicked += (s, ev) => {
					if (ev.ButtonIndex == 0) {
						OkAction ();
					}
				};
			}
		}

		/// <summary>
		/// Shows a 2-button message. The actions to be executed on each button must be passed.
		/// </summary>
		/// <param name="Title">Title.</param>
		/// <param name="Description">Description.</param>
		/// <param name="stringForActionOne">String for button one.</param>
		/// <param name="actionOne">Action to be executed when button one is pressed.</param>
		/// <param name="stringForActionTwo">String for button two.</param>
		/// <param name="actionTwo">Action to be executed when button two is pressed.</param>
		/// <param name="controller">Controller to show the info message.</param>
		public static void TwoChoiceView (string Title, string Description, string stringForActionOne, Action actionOne, string stringForActionTwo, Action actionTwo, UIViewController controller)
		{

			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var TwoChoiceAlert = UIAlertController.Create (Title, Description,
											UIAlertControllerStyle.Alert);
				TwoChoiceAlert.AddAction (UIAlertAction.Create (stringForActionOne, UIAlertActionStyle.Default,
					alert => {
						actionOne ();
					}));
				TwoChoiceAlert.AddAction (UIAlertAction.Create (stringForActionTwo, UIAlertActionStyle.Default,
					alert => {
						actionTwo ();
					}));
				controller.PresentViewController (TwoChoiceAlert, true, null);
			} else {
				UIAlertView alert = new UIAlertView {
					Title = Title,
					Message = Description
				};
				alert.AddButton (stringForActionOne);
				alert.AddButton (stringForActionTwo);
				alert.Show ();
				alert.Clicked += (s, ev) => {
					if (ev.ButtonIndex == 0) {
						actionOne ();
					} else if (ev.ButtonIndex == 1) {
						actionTwo ();
					}
				};
			}
		}

	}
}

