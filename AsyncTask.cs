using System;
using System.ComponentModel;

namespace Mobile.iOS
{
	public abstract class AsyncTask
	{
		protected BackgroundWorker Worker;
		protected bool IsTaskSuccessful;
		protected bool IsCancelled;

		private readonly bool ConcurrentDownload;
		protected bool ShowNotifications;

		protected AsyncTask (bool ConcurrentDownload, bool ShowNotifications)
		{
			this.ConcurrentDownload = ConcurrentDownload;
			this.ShowNotifications = ShowNotifications;

			Worker = new BackgroundWorker { WorkerReportsProgress = true };
			Worker.DoWork += DoInBackground;
			Worker.ProgressChanged += ProgressChanged;
			Worker.RunWorkerCompleted += OnPostExecute;
		}

		public void Execute ()
		{
			if (Worker.IsBusy)
				return;
			if (ShouldStart ()) {
				OnPreExecute ();
				Worker.RunWorkerAsync ();
			}
		}

		#region Virtual - abstract methods
		protected abstract void DoInBackground (object sender, DoWorkEventArgs e);

		protected virtual void ProgressChanged (object sender, ProgressChangedEventArgs e) { }

		protected virtual void OnPreExecute ()
		{
			if (!ConcurrentDownload) {
                //	TODO: public bool DownloadIsOnTheWay = true
            }
            if (ShowNotifications) {
				//	TODO: Show 'Downloading started' message
			}
		}

		protected virtual void OnPostExecute (object sender, RunWorkerCompletedEventArgs e)
		{
			Worker.Dispose ();
			if (!ConcurrentDownload) {
                //	TODO: public bool DownloadIsOnTheWay = false
            }
            if (!ConcurrentDownload)
				return;
			if (IsTaskSuccessful) {
				// TODO: Show 'download success' message
			} else {
				// TODO: Show 'download fail' message

			}
		}
		#endregion

		/// <summary>
		/// Checks internet status and other concurrent tasks
		/// </summary>
		protected virtual bool ShouldStart ()
		{
			// internet connection
			if (!IOS_Methods.IsOnline ()) {
				if (ShowNotifications) {
					// TODO: Show 'not online' message
				}
				return false;
			}

			// concurrent downloads
			if (!ConcurrentDownload  /* && TODO: check public bool DownloadIsOnTheWay*/) {
				if (ShowNotifications) {
					//	TODO: Show 'Is Downloading' message
				}
				return false;
			}

			return true;
		}

		public void Cancel ()
		{
			IsCancelled = true;
		}
	}

}

