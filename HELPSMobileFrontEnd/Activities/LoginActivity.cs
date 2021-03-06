﻿using System;
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
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "UTS: HELPS", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/ActionBarTheme")]
	public class LoginActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			string userId = "";
			string password = "";
			ProgressDialog ProgressDialogLogin = null;

			try
			{
				base.OnCreate (bundle);
				SetContentView (Resource.Layout.Login);

				Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
				EditText tbStudentID = FindViewById<EditText>(Resource.Id.tbStudentID);
				EditText tbPassword = FindViewById<EditText>(Resource.Id.tbPassword);
				LinearLayout llRoot = FindViewById<LinearLayout>(Resource.Id.llRoot);
				TextView txtmessage = FindViewById<TextView>(Resource.Id.txtmessage);

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
					try
					{
						userId = tbStudentID.Text;
									
						if (String.IsNullOrWhiteSpace(tbStudentID.Text) || String.IsNullOrWhiteSpace(tbPassword.Text))
						{
							txtmessage.Text = "Please enter your Student Id and Password.";
							txtmessage.Visibility = ViewStates.Visible;
						}
						else
						{
							if (ProgressDialogLogin == null)
							{
								ProgressDialogLogin = ProgressDialog.Show(this, "", "Logging In...");
							
								Student student = await RESTClass.GetStudent(userId);

								if (student != null)
								{
									Globals.LoggedStudent = student;
											
									if (Globals.StudentExists(student.studentID)) //Finds out if the studentid is in the text file and fills out globals if true
									{
										Globals.IsNewStudent = false;
										Globals.SetGlobalVars(student.studentID);
										StartActivity(new Intent(this, typeof(MainMenuActivity)));
									}
									else
									{
										Globals.IsNewStudent = true;
										StartActivity(new Intent(this, typeof(ProfileActivity)));
									}
								}
								else
								{
									txtmessage.Text = "This is not a vaid UTS ID.";
									txtmessage.Visibility = ViewStates.Visible;
								}

								if (ProgressDialogLogin != null) 
								{
									ProgressDialogLogin.Dismiss ();
									ProgressDialogLogin = null;
								}
							}
						}
					}
					catch (Exception e) 
					{
						ErrorHandling.LogError (e, this);
						if (ProgressDialogLogin != null) 
						{
							ProgressDialogLogin.Dismiss ();
							ProgressDialogLogin = null;
						}
					}
				};
			}
			catch (Exception e) 
			{
				ErrorHandling.LogError (e, this);
			}
			finally 
			{
				if (ProgressDialogLogin != null) 
				{
					ProgressDialogLogin.Dismiss ();
					ProgressDialogLogin = null;
				}
			}
		}
	}
}
