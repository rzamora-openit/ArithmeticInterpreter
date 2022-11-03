using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Constants
{
	public class AppraisalFors
	{
		public const string ALL = "All";

		public const string EMPLOYEE = "Employee";

		public const string LINE_MANAGER = "Line Manager";

		public const string HR = "HR";

		public static List<string> LIST = new List<string>() { ALL, EMPLOYEE, LINE_MANAGER, HR };
	}
}
