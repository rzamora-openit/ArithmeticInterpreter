namespace OpeniT.Timesheet.Web.Models
{
	using System;

	public class UserContract : BaseMonitor
	{
		public double AllowedVLDays { get; set; }

		public double AllowedSLDays { get; set; }

		public double AllowedTODays { get; set; }

		public double DailyHoursRequired { get; set; }

        public double DailyHoursPercentReduction { get; set; }

        public double MonthlyHoursRequired { get; set; }

        public double PercentReduction { get; set; }

        public string RequiredHours { get; set; }

		public double ExcessHoursMonthLimit { get; set; }
		//public double ExcessHoursLimitYear { get; set; }


		public User User { get; set; }

		public DateTime? ValidFrom { get; set; }

		public DateTime? ValidUntil { get; set; }

		public string Script { get; set; }
	}
}