namespace OpeniT.Timesheet.Web.ViewModels
{
	using System.Runtime.Serialization;

	using Models;

	public class StatisticsDetailViewModel
	{
		public string Email { get; set; }

		public string DisplayName { get; set; }

		public string Department { get; set; }

		public string LineManagerDisplayName { get; set; }

		public double DailyHoursRequired { get; set; }

		public double MonthlyHoursRequired { get; set; }

		public double WorkingHours { get; set; }

		public double RecordedHours { get; set; }

		public double ExcessHours { get; set; }

		public double RemainingVL { get; set; }

		public double RemainingSL { get; set; }

		public string UserLocationName { get; set; }

		public string UserLocationCode { get; set; }

		public string UserTypeStatus { get; set; }

		public dynamic MonthDetails { get; set; }

		public double Elapsed { get; set; }
	}
}