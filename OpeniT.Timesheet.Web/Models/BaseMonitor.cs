using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Models
{
	public class BaseMonitor
	{
		public int Id { get; set; }

		public string AddedBy { get; set; }
		public DateTime AddedDate { get; set; }

		public string LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDate { get; set; }
	}
}
