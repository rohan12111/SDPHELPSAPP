﻿using System;
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
		ProgressDialog ProgressDialogLogin = null;
		ExpandableListView elvExListBox;
		List<WorkshopSessions> _WorkshopSessions;
		Int32 lastExpandedPosition = -1;
		Dictionary<string, WorkshopSessions> dictGroup = new Dictionary<string, WorkshopSessions> ();
		List<string> lstKeys = new List<string> ();

		protected async override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);
				SetContentView (Resource.Layout.MakeBookingSesh);

				ActionBar.SetHomeButtonEnabled(true);
				ActionBar.SetDisplayHomeAsUpEnabled(true);

				if (ProgressDialogLogin == null)
				{
					ProgressDialogLogin = ProgressDialog.Show(this, "", "Loading...");
				}

				string WorkshopSetId = Intent.GetStringExtra("WorkshopSetId");
				_WorkshopSessions = await RESTClass.GetWorkshopSessions("?workshopSetId=" + WorkshopSetId);

				CreateExpandableListData();

				elvExListBox = FindViewById<ExpandableListView> (Resource.Id.elvExListBox);
				elvExListBox.SetAdapter (new ExpandListSessionAdapter (this, dictGroup));

				if (ProgressDialogLogin != null)
				{
					ProgressDialogLogin.Dismiss();
					ProgressDialogLogin = null;
				}

				elvExListBox.ChildClick += delegate(object sender, ExpandableListView.ChildClickEventArgs e) {
					string itmGroup = lstKeys [e.GroupPosition];
					WorkshopSessions itmChild = dictGroup [itmGroup];
				};

				elvExListBox.GroupExpand += delegate(object sender, ExpandableListView.GroupExpandEventArgs e) {
					if (lastExpandedPosition != -1 && e.GroupPosition != lastExpandedPosition)
					{
						elvExListBox.CollapseGroup(lastExpandedPosition);
					}

					lastExpandedPosition = e.GroupPosition;
				};

//				Button btnViewDetails = FindViewById<Button>(Resource.Id.btnViewDetails);
//				btnViewDetails.Click += delegate {
//					StartActivity(new Intent(this, typeof(MakeBookingListActivity)));
//				};
			}
			catch (Exception e) 
			{
				ErrorHandling.LogError (e, this);
			}
			finally 
			{
				if (ProgressDialogLogin != null)
				{
					ProgressDialogLogin.Dismiss();
					ProgressDialogLogin = null;
				}
			}
		}

		public void CreateExpandableListData ()
		{
			try
			{
				foreach (WorkshopSessions sesh in _WorkshopSessions)
				{
					AddListItem(sesh);
				}

				lstKeys = new List<string> (dictGroup.Keys);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public void AddListItem(WorkshopSessions sesh)
		{
			try
			{
				if (!dictGroup.ContainsKey (sesh.WorkshopId.ToString())) 
				{
					dictGroup.Add (sesh.WorkshopId.ToString(), sesh);
				} 
				else 
				{
					dictGroup.Add ((sesh.WorkshopId + sesh.WorkShopSetID).ToString(), sesh);
				}
			}
			catch (Exception e) 
			{
				throw e;
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
				ErrorHandling.LogError (e, this);
				return false;
			}
		}
	}
}