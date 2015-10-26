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

namespace HELPSMobileFrontEnd
{
    public class SlidingTabsFragment : Fragment
    {
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			return inflater.Inflate(Resource.Layout.SlidingFragmentLayout, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            mViewPager.Adapter = new ViewBookingsAdapter();

            mSlidingTabScrollView.ViewPager = mViewPager;
        }

        public class ViewBookingsAdapter : PagerAdapter
        {
            List<string> items = new List<string>();

            public ViewBookingsAdapter() : base()
            {
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
				View view;
				if (position == 0) //Current Booking tab
				{ 
					view = LayoutInflater.From (container.Context).Inflate (Resource.Layout.CurrentBookings, container, false);
					container.AddView (view);
				}
				else //Past Bookings
				{
					view = LayoutInflater.From (container.Context).Inflate (Resource.Layout.PastBookings, container, false);
					container.AddView (view);
				}

                return view;
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