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
	[Activity (Label = "  Make Booking", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]			
	public class MakeBookingListActivity : Activity
	{
		Adapters.TaskListAdapter taskList;
		IList<WorkshopSets> WrkSets;
		ListView lvWorkShops;

		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);
				SetContentView (Resource.Layout.MakeBookingList);

				ActionBar.SetHomeButtonEnabled(true);
				ActionBar.SetDisplayHomeAsUpEnabled(true);

				lvWorkShops = FindViewById<ListView>(Resource.Id.lvWorkShops);
				lvWorkShops.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
					new AlertDialog.Builder (this)
						.SetMessage("Click")
						.SetTitle("Application Error")
						.Show();
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

//		public void GetListData() 
//		{
//			foreach (WorkshopSets wrkshop in InWrkshops) 
//			{
//				List<string> lstTiles = new List<string> ();
//
//				lstTiles.Add ("subtext" + wrkshop.id);
//
//				dictGroup.Add (wrkshop.name, lstTiles);
//			}
//		}

		protected async override void OnResume ()
		{
			try
			{
				base.OnResume ();

				await GetWorkshops();
			}
			catch (Exception e)
			{
				new AlertDialog.Builder (this)
					.SetMessage(e.Message + "\n" + e.StackTrace)
					.SetTitle("Application Error")
					.Show();
			}
		}

		public async Task<bool> GetWorkshops(Boolean blnOnline = true)
		{
			try
			{
				if (blnOnline == true)
				{
					try
					{
						WrkSets = await RESTClass.GetWorkshopList();
	//						List<WorkshopSets> TempWrkshopSets = await RESTClass.GetWorkshopList();
	//						WrkSets = (IList<WorkshopSets>)TempWrkshopSets;
					}
					catch (WebException e)
					{
						if (e.Status == WebExceptionStatus.ConnectFailure) 
						{
							GetWorkshops (false);
						}
						else
						{
							throw;
						}
					}
				}
				else
				{
					WrkSets = new List<WorkshopSets> {};

					WrkSets.Add(new WorkshopSets(1, "Cannot connect to server... Try again later", "01/02/2015"));
					WrkSets.Add(new WorkshopSets(2, "Offline WritingSkills", "01/02/2015"));
					WrkSets.Add(new WorkshopSets(3, "Offline Study + Reading skills", "01/02/2015"));
					WrkSets.Add(new WorkshopSets(4, "Offline Presentation + Speaking Skills", "01/02/2015"));
				}

				// create our adapter
				taskList = new Adapters.TaskListAdapter(this, WrkSets);

				//Hook up our adapter to our ListView
				lvWorkShops.Adapter = taskList;

				return true;
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
