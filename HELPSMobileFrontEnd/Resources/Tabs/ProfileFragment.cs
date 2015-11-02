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
		private ViewGroup container;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup containerIn, Bundle savedInstanceState)
		{
			this.container = containerIn;
			return inflater.Inflate(Resource.Layout.SlidingFragmentLayout, containerIn, false);
		}

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
			mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
			mViewPager.Adapter = new ProfileAdapter(view);

			mSlidingTabScrollView.ViewPager = mViewPager;
		}

		public class ProfileAdapter : PagerAdapter
		{
			List<string> items = new List<string>();
			View parentView;

			TextView txtStudentName;
			EditText tbStudentName;
			EditText tbPrefferedName;
			TextView txtDOB;	
			EditText tbDOB;
			RadioGroup rgGender;
			RadioButton rbMale;
			RadioButton rbFemale;
			RadioGroup rgStatus;
			RadioButton rbPermanent;
			RadioButton rbInternational;
			Spinner ddlFirstLanguage;
			Spinner ddlCountryOfOrigin;
			TextView txtEmail;
			EditText tbEmail;
			TextView txtMobile;
			EditText tbMobile;
			EditText tbNumber;
			TextView txtCourse;
			EditText tbCourse;
			TextView txtFaculty;
			EditText tbFaculty;
			RadioGroup rgDegree;
			RadioButton rbUndergraduate;
			RadioButton rbPostgraduate;
			Spinner ddlYear;
			CheckBox cbHSC;
			CheckBox cbIELTS;
			CheckBox cbTOEFL;
			CheckBox cbTAFE;
			CheckBox cbCULT;
			CheckBox cbDEEP;
			CheckBox cbInDiploma;
			CheckBox cbFoundationCourse;
			EditText tbOther;

			public ProfileAdapter(View view) : base()
			{
				parentView = view;
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

					txtStudentName = view.FindViewById<TextView>(Resource.Id.txtStudentName);
					tbStudentName = view.FindViewById<EditText>(Resource.Id.tbStudentName);
					tbPrefferedName = view.FindViewById<EditText>(Resource.Id.tbPrefferedName);
					txtDOB = view.FindViewById<TextView>(Resource.Id.txtDOB);
					tbDOB = view.FindViewById<EditText>(Resource.Id.tbDOB);
					rgGender = view.FindViewById<RadioGroup>(Resource.Id.rgGender);
					rbMale = view.FindViewById<RadioButton>(Resource.Id.rbMale);
					rbFemale = view.FindViewById<RadioButton>(Resource.Id.rbFemale);
					rgStatus = view.FindViewById<RadioGroup>(Resource.Id.rgStatus);
					rbPermanent = view.FindViewById<RadioButton>(Resource.Id.rbPermanent);
					rbInternational = view.FindViewById<RadioButton>(Resource.Id.rbInternational);
					ddlFirstLanguage = view.FindViewById<Spinner>(Resource.Id.ddlFirstLanguage);
					ddlCountryOfOrigin = view.FindViewById<Spinner>(Resource.Id.ddlCountryOfOrigin);

					List<string> languageList = new List<string>() {"Nepal", "USA", "Australia", "UK"};

					ArrayAdapter<String> adapter = new ArrayAdapter<String>(view.Context, Android.Resource.Layout.SimpleDropDownItem1Line, languageList);

					ddlFirstLanguage.Adapter = adapter;
					ddlCountryOfOrigin.Adapter = adapter;

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

					txtEmail = view.FindViewById<TextView>(Resource.Id.txtEmail);
					tbEmail = view.FindViewById<EditText>(Resource.Id.tbEmail);
					txtMobile = view.FindViewById<TextView>(Resource.Id.txtMobile);
					tbMobile = view.FindViewById<EditText>(Resource.Id.tbMobile);
					tbNumber = view.FindViewById<EditText>(Resource.Id.tbNumber);

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

					txtCourse = view.FindViewById<TextView>(Resource.Id.txtCourse);
					tbCourse = view.FindViewById<EditText>(Resource.Id.tbCourse);
					txtFaculty = view.FindViewById<TextView>(Resource.Id.txtFaculty);
					tbFaculty = view.FindViewById<EditText>(Resource.Id.tbFaculty);
					rgDegree = view.FindViewById<RadioGroup>(Resource.Id.rgDegree);
					rbUndergraduate = view.FindViewById<RadioButton>(Resource.Id.rbUndergraduate);
					rbPostgraduate = view.FindViewById<RadioButton>(Resource.Id.rbPostgraduate);
					ddlYear = view.FindViewById<Spinner>(Resource.Id.ddlYear);

					if (!String.IsNullOrWhiteSpace (Globals.StuName)) 
					{
						tbCourse.Visibility = ViewStates.Gone;
						tbFaculty.Visibility = ViewStates.Gone;
						txtCourse.Visibility = ViewStates.Visible;
						txtFaculty.Visibility = ViewStates.Visible;

						txtCourse.Text = Globals.StuCourse;
						txtFaculty.Text = Globals.StuFaculty;
					}
					else
					{
						tbCourse.Visibility = ViewStates.Visible;
						tbFaculty.Visibility = ViewStates.Visible;
						txtCourse.Visibility = ViewStates.Gone;
						txtFaculty.Visibility = ViewStates.Gone;
					}
				}
				else //Education tab
				{
					view = LayoutInflater.From (container.Context).Inflate (Resource.Layout.Education, container, false);
					container.AddView (view);

					cbHSC = view.FindViewById<CheckBox>(Resource.Id.cbHSC);
					cbIELTS = view.FindViewById<CheckBox>(Resource.Id.cbIELTS);
					cbTOEFL = view.FindViewById<CheckBox>(Resource.Id.cbTOEFL);
					cbTAFE = view.FindViewById<CheckBox>(Resource.Id.cbTAFE);
					cbCULT = view.FindViewById<CheckBox>(Resource.Id.cbCULT);
					cbDEEP = view.FindViewById<CheckBox>(Resource.Id.cbDEEP);
					cbInDiploma = view.FindViewById<CheckBox>(Resource.Id.cbInDiploma);
					cbFoundationCourse = view.FindViewById<CheckBox>(Resource.Id.cbFoundationCourse);
					tbOther = view.FindViewById<EditText>(Resource.Id.tbOther);
				}

				Button btnSave;

				btnSave = parentView.FindViewById<Button> (Resource.Id.btnSave);
				if (btnSave.HasOnClickListeners == false) 
				{
					btnSave.Click += btnClickHandler;
				}

				return view;
			}

			async void btnClickHandler (object sender, EventArgs args)
			{
				Student tempStud = new Student ();

				if (String.IsNullOrWhiteSpace (Globals.StuName)) 
				{
					Globals.StuName = tbStudentName.Text;
					tempStud.dob = tbDOB.Text;
					Globals.StuEmail = tbEmail.Text;
					Globals.StuMobile = tbMobile.Text;
					Globals.StuCourse = tbCourse.Text;
					Globals.StuFaculty = tbFaculty.Text;
				}

				tempStud.preferred_name = tbPrefferedName.Text;
				if (rbMale.Selected == true)
					tempStud.gender = "M";
				else
					tempStud.gender = "F";
				if (rbPermanent.Selected == true)
					tempStud.status = "Permanent";
				else
					tempStud.status = "International";
				//FirstLanguage
				//Country of Origin
				tempStud.alternative_contact = tbNumber.Text;
				if (rbUndergraduate.Selected == true)
					tempStud.degree = "UG";
				else
					tempStud.degree = "PG";
				Globals.StuYear = ddlYear.SelectedItem.ToString();
				tempStud.HSC = (cbHSC.Checked) ? "true" : "false";
				tempStud.IELTS = cbIELTS.Checked ? "true" : "false";
				tempStud.TOEFL = cbTOEFL.Checked ? "true" : "false";
				tempStud.TAFE = cbTAFE.Checked ? "true" : "false";
				tempStud.CULT = cbCULT.Checked ? "true" : "false";
				tempStud.InsearchDEEP = cbDEEP.Checked ? "true" : "false";
				tempStud.InsearchDiploma = cbInDiploma.Checked ? "true" : "false";
				tempStud.foundationcourse = cbFoundationCourse.Checked ? "true" : "false";
				Globals.StuOther = tbOther.Text;

				await Globals.SaveProfile (tempStud);

				new AlertDialog.Builder (parentView.Context)
					.SetTitle("Saved")
					.SetMessage("Data Saved!")
					.Show();
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
