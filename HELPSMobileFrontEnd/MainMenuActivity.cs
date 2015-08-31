using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Content.PM;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "UTS: HELPS", ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);

				SetContentView (Resource.Layout.MainMenu);

				//Hides the top label
//				ActionBar.Hide (); 

				// Get our button from the layout resource, and attach an event to it
				Button btnBooking = FindViewById<Button> (Resource.Id.btnBooking);
				Button btnLogout = FindViewById<Button> (Resource.Id.btnLogout);

				btnBooking.Click += delegate {
					StartActivity(new Intent(this, typeof(ListSessionsActivity)));
				};

				btnLogout.Click += delegate {
					Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
				};
			}
			catch (Exception e)
			{
				new AlertDialog.Builder (this)
					.SetMessage(e.Message)
					.SetTitle("Application Error")
					.Show();
			}
		}
	}
}