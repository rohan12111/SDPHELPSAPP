using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "  Mark Attendance", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]			
	public class CheckInActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.MarkAttendance);

			ActionBar.SetHomeButtonEnabled(true);
			ActionBar.SetDisplayHomeAsUpEnabled(true);

			Button btnSubmit = FindViewById<Button> (Resource.Id.btnSubmit);

			btnSubmit.Click += delegate {
				new AlertDialog.Builder (this)
					.SetTitle("Attendance")
					.SetMessage("You have been marked as attended for this session.")
					.Show();
			};
		}
	}
}

