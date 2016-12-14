using System;
using System.IO;
using System.Net;
using Plugin.Connectivity;
using CoreGraphics;
using UIKit;
using CoreAnimation;
using Foundation;
using System.Drawing;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Mobile.iOS
{
	public class IOS_Methods
	{
		/// <summary>
		/// Returns 'true' if device is connected to a network.
		/// </summary>
		/// <returns><c>true</c>, if online was ised, <c>false</c> otherwise.</returns>
		public static bool IsOnline ()
		{
			return CrossConnectivity.Current.IsConnected;
		}

		#region TextField validation

		public static bool IsTextFieldEmpty (UITextField textfield, string errorMessage, UIViewController viewController)
		{
			if (textfield == null)
				return true;
			if (!string.IsNullOrEmpty (textfield.Text))
				return false;

			if (!string.IsNullOrEmpty (errorMessage))
				InfoMessage.SimpleMessage (Localize.GetString ("error_title"), errorMessage, viewController);
			return true;
		}

		#endregion

		#region Block UI if loading

		static LoadingOverlay loadingOverlay;
		public static void BlockUI (UIView view, string msgToShow = "")
		{
			if (loadingOverlay != null)
				return;
			loadingOverlay = new LoadingOverlay (view.Bounds, msgToShow);
			view.Add (loadingOverlay);
		}

		public static void UnBlockUI ()
		{
			if (loadingOverlay != null) {
				loadingOverlay.Hide ();
				loadingOverlay = null;
			}
		}

		#endregion

		#region Check if URL is reachable
		/// <summary>
		/// Returns true if URL returns 200 (HttpStatus.Ok) and false for every other code OR no Internet connection
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static bool IsUrlReachable (string url)
		{
			if (!IsOnline ()) {
				return false;
			}
			try {
				HttpWebRequest request = WebRequest.Create (url) as HttpWebRequest;
				request.Method = "GET";
				request.AllowAutoRedirect = true;
				HttpWebResponse response = request.GetResponse () as HttpWebResponse;
				response.Close ();
				return response.StatusCode == HttpStatusCode.OK;
			} catch {
				return false;
			}
		}

		public static bool IsEmailValid (string mail)
		{
			return !string.IsNullOrWhiteSpace (mail) && Regex.Match (mail, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success;
		}

		#endregion

		#region Set/Remove Indicator on View
		private static UIActivityIndicatorView circleIndicator;
		public static void SetIndicator (UIView view, CGRect bounds)
		{
			if (circleIndicator != null)
				return;
			circleIndicator = new UIActivityIndicatorView ();
			circleIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray;
			circleIndicator.Frame = bounds;
			view.AddSubview (circleIndicator);
			circleIndicator.StartAnimating ();
		}

		public static void RemoveIndicator ()
		{
			if (circleIndicator != null) {
				circleIndicator.RemoveFromSuperview ();
			}
		}
		#endregion

		#region Borders and Views

		public static void SetBorderForView (UIView view, UIColor color)
		{
			if (view == null)
				return;
			view.Layer.BorderColor = color.CGColor;
			view.Layer.BorderWidth = 2f;
		}

		public static void SetBorderForView (UIView view, UIColor color, float borderWidth)
		{
			if (view == null)
				return;
			view.Layer.BorderColor = color.CGColor;
			view.Layer.BorderWidth = borderWidth;
		}

		public static void SetClearBorder (UIView view)
		{
			if (view == null)
				return;
			view.Layer.BorderColor = UIColor.Clear.CGColor;
			view.Layer.BorderWidth = 0f;
		}

		public static void SetBottomBorder (UIView view, UIColor color)
		{
			CALayer bottom = new CALayer () {
				Frame = new CGRect (0f, view.Bounds.Height, view.Bounds.Width, 1f),
				BackgroundColor = color.CGColor
			};
			view.Layer.AddSublayer (bottom);
		}

		public static void SetBottomBorder (UIView view, nfloat width, UIColor color)
		{
			CALayer bottom = new CALayer () {
				Frame = new CGRect (0f, view.Bounds.Height, width, 1f),
				BackgroundColor = color.CGColor
			};
			view.Layer.AddSublayer (bottom);
		}

		public static void SetUpperBorder (UIView view, UIColor color)
		{
			CALayer up = new CALayer () {
				Frame = new CGRect (0f, 0f, view.Bounds.Width, 1f),
				BackgroundColor = color.CGColor
			};
			view.Layer.AddSublayer (up);
		}

		#endregion

		private const string DateFormat = "dd-MM-yyyy";
		/// <summary>
		/// Converts DateTime to string in format "dd-MM-yyyy".
		/// </summary>
		/// <returns>The date string.</returns>
		/// <param name="dt">Dt.</param>
		public static string DatetimeToString (DateTime dt)
		{
			return dt.ToString (DateFormat);
		}

		#region NoResults Label on Empty Table

		private static UILabel SetNoResultsLabel (CGRect bounds)
		{
			return new UILabel () {
				Frame = new CGRect (bounds.X, bounds.Y - 100f, bounds.Width, bounds.Height),
				TextAlignment = UITextAlignment.Center
			};
		}

		/// <summary>
		/// Toggles the NoResults label.
		/// </summary>
		/// <param name="results">Results Count.</param>
		/// <param name="table">Table to show NoResults label as BackgroundView.</param>
		public static void ToggleNoResultsLabel (int results, UITableView table)
		{
			if (results > 0) {
				table.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
				table.TableFooterView = new UIView ();
				table.BackgroundView = null;

			} else {
				if (table.BackgroundView == null) {
					table.BackgroundView = SetNoResultsLabel (table.Bounds);
				}
				table.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			}
		}

		#endregion

		#region Sets Two Line TitleView
		public static void SetTwoLineTitleView (UINavigationItem item, string titleText, string subtitleText)
		{

			if (string.IsNullOrEmpty (titleText) || string.IsNullOrEmpty (subtitleText))
				return;

			var navigation = Components.Instance.NavController;

			UIView titleview = new UIView ();

			titleview.Frame = new CoreGraphics.CGRect (0f, 0f, navigation.NavigationBar.Frame.Width - 130f, navigation.NavigationBar.Frame.Height);

			UILabel first = new UILabel ();
			first.Frame = new CoreGraphics.CGRect (0f, 0f, titleview.Bounds.Width, titleview.Bounds.Height * 0.6f);
			first.Text = titleText;

			UILabel second = new UILabel ();
			second.Frame = new CoreGraphics.CGRect (0f, titleview.Bounds.Height * 0.6f, titleview.Bounds.Width, titleview.Bounds.Height * 0.4f);
			second.Text = subtitleText;

			first.TextColor = second.TextColor = UIColor.White;

			first.TextAlignment = second.TextAlignment = UITextAlignment.Left;

			titleview.AddSubview (first);
			titleview.AddSubview (second);

			item.TitleView = titleview;
		}
		#endregion

		#region Touches and Views

		/// <summary>
		/// Resigns if touch outside view.
		/// </summary>
		/// <param name="touch">Touch.</param>
		/// <param name="view">View.</param>
		public static void ResignIfTouchOutView (UITouch touch, UIView view)
		{
			if (IsTouchOutView (touch, view) && view.IsFirstResponder)
				view.ResignFirstResponder ();
		}

		/// <summary>
		/// Dismisses ViewController if touch out view.
		/// </summary>
		/// <param name="touch">Touch.</param>
		/// <param name="view">View.</param>
		/// <param name="viewContr">View Controller.</param>
		public static void DismissIfTouchOutView (UITouch touch, UIView view, UIViewController viewContr)
		{
			if (IsTouchOutView (touch, view))
				viewContr.DismissViewController (true, null);

		}

		/// <summary>
		/// Checks if touch is outside view.
		/// </summary>
		/// <returns><c>true</c>, if touch out of view, <c>true</c> otherwise.</returns>
		/// <param name="touch">Touch.</param>
		/// <param name="view">View.</param>
		public static bool IsTouchOutView (UITouch touch, UIView view)
		{
			return !view.Frame.Contains (touch.LocationInView (view));
		}

		#endregion

		/// <summary>
		/// Opens PopOver menu with device's apps that can open the file at given filePath.
		/// </summary>
		/// <param name="filePath">Full path of the file to be opened.</param>
		/// <param name="viewContr">View controller that called this function.</param>
		/// <param name="sender">View to show the PopOver menu from.</param>
		public static void OpenFileWithDeviceApp (string filePath, UIViewController viewContr, UIView sender = null)
		{
			UIActivityViewController activityVC = new UIActivityViewController (
							new NSObject [] { new NSString ("PDF Document"), NSUrl.FromFilename (filePath) }, null);
			if (IOS_Constants.IsDeviceIpad) {
				activityVC.PopoverPresentationController.SourceView = viewContr.View;
				if (sender != null)
					activityVC.PopoverPresentationController.SourceRect = sender.Frame;
				else
					activityVC.PopoverPresentationController.SourceRect = viewContr.View.Frame;
			}
			viewContr.PresentViewController (activityVC, true, null);
		}

		public static UIImage MaxResizeImage (UIImage sourceImage, nfloat maxWidth, nfloat maxHeight)
		{
			var sourceSize = sourceImage.Size;
			var maxResizeFactor = Math.Min (maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);
			if (maxResizeFactor > 1)
				return sourceImage;
			var width = maxResizeFactor * sourceSize.Width;
			var height = maxResizeFactor * sourceSize.Height;
			UIGraphics.BeginImageContextWithOptions (new SizeF ((float)width, (float)height), false, 2.0f);
			sourceImage.Draw (new RectangleF (0, 0, (float)width, (float)height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return resultImage;
		}

		// resize the image (without trying to maintain aspect ratio)
		public static UIImage ResizeImage (UIImage sourceImage, float width, float height)
		{
			UIGraphics.BeginImageContextWithOptions (new SizeF (width, height), false, 2.0f);
			sourceImage.Draw (new RectangleF (0, 0, width, height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return resultImage;
		}
	}
}
