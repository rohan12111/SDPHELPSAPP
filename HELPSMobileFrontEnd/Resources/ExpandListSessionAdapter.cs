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
using HELPSMobileFrontEnd;
using System.Globalization;
using Android.Graphics;


namespace ExpendListBox
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
				{ //07/31/2012 17:00:00
					StartDte = DateTime.ParseExact(item.StartDate, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
				}

				if (item.EndDate != null) 
				{
					EndDte = DateTime.ParseExact(item.EndDate, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
				}

				SetTVText(tvRoom, item.campus);
				SetTVText(tvStartDate, StartDte.ToShortDateString());
				SetTVText(tvTime, StartDte.ToShortTimeString().Remove(5) + " - " + EndDte.ToShortTimeString().Remove(5));
				SetTVText(tvFinishDate, EndDte.ToShortDateString());
				SetTVText(tvGroup, ((item.targetingGroup != null) ? item.targetingGroup : "No Target"));
				SetTVText(tvPlaces, item.maximum - item.BookingCount);
				SetTVText(tvWaitlist, -1);
					
				btnBook.SetTextColor(Color.White);
				btnBook.Click += delegate {
					if (item.maximum - item.BookingCount > 0) 
					{
						new AlertDialog.Builder ()
							.SetMessage(e.Message + "\n" + e.StackTrace)
							.SetTitle("Application Error")
							.Show();
					}
					else
					{
						
					}
				};

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

