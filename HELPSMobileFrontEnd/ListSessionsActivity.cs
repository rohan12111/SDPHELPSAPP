using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Json;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using System.Runtime.Serialization.Json;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "ListSessionsActivity")]			
	public class ListSessionsActivity : Activity
	{
		protected async override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);
				SetContentView (Resource.Layout.ListSessions);
				ActionBar.Hide ();
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