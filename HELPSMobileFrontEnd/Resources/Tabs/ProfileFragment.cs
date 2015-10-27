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
				items.Add("Contact");
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

					TextView txtStudentName = view.FindViewById<TextView>(Resource.Id.txtStudentName);
					EditText tbStudentName = view.FindViewById<EditText>(Resource.Id.tbStudentName);
					EditText tbPrefferedName = view.FindViewById<EditText>(Resource.Id.tbPrefferedName);
					TextView txtDOB = view.FindViewById<TextView>(Resource.Id.txtDOB);
					EditText tbDOB = view.FindViewById<EditText>(Resource.Id.tbDOB);
					RadioGroup rgGender = view.FindViewById<RadioGroup>(Resource.Id.rgGender);
					RadioGroup rgStatus = view.FindViewById<RadioGroup>(Resource.Id.rgStatus);
					Spinner ddlFirstLanguage = view.FindViewById<Spinner>(Resource.Id.ddlFirstLanguage);
					Spinner ddlCountryOfOrigin = view.FindViewById<Spinner>(Resource.Id.ddlCountryOfOrigin);

					List<string> languageList = new List<string>() {"Nepal", "USA", "Australia", "UK"};

					ArrayAdapter<String> adapter = new ArrayAdapter<String>(view.Context, Android.Resource.Layout.SimpleDropDownItem1Line, languageList);

					ddlFirstLanguage.Adapter = adapter;
					ddlCountryOfOrigin.Adapter = adapter;

//					String preActivity = container.ToString ();//Intent.GetStringExtra("PreviousActivity");

					if (!String.IsNullOrWhiteSpace(Globals.StuName))
					{
						tbStudentName.Visibility = ViewStates.Gone;
						tbDOB.Visibility = ViewStates.Gone;

						txtStudentName.Visibility = ViewStates.Visible;
						txtDOB.Visibility = ViewStates.Visible;

						txtStudentName.Text = Globals.StuName;
						tbPrefferedName.Text = Globals.LoggedStudent.preferred_name;
						txtDOB.Text = Globals.LoggedStudent.dob;
						if (Globals.LoggedStudent.gender.Trim() == "M")
							rgGender.Check(Resource.Id.rbMale);
						else
							rgGender.Check(Resource.Id.rbFemale);
						
						if (Globals.LoggedStudent.status.Trim() == "International")
							rgStatus.Check(Resource.Id.rbInternational);
						else
							rgStatus.Check(Resource.Id.rbPermanent);
						//Need to do language and Country;
					}
					else
					{
						tbStudentName.Visibility = ViewStates.Visible;
						tbDOB.Visibility = ViewStates.Visible;

						txtStudentName.Visibility = ViewStates.Gone;
						txtDOB.Visibility = ViewStates.Gone;
					}
				}
				else if(position == 1) //Contact tab
				{
					view = LayoutInflater.From (container.Context).Inflate (Resource.Layout.Contact, container, false);
					container.AddView (view);

					TextView txtEmail = view.FindViewById<TextView>(Resource.Id.txtEmail);
					EditText tbEmail = view.FindViewById<EditText>(Resource.Id.tbEmail);
					TextView txtMobile = view.FindViewById<TextView>(Resource.Id.txtMobile);
					EditText tbMobile = view.FindViewById<EditText>(Resource.Id.tbMobile);
					EditText tbNumber = view.FindViewById<EditText>(Resource.Id.tbNumber);

					if (!String.IsNullOrWhiteSpace (Globals.StuName)) 
					{
						tbEmail.Visibility = ViewStates.Gone;
						tbMobile.Visibility = ViewStates.Gone;
						txtEmail.Visibility = ViewStates.Visible;
						txtMobile.Visibility = ViewStates.Visible;

						txtEmail.Text = Globals.StuEmail;
						txtMobile.Text = Globals.StuMobile;
						tbNumber.Text = Globals.LoggedStudent.alternative_contact;
					}
					else
					{
						tbEmail.Visibility = ViewStates.Visible;
						tbMobile.Visibility = ViewStates.Visible;
						txtEmail.Visibility = ViewStates.Gone;
						txtMobile.Visibility = ViewStates.Gone;
					}
				}
				else if(position == 2) //Course tab
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

