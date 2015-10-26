using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using System.Runtime.Serialization;
using Android.Views.InputMethods;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "UTS: HELPS", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]
	public class LoginActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			string userId = "";
			string password = "";


			try
			{

				base.OnCreate (bundle);
				SetContentView (Resource.Layout.Login);
				TextView txtmessage = FindViewById<TextView>(Resource.Id.txtmessage);
				Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
				EditText tbStudentID = FindViewById<EditText>(Resource.Id.tbStudentID);
				EditText tbPassword = FindViewById<EditText>(Resource.Id.tbPassword);
				LinearLayout llRoot = FindViewById<LinearLayout>(Resource.Id.llRoot);


				txtmessage.Visibility = ViewStates.Invisible;
				tbStudentID.Text = userId;
				tbPassword.Text = password;


				llRoot.Click += delegate {
					//Dismiss Keybaord
					InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
					imm.HideSoftInputFromWindow(tbStudentID.WindowToken, 0);
					btnLogin.RequestFocus();
				};

				btnLogin.Click += async delegate {
					userId = tbStudentID.Text;
					if (tbStudentID.Text =="" || tbPassword.Text =="")
					{
						txtmessage.Text = "Please Supply the Student ID and Password!!!";
						txtmessage.Visibility = ViewStates.Visible;

					}
					else
					{
						StartActivity(new Intent(this, typeof(MainMenuActivity)));
						/*
						Student student = await RESTClass.GetStudent(userId);
						if (tbStudentID.Text == student.studentID)
						{
							StartActivity(new Intent(this, typeof(MainMenuActivity)));
						}
						else
						{
							txtmessage.Text = "Supplied student Id does not exist!!!";
							txtmessage.Visibility = ViewStates.Visible;
						}
						*/

					}

				};
			}
			catch (Exception e) 
			{
				ErrorHandling.LogError (e, this);
			}
		}
	}
}
