
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
	[Activity (Label = "Search", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]			
	public class SearchActivity : Activity
	{


		Adapters.SearchAdapter taskList;
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
				List<WorkshopSets> WrkSets;
				EditText txtsearch = FindViewById<EditText>(Resource.Id.txtsearch);
				Spinner searchWorkshop= FindViewById<Spinner>(Resource.Id.ddpworkshop);
				ExpandableListView ddpSearchList = FindViewById<ExpandableListView> (Resource.Id.ddpSearch);
				ImageButton btnsearch = FindViewById<ImageButton>(Resource.Id.imgsearch);
				ListView workshoplist= FindViewById<ListView>(Resource.Id.lvlWorkShops);
				string search;


				WorkshopList = new List<string>();
				WorkshopList.Add("Session");
				WorkshopList.Add("Workshop");
				WorkshopList.Add("Programs");
				ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,Android.Resource.Layout.SimpleDropDownItem1Line,WorkshopList);
				searchWorkshop.Adapter = adapter;

				btnsearch.Click += async delegate 
				{
					try
					{
						search= txtsearch.Text;
						string searchBy;
						searchBy = searchWorkshop.SelectedItem.ToString();

						progressDialog = ProgressDialog.Show(this, "", "Searching...");

						if (searchBy == "Session")
						{
							//string WorkshopSetId = Intent.GetStringExtra(search);
							_WorkshopSessions = await RESTClass.GetWorkshopSessions("?workshopSetId=" + search);
							//_WorkshopSessions = await RESTClass.GetWorkshopSessions("?topic=" + WorkshopSetId + "&active=true&etc.");

						}
						else if(searchBy == "Workshop")
						{
							WrkSets = await RESTClass.GetWorkshopList("?active=true");

							taskList = new Adapters.SearchAdapter(this, WrkSets);
							workshoplist.Adapter= taskList;
						//	lvWorkShops.Adapter = taskList;


				//			new AlertDialog.Builder (this)
				//				.SetMessage(taskList.GetId(0))
				//				.SetTitle("Application Error")
				//				.Show();
													 
						}
						else if(searchBy == "Programs")
						{
							//await Ge
						}
						dictGroup.Clear();
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
				} ;

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
				} ;

				ddpSearchList.GroupExpand += delegate(object sender, ExpandableListView.GroupExpandEventArgs e) 
				{
					if (lastExpandedPosition != -1 && e.GroupPosition != lastExpandedPosition)
					{
						ddpSearchList.CollapseGroup(lastExpandedPosition);
					}

					lastExpandedPosition = e.GroupPosition;
				} ;

				//				Button btnViewDetails = FindViewById<Button>(Resource.Id.btnViewDetails);
				//				btnViewDetails.Click += delegate {
				//					StartActivity(new Intent(this, typeof(MakeBookingListActivity)));
				//				} ;
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
