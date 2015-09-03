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

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "UTS: HELPS", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class LoginActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);

				SetContentView (Resource.Layout.Login);

				Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

				btnLogin.Click += delegate {
					StartActivity(new Intent(this, typeof(MainMenuActivity)));
				};
			}
			catch (Exception e)
			{
				new AlertDialog.Builder (this)
					.SetMessage(e.Message)
					.SetTitle("Application Error")
					.Show();
			}
		}
	}
}

/*
 * 
 * login
 *       string userName = txtusername.text;
 *		 string password = txtpassword.text;
 *		
 * public void Login(string userName ,string password)
 * {
 * 		
 *		if (username = "" && password = "")
 *{
 *		lblmessage.text = "Please Supply username or Password"
 *}
 *else
 *{
 *     Connection();
 *     for (datarow row in login data set)
 *      {
 * 		if (row.itemsarray[0].equals(username) && row.ItemsArray[1].Equals(password))
 * {
 * lblmessage.text = "User Successfully logged in!!";
 * 
 * }
 * 

*}
 *
 * }
 **/
