namespace OpeniT.Timesheet.Web.ViewModels
{
	using System.Collections.Generic;

	public class CarryoverVLViewModel
	{
		public string Email { get; set; }

		public string DisplayName { get; set; }

		public string Department { get; set; }

		public double DailyHoursRequired { get; set; }

		public string LineManagerDisplayName { get; set; }

		public double MonthlyHoursRequired { get; set; }

		public string UserLocationName { get; set; }

		public string UserLocationCode { get; set; }

		public string UserTypeStatus { get; set; }

		public IEnumerable<dynamic> VLYears { get; set; }

		public double Elapsed { get; set; }
	}
}