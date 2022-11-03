using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Constants
{
	public static class RequestStatus
	{
		public const string PENDING = "Pending";
		public const string CANCELLED = "Cancelled";
		public const string APPROVED = "Approved";
		public const string DENIED = "Denied";
		public const string ACKNOWLEDGED = "Acknowledged";
		public const string LAPSED = "Lapsed";
	}
}
