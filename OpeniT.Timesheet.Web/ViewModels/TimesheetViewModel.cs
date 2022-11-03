namespace OpeniT.Timesheet.Web.ViewModels
{
	public class TimesheetViewModel
	{
		public string Email { get; set; }

		public string DisplayName { get; set; }

		public double MonthTotal { get; set; }

		public double RequiredHoursToday { get; set; }

		public double? ActiveTimerDuration { get; set; }

		public bool IsLocked { get; set; }
	}
}