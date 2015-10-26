
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
	[Activity (Label = "Search", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]			
	public class SearchActivity : Activity
	{

		string search;

		ProgressDialog progressDialog;
		List<WorkshopSessions> _WorkshopSessions;
		Int32 lastExpandedPosition = -1;
		Dictionary<string, WorkshopSessions> dictGroup = new Dictionary<string, WorkshopSessions> ();
		List<string> lstKeys = new List<string> ();

		protected async override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);
				SetContentView (Resource.Layout.Search);	
				List<string> WorkshopList;
				EditText txtsearch = FindViewById<EditText>(Resource.Id.txtsearch);
				Spinner searchWorkshop= FindViewById<Spinner>(Resource.Id.ddpworkshop);
				ExpandableListView ddpSearchList = FindViewById<ExpandableListView> (Resource.Id.ddpSearch);
				ImageButton btnsearch = FindViewById<ImageButton>(Resource.Id.imgsearch);
				search= txtsearch.Text;

				WorkshopList = new List<string>();
				WorkshopList.Add("Workshop");
				WorkshopList.Add("Session");
				WorkshopList.Add("Programs");
				ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,Android.Resource.Layout.SimpleDropDownItem1Line,WorkshopList);
				searchWorkshop.Adapter = adapter;

				btnsearch.Click += async delegate 
				{
					try
					{
						string searchBy;
						searchBy = searchWorkshop.SelectedItem.ToString();

						progressDialog = ProgressDialog.Show(this, "", "Searching...");

						if (searchBy == "Session")
						{
							string WorkshopSetId = Intent.GetStringExtra(search);
							//_WorkshopSessions = await RESTClass.GetWorkshopSessions("?workshopSetId=" + WorkshopSetId);
							_WorkshopSessions = await RESTClass.GetWorkshopSessions("?topic=" + WorkshopSetId);
						}
						else if(searchBy == "Workshop")
						{
							//await Ge
						}

						CreateExpendableListData();

						ddpSearchList.SetAdapter (new ExpandListSessionAdapter (this, dictGroup));
					}
					catch (Exception e)
					{
						ErrorHandling.LogError(e, this);
					}
					finally 
					{
						progressDialog.Dismiss();
						progressDialog.Dispose();
					}
				};

				ddpSearchList.ChildClick += delegate(object sender, ExpandableListView.ChildClickEventArgs e) 
				{
					try
					{
						string itmGroup = lstKeys [e.GroupPosition];
						WorkshopSessions itmChild = dictGroup [itmGroup]; 
					}
					catch (Exception Ex) 
					{
						ErrorHandling.LogError (Ex, this);
					}
				};

				ddpSearchList.GroupExpand += delegate(object sender, ExpandableListView.GroupExpandEventArgs e) 
				{
					if (lastExpandedPosition != -1 && e.GroupPosition != lastExpandedPosition)
					{
						ddpSearchList.CollapseGroup(lastExpandedPosition);
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
				//				progressDialog.Dismiss();
				//				progressDialog.Dispose ();
			}
		}

		public void CreateExpendableListData ()
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