using System;
using UIKit;
using CoreGraphics;

namespace Mobile.iOS
{
	public sealed class LoadingOverlay : UIView
	{
		// control declarations
		UIActivityIndicatorView activitySpinner;
		UILabel loadingLabel;
		UILabel waitLabel;

		public LoadingOverlay (CGRect frame, string loadingText) : base (frame)
		{
			// configurable bits
			BackgroundColor = UIColor.Black;
			Alpha = 0.75f;
			AutoresizingMask = UIViewAutoresizing.All;

			nfloat labelHeight = 22;
			nfloat labelWidth = Frame.Width - 20;

			// derive the center x and y
			nfloat centerX = Frame.Width / 2;
			nfloat centerY = Frame.Height / 2;

			// create the activity spinner, center it horizontall and put it 5 points above center x
			activitySpinner = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.WhiteLarge);
			activitySpinner.Frame = new CGRect (
				centerX - (activitySpinner.Frame.Width / 2),
				centerY - activitySpinner.Frame.Height - 20,
				activitySpinner.Frame.Width,
				activitySpinner.Frame.Height);
			activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
			AddSubview (activitySpinner);
			activitySpinner.StartAnimating ();

			// create and configure the "Loading Data" label
			loadingLabel = new UILabel (new CGRect (
				centerX - (labelWidth / 2),
				centerY + 20,
				labelWidth,
				labelHeight
			));

			loadingLabel.BackgroundColor = UIColor.Clear;
			loadingLabel.TextColor = UIColor.White;
			loadingLabel.Text = loadingText;
			loadingLabel.TextAlignment = UITextAlignment.Center;
			loadingLabel.AutoresizingMask = UIViewAutoresizing.All;
			//			AddSubview (loadingLabel);

			waitLabel = new UILabel (new CGRect (
				centerX - (labelWidth / 2),
				centerY + 3 * labelHeight,
				labelWidth,
				labelHeight
			));
			waitLabel.AdjustsFontSizeToFitWidth = true;
			waitLabel.BackgroundColor = UIColor.Clear;
			waitLabel.TextColor = UIColor.White;
			waitLabel.TextAlignment = UITextAlignment.Center;
			waitLabel.AutoresizingMask = UIViewAutoresizing.All;
			//			waitLabel.Text = Localize.GetString ("pleasewait");
			waitLabel.Text = "";
			//			AddSubview (waitLabel);
		}

		/// <summary>
		/// Fades out the control and then removes it from the super view
		/// </summary>
		public void Hide ()
		{
			UIView.Animate (
				0.5, // duration
				() => {
					Alpha = 0;
				},
				() => {
					RemoveFromSuperview ();
				}
			);
		}
	}
}

