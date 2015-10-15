﻿using System;
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
	[Activity (Label = "View Bookings", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]
	public class ViewBookingsActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);

				SetContentView(Resource.Layout.ViewBookings);

				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				SlidingTabsFragment fragment = new SlidingTabsFragment();
				transaction.Replace(Resource.Id.flTabs, fragment);
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
	}
}

