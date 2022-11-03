using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Constants
{
	public static class UserContract
	{
		public const string FIXED_REQUIRED_HOURS = "Fixed";
		public const string PER_MONTH_REQUIRED_HOURS = "Per Month";
		public const string SIXTEEN_WORKING_DAYS_REQUIRED_HOURS = "16 Working Days";

		public static List<string> REQUIRED_HOURS = new List<string>()
		{
			FIXED_REQUIRED_HOURS,
			PER_MONTH_REQUIRED_HOURS,
			SIXTEEN_WORKING_DAYS_REQUIRED_HOURS
		};
	}
}
