using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

namespace Mobile.iOS
{
	/// <summary>
	/// Parent controller for UITableViewControllers.
	/// Currently includes vanilla code for NSNotification observers (adds and removes them accordingly).
	/// </summary>
	public partial class ParentTableController : UITableViewController
	{
		public ParentTableController (IntPtr handle) : base (handle)
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

		#region Adds & Remove Notification Observers for child UITableViewControllers

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
	}
}