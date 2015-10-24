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
	[Activity (Label = "UTS: HELPS", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]
	public class MainMenuActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);

				SetContentView (Resource.Layout.MainMenu);

				// Get our button from the layout resource, and attach an event to it
				Button btnMakeBooking = FindViewById<Button> (Resource.Id.btnMakeBooking);
				Button btnViewBooking = FindViewById<Button> (Resource.Id.btnViewBooking);
				Button btnSearch = FindViewById<Button> (Resource.Id.btnSearch);
				Button btnHelp = FindViewById<Button> (Resource.Id.btnHelp);
				Button btnProfile = FindViewById<Button> (Resource.Id.btnProfile);
				Button btnLogout = FindViewById<Button> (Resource.Id.btnLogout);
				Button btnMarkAttendance = FindViewById<Button> (Resource.Id.btnMarkAttendance);
				ImageView ivMakeBooking = FindViewById<ImageView> (Resource.Id.ivMakeBooking);
				ImageView ivMarkAttendance = FindViewById<ImageView> (Resource.Id.ivMarkAttendance);
				ImageView ivViewBooking = FindViewById<ImageView> (Resource.Id.ivViewBooking);
				ImageView ivSearch = FindViewById<ImageView> (Resource.Id.ivSearch);
				ImageView ivHelp = FindViewById<ImageView> (Resource.Id.ivHelp);
				ImageView ivProfile = FindViewById<ImageView> (Resource.Id.ivProfile);

				btnMakeBooking.Click += delegate  {
					StartActivity(new Intent(this, typeof(MakeBookingListActivity)));
				};
				ivMakeBooking.Click	+=delegate  {
					StartActivity(new Intent(this, typeof(MakeBookingListActivity)));
				};

				btnMarkAttendance.Click += delegate {
					StartActivity(new Intent(this, typeof(CheckInActivity)));
				};
				ivMarkAttendance.Click += delegate {
					StartActivity(new Intent(this, typeof(CheckInActivity)));
				};

				btnViewBooking.Click += delegate {
					StartActivity(new Intent(this, typeof(ViewBookingsActivity)));
				};
				ivViewBooking.Click += delegate {
					StartActivity(new Intent(this, typeof(ViewBookingsActivity)));
				};

				btnSearch.Click += delegate {
					StartActivity(new Intent(this, typeof(SearchActivity)));
				};
				ivSearch.Click += delegate {
					StartActivity(new Intent(this, typeof(SearchActivity)));
				};

				btnHelp.Click += delegate {
					StartActivity(new Intent(this, typeof(HelpActivity)));
				};
				ivHelp.Click += delegate {
					StartActivity(new Intent(this, typeof(HelpActivity)));
				};

				btnProfile.Click += delegate {
					StartActivity(new Intent(this, typeof(ProfileActivity)));
				};
				ivProfile.Click += delegate {
					StartActivity(new Intent(this, typeof(ProfileActivity)));
				};

				btnLogout.Click += delegate {
					Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
				};

			}
			catch (Exception e) 
			{
				ErrorHandling.LogError (e, this);
			}
		}
	}
}