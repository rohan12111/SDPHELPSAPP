using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace HELPSMobileFrontEnd
{
	public static class RESTClass
	{
		public static async Task<List<WorkshopSets>> GetWorkshopList()
		{
			try
			{
				using (HttpClient _HttpClient = new HttpClient())
				{
					_HttpClient.Timeout = new TimeSpan(0, 0, 1, 0);
					using (HttpRequestMessage _HttpRequest = new HttpRequestMessage(HttpMethod.Get, "http://sdpmachine.cloudapp.net/" + "api/workshop/workshopSets"))
					{
						_HttpRequest.Headers.Add("AppKey", "123456");

						using (HttpResponseMessage _HttpResponse = await _HttpClient.SendAsync(_HttpRequest))
						{
							if (_HttpResponse.IsSuccessStatusCode)
							{
								String strResponse = await _HttpResponse.Content.ReadAsStringAsync();
								RootObject _RootObj = new RootObject(strResponse);
								return _RootObj.Results;
							}
							else
							{
								return null;
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}
	}

	public class WorkshopSets
	{
		public int id { get; set; }
		public string name { get; set; }
		public string archived { get; set; }
	}

	public class RootObject
	{
		public RootObject(String strJson)
		{
			try
			{
				String text = "";
				JObject jObject = JObject.Parse(strJson);

				foreach (var d in jObject["Results"].Children())
				{
					Results.Add(new WorkshopSets(){
						id = (int)d["id"],
						name = (string)d["name"],
						archived = (string)d["archived"]
					});
				}
			}
			catch 
			{
				throw;
			}
		}

		public List<WorkshopSets> Results { get; set; }
		public bool IsSuccess { get; set; }
		public object DisplayMessage { get; set; }
	}
}

