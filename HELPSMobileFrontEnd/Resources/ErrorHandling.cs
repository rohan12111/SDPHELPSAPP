using System;
using Android.App;
using System.IO;
using Android.Content;

namespace HELPSMobileFrontEnd
{
	public static class ErrorHandling
	{
		public static void LogError(Exception e, Context cont)
		{
			SaveText ("HELPSErrorLog.txt", DateTime.UtcNow.ToString() + "\n|||||||||||\n" + e.Message + "\n|||||||||||\n" + e.StackTrace);

			new AlertDialog.Builder (cont)
				.SetMessage(e.Message + "\n" + e.StackTrace)
				.SetTitle("Application Error")
				.Show();
		}

		public static void SaveText (string filename, string text) 
		{
			var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			var ErrorLog = Path.Combine (documentsPath, filename);
			File.WriteAllText (ErrorLog, text);
		}
	}
}

