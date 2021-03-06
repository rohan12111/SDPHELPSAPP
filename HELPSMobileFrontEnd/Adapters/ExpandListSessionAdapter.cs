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
using HELPSMobileFrontEnd;
using System.Globalization;
using Android.Graphics;
using Android.Provider;


namespace HELPSMobileFrontEnd
{
	class ExpandListSessionAdapter : BaseExpandableListAdapter
	{
		Dictionary<string, WorkshopSessions> _dictGroup = null;
		List<string> _lstGroupID = null;
		Activity _activity;

		public ExpandListSessionAdapter (Activity activity, Dictionary<string, WorkshopSessions> dictGroupIn)
		{
			try
			{
				_activity = activity;
				_dictGroup = dictGroupIn;
				_lstGroupID = dictGroupIn.Keys.ToList();
			}
			catch (Exception e) 
			{
				ErrorHandling.LogError (e, activity);
			}
		}

		public override Java.Lang.Object GetChild (int groupPosition, int childPosition)
		{
			return null;
		}

		public override long GetChildId (int groupPosition, int childPosition)
		{
			try
			{
				return childPosition;
			}
			catch (Exception e) 
			{
				throw e;
			}
		}

		public override int GetChildrenCount (int groupPosition)
		{
//			return _dictGroup [_lstGroupID [groupPosition]].Count;
			return 1;
		}

		public override View GetChildView (int groupPosition, int childPosition, bool isLastChild,
			View convertView, ViewGroup parent)
		{
			try
			{
				DateTime StartDte = DateTime.Today;
				DateTime EndDte = DateTime.Today;

				var item = _dictGroup [_lstGroupID [groupPosition]];

				if (convertView == null) 
				{
					convertView = _activity.LayoutInflater.Inflate (Resource.Layout.ListControl_BookingChild, null);
				}
					
				TextView tvRoom = convertView.FindViewById<TextView> (Resource.Id.tvRoom);
				TextView tvStartDate = convertView.FindViewById<TextView> (Resource.Id.tvStartDate);
				TextView tvTime = convertView.FindViewById<TextView> (Resource.Id.tvTime);
				TextView tvFinishDate = convertView.FindViewById<TextView> (Resource.Id.tvFinishDate);
				TextView tvGroup = convertView.FindViewById<TextView> (Resource.Id.tvGroup);
				TextView tvPlaces = convertView.FindViewById<TextView> (Resource.Id.tvPlaces);
				TextView tvWaitlist = convertView.FindViewById<TextView> (Resource.Id.tvWaitlist);

				Button btnViewDetails = convertView.FindViewById<Button> (Resource.Id.btnViewDetails);
				Button btnBook = convertView.FindViewById<Button> (Resource.Id.btnBook);

				if (item.StartDate != null) 
				{ // 07/31/2012 17:00:00
					StartDte = DateTime.ParseExact(item.StartDate, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
				}

				if (item.EndDate != null) 
				{
					EndDte = DateTime.ParseExact(item.EndDate, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
				}

				SetTVText(tvRoom, item.campus);
				SetTVText(tvStartDate, StartDte.ToShortDateString());
				SetTVText(tvTime, StartDte.ToShortTimeString().Remove(5) + " - " + EndDte.ToShortTimeString().Remove(5)); // get just the time aspect from dates
				SetTVText(tvFinishDate, EndDte.ToShortDateString());
				SetTVText(tvGroup, ((item.targetingGroup != null) ? item.targetingGroup : "No Target"));
				SetTVText(tvPlaces, item.maximum - item.BookingCount);
				SetTVText(tvWaitlist, 0);
					
				btnBook.SetTextColor(Color.White);
				if (btnBook.HasOnClickListeners == false) {
					btnBook.Click += delegate {
						if (tvPlaces.Text.Trim() == "0") //If there are places left in the session
						{
							new AlertDialog.Builder (_activity)
								.SetTitle("Session Full")
								.SetMessage("The session you are attempting to book is currently full, would you like to be added to the waitlist?" +
									" You will be automatically added to the session when a spot becomes available.")
								.SetCancelable(true)
								.SetPositiveButton("Confirm", async delegate(object sender, DialogClickEventArgs e) {
									await RESTClass.PostMakeWaitlist(item.WorkshopId.ToString(), Globals.LoggedStudent.studentID, Globals.LoggedStudent.studentID);
								})
								.Show();
						}
						else
						{
							new AlertDialog.Builder (_activity)
								.SetTitle("Booked")
								.SetMessage("Are you sure you want to book this session?")
								.SetCancelable(true)
								.SetPositiveButton("Confirm", async delegate(object sender, DialogClickEventArgs e) {
									await RESTClass.PostMakeBooking( item.WorkshopId.ToString(), Globals.LoggedStudent.studentID, Globals.LoggedStudent.studentID);
									//create calendar item
//									ContentValues eventValues = new ContentValues ();
//
//									eventValues.Put (CalendarContract.Events.InterfaceConsts.CalendarId,
//										_calId);
//									eventValues.Put (CalendarContract.Events.InterfaceConsts.Title,
//										"Test Event from M4A");
//									eventValues.Put (CalendarContract.Events.InterfaceConsts.Description,
//										"This is an event created from Xamarin.Android");
//									eventValues.Put (CalendarContract.Events.InterfaceConsts.Dtstart,
//										GetDateTimeMS (2011, 12, 15, 10, 0));
//									eventValues.Put (CalendarContract.Events.InterfaceConsts.Dtend,
//										GetDateTimeMS (2011, 12, 15, 11, 0));
//
//									eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, 
//										"UTC");
//									eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, 
//										"UTC");
//
//									var uri = ContentResolver.Insert (CalendarContract.Events.ContentUri,
//										eventValues);alendarContract.Calendars.InterfaceConsts.AccountName
//									};
								})
								.Show();
						}
					};
				}

				return convertView;
			}
			catch (Exception e) 
			{
				throw e;
			}
		}

		public void SetTVText(TextView tv, object value)
		{
			try
			{
				if (value != null) 
				{
					tv.SetText (value.ToString (), TextView.BufferType.Normal);
				}
			}
			catch (Exception e) 
			{
				throw e;
			}
		}

		public override Java.Lang.Object GetGroup (int groupPosition)
		{
			try
			{
				return _lstGroupID [groupPosition];
			}
			catch (Exception e) 
			{
				throw e;
			}
		}

		public override long GetGroupId (int groupPosition)
		{
			try
			{
				return groupPosition;
			}
			catch (Exception e) 
			{
				throw e;
			}
		}

		public override View GetGroupView (int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
		{
			try
			{
				var item = _dictGroup [_lstGroupID [groupPosition]];

				if (convertView == null)
					convertView = _activity.LayoutInflater.Inflate (Resource.Layout.ListControl_Group, null);

				var textBox = convertView.FindViewById<TextView> (Resource.Id.txtLarge);
				textBox.SetText (item.topic, TextView.BufferType.Normal);

				return convertView;
			}
			catch (Exception e) 
			{
				throw e;
			}
		}

		public override bool IsChildSelectable (int groupPosition, int childPosition)
		{
			return true;
		}

		public override int GroupCount 
		{
			get { return _dictGroup.Count; }
		}

		public override bool HasStableIds 
		{
			get { return true; }
		}
	}
}

