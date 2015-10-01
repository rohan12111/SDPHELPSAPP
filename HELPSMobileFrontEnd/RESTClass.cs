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
		public static async Task<List<WorkshopSets>> GetWorkshopList(String strParamters = "")
		{
			try
			{
				String result = await GetRESTCall("/workshop/workshopSets", strParamters);
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
				RootStudent _RootObj = new RootStudent(result);
				return _RootObj.Result;
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
		public Student(String strstudentID, String strdob, String strgender, 
			String strdegree, String strstatus, String strfirst_language, 
			String strcountry_origin, String strbackground, String strHSC, 
			String intHSC_mark, String strIELTS, String intIELTS_mark, 
			String strTOEFL, String intTOEFL_mark, String strTAFE, 
			String intTAFE_mark, String strCULT, String intCULT_mark, 
			String strInsearchDEEP, String intInsearchDEEP_mark, 
			String strInsearchDiploma, String intInsearchDiploma_mark, 
			String strfoundationcourse, String intfoundationcourse_mark, 
			String strcreated, String strcreatorID, String strdegree_details, 
			String stralternative_contact, String strpreferred_name)
		{
			studentID = strstudentID;
			dob = strdob;
			gender = strgender;
			degree = strdegree;
			status = strstatus;
			first_language = strfirst_language;
			country_origin = strcountry_origin;
			background = strbackground;
			HSC = strHSC;
			HSC_mark = intHSC_mark;
			IELTS = strIELTS;
			IELTS_mark = intIELTS_mark;
			TOEFL = strTOEFL;
			TOEFL_mark = intTOEFL_mark;
			TAFE = strTAFE;
			TAFE_mark = intTAFE_mark;
			CULT = strCULT;
			CULT_mark = intCULT_mark;
			InsearchDEEP = strInsearchDEEP;
			InsearchDEEP_mark = intInsearchDEEP_mark;
			InsearchDiploma = strInsearchDiploma;
			InsearchDiploma_mark = intInsearchDiploma_mark;
			foundationcourse = strfoundationcourse;
			foundationcourse_mark = intfoundationcourse_mark;
			created = strcreated;
			creatorID = strcreatorID;
			degree_details = strdegree_details;
			alternative_contact = stralternative_contact;
			preferred_name = strpreferred_name;
		}

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
				Result = new Student((String)d["studentID"], (String)d["dob"], (String)d["gender"], 
					(String)d["degree"], (String)d["status"], (String)d["first_language"], 
					(String)d["country_origin"], (String)d["background"], 
					(String)d["HSC"], (String)d["HSC_mark"], 
					(String)d["IELTS"], (String)d["IELTS_mark"], 
					(String)d["TOEFL"], (String)d["TOEFL_mark"], 
					(String)d["TAFE"], (String)d["TAFE_mark"], 
					(String)d["CULT"], (String)d["CULT_mark"], 
					(String)d["InsearchDEEP"], (String)d["InsearchDEEP_mark"], 
					(String)d["InsearchDiploma"], (String)d["InsearchDiploma_mark"], 
					(String)d["foundationcourse"], (String)d["foundationcourse_mark"], 
					(String)d["created"], (String)d["creatorID"], 
					(String)d["degree_details"], (String)d["alternative_contact"], (String)d["preferred_name"]);
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
		public SessionTypes(Int32 intID, String strAbbName, String strFullName, Boolean strIscurrent)
		{
			id = intID;
			abbName = strAbbName;
			fullName = strFullName;
			iscurrent = strIscurrent;
		}

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
					Results.Add(new SessionTypes((Int32)d["id"], (String)d["abbName"], (String)d["fullName"], (Boolean)d["iscurrent"]));
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
		public WorkshopSets(Int32 intID, String strName, String strArchived)
		{
			id = intID;
			name = strName;
			archived = strArchived;
		}

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
					Results.Add(new WorkshopSets((int)d["id"], (String)d["name"], (String)d["archived"]));
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

