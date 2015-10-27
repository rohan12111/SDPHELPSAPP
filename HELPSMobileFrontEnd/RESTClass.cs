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
		public static async Task<List<WorkshopSessions>> GetWorkshopSessions(String strParameters = "")
		{
			try
			{
				String result = await GetRESTCall("/workshop/search", strParameters);
				RootWorkshopSessions _RootObj = new RootWorkshopSessions(result);
				return _RootObj.Results;
			}
			catch
			{
				throw;
			}
		}

		public static async Task<List<WorkshopSets>> GetWorkshopList(String strParameters = "")
		{
			try
			{
				String result = await GetRESTCall("/workshop/workshopSets", strParameters);
				RootWorkshopSets _RootObj = new RootWorkshopSets(result);
				return _RootObj.Results;
			}
			catch
			{
				throw;
			}
		}



		public static async Task<WorkshopBooking> GetWorkshopBookings(String strParamters = "")
		{
			try
			{
				String result = await GetRESTCall("/workshop/booking/search", strParamters);
				RootWorkshopBooking _RootObj = new RootWorkshopBooking(result);
				return _RootObj.Result;
			}
			catch
			{
				throw;
			}
		}

		public static async Task<List<SessionTypes>> GetSessionTypes(String strParamters = "")
		{
			try
			{
				String result = await GetRESTCall("/session/sessionTypes", strParamters);
				RootSessionTypes _RootObj = new RootSessionTypes(result);
				return _RootObj.Results;
			}
			catch
			{
				throw;
			}
		}

		public static async Task<Student> GetStudent(String strStudentID)
		{
			try
			{
				String result = await GetRESTCall("/student/" + strStudentID);
				if (!String.IsNullOrWhiteSpace(result))
				{
					RootStudent _RootObj = new RootStudent(result);
					return _RootObj.Result;
				}
				else
				{
					return null;
				}
			}
			catch
			{
				throw;
			}
		}

		private static async Task<String> GetRESTCall(String strCall, String strParameters = "")
		{
			try
			{
				using (HttpClient _HttpClient = new HttpClient())
				{
					_HttpClient.Timeout = new TimeSpan(0, 0, 1, 0);
					using (HttpRequestMessage _HttpRequest = new HttpRequestMessage(HttpMethod.Get, "http://sdpmachine.cloudapp.net/api" + strCall + "/" + strParameters))
					{
						_HttpRequest.Headers.Add("AppKey", "123456");

						using (HttpResponseMessage _HttpResponse = await _HttpClient.SendAsync(_HttpRequest))
						{
							if (_HttpResponse.IsSuccessStatusCode)
							{
								String strTemp = await _HttpResponse.Content.ReadAsStringAsync();
								return strTemp;
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





	public class Student
	{
		public String studentID { get; set; }
		public String dob { get; set; }
		public String gender { get; set; }
		public String degree { get; set; }
		public String status { get; set; }
		public String first_language { get; set; }
		public String country_origin { get; set; }
		public String background { get; set; }
		public String HSC { get; set; }
		public String HSC_mark { get; set; }
		public String IELTS { get; set; }
		public String IELTS_mark { get; set; }
		public String TOEFL { get; set; }
		public String TOEFL_mark { get; set; }
		public String TAFE { get; set; }
		public String TAFE_mark { get; set; }
		public String CULT { get; set; }
		public String CULT_mark { get; set; }
		public String InsearchDEEP { get; set; }
		public String InsearchDEEP_mark { get; set; }
		public String InsearchDiploma { get; set; }
		public String InsearchDiploma_mark { get; set; }
		public String foundationcourse { get; set; }
		public String foundationcourse_mark { get; set; }
		public String created { get; set; }
		public String creatorID { get; set; }
		public String degree_details { get; set; }
		public String alternative_contact { get; set; }
		public String preferred_name { get; set; }
	}
	public class RootStudent
	{
		public RootStudent(String strJson)
		{
			try
			{
				JObject jObject = JObject.Parse(strJson);
				var d = jObject["Result"];
				Result = (Student)d.ToObject(typeof(Student));
			}
			catch 
			{
				throw;
			}
		}
		public Student Result { get; set; }
		public Boolean IsSuccess { get; set; }
		public object DisplayMessage { get; set; }
	}

	public class SessionTypes
	{
		public Int32 id { get; set; }
		public String abbName { get; set; }
		public String fullName { get; set; }
		public Boolean iscurrent { get; set; }
	}
	public class RootSessionTypes
	{
		public RootSessionTypes(String strJson)
		{
			try
			{
				JObject jObject = JObject.Parse(strJson);
				Results = new List<SessionTypes>();
				foreach (var d in jObject["Results"].Children()) 
				{ 
					Results.Add((SessionTypes)d.ToObject(typeof(SessionTypes)));
				}
			}
			catch 
			{
				throw;
			}
		}

		public List<SessionTypes> Results { get; set; }
		public bool IsSuccess { get; set; }
		public object DisplayMessage { get; set; }
	}

	public class WorkshopSets
	{
		public Int32 id { get; set; }
		public String name { get; set; }
		public String archived { get; set; }
	}
	public class RootWorkshopSets
	{
		public RootWorkshopSets(String strJson)
		{
			try
			{
				JObject jObject = JObject.Parse(strJson);
				Results = new List<WorkshopSets>();
				foreach (var d in jObject["Results"].Children()) 
				{ 
					Results.Add((WorkshopSets)d.ToObject(typeof(WorkshopSets)));
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

	public class WorkshopSessions
	{
		public int WorkshopId { get; set; }
		public string topic { get; set; }
		public string description { get; set; }
		public string targetingGroup { get; set; }
		public string campus { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public int maximum { get; set; }
		public int WorkShopSetID { get; set; }
		public object cutoff { get; set; }
		public string type { get; set; }
		public int reminder_num { get; set; }
		public int reminder_sent { get; set; }
		public object DaysOfWeek { get; set; }
		public int BookingCount { get; set; }
		public object archived { get; set; }
	}
	public class RootWorkshopSessions
	{
		public RootWorkshopSessions(String strJson)
		{
			try
			{
				JObject jObject = JObject.Parse(strJson);
				Results = new List<WorkshopSessions>();
				foreach (var d in jObject["Results"].Children()) 
				{ 
					Results.Add((WorkshopSessions)d.ToObject(typeof(WorkshopSessions)));
				}
			}
			catch 
			{
				throw;
			}
		}
		public List<WorkshopSessions> Results { get; set; }
		public bool IsSuccess { get; set; }
		public object DisplayMessage { get; set; }
	}


	public class WorkshopBooking
	{
		public Int32 BookingId { get; set; }
		public Int32 workshopID { get; set; }
		public String studentID { get; set; }
		public String topic { get; set; }
		public String description { get; set; }
		public String targetingGroup { get; set; }
		public Int32 campusID { get; set; }
		public String starting { get; set; }
		public String ending { get; set; }
		public Int32 maximum { get; set; }
		public String cutoff { get; set; }
		public String canceled { get; set; }
		public String attended { get; set; }
		public Int32 WorkShopSetID { get; set; }
		public String type { get; set; }
		public Int32 reminder_num { get; set; }
		public Int32 reminder_sent { get; set; }
		public String WorkshopArchived { get; set; }
		public String BookingArchived { get; set; }
	}
	public class RootWorkshopBooking
	{
		public RootWorkshopBooking(String strJson)
		{
			try
			{
				JObject jObject = JObject.Parse(strJson);
				var d = jObject["Result"];
				Result = (WorkshopBooking)d.ToObject(typeof(WorkshopBooking));
			}
			catch 
			{
				throw;
			}
		}
		public WorkshopBooking Result { get; set; }
		public bool IsSuccess { get; set; }
		public object DisplayMessage { get; set; }
	}
}

