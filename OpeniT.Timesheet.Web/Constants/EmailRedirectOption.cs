using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Constants
{
	public class EmailRedirectOption
	{
		public const string ReplaceRecipient = "Replace Recipient";

		public const string PlaceOrignalAsCC = "Place Orignal As CC";

		public const string AddRedirectToCC = "Add Redirect To CC";

		public const string None = "None";

		public static List<string> LIST = new List<string>() { ReplaceRecipient, PlaceOrignalAsCC, AddRedirectToCC, None };
	}
}
