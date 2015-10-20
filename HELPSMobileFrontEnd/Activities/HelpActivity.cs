
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
using ExpendListBox;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "   Help", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]			
	public class HelpActivity : Activity
	{
		Dictionary<string, List<string> > dictGroup = new Dictionary<string, List<string> > ();
		List<string> lstKeys = new List<string> ();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.HelpPage);

			ActionBar.SetHomeButtonEnabled(true);
			ActionBar.SetDisplayHomeAsUpEnabled(true);

			CreateExpendableListData ();

			var ctlExListBox = FindViewById<ExpandableListView> (Resource.Id.ctlExListBox);
			ctlExListBox.SetAdapter (new ExpendListAdapter (this, dictGroup));

			ctlExListBox.ChildClick += delegate(object sender, ExpandableListView.ChildClickEventArgs e) {
				var itmGroup = lstKeys [e.GroupPosition];
				var itmChild = dictGroup [itmGroup] [e.ChildPosition];
			};
		}

		void CreateExpendableListData ()
		{

			List<string> lstFAQ = new List<string> ();
			List<string> lstBooking = new List<string> ();
			List<string> lstTiles = new List<string> ();

			lstTiles.Add (
				"- Make Booking\n" +
				"Make booking allows you to look at all available classes and book yourself into a class, using the \"book\" button.\n\n" +
				"- View Booking\n" +
				"Shows you all of your current and past bookings.\n\n" +
				"- Check-In\n" +
				"Allows you to mark your name off when you arive in a class.\n\n" +
				"- Search\n" +
				"General purpose search for finding classes.\n\n" +
				"- Profile\n" +
				"View and change your personal information.\n\n" +
				"- Help\n" +
				"Brings you to this help screen.\n\n" +
				"- Logout\n" +
				"Logs you out of the system.");
			lstBooking.Add (
				"1.\tSelect the workshop you are interested in.\n" +
				"2.\tSelect the time slot you wish to be in.\n" +
				"3.\tOptionally set yourself reminders.");
			lstFAQ.Add (
				"- Who can use HELPS?\n" +
				"Any Student enrolled in any faculty at UTS, and all members of UTS Staff.\n\n" +
				"- Where is HELPS?\n" +
				"HELPS is located in Building 1, Level 3, Room 8 (opposite the Careers service)");

			dictGroup.Add ("What tiles do what", lstTiles);
			dictGroup.Add ("How do I make a Booking", lstBooking);
			dictGroup.Add ("Frequently asked Question", lstFAQ);

			lstKeys = new List<string> (dictGroup.Keys);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
			case Android.Resource.Id.Home:
				Finish();
				return true;

			default:
				return base.OnOptionsItemSelected(item);
			}
		}
	}
}
