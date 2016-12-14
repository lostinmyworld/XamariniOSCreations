using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

namespace Mobile.iOS
{
	/// <summary>
	/// Parent controller for UIViewControllers.
	/// Currently includes vanilla code for NSNotification observers (adds and removes them accordingly).
	/// </summary>
	public partial class ParentController : UIViewController
	{
		public ParentController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			RemoveNotificationObservers ();
		}

		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			RemoveNotificationObservers ();
		}

		#region Adds & Remove Notification Observers for child ViewControllers

		bool keepObserverAfterUse;
		List<NSObject> NotificationObjs = new List<NSObject> ();

		/// <summary>
		/// Adds a notification observer.
		/// </summary>
		/// <param name="NotificationId">Notification identifier.</param>
		/// <param name="ActionToExecute">Action to execute.</param>
		protected void AddNotificationObserver (string NotificationId, Action<NSNotification> ActionToExecute, bool keepObserverAfterUse = false)
		{
			this.keepObserverAfterUse = keepObserverAfterUse;
			NotificationObjs.Add (NSNotificationCenter.DefaultCenter.AddObserver ((NSString)NotificationId, ActionToExecute));
		}

		private void RemoveNotificationObservers ()
		{
			if (keepObserverAfterUse)
				return;
			try {
				if (NotificationObjs != null && NotificationObjs.Any ()) {
					foreach (NSObject observer in NotificationObjs) {
						if (observer != null) {
							NSNotificationCenter.DefaultCenter.RemoveObserver (observer);
							NotificationObjs.Remove (observer);
						}
					}
				}
			} catch { }
		}

		#endregion

		#region Set Bar Buttons

		protected void AddMenuButton (UIColor lineColor)
		{
			UIButton navBtn = new UIButton (UIButtonType.Custom);
			navBtn.Frame = new CoreGraphics.CGRect (0f, 0f, 30f, 30f);

			navBtn.SetImage (UIImage.FromBundle ("Images/Menu/menu_bottom_first"), UIControlState.Normal);

			navBtn.TouchUpInside += (sender, e) => Components.Instance.SidebarController.ToggleMenu ();
			UIBarButtonItem navBarBtn = new UIBarButtonItem (navBtn);
			NavigationItem.SetLeftBarButtonItem (navBarBtn, true);
		}

		protected void AddBackButton ()
		{
			UIButton navBtn = new UIButton (UIButtonType.Custom);
			navBtn.Frame = new CoreGraphics.CGRect (0f, 0f, 30f, 30f);

			navBtn.SetImage (UIImage.FromBundle ("Images/Menu/actionbar_back"), UIControlState.Normal);

			navBtn.TouchUpInside += (sender, e) => Components.Instance.NavController.PopViewController (true);
			UIBarButtonItem navBarBtn = new UIBarButtonItem (navBtn);
			NavigationItem.SetLeftBarButtonItem (navBarBtn, true);
		}

		/// <summary>
		/// Adds right bar menu button.
		/// </summary>
		protected void AddRightBarButton (UIImage img, Action action)
		{
			UIButton navBtn = new UIButton (UIButtonType.Custom);
			navBtn.Frame = new CoreGraphics.CGRect (0f, 0f, 30f, 30f);

			navBtn.SetImage (img, UIControlState.Normal);

			navBtn.TouchUpInside += (sender, e) => {
				action ();
			};
			UIBarButtonItem navBarBtn = new UIBarButtonItem (navBtn);
			NavigationItem.SetRightBarButtonItem (navBarBtn, true);
		}

		#endregion
	}
}