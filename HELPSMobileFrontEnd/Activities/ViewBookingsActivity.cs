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
		Fragment[] _fragments;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.ViewBookings);
			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

			_fragments = new Fragment[] { new CurrentBookingsFrag(), new PastBookingsFrag() };

			AddTabToActionBar("Current");
			AddTabToActionBar("Past");
		}

		private void AddTabToActionBar(String strLabel)
		{
			ActionBar.Tab tab = ActionBar.NewTab();
			tab.SetText (strLabel);
			tab.TabSelected += Tab_TabSelected;
			ActionBar.AddTab(tab);
		}

		private void Tab_TabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
		{
			ActionBar.Tab tab = (ActionBar.Tab)sender;

			Fragment frag = _fragments[tab.Position];
			tabEventArgs.FragmentTransaction.Replace(Resource.Id.flTabs, frag);
		}
	}
}

