using System;
using Foundation;

namespace Mobile.iOS
{
	public static class Localize
	{
		public static NSBundle LangBundle {
			get {
				var db = DatabaseHelpers.GetDatabase ();
				string languageCode = "el";
				var path = NSBundle.MainBundle.PathForResource (languageCode, "lproj");
				return NSBundle.FromPath (path);
			}
		}

		public static string GetString (string localizedString)
		{
			var db = DatabaseHelpers.GetDatabase ();
			string languageCode = "el";
			var path = NSBundle.MainBundle.PathForResource (languageCode, "lproj");
			var languageBundle = NSBundle.FromPath (path);
			return languageBundle.LocalizedString (localizedString, "", "");
		}
	}
}

