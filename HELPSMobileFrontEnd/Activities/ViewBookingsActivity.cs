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
using System.Globalization;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "View Bookings", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]
	public class ViewBookingsActivity : Activity
	{
		ProgressDialog ProgressDialogLogin = null;
//		Dictionary<string, WorkshopBooking> dictGroupCurr = new Dictionary<string, WorkshopBooking> ();
//		Dictionary<string, WorkshopBooking> dictGroupPast = new Dictionary<string, WorkshopBooking> ();
//		List<string> lstKeysCurr = new List<string> ();
//		List<string> lstKeysPast = new List<string> ();

		protected override async void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);
				SetContentView(Resource.Layout.TabsLayout);

				ActionBar.SetHomeButtonEnabled(true);
				ActionBar.SetDisplayHomeAsUpEnabled(true);

				if (ProgressDialogLogin == null)
				{
					ProgressDialogLogin = ProgressDialog.Show(this, "", "Loading...");
				}

				Globals.StuBookings = await RESTClass.GetWorkshopBookings(String.Format("?active=true&studentId={0}", Globals.LoggedStudent.studentID));

				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				ViewBookingsFragment sltFragment = new ViewBookingsFragment();
				transaction.Replace(Resource.Id.flTabs, sltFragment);
				transaction.Commit();

				if (ProgressDialogLogin != null)
				{
					ProgressDialogLogin.Dismiss();
					ProgressDialogLogin = null;
				}
			}
			catch (Exception e)
			{
				new AlertDialog.Builder (this)
					.SetMessage(e.Message + "\n" + e.StackTrace)
					.SetTitle("Application Error")
					.Show();
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

//		public void CreateExpandableListData ()
//		{
//			try
//			{
//				foreach (WorkshopBooking book in Globals.StuBookings)
//				{
//					AddListItem(book);
//				}
//
//				lstKeysCurr = new List<string> (dictGroupCurr.Keys);
//				lstKeysPast = new List<string> (dictGroupPast.Keys);
//			}
//			catch (Exception e)
//			{
//				throw e;
//			}
//		}

//		public void AddListItem(WorkshopBooking book)
//		{
//			try
//			{
//				if (DateTime.ParseExact(book.ending, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture) < DateTime.Now) //Past
//				{
//					if (!dictGroupPast.ContainsKey (book.BookingId.ToString())) 
//					{
//						dictGroupPast.Add (book.BookingId.ToString(), book);
//					} 
//					else
//					{
//						dictGroupPast.Add ((book.BookingId + book.studentID).ToString(), book);
//					}
//				}
//				else
//				{
//					if (!dictGroupCurr.ContainsKey (book.BookingId.ToString())) 
//					{
//						dictGroupCurr.Add (book.BookingId.ToString(), book);
//					} 
//					else
//					{
//						dictGroupCurr.Add ((book.BookingId + book.studentID).ToString(), book);
//					}
//				}
//
//			}
//			catch (Exception e) 
//			{
//				throw e;
//			}
//		}

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

