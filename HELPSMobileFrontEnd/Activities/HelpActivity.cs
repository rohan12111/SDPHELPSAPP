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
	[Activity (Label = "  Help", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]			
	public class HelpActivity : Activity
	{
		Dictionary<string, List<string> > dictGroup = new Dictionary<string, List<string> > ();
		List<string> lstKeys = new List<string> ();
		Int32 lastExpandedPosition = -1;

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

			ctlExListBox.GroupExpand += delegate(object sender, ExpandableListView.GroupExpandEventArgs e) {
				if (lastExpandedPosition != -1 && e.GroupPosition != lastExpandedPosition)
				{
					ctlExListBox.CollapseGroup(lastExpandedPosition);
				}

				lastExpandedPosition = e.GroupPosition;
			};
		}

		void CreateExpendableListData ()
		{

			List<string> lstFAQ = new List<string> ();
			List<string> lstBooking = new List<string> ();
			List<string> lstTiles = new List<string> ();

			lstTiles.Add (
				"Make Booking\n" +
				"Make booking allows you to look at all available classes and book yourself into a class, using the \"book\" button.\n\n" +
				"View Booking\n" +
				"Shows you all of your current and past bookings.\n\n" +
				"Check-In\n" +
				"Allows you to mark your name off when you arive in a class.\n\n" +
				"Search\n" +
				"General purpose search for finding classes.\n\n" +
				"Profile\n" +
				"View and change your personal information.\n\n" +
				"Help\n" +
				"Brings you to this help screen.\n\n" +
				"Logout\n" +
				"Logs you out of the system.");
			lstBooking.Add (
				"\n"+
				"1.\tSelect the workshop you are interested in.\n" +
				"2.\tSelect the time slot you wish to be in.\n" +
				"3.\tOptionally set yourself reminders.\n");
			lstFAQ.Add (
				"\n" +
				"Q: Who can use HELPS?\n" +
				"A: Any Student enrolled in any faculty at UTS, and all members of UTS Staff.\n\n" +
				"Q: Where is HELPS?\n" +
				"A: HELPS is located in Building 1, Level 3, Room 8 (opposite the Careers service.\n\n" +
			    "Q: How much does it cost?\n" +
				"A: Service are free of tuition fees for non-credit workshops and individual consultations.\n\n" +
				"Q: Can you help me with my assignment?\n" +
				"A: Yes. HELPS offers various workshops and individual consultations. For more information, check out our website.\n\n" +
				"Q: Can you proofread and correct my assignment?\n" +
				"A: No. Our role is not to correct grammar or other errors in an assignment. We can help you develop your own editing strategies. You should also use a computer spell-check, find a competent friend and a good dictionary.\n\n" +
				"Q: Can you help me with the content of my assignment?\n" +
				"A: No. We can’t tell you what to say, we can only help you say it better and more clearly. While we’re happy to act as a sounding board for your ideas, content questions require the specialised disciplinary knowledge of lecturers and tutors in your faculty. You should take specific content questions directly to them.\n\n" +
				"Q: My lecturer says I need to improve my grammar. Can you help me?\n" +
				"A: Yes. Please check out our website or Learning resources.\n\n" +
				"Q: Can you help me with my pronunciation?\n" +
				"A: Yes. Please check website or Learning resources.\n\n" +
				"Q: Can I practise my seminar presentation with someone?\n" +
				"A: Yes. You can attend our workshops or drop in for an individual consultation session.\n");

			dictGroup.Add ("What do the button do?", lstTiles);
			dictGroup.Add ("How do I make a booking?", lstBooking);
			dictGroup.Add ("Frequently Asked Questions", lstFAQ);

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
