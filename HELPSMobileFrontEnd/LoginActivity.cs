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

namespace HELPSMobileFrontEnd
{
	[Activity (Label = "LoginActivity")]			
	public class LoginActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			try
			{
				base.OnCreate (bundle);

				// Create your application here
			}
			catch (Exception e)
			{
				new AlertDialog.Builder (this)
					.SetMessage(e.Message)
					.SetTitle("Application Error")
					.Show();
			}
		}

//		private async Task<SLIMSMobile_Handshake> MessageFixed(SLIMSMobile_Handshake _SM_Handshake)
//		{
//			try
//			{
//
//				using (HttpClient _HttpClient = new HttpClient())
//				{
//					_HttpClient.Timeout = new TimeSpan(0, 0, 1, 0);
//					using (HttpRequestMessage _HttpRequest = new HttpRequestMessage(HttpMethod.Get, SYNC_URL + "MessageFixed"))
//					{
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
//					}
//				}
//
//			}
//			catch
//			{
//				throw;
//			}
//		}
	}
}

