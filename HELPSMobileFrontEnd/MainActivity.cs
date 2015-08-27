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
	[Activity (Label = "UTS: HELPS")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);

				SetContentView (Resource.Layout.Main);

				//Hides the top label
//				ActionBar.Hide (); 

				// Get our button from the layout resource, and attach an event to it
				Button btnGetData = FindViewById<Button> (Resource.Id.button1);
				Button btnNavigateClassList = FindViewById<Button> (Resource.Id.button2);
				
				btnGetData.Click += delegate {
					RESTDataCall();
				};

				btnNavigateClassList.Click += delegate {
					StartActivity(new Intent(this, typeof(ListSessionsActivity)));
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

		private void RESTDataCall()
		{
			try
			{
				TextView lblData = FindViewById<TextView> (Resource.Id.button1);
				lblData.Text = "test";
			}
			catch 
			{
				throw;
			}
		}
	}
}