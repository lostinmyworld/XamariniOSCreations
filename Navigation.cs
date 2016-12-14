using System;
using UIKit;

namespace Mobile.iOS
{
	public class Navigation
	{
		private static Navigation instance;

		private Navigation () { }

		public static Navigation Instance {
			get {
				if (instance == null) {
					instance = new Navigation ();
				}
				return instance;
			}
		}

		public bool pushToViewStack<T> (string viewControllerStr) where T : UIViewController
		{

			bool success = false;
			var contr = (T)Components.Instance.Storyboard.InstantiateViewController (viewControllerStr);
			var controller = Components.Instance.NavController;

			if (controller.TopViewController as T == null) {
				controller.PushViewController (contr, true);
				success = true;
			}

			return success;

		}

		public bool PresentController<T> (string viewControllerStr) where T : UIViewController
		{

			bool success = false;
			var contr = (T)Components.Instance.Storyboard.InstantiateViewController (viewControllerStr);
			var controller = Components.Instance.NavController;

			if (controller.TopViewController as T == null) {
				controller.PresentViewController (contr, true, null);
				success = true;
			}

			return success;

		}

		public bool PresentModalController<T> (string viewControllerStr) where T : UIViewController
		{

			bool success = false;
			var contr = (T)Components.Instance.Storyboard.InstantiateViewController (viewControllerStr);
			contr.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
			var controller = Components.Instance.NavController;

			if (controller.TopViewController as T == null) {
				controller.PresentViewController (contr, true, null);
				success = true;
			}

			return success;

		}

		public void InitComponents ()
		{

			var controller = (RootController)Components.Instance.Storyboard.InstantiateViewController ("RootController");
			if (AppDelegate.window != null) {
				AppDelegate.window.RootViewController = controller;
				controller.DismissViewController (true, null);
			}

		}

	}
}

