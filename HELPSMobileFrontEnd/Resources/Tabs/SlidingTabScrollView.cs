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
using Android.Support.V4.View;
using Android.Util;
using Android.Graphics;

namespace HELPSMobileFrontEnd
{
    public class SlidingTabScrollView : HorizontalScrollView
    {
//        private int mTabViewLayoutID;
//        private int mTabViewTextViewID;

        private const int TITLE_OFFSET_DIPS = 10;
        private const int TAB_VIEW_PADDING_DIPS = 14;
        private const int TAB_VIEW_TEXT_SIZE_DIPS = 14;

        private int mTitleOffset;

        private ViewPager mViewPager;
        private ViewPager.IOnPageChangeListener mViewPagerPageChangeListener;

        private static SlidingTabStrip mTabStrip;

        private int mScrollState;

        public interface TabColorizer
        {
            int GetIndicatorColor(int position);
            int GetDividerColor(int position);
        }

        public SlidingTabScrollView(Context context) : this(context, null) { }

        public SlidingTabScrollView(Context context, IAttributeSet attrs) : this(context, attrs, 0) { }

        public SlidingTabScrollView (Context context, IAttributeSet attrs, int defaultStyle) : base(context, attrs, defaultStyle)
        {
            //Disable the scroll bar
            HorizontalScrollBarEnabled = false;

            //Make sure the tab strips fill the view
            FillViewport = true;
            this.SetBackgroundColor(Android.Graphics.Color.Rgb(0x19, 0x76, 0xd2)); //blueish color

            mTitleOffset = (int)(TITLE_OFFSET_DIPS * Resources.DisplayMetrics.Density);

            mTabStrip = new SlidingTabStrip(context);
			this.AddView(mTabStrip, LayoutParams.MatchParent, LayoutParams.MatchParent);
        }

        public TabColorizer CustomTabColorizer
        {
            set { mTabStrip.CustomTabColorizer = value; }
        }

        public int [] SelectedIndicatorColor
        {
            set { mTabStrip.SelectedIndicatorColors = value; }
        }

        public int [] DividerColors
        {
            set { mTabStrip.DividerColors = value; }
        }

        public ViewPager.IOnPageChangeListener OnPageListener
        {
            set { mViewPagerPageChangeListener = value; }
        }

        public ViewPager ViewPager
        {
            set
            {
                mTabStrip.RemoveAllViews();

                mViewPager = value;
                if (value != null)
                {
                    value.PageSelected += value_PageSelected;
                    value.PageScrollStateChanged += value_PageScrollStateChanged;
                    value.PageScrolled += value_PageScrolled;
                    PopulateTabStrip();
                }
            }
        }

        void value_PageScrolled(object sender, ViewPager.PageScrolledEventArgs e)
        {
            int tabCount = mTabStrip.ChildCount;

            if ((tabCount == 0) || (e.Position < 0) || (e.Position >= tabCount))
            {
                //if any of these conditions apply, return, no need to scroll
                return;
            }

            mTabStrip.OnViewPagerPageChanged(e.Position, e.PositionOffset);

            View selectedTitle = mTabStrip.GetChildAt(e.Position);

            int extraOffset = (selectedTitle != null ? (int)(e.Position * selectedTitle.Width) : 0);

            ScrollToTab(e.Position, extraOffset);

            if (mViewPagerPageChangeListener != null)
            {
                mViewPagerPageChangeListener.OnPageScrolled(e.Position, e.PositionOffset, e.PositionOffsetPixels);
            }

        }

        void value_PageScrollStateChanged(object sender, ViewPager.PageScrollStateChangedEventArgs e)
        {
            mScrollState = e.State;

            if (mViewPagerPageChangeListener != null)
            {
                mViewPagerPageChangeListener.OnPageScrollStateChanged(e.State);
            }
        }

        void value_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            if (mScrollState == ViewPager.ScrollStateIdle)
            {
                mTabStrip.OnViewPagerPageChanged(e.Position, 0f);
                ScrollToTab(e.Position, 0);

            }

            if (mViewPagerPageChangeListener != null)
            {
                mViewPagerPageChangeListener.OnPageSelected(e.Position);
            }
        }

        private void PopulateTabStrip()
        {
            PagerAdapter adapter = mViewPager.Adapter;
            
            for (int i = 0; i < adapter.Count; i++)
            {
                TextView tabView = CreateDefaultTabView(Context);
				if (adapter.Count == 2) 
				{
					tabView.Text = ((ViewBookingsFragment.ViewBookingsAdapter)adapter).GetHeaderTitle (i);
				}
				else 
				{
					tabView.Text = ((ProfileAdapter)adapter).GetHeaderTitle (i);
				}
				tabView.SetTextColor(new Color(0xFF, 0xFF, 0xFF, 0x73)); //tab text colour

                tabView.Tag = i;
                tabView.Click += tabView_Click;

				if (adapter.Count > 3) 
				{
					tabView.SetWidth ((tabView.Text.Length + 1) * 35);
				}
				else
				{	
					tabView.SetWidth (Resources.DisplayMetrics.WidthPixels / adapter.Count); //width of tabs
				}

                mTabStrip.AddView(tabView);
            }
        }

		static public void FadeTabs(int inPos, float inPosOffset)
		{
			for (int i = 0; i < mTabStrip.ChildCount; i++) 
			{
				TextView tabView = (TextView)mTabStrip.GetChildAt (i);

				if (i == inPos && inPosOffset == 0) 
				{
					tabView.SetTextColor(new Color(0xFF, 0xFF, 0xFF, 0xFF));
				} 
				else
				{
					tabView.SetTextColor(new Color(0xFF, 0xFF, 0xFF, 0xAA));
				}
			}
		}

        void tabView_Click(object sender, EventArgs e)
        {
            TextView clickTab = (TextView)sender;
            int pageToScrollTo = (int)clickTab.Tag;
            mViewPager.CurrentItem = pageToScrollTo;
        }

        private TextView CreateDefaultTabView(Android.Content.Context context)
        {
            TextView textView = new TextView(context);
            textView.Gravity = GravityFlags.Center;
			textView.SetTextSize(ComplexUnitType.Dip, TAB_VIEW_TEXT_SIZE_DIPS); //Tab font size
            textView.Typeface = Android.Graphics.Typeface.Default;

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Honeycomb)
            {
                TypedValue outValue = new TypedValue();
                Context.Theme.ResolveAttribute(Android.Resource.Attribute.SelectableItemBackground, outValue, false);
                textView.SetBackgroundResource(outValue.ResourceId);
            }

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.IceCreamSandwich)
            {
                textView.SetAllCaps(true);
            }

            int padding = (int)(TAB_VIEW_PADDING_DIPS * Resources.DisplayMetrics.Density);
            textView.SetPadding(padding, padding, padding, padding);

            return textView;
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();

            if (mViewPager != null)
            {
                ScrollToTab(mViewPager.CurrentItem, 0);
            }
        }

        private void ScrollToTab(int tabIndex, int extraOffset)
        {
            int tabCount = mTabStrip.ChildCount;

            if (tabCount == 0 || tabIndex < 0 || tabIndex >= tabCount)
            {
               //No need to go further, dont scroll
                return;
            }

            View selectedChild = mTabStrip.GetChildAt(tabIndex);
            if (selectedChild != null)
            {
                int scrollAmountX = selectedChild.Left + extraOffset;

                if (tabIndex >0 || extraOffset > 0)
                {
                    scrollAmountX -= mTitleOffset;
                }

                this.ScrollTo(scrollAmountX, 0);
            }

        }

    }
}