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
using Android.Util;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "Profile", ScreenOrientation = ScreenOrientation.Portrait, Theme="@style/ActionBarTheme")]			
	public class ProfileActivity : Activity
	{
//		private List<string> languageList;

		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);
//				SetContentView (Resource.Layout.Profile);
				SetContentView (Resource.Layout.TabsLayout);

				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				ProfileFragment sltFragment = new ProfileFragment();
				transaction.Replace(Resource.Id.flTabs, sltFragment);
				transaction.Commit();

//				TextView txtStudentName = FindViewById<TextView>(Resource.Id.txtStudentName);
//				EditText tbStudentName = FindViewById<EditText>(Resource.Id.tbStudentName);
//				EditText tbPrefferedName = FindViewById<EditText>(Resource.Id.tbPrefferedName);
//				TextView txtDOB = FindViewById<TextView>(Resource.Id.txtDOB);
//				EditText tbDOB = FindViewById<EditText>(Resource.Id.tbDOB);
//				RadioGroup rgGender = FindViewById<RadioGroup>(Resource.Id.rgGender);
//				TextView txtEmail = FindViewById<TextView>(Resource.Id.txtEmail);
//				EditText tbEmail = FindViewById<EditText>(Resource.Id.tbEmail);
//				TextView txtMobile = FindViewById<TextView>(Resource.Id.txtMobile);
//				EditText tbMobile = FindViewById<EditText>(Resource.Id.tbMobile);
//				EditText tbNumber = FindViewById<EditText>(Resource.Id.tbNumber);
//				RadioGroup rgStatus = FindViewById<RadioGroup>(Resource.Id.rgStatus);
//				Spinner ddlFirstLanguage = FindViewById<Spinner>(Resource.Id.ddlFirstLanguage);
//				Spinner ddlCountryOfOrigin = FindViewById<Spinner>(Resource.Id.ddlCountryOfOrigin);
//
//				languageList = new List<string>() {"Nepal", "USA", "Australia", "UK"};
//
//				ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleDropDownItem1Line, languageList);
//
//				ddlFirstLanguage.Adapter = adapter;
//				ddlCountryOfOrigin.Adapter = adapter;
//
//				String preActivity = this.Intent.GetStringExtra("PreviousActivity");
//
//				if (preActivity == "Main")
//				{
//					tbStudentName.Visibility = ViewStates.Gone;
//					tbDOB.Visibility = ViewStates.Gone;
//					tbEmail.Visibility = ViewStates.Gone;
//					tbMobile.Visibility = ViewStates.Gone;
//
//					txtStudentName.Visibility = ViewStates.Visible;
//					txtDOB.Visibility = ViewStates.Visible;
//					txtEmail.Visibility = ViewStates.Visible;
//					txtMobile.Visibility = ViewStates.Visible;
//
//					txtStudentName.Text = Globals.StuName;
//					tbPrefferedName.Text = Globals.LoggedStudent.preferred_name;
//					txtDOB.Text = Globals.LoggedStudent.dob;
//					if (Globals.LoggedStudent.gender.Trim() == "M")
//						rgGender.Check(Resource.Id.rbMale);
//					else
//						rgGender.Check(Resource.Id.rbFemale);
//					txtEmail.Text = Globals.StuEmail;
//					txtMobile.Text = Globals.StuMobile;
//					tbNumber.Text = Globals.LoggedStudent.alternative_contact;
//					if (Globals.LoggedStudent.status.Trim() == "International")
//						rgStatus.Check(Resource.Id.rbInternational);
//					else
//						rgStatus.Check(Resource.Id.rbPermanent);
//					//Need to do language and Country;
//				}
//				else
//				{
//					tbStudentName.Visibility = ViewStates.Visible;
//					tbDOB.Visibility = ViewStates.Visible;
//					tbEmail.Visibility = ViewStates.Visible;
//					tbMobile.Visibility = ViewStates.Visible;
//
//					txtStudentName.Visibility = ViewStates.Gone;
//					txtDOB.Visibility = ViewStates.Gone;
//					txtEmail.Visibility = ViewStates.Gone;
//					txtMobile.Visibility = ViewStates.Gone;
//				}
			}
			catch (Exception e) 
			{
				new AlertDialog.Builder (this)
					.SetMessage(e.Message + "\n\n" + e.StackTrace)
					.SetTitle("Application Error")
					.Show();
			}
		}
	}
}