
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
	[Activity (Label = "Profile", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class ProfileActivity : Activity
	{
		protected async override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);

				SetContentView (Resource.Layout.Profile);

				TabWidget tabWidg = FindViewById<TabWidget> (Resource.Id.tabWidget1);

				tabWidg.AddView(new View(this));
				tabWidg.AddView(new View(this));
				tabWidg.AddView(new View(this));

//				Student student = await RESTClass.GetStudent("00000000");
//				throw new Exception(student.status + student.country_origin);
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

