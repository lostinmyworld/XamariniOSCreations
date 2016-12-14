using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace Mobile.iOS
{
	public sealed class IsUrlReachableTask : AsyncTask
	{
		Action<bool> _onPostExecute;
		string _url;

		public IsUrlReachableTask (string url, Action<bool> onPostExecute) : base (ConcurrentDownload: false, ShowNotifications: false)
		{
			this._url = url;
			this._onPostExecute = onPostExecute;
		}

		protected override void DoInBackground (object sender, DoWorkEventArgs e)
		{
			IsTaskSuccessful = !string.IsNullOrWhiteSpace (_url) && IOS_Methods.IsUrlReachable (_url);
		}

		/// <summary>
		/// Run after Task is finished.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		protected override void OnPostExecute (object sender, RunWorkerCompletedEventArgs e)
		{
			base.OnPostExecute (sender, e);
			if (_onPostExecute != null)
				_onPostExecute (IsTaskSuccessful);
		}
	}
}

