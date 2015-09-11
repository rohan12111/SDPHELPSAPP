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
								return await _HttpResponse.Content.ReadAsStringAsync();
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
		public Student(String strstudentID, String strdob, String strgender, String strdegree, String strstatus, String strfirst_language, String strcountry_origin, String strbackground, Boolean blnHSC, Int32 intHSC_mark, Boolean blnIELTS, Int32 intIELTS_mark, Boolean blnTOEFL, Int32 intTOEFL_mark, Boolean blnTAFE, Int32 intTAFE_mark, Boolean blnCULT, Int32 intCULT_mark, Boolean blnInsearchDEEP, Int32 intInsearchDEEP_mark, Boolean blnInsearchDiploma, Int32 intInsearchDiploma_mark, Boolean blnfoundationcourse, Int32 intfoundationcourse_mark, String strcreated, String strcreatorID, String strdegree_details, String stralternative_contact, String strpreferred_name)
		{
			studentID = strstudentID;
			dob = strdob;
			gender = strgender;
			degree = strdegree;
			status = strstatus;
			first_language = strfirst_language;
			country_origin = strcountry_origin;
			background = strbackground;
			HSC = blnHSC;
			HSC_mark = intHSC_mark;
			IELTS = blnIELTS;
			IELTS_mark = intIELTS_mark;
			TOEFL = blnTOEFL;
			TOEFL_mark = intTOEFL_mark;
			TAFE = blnTAFE;
			TAFE_mark = intTAFE_mark;
			CULT = blnCULT;
			CULT_mark = intCULT_mark;
			InsearchDEEP = blnInsearchDEEP;
			InsearchDEEP_mark = intInsearchDEEP_mark;
			InsearchDiploma = blnInsearchDiploma;
			InsearchDiploma_mark = intInsearchDiploma_mark;
			foundationcourse = blnfoundationcourse;
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
		public Boolean HSC { get; set; }
		public Int32 HSC_mark { get; set; }
		public Boolean IELTS { get; set; }
		public Int32 IELTS_mark { get; set; }
		public Boolean TOEFL { get; set; }
		public Int32 TOEFL_mark { get; set; }
		public Boolean TAFE { get; set; }
		public Int32 TAFE_mark { get; set; }
		public Boolean CULT { get; set; }
		public Int32 CULT_mark { get; set; }
		public Boolean InsearchDEEP { get; set; }
		public Int32 InsearchDEEP_mark { get; set; }
		public Boolean InsearchDiploma { get; set; }
		public Int32 InsearchDiploma_mark { get; set; }
		public Boolean foundationcourse { get; set; }
		public Int32 foundationcourse_mark { get; set; }
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
					(String)d["country_origin"], (String)d["background"], (Boolean)d["HSC"], 
					(Int32)d["HSC_mark"], (Boolean)d["IELTS"], (Int32)d["IELTS_mark"], 
					(Boolean)d["TOEFL"], (Int32)d["TOEFL_mark"], (Boolean)d["TAFE"], (Int32)d["TAFE_mark"], 
					(Boolean)d["CULT"], (Int32)d["CULT_mark"], (Boolean)d["InsearchDEEP"], 
					(Int32)d["InsearchDEEP_mark"], (Boolean)d["InsearchDiploma"], (Int32)d["InsearchDiploma_mark"], 
					(Boolean)d["foundationcourse"], (Int32)d["foundationcourse_mark"], (String)d["created"], 
					(String)d["creatorID"], (String)d["degree_details"], (String)d["alternative_contact"], 
					(String)d["preferred_name"]);
			}
			catch 
			{
				throw;
			}
		}
		public Student Result { get; set; }
		public bool IsSuccess { get; set; }
		public object DisplayMessage { get; set; }
	}

	public class SessionTypes
	{
		public SessionTypes(int intID, String strAbbName, String strFullName, bool strIscurrent)
		{
			id = intID;
			abbName = strAbbName;
			fullName = strFullName;
			iscurrent = strIscurrent;
		}

		public int id { get; set; }
		public String abbName { get; set; }
		public String fullName { get; set; }
		public bool iscurrent { get; set; }
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
					Results.Add(new SessionTypes((int)d["id"], (String)d["abbName"], (String)d["fullName"], (bool)d["iscurrent"]));
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
		public WorkshopSets(int intID, String strName, String strArchived)
		{
			id = intID;
			name = strName;
			archived = strArchived;
		}

		public int id { get; set; }
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
}

