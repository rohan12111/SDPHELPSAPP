using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Json;
using System.Diagnostics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using System.Runtime.Serialization.Json;
using Android.Content.PM;
using ExpendListBox;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "  Make a Booking", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]			
	public class MakeBookingList : Activity
	{
		Dictionary<string, List<string> > dictGroup = new Dictionary<string, List<string> > ();
		List<string> lstKeys = new List<string> ();
		Int32 lastExpandedPosition = -1;

		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);
				SetContentView (Resource.Layout.MakeBookingList);

				ActionBar.SetHomeButtonEnabled(true);
				ActionBar.SetDisplayHomeAsUpEnabled(true);

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

		void CreateExpendableListData (List<WorkshopSets> InWrkshops)
		{
			foreach (WorkshopSets wrkshop in InWrkshops) 
			{
				List<string> lstTiles = new List<string> ();

				lstTiles.Add ("subtext" + wrkshop.id);

				dictGroup.Add (wrkshop.name, lstTiles);
			}

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
