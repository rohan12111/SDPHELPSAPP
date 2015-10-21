
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
using ExpendListBox;
using Android.Content.PM;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "Make Booking", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme" )]			
	public class MakeBookingSeshActivity : Activity
	{
		Dictionary<string, List<string> > dictGroup = new Dictionary<string, List<string> > ();
		List<string> lstKeys = new List<string> ();
		Int32 lastExpandedPosition = -1;

		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);
				SetContentView (Resource.Layout.MakeBookingSesh);

				//List<WorkshopSets> WrkSets = await RESTClass.GetWorkshopList();
				List<WorkshopSets> WrkSets = new List<WorkshopSets> {};

				WrkSets.Add(new WorkshopSets(1, "Sesh1", "01/02/2015"));
				WrkSets.Add(new WorkshopSets(2, "Sesh2", "01/02/2015"));

				CreateExpendableListData (WrkSets);

				var elvExListBox = FindViewById<ExpandableListView> (Resource.Id.elvExListBox);
				elvExListBox.SetAdapter (new ExpendListAdapter (this, dictGroup));

				elvExListBox.ChildClick += delegate(object sender, ExpandableListView.ChildClickEventArgs e) {
					var itmGroup = lstKeys [e.GroupPosition];
					var itmChild = dictGroup [itmGroup] [e.ChildPosition];
				};

				elvExListBox.GroupExpand += delegate(object sender, ExpandableListView.GroupExpandEventArgs e) {
					if (lastExpandedPosition != -1 && e.GroupPosition != lastExpandedPosition)
					{
						elvExListBox.CollapseGroup(lastExpandedPosition);
					}

					lastExpandedPosition = e.GroupPosition;
				};
			}
			catch (Exception e)
			{
				new AlertDialog.Builder (this)
					.SetMessage(e.Message + "\n" + e.StackTrace)
					.SetTitle("Application Error")
					.Show();
			}
		}

		public void CreateExpendableListData (List<WorkshopSets> InWrkshops)
		{
			try
			{
				lstKeys = new List<string> (dictGroup.Keys);
			}
			catch (Exception e)
			{
				new AlertDialog.Builder (this)
					.SetMessage(e.Message + "\n" + e.StackTrace)
					.SetTitle("Application Error")
					.Show();
			}
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			try
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
			catch (Exception e)
			{
				new AlertDialog.Builder (this)
					.SetMessage(e.Message + "\n" + e.StackTrace)
					.SetTitle("Application Error")
					.Show();
				return false;
			}
		}
	}
}

