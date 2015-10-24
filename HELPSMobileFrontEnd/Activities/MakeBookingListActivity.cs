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
using System.Threading;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "  Make Booking", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]			
	public class MakeBookingListActivity : Activity
	{
		Adapters.TaskListAdapter taskList;
		IList<WorkshopSets> WrkSets;
		ListView lvWorkShops;
		ProgressDialog progressDialog;

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
					var BookingSeshions = new Intent(this, typeof(MakeBookingSeshActivity));			
					BookingSeshions.PutExtra("WorkshopSetId", WrkSets.ElementAt<WorkshopSets>((int)e.Id).id.ToString());
					StartActivity(BookingSeshions);
				};
			}
			catch (Exception e) 
			{
				ErrorHandling.LogError (e, this);
			}
		}

		protected async override void OnResume ()
		{
			try
			{
				base.OnResume ();

				progressDialog = ProgressDialog.Show(this, "", "Loading...");

				await GetWorkshops();
			}
			catch (Exception e) 
			{
				ErrorHandling.LogError (e, this);
			}
			finally 
			{
				progressDialog.Dismiss();
				progressDialog.Dispose ();
			}
		}

		public async Task<bool> GetWorkshops(Boolean blnOnline = true)
		{
			try
			{
//				if (blnOnline == true)
//				{
				WrkSets = await RESTClass.GetWorkshopList("?active=true");
//				}
//				else
//				{
//					WrkSets = new List<WorkshopSets> {};
//
//					WrkSets.Add(new WorkshopSets(1, "Cannot connect to server... Try again later", "01/02/2015"));
//					WrkSets.Add(new WorkshopSets(2, "Offline WritingSkills", "01/02/2015"));
//					WrkSets.Add(new WorkshopSets(3, "Offline Study + Reading skills", "01/02/2015"));
//					WrkSets.Add(new WorkshopSets(4, "Offline Presentation + Speaking Skills", "01/02/2015"));
//				}

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
