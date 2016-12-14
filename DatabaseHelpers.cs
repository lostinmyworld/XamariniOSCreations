using System;
using System.IO;
using Mobile.Data;
using SQLite.Net;

namespace Mobile.iOS
{
	public class DatabaseHelpers
	{
		private const string SqliteFileName = "mydb.db3";

		private static string _path;
		private static string DbPath {
			get {
				if (string.IsNullOrEmpty (_path)) {
					string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
					_path = Path.Combine (documentsPath, SqliteFileName);
				}
				return _path;
			}
		}

		public static Database GetDatabase ()
		{
			return Database.GetInstance (GetSqlConnection);
		}


		private static SQLiteConnection GetSqlConnection ()
		{
			return new SQLiteConnection (new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS (), DbPath);
		}


		public static void InitializeDatabase ()
		{
			// TODO check database version    
			using (var db = GetDatabase ()) {
				db.CreateDb ();
			}
		}
	}
}

