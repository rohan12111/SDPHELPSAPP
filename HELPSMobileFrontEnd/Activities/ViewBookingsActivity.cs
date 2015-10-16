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
using ExpendListBox;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "View Bookings", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]
	public class ViewBookingsActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);

				SetContentView(Resource.Layout.ViewBookings);

				ActionBar.SetHomeButtonEnabled(true);
				ActionBar.SetDisplayHomeAsUpEnabled(true);

				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				SlidingTabsFragment sltFragment = new SlidingTabsFragment();
				transaction.Replace(Resource.Id.flTabs, sltFragment);
				transaction.Commit();

			}
			catch (Exception e)
			{
				new AlertDialog.Builder (this)
					.SetMessage(e.Message + "\n" + e.StackTrace)
					.SetTitle("Application Error")
					.Show();
			}
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Android.Resource.Id.Home:
					Finish();
					return true;

				default:
					return base.OnOptionsItemSelected(item);
			}
		}
	}
}

