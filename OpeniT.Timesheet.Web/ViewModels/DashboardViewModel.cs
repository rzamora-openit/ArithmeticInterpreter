namespace OpeniT.Timesheet.Web.ViewModels
{
	using System.Collections.Generic;

	using Models;

	public class DashboardViewModel
	{
		public string Email { get; set; }

		public string DisplayName { get; set; }

		public string Department { get; set; }

		public string JobTitle { get; set; }

		public UserLocation UserLocation { get; set; }

		public double RecordedHours { get; set; }

		public double WorkingHours { get; set; }

		public string LineManagerDisplayName { get; set; }

		public IEnumerable<dynamic> PerfProcess { get; set; }

		public IEnumerable<dynamic> PerfSubProcess { get; set; }

		public IEnumerable<dynamic> PerfTask { get; set; }

		public IEnumerable<dynamic> PerfDailyProcess { get; set; }
	}
}