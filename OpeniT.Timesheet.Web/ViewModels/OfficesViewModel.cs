namespace OpeniT.Timesheet.Web.ViewModels
{
	public class OfficesViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }

		public string ClassName { get; set; }

		public double MonthlyHours { get; set; }

		public double DailyHours { get; set; }

		public string RequiredHours { get; set; }

		public double AllowedSLDays { get; set; }

		public double AllowedVLDays { get; set; }

		public double AllowedTODays { get; set; }

		public bool IsDeleted { get; set; }

        public double ExcessHoursMonthLimit { get; set; }
		public string HighlightColor { get; set; }
	}
}