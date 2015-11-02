using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HELPSMobileFrontEnd
{
	public static class Globals
	{
		public static Student LoggedStudent { get; set; }
		public static String StuName { get; set; }
		public static String StuEmail { get; set; }
		public static String StuMobile { get; set; }
		public static String StuCourse { get; set; }
		public static String StuFaculty { get; set; }
		public static String StuYear { get; set; }
		public static String StuOther { get; set; }

		private static String path 
		{
			get 
			{
				string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
				return Path.Combine(path, "HELPSLoginDetails.txt");
			}
		}

		public static async Task SaveProfile(Student student)
		{
			WriteToStudentFile (student.studentID);
			KeyValuePair<string, object>[] values = new KeyValuePair<string, object>[]{
				new KeyValuePair<string, object>("StudentId", student.studentID),
				new KeyValuePair<string, object>("DateOfBirth", student.dob),
				new KeyValuePair<string, object>("Gender", student.gender),
				new KeyValuePair<string, object>("Degree", student.degree),
				new KeyValuePair<string, object>("Status", student.status),
				new KeyValuePair<string, object>("FirstLanguage", student.first_language),
				new KeyValuePair<string, object>("CountryOrigin", student.country_origin),
				new KeyValuePair<string, object>("Background", student.background),
				new KeyValuePair<string, object>("DegreeDetails", student.degree_details),
				new KeyValuePair<string, object>("AltContact", student.alternative_contact),
				new KeyValuePair<string, object>("PreferredName", student.preferred_name),
				new KeyValuePair<string, object>("HSC", student.HSC),
				new KeyValuePair<string, object>("HSCMark", student.HSC_mark),
				new KeyValuePair<string, object>("IELTS", student.IELTS),
				new KeyValuePair<string, object>("IELTSMark", student.IELTS_mark),
				new KeyValuePair<string, object>("TOEFL", student.TOEFL),
				new KeyValuePair<string, object>("TOEFLMark", student.TOEFL_mark),
				new KeyValuePair<string, object>("TAFE", student.TAFE),
				new KeyValuePair<string, object>("TAFEMark", student.TAFE_mark),
				new KeyValuePair<string, object>("CULT", student.CULT),
				new KeyValuePair<string, object>("CULTMark", student.CULT_mark),
				new KeyValuePair<string, object>("InsearchDEEP", student.InsearchDEEP),
				new KeyValuePair<string, object>("InsearchDEEPMark", student.InsearchDEEP_mark),
				new KeyValuePair<string, object>("InsearchDiploma", student.InsearchDiploma),
				new KeyValuePair<string, object>("InsearchDiplomaMark", student.InsearchDiploma_mark),
				new KeyValuePair<string, object>("FoundationCourse", student.foundationcourse),
				new KeyValuePair<string, object>("FoundationCourseMark", student.foundationcourse_mark),
				new KeyValuePair<string, object>("CreatorId", student.creatorID) 
			};

			await RESTClass.PostStudent(values);
		}

		public static void WriteToStudentFile(String StuID = "", Boolean fileExists = true)
		{
			String StudentID = String.IsNullOrWhiteSpace (StuID) ? LoggedStudent.studentID : StuID;

			if (fileExists && StudentExists (StudentID)) //record already exists
			{
				string[] lines = System.IO.File.ReadAllLines(path);
				for (int i = 0; i < lines.Length; i++)
				{
					if (lines[i].Contains (StudentID))
					{
						lines[i] = StudentID + "," + StuName + "," + StuEmail + "," + StuMobile.ToString ()
							+ "," + StuCourse + "," + StuFaculty + "," + StuYear + "," + StuOther;
						break;
					}
				}
				System.IO.File.WriteAllLines(path, lines);
			}
			else //append to end of file
			{
				using (var streamWriter = new StreamWriter (path, true))
				{
					// StudentID, Name, Email, Mobile, Course, Faculty, Year, Other
					streamWriter.WriteLine (StudentID + "," + StuName + "," + StuEmail + "," + 
						StuMobile.ToString() + "," + StuCourse + "," + StuFaculty + "," + StuYear + "," + StuOther);
				}
			}
		}

		public static Boolean StudentExists(String StuID)
		{
			String content;
			if (File.Exists (path)) 
			{
				using (var streamReader = new StreamReader(path))
				{
					content = streamReader.ReadToEnd();
				}
				return content.Contains (StuID.Trim());
			}
			else
			{
				Globals.StuName = "Rohan Williams";
				Globals.StuEmail = "rohan.williams@student.uts.edu.au";
				Globals.StuMobile = "0403143661";
				Globals.StuCourse = "C10143";
				Globals.StuFaculty = "Engineering & Information Technology";
				Globals.StuYear = "2nd Year";
				Globals.StuOther= "Other Text";
				Globals.WriteToStudentFile("11116161", false);//file doesnt exist

				return false;
			}
		}

		public static void SetGlobalVars(String StuID)
		{
			string[] lines = System.IO.File.ReadAllLines(path);
			for (int i = 0; i < lines.Length; i++)
			{
				if (lines [i].Contains (StuID.Trim())) 
				{
					// StudentID, Name, Email, Mobile, Course, Faculty, Year, Other
					String[] data = lines[i].Split(',');

					StuName = data[1];
					StuEmail = data[2];
					StuMobile = data[3];
					StuCourse = data[4];
					StuFaculty = data[5];
					StuYear = data[6];
					StuOther = data[7];
				}
			}
		}
	}
}

