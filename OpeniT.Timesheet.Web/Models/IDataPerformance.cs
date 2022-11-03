namespace OpeniT.Timesheet.Web.Models
{
	using OpeniT.Timesheet.Web.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IDataPerformance
	{
		Task<IEnumerable<dynamic>> GetSubProcessLastXDays(string owner, int day);

		Task<IEnumerable<dynamic>> GetProcessLastXDays(string owner, int day);

		Task<IEnumerable<dynamic>> GetTaskLastXDays(string owner, int day);

		Task<IEnumerable<dynamic>> GetHoursYTD(string owner);

		Task<IEnumerable<dynamic>> GetHourPerProcessYTD(string owner);

		Task<IEnumerable<dynamic>> GetHoursThisWeek(string owner);

		Task<IEnumerable<RecordSummaryViewModel>> GetMonthlySummary(User user, DateTime year, bool includeComingYears = false);

		Task<IEnumerable<dynamic>> GetYearDetails(User user, int year);

		Task<IEnumerable<Record>> GetLeaveYearDetails(int year, IEnumerable<int> leaveProcessIds = null);

		dynamic GetMonthDetails(User user, DateTime month);

		double GetUserVL(User user, DateTime month, double vlUsed);

		double GetUserSL(User user, DateTime month, double slUsed);

		double GetUserTO(User user, DateTime month, double toUsed);

		double GetUserBL(User user, double toUsed);
	}
}