
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
		private List<string> languageList;
		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);

				SetContentView (Resource.Layout.Profile);

				TextView name = FindViewById<TextView>(Resource.Id.lblFirstName);
				EditText preferredName = FindViewById<EditText>(Resource.Id.txtName);
				TextView birthday = FindViewById<TextView>(Resource.Id.lbldateofbirth);
				RadioButton male= FindViewById<RadioButton>(Resource.Id.male);
				RadioButton fales= FindViewById<RadioButton>(Resource.Id.female);
				EditText contactNumber = FindViewById<EditText>(Resource.Id.txtcontactnumber);
				RadioButton permanent = FindViewById<RadioButton>(Resource.Id.permanent);
				RadioButton international= FindViewById<RadioButton>(Resource.Id.international);
				Spinner languange= FindViewById<Spinner>(Resource.Id.ddllanguage);
				Spinner country= FindViewById<Spinner>(Resource.Id.ddlcountry);

				name.Text = "Cyon";
				birthday.Text= "01/01/1995";

				languageList = new List<string>();
				languageList.Add("Nepal");
				languageList.Add("USA");
				languageList.Add("Australia");
				languageList.Add("UK");
				ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,Android.Resource.Layout.SimpleDropDownItem1Line,languageList);
				languange.Adapter = adapter;


				country.Adapter = adapter;
			//	Student student = await RESTClass.GetStudent("00000000");

	   			


			//	name.Text= student.
//				throw new Exception(student.status + student.country_origin);
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