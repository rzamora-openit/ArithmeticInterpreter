namespace OpeniT.Timesheet.Web.ViewModels
{
	using System;
	using System.Collections.Generic;

	using Models;
	using OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Models;
	using static OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Helpers.ContractHelper;

	public class StatisticsViewModel
	{
		public string Email { get; set; }

		public string DisplayName { get; set; }

		public string Department { get; set; }

		public DateTime? BirthDate { get; set; }

		public double DailyHoursRequired { get; set; }

		public string LineManagerDisplayName { get; set; }

		public double MonthlyHoursRequired { get; set; }

		public double RemainingVL { get; set; }

		public double RemainingSL { get; set; }

		public double RemainingTO { get; set; }

		public double RemainingBL { get; set; }

		public string UserLocationName { get; set; }

		public string UserLocationCode { get; set; }

		public string UserTypeStatus { get; set; }

		public IEnumerable<dynamic> MonthlySummary { get; set; }

		public IEnumerable<dynamic> RecordSummary { get; set; }

		public IEnumerable<UserExcessHours> ExcessHours { get; set; }

		public IEnumerable<UserExcessVL> ExcessVL { get; set; }

		public IEnumerable<int> DistinctYears { get; set; }

		public double CurrentExcessHours { get; set; }

		public double Elapsed { get; set; }
		public List<StatisticsColumnRecord> StatisticsColumnRecords { get;set; }

	}
}