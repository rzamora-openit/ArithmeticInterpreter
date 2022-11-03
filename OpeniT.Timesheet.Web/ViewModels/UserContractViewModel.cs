using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.ViewModels
{
	public class UserContractViewModel : BaseMonitorViewModel
	{
		public double AllowedVLDays { get; set; }

		public double AllowedSLDays { get; set; }

		public double AllowedTODays { get; set; }

		public double DailyHoursRequired { get; set; }

		public double DailyHoursPercentReduction { get; set; }

		public double MonthlyHoursRequired { get; set; }

		public double PercentReduction { get; set; }

		public string RequiredHours { get; set; }

		public UserViewModel User { get; set; }

		public double ExcessHoursMonthLimit { get; set; }

		public DateTime? ValidFrom { get; set; }

		public DateTime? ValidUntil { get; set; }
	}
}
