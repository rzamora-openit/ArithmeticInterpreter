namespace OpeniT.Timesheet.Web.Models
{
	using System;

	public class RecordSummary : BaseMonitor
	{
		public User User { get; set; }

		public DateTime Month { get; set; }

		public DateTimeOffset? SubmittedOn { get; set; }

		public double Hours { get; set; }

		public double RequiredHours { get; set; }

		public double LWOPHours { get; set; }

		public double Difference { get; set; }

		public double TotalHours { get; set; }

		public double ExcessHours { get; set; }

		public double SalaryDeduction { get; set; }
	}
}