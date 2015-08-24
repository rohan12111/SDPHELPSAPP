using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.IO;
using System.Json;
using System.Threading.Tasks;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "HELPSMobileFrontEnd", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
//		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			ActionBar.Hide (); //Hides the top label

			// Get our button from the layout resource,
			// and attach an event to it
			Button btnGetData = FindViewById<Button> (Resource.Id.btnGetData);
			
			btnGetData.Click += delegate {
				RESTDataCall();
			};
		}

		private void RESTDataCall()
		{
			//String url = "";

//			JsonValue json = await FetchAsync(url);
			TextView lblData = FindViewById<TextView> (Resource.Id.lblData);
			lblData.Text = "test";
		}

//		private async Task<JsonValue> FetchAsync(String url)
//		{
//			 
//		}
	}
}


