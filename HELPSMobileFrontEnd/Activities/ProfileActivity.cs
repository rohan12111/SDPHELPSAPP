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
using Android.Util;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "Profile", ScreenOrientation = ScreenOrientation.Portrait, Theme="@style/ActionBarTheme")]			
	public class ProfileActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);
				SetContentView (Resource.Layout.TabsLayout);

				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				ProfileFragment sltFragment = new ProfileFragment();
				transaction.Replace(Resource.Id.flTabs, sltFragment);
				transaction.Commit();
			}
			catch (Exception e) 
			{
				new AlertDialog.Builder (this)
					.SetMessage(e.Message + "\n\n" + e.StackTrace)
					.SetTitle("Application Error")
					.Show();
			}
		}
	}
}