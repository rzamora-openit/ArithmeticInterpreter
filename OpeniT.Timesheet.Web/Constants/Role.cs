using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Constants
{
	public static class Role
	{
		public const string ADMINISTRATOR_ROLE = "Administrator";
		public const string HR_ROLE = "HR";
		public const string INTERNAL_TOOLS_ADMINISTRATOR_ROLE = "Internal Tools Administrator";
		public const string INTERNAL_TOOLS_ROLE = "Internal Tools";
		public const string USER_ROLE = "User";

		public static readonly List<string> SITE_VALUES_ALLOWED_ROLES = new List<string>() { ADMINISTRATOR_ROLE, INTERNAL_TOOLS_ADMINISTRATOR_ROLE };
	}
}
