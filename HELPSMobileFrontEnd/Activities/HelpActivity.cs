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
			List<string> lstButtons = new List<string> ();

			lstButtons.Add (
				"Make Booking\n" +
				"Make a HELPS booking for a session, workshop or programme.\n\n" +
				"View Booking\n" +
				"View all of your current or past HELPS bookings.\n\n" +
				"Mark Attendance\n" +
				"Marks you off as attending a booking by submitting a unique code.\n\n" +
				"Search\n" +
				"Search and display available sessions, workshops or programmes; search can be by date, location, topic, tutor.\n\n" +
				"Profile\n" +
				"View and edit student information supplied to UTS: HELPS.\n\n" +
				"HELP\n" +
				"Outlines details on how to use the application and FAQs.");
			lstBooking.Add (
				"1.\tSelect a skill-set\n\n" +
				"2.\tSelect a session, workshop or programme\n\n" +
				"3.\tSet reminder(s)\n\n" +
				"Important information:\n" +
				"- Follow each step to complete your booking.\n" +
				"- Check the time to ensure that there is no timetable clash.\n" +
				"- Check your email (UTS email address) for the booking confirmation.");
			lstFAQ.Add (
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
				"A: Yes. You can attend our workshops or drop in for an individual consultation session.");

			dictGroup.Add ("What do the buttons do?", lstButtons);
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
