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
	public class MainMenuActivity : Activity
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
				Button btnMakeBooking = FindViewById<Button> (Resource.Id.btnMakeBooking);
				Button btnViewBooking = FindViewById<Button> (Resource.Id.btnViewBooking);
				Button btnSearch = FindViewById<Button> (Resource.Id.btnSearch);
				Button btnHelp = FindViewById<Button> (Resource.Id.btnHelp);
				Button btnProfile = FindViewById<Button> (Resource.Id.btnProfile);
				Button btnLogout = FindViewById<Button> (Resource.Id.btnLogout);



				btnMakeBooking.Click += delegate {
					StartActivity(new Intent(this, typeof(ListSessionsActivity)));
				};

				btnViewBooking.Click += delegate {
					StartActivity(new Intent(this, typeof(ViewBookingsActivity)));
				};

				btnSearch.Click += delegate {
					StartActivity(new Intent(this, typeof(SearchActivity)));
				};

				btnHelp.Click += delegate {
					StartActivity(new Intent(this, typeof(HelpActivity)));
				};

				btnProfile.Click += delegate {
					StartActivity(new Intent(this, typeof(ProfileActivity)));
				};

				btnLogout.Click += delegate {
					Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
				};
			}
			catch (Exception e)
			{
				new AlertDialog.Builder (this)
					.SetMessage(e.Message + "\n" + e.StackTrace)
					.SetTitle("Application Error")
					.Show();
			}
		}
	}
}