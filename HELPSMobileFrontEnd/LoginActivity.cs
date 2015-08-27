using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using System.Runtime.Serialization;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "LoginActivity", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class LoginActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);

				SetContentView (Resource.Layout.Login);

				Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

				btnLogin.Click += delegate {
					StartActivity(new Intent(this, typeof(MainActivity)));
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

