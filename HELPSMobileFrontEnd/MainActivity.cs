using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
namespace HELPSMobileFrontEnd
{
	[Activity (Label = "HELPSMobileFrontEnd", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		
		private List<string> aItems;
		private ListView mListview;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			mListview = FindViewById<ListView> (Resource.Id.mylistview);
			aItems = new List<string>{};
			aItems.Add("Booking ");
			aItems.Add ("Search");
			aItems.Add ("Cancel");
			ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1,aItems);

			mListview.Adapter = adapter;


		}
	}
}


