namespace OpeniT.Timesheet.Web.ViewModels
{
	using System;

	using Models;

	public class RecordSummaryViewModel : BaseMonitorViewModel
	{
		public DateTimeOffset? SubmittedOn { get; set; }

		public DateTime Month { get; set; }

		public double Hours { get; set; }

		public double RequiredHours { get; set; }

		public double LWOPHours { get; set; }

		public double Difference { get; set; }

		public double TotalHours { get; set; }

		public double ExcessHours { get; set; }

		public double SalaryDeduction { get; set; }

		public string UserDisplayName { get; set; }

		public string UserEmail { get; set; }

		public string UserLineManagerDisplayName { get; set; }

		public string UserUserLocationName { get; set; }

		public string UserUserLocationCode { get; set; }
	}
}