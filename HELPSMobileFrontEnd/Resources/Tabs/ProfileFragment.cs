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
	public class ProfileFragment : Fragment
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
			mViewPager.Adapter = new ProfileAdapter();

			mSlidingTabScrollView.ViewPager = mViewPager;
		}

		public class ProfileAdapter : PagerAdapter
		{
			List<string> items = new List<string>();

			public ProfileAdapter() : base()
			{
				items.Add("Profile");
				items.Add("Course");
				items.Add("Education");
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
				if (position == 0) //Profile tab
				{ 
					view = LayoutInflater.From (container.Context).Inflate (Resource.Layout.Profile, container, false);
					container.AddView (view);
				}
				else if(position == 1) //Course tab
				{
					view = LayoutInflater.From (container.Context).Inflate (Resource.Layout.Course, container, false);
					container.AddView (view);
				}
				else //Education tab
				{
					view = LayoutInflater.From (container.Context).Inflate (Resource.Layout.Education, container, false);
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

