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
//		public RESTClass ()
//		{
//		}

		public static async Task GetWorkshopList()
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
								String _temp = await _HttpResponse.Content.ReadAsStringAsync();
								Result _Result = new Result(_temp);
								throw new Exception("");
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

	public class Result
	{
		public Result(String strJson)
		{
			try
			{
				JObject jObject = JObject.Parse(strJson);
				JToken jUser = jObject["user"];

				id = (int) jUser["id"];
				name = (string) jUser["name"];
				archived = (string) jUser["archived"];
			}
			catch 
			{
				throw;
			}
		}

		public int id { get; set; }
		public string name { get; set; }
		public string archived { get; set; }
	}

	public class RootObject
	{
		public List<Result> Results { get; set; }
		public bool IsSuccess { get; set; }
		public object DisplayMessage { get; set; }
	}
}

