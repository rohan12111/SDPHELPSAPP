
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
	[Activity (Label = "View Bookings", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class ViewBookingsActivity : Activity
	{
		private List<string> mItems;
		private ListView mListView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.BookingList);
			mListView = FindViewById<ListView> (Resource.Id.bookinglist);


			mItems = new List<string> ();
			mItems.Add ("Class1");
			mItems.Add ("Class2");
			mItems.Add ("Class3");
			mItems.Add ("Class4");
			ArrayAdapter<string> adapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItem1, mItems);
			mListView.Adapter = adapter;
			mListView.ItemClick += MListView_ItemClick;
			// Create your application here
		}

		void MListView_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			
		}
	}
}

