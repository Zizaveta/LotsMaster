using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
	public class Log   // for exceptions
	{
		public static void Logger(string m)
		{
			using (StreamWriter s = new StreamWriter(@"D:\Exeptions.txt", true))
			{
				s.WriteLine(DateTime.Now + ": " + m);
			}
		}
	}
}
