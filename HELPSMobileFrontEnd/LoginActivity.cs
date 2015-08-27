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

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "LoginActivity", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]			
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
					StartActivity(new Intent(this, typeof(MainActivity)));
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

//		private Task<SLIMSMobile_Handshake> MessageFixed(SLIMSMobile_Handshake _SM_Handshake)
		private void MessageFixed()
		{
			try
			{
				using (HttpClient _HttpClient = new HttpClient())
				{
					_HttpClient.Timeout = new TimeSpan(0, 0, 1, 0);
					using (HttpRequestMessage _HttpRequest = new HttpRequestMessage(HttpMethod.Get, "http://SDPMachine.cloudapp.net/myservice.svc" + "ListWorkshopSets"))
					{
						if (_HttpRequest.Content != null)
						{
							new AlertDialog.Builder (this)
								.SetMessage("Success!!!@#!FSDG")
								.SetTitle("Woot woot")
								.Show();
						}
						else
						{
							new AlertDialog.Builder (this)
								.SetMessage("Failed")
								.SetTitle("Aww")
								.Show();
						}
//						_HttpRequest.Content = new StringContent(HandshakeToString(_SM_Handshake), Encoding.UTF8, "application/json");
//						using (HttpResponseMessage _HttpResponse = await _HttpClient.SendAsync(_HttpRequest))
//						{
//							if (_HttpResponse.IsSuccessStatusCode)
//							{
//								SLIMSMobile_Handshake _SM_Handshake_Response = StringToHandshake(await _HttpResponse.Content.ReadAsStringAsync());
//								return _SM_Handshake_Response;
//							}
//							else
//							{
//								return null;
//							}
//						}
					}
				}
			}
			catch
			{
				throw;
			}
		}
	}

	[DataContract] public class workshopSets
	{
		[DataMember] public String id = "";
		[DataMember] public String name = "";
		[DataMember] public String archived = "";
	}
}

