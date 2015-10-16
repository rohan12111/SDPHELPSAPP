
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
	[Activity (Label = "HelpActivity", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]			
	public class HelpActivity : Activity
	{
		Dictionary<string, List<string> > dictGroup = new Dictionary<string, List<string> > ();
		List<string> lstKeys = new List<string> ();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.HelpPage);

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

			List<string> lstChild = new List<string> ();

			lstChild.Add (string.Format ("Help : tile klajdsflkajsldkfaj;sdlkfj;ldksja; kldjf ;aklksjlkajdlkajdl;fkajlskdfjal;", 1, 1));
			dictGroup.Add (string.Format ("What tiles do what", 0), lstChild);
			dictGroup.Add (string.Format ("How do I make a Booking", 0), lstChild);
			dictGroup.Add (string.Format ("Frequently asked Question", 0), lstChild);

			lstKeys = new List<string> (dictGroup.Keys);
		}
	}
}
