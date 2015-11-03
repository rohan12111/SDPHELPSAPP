using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using System.Threading.Tasks;
using System.Globalization;

namespace HELPSMobileFrontEnd
{
    public class ViewBookingsFragment : Fragment
    {
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			return inflater.Inflate(Resource.Layout.SlidingFragmentLayout, container, false);

			Button btnSave = this.Activity.FindViewById<Button> (Resource.Id.btnSave);
			btnSave.Visibility = ViewStates.Gone;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
			mViewPager.Adapter = new ViewBookingsAdapter(this.Activity);

            mSlidingTabScrollView.ViewPager = mViewPager;
        }

        public class ViewBookingsAdapter : PagerAdapter
        {
			List<string> items = new List<string> ();
			Activity parent;
			Dictionary<string, WorkshopBooking> dictGroupCurr = new Dictionary<string, WorkshopBooking> ();
			Dictionary<string, WorkshopBooking> dictGroupPast = new Dictionary<string, WorkshopBooking> ();
			List<string> lstKeysCurr = new List<string> ();
			List<string> lstKeysPast = new List<string> ();

			FrameLayout flCurrentBookings;
			FrameLayout flNoCurrentBookings ;
			ExpandableListView elvCurrentBookings;
			FrameLayout flPastBookings;
			FrameLayout flNoPastBookings;
			ExpandableListView elvPastBookings;

			public ViewBookingsAdapter(Activity parentIn) : base()
            {
				parent = parentIn;
                items.Add("Current");
                items.Add("Past");
            }

            public override int Count
            {
                get { return items.Count; }
            }

            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
				try
				{
					View view;

					Int32 lastExpandedPosition = -1;

					CreateExpandableListData ();

					if (position == 0) //Current Booking tab
					{ 
						view = LayoutInflater.From (container.Context).Inflate (Resource.Layout.CurrentBookingLayout, container, false);
						container.AddView (view);

						flCurrentBookings = container.FindViewById<FrameLayout> (Resource.Id.flCurrentBookings);
						flNoCurrentBookings = container.FindViewById<FrameLayout> (Resource.Id.flNoCurrentBookings);
						elvCurrentBookings = container.FindViewById<ExpandableListView> (Resource.Id.elvCurrentBookings);

						if (dictGroupCurr.Count > 0)
						{
							flNoCurrentBookings.Visibility = ViewStates.Gone;
							flCurrentBookings.Visibility = ViewStates.Visible;

							elvCurrentBookings.SetAdapter (new ExpandViewBookingAdapter (parent, dictGroupCurr));

							elvCurrentBookings.ChildClick += delegate(object sender, ExpandableListView.ChildClickEventArgs e) {
								string itmGroup = lstKeysCurr [e.GroupPosition];
								WorkshopBooking itmChild = dictGroupCurr [itmGroup];
							};

							elvCurrentBookings.GroupExpand += delegate(object sender, ExpandableListView.GroupExpandEventArgs e) {
								if (lastExpandedPosition != -1 && e.GroupPosition != lastExpandedPosition) {
									elvCurrentBookings.CollapseGroup (lastExpandedPosition);
								}

								lastExpandedPosition = e.GroupPosition;
							};
						}
						else
						{
							flNoCurrentBookings.Visibility = ViewStates.Visible;
							flCurrentBookings.Visibility = ViewStates.Gone;
						}
					}
					else //Past Bookings
					{
						view = LayoutInflater.From (container.Context).Inflate (Resource.Layout.PastBookings, container, false);
						container.AddView (view);

						flPastBookings = container.FindViewById<FrameLayout> (Resource.Id.flPastBookings);
						flNoPastBookings = container.FindViewById<FrameLayout> (Resource.Id.flNoPastBookings);
						elvPastBookings = container.FindViewById<ExpandableListView> (Resource.Id.elvPastBookings);

						if (dictGroupPast.Count > 0) 
						{
							flNoPastBookings.Visibility = ViewStates.Gone;
							flPastBookings.Visibility = ViewStates.Visible;
							elvPastBookings.SetAdapter (new ExpandViewBookingAdapter (parent, dictGroupPast));

							elvPastBookings.ChildClick += delegate(object sender, ExpandableListView.ChildClickEventArgs e) {
								string itmGroup = lstKeysPast [e.GroupPosition];
								WorkshopBooking itmChild = dictGroupPast [itmGroup];
							};

							elvPastBookings.GroupExpand += delegate(object sender, ExpandableListView.GroupExpandEventArgs e) {
								if (lastExpandedPosition != -1 && e.GroupPosition != lastExpandedPosition) {
									elvPastBookings.CollapseGroup (lastExpandedPosition);
								}

								lastExpandedPosition = e.GroupPosition;
							};
						}
						else 
						{
							flNoPastBookings.Visibility = ViewStates.Visible;
							flPastBookings.Visibility = ViewStates.Gone;
						}
					}

	                return view;
				}
				catch (Exception e) 
				{
					ErrorHandling.LogError (e, container.Context);
					return -1;
				}
            }

			public void CreateExpandableListData ()
			{
				try
				{
					dictGroupCurr = new Dictionary<string, WorkshopBooking> ();
					dictGroupPast = new Dictionary<string, WorkshopBooking> ();
					lstKeysCurr = new List<string> ();
					lstKeysPast = new List<string> ();

					foreach (WorkshopBooking book in Globals.StuBookings)
					{
						AddListItem(book);
					}

					lstKeysCurr = new List<string> (dictGroupCurr.Keys);
					lstKeysPast = new List<string> (dictGroupPast.Keys);
				}
				catch (Exception e)
				{
					throw e;
				}
			}

			public void AddListItem(WorkshopBooking book)
			{
				try
				{
					if (DateTime.ParseExact(book.ending, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture) < DateTime.Now) //Past
					{
						if (!dictGroupPast.ContainsKey (book.BookingId.ToString())) 
						{
							dictGroupPast.Add (book.BookingId.ToString(), book);
						} 
						else
						{
							dictGroupPast.Add ((book.BookingId + book.studentID).ToString(), book);
						}
					}
					else
					{
						if (!dictGroupCurr.ContainsKey (book.BookingId.ToString())) 
						{
							dictGroupCurr.Add (book.BookingId.ToString(), book);
						} 
						else
						{
							dictGroupCurr.Add ((book.BookingId + book.studentID).ToString(), book);
						}
					}

				}
				catch (Exception e) 
				{
					throw e;
				}
			}

			public async Task<List<WorkshopBooking>>GetBookings()
			{
				return await RESTClass.GetWorkshopBookings ("active=true&studentId=" + Globals.LoggedStudent);
			}

            public string GetHeaderTitle (int position)
            {
                return items[position];
            }

            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
            {
                container.RemoveView((View)obj);
            }
        }
    }
}