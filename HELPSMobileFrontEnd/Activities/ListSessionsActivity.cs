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

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "Make a booking", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class ListSessionsActivity : ListActivity
	{
		string[] items;
		protected async override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);
//				SetContentView (Resource.Layout.ListSessions);

<<<<<<< Upstream, based on origin/master
				ActionBar.Hide ();
=======
>>>>>>> 243596e Added Sessionslistviewlayout
				ListView lvWorkshops = FindViewById<ListView> (Resource.Id.lvClasses);

				List<WorkshopSets> WrkSets = await RESTClass.GetWorkshopList();

<<<<<<< Upstream, based on origin/master
				string[] items = new string[WrkSets.Count];
=======
//				string[] items = new string[WrkSets.Count];
				items = new string[WrkSets.Count];
>>>>>>> 243596e Added Sessionslistviewlayout

				for (int i = 0; i < WrkSets.Count; i++)
				{
					items[i] = WrkSets[i].name;
				}

<<<<<<< Upstream, based on origin/master
				HomeScreenAdapter ListAdapter = new HomeScreenAdapter(this, items);

				lvWorkshops.Adapter = ListAdapter;
=======
				ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
//				HomeScreenAdapter ListAdapter = new HomeScreenAdapter(this, items);
//				lvWorkshops.Adapter = ListAdapter;
>>>>>>> 243596e Added Sessionslistviewlayout
			}
			catch (Exception e)
			{
				new AlertDialog.Builder (this)
					.SetMessage(e.Message + "\n" + e.StackTrace)
					.SetTitle("Application Error")
					.Show();
			}
		}
	}

//	public class HomeScreenAdapter : BaseAdapter<string> 
//	{
//		string[] items;
//		Activity context;
//
//		public HomeScreenAdapter(Activity context, string[] items) : base() 
//		{
//			this.context = context;
//			this.items = items;
//		}

//		public override long GetItemId(int position)
//		{
//			return position;
//		}
//
//		public override string this[int position] 
//		{
//			get { return items[position]; }
//		}
//
//		public override int Count 
//		{
//			get { return items.Length; }
//		}
///		public override View GetView(int position, View convertView, ViewGroup parent)
//		{
//			View view = convertView; // re-use an existing view, if one is available
//			if (view == null) // otherwise create a new one
//			{
//				view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
//			}
//
//			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position];
//			return view;
//		}
//	}
}
