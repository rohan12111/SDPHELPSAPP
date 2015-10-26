using System;
using System.IO;

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

		public static void WriteToStudentFile(String StuID = "")
		{
			String StudentID = String.IsNullOrWhiteSpace (StuID) ? LoggedStudent.studentID : StuID;

			if (StudentExists (StudentID)) //record already exists
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
			using (var streamReader = new StreamReader(path))
			{
				content = streamReader.ReadToEnd();
			}
			return content.Contains (StuID.Trim());
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

