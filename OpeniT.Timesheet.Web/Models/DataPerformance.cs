namespace OpeniT.Timesheet.Web.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Helpers;

	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using OpeniT.Timesheet.Web.ViewModels;

	public class DataPerformance : IDataPerformance
	{
		private readonly DataContext context;

		private readonly ILogger<DataPerformance> logger;

		public DataPerformance(ILogger<DataPerformance> logger, DataContext context)
		{
			this.logger = logger;
			this.context = context;
		}

		public async Task<IEnumerable<dynamic>> GetSubProcessLastXDays(string owner, int day)
		{
			var firstDay = DateTime.Today.AddDays(day);
			var lastDay = DateTime.Today;

			return await (from record in this.context.Records
					where record.Owner == owner && record.Date >= firstDay && record.Date < lastDay
					group record by record.SubProcess.Name
					into g
					select new object[] { g.Key, g.Sum(i => i.Hours) }).AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<dynamic>> GetProcessLastXDays(string owner, int day)
		{
			var firstDay = DateTime.Today.AddDays(day);
			var lastDay = DateTime.Today;

			var results = await (from record in this.context.Records
					where record.Owner == owner && record.Date >= firstDay && record.Date < lastDay &&
						record.SubProcess.Process.Type == "Work"
					group record by record.SubProcess.Process.Name
					into g
					select new { Categories = g.Key, Data = g.Sum(i => i.Hours) }).AsNoTracking()
				.ToListAsync();

			var categories = new List<string>();
			var data = new List<double>();
			foreach (var result in results)
			{
				categories.Add(result.Categories);
				data.Add(result.Data);
			}

			return new[] { new { categories, data } };
		}

		public async Task<IEnumerable<dynamic>> GetTaskLastXDays(string owner, int day)
		{
			var firstDay = DateTime.Today.AddDays(day);
			var lastDay = DateTime.Today;

			var results =
				await (from record in this.context.Records
						where record.Owner == owner && record.Date >= firstDay && record.Date < lastDay
						group record by record.Task
						into g
						select new { Categories = g.Key, Data = g.Sum(i => i.Hours) }).AsNoTracking()
					.OrderByDescending(g => g.Data)
					.Take(20)
					.ToListAsync();

			var categories = new List<string>();
			var data = new List<double>();
			foreach (var result in results)
			{
				categories.Add(result.Categories ?? "(blank)");
				data.Add(result.Data);
			}

			return new[] { new { categories, data } };
		}

		public async Task<IEnumerable<dynamic>> GetHoursYTD(string owner)
		{
			var firstDay = new DateTime(DateTime.Now.Year, 1, 1);
			var lastDay = DateTime.Today;

			var results =
				await (from record in this.context.Records
						where record.Owner == owner && record.Date >= firstDay && record.Date <= lastDay
						group record by record.Date
						into g
						select new { g.Key, Value = g.Sum(i => i.Hours) }).AsNoTracking()
					.ToAsyncEnumerable()
					.ToDictionary(x => x.Key, x => x.Value);

			var days = new Dictionary<DateTime, double>();
			Enumerable.Range(0, (lastDay - firstDay).Days).ToList().ForEach(x => days[firstDay.AddDays(x)] = 0);
			var filledResults = results.Concat(days.Where(kvp => !results.ContainsKey(kvp.Key))).OrderBy(x => x.Key);

			return filledResults.Select(t => new object[] { t.Key.ToUniversalTime().ToEpochJS(), t.Value })
				.Cast<object>()
				.ToList();
		}

		public async Task<IEnumerable<dynamic>> GetHourPerProcessYTD(string owner)
		{
			var firstDay = DateTime.Today.AddDays(-90);
			var lastDay = DateTime.Today;

			var results = await (from record in this.context.Records
					where record.Owner == owner && record.Date >= firstDay && record.Date <= lastDay
					group record by new { record.SubProcess.Process.Name, record.Date }
					into g
					select new { g.Key.Name, Data = new object[] { g.Key.Date.ToUniversalTime().ToEpochJS(), g.Sum(i => i.Hours) } })
				.AsNoTracking()
				.ToAsyncEnumerable()
				.ToList();

			var output = new Dictionary<string, dynamic>();
			foreach (var result in results)
			{
				var data = new List<dynamic>();

				if (output.ContainsKey(result.Name))
				{
					output[result.Name].Add(result.Data);
				}
				else
				{
					data.Add(result.Data);
					output.Add(result.Name, data);
				}
			}

			return output.Select(t => new { name = t.Key, data = t.Value }).Cast<object>().ToList();
		}

		public async Task<IEnumerable<dynamic>> GetHoursThisWeek(string owner)
		{
			var firstDay = DateTime.Today.StartOfWeek(DayOfWeek.Sunday);
			var lastDay = firstDay.AddDays(7).Date;

			var results =
				await (from record in this.context.Records
						where record.Owner == owner && record.Date >= firstDay && record.Date <= lastDay
						group record by record.Date
						into g
						select new { g.Key, Value = g.Sum(i => i.Hours) }).AsNoTracking()
					.ToAsyncEnumerable()
					.ToDictionary(x => x.Key, x => x.Value);

			var days = new Dictionary<DateTime, double>();
			Enumerable.Range(0, (lastDay - firstDay).Days).ToList().ForEach(x => days[firstDay.AddDays(x)] = 0);
			var filledResults = results.Concat(days.Where(kvp => !results.ContainsKey(kvp.Key))).OrderBy(x => x.Key);

			return filledResults.Select(t => new object[] { t.Value }).Cast<object>().ToList();
		}

		public async Task<IEnumerable<RecordSummaryViewModel>> GetMonthlySummary(User user, DateTime date, bool includeComingYears = false)
		{
			var recordSummaries = await this.context.RecordSummaries
				.Where(rs => rs.User == user
					&& (includeComingYears ? rs.Month.Year >= date.Year : rs.Month.Year == date.Year))
				.AsNoTracking()
				.ToListAsync();

			var records = await this.context.Records
				.Where(r => r.Owner == user.Email 
					&& (includeComingYears ? r.Date.Year >= date.Year : r.Date.Year == date.Year))
				.GroupBy(r => new { r.Date.Year, r.Date.Month })
				.Select(
					g => new
					{
						Month = DateTime.Parse($"{g.Key.Year}-{g.Key.Month}"),
						RequiredHours = DateTime.Parse($"{g.Key.Year}-{g.Key.Month}").ToWorkingHours(user),
						Hours = g.Sum(x => x.Hours)
					})
				.AsNoTracking()
				.ToListAsync();

			var leaveWithoutPayRequests = await this.context.Requests
				.Where(rs => rs.StartDate.Year >= 2022 && rs.Requestor.UserLocation.Code == "co-ph" && rs.Requestor == user && rs.Type == Constants.RequestType.LEAVEWITHOUTPAY && rs.Status == Constants.RequestStatus.APPROVED 
					&& (includeComingYears ? rs.StartDate.Year >= date.Year : rs.StartDate.Year == date.Year))
				.AsNoTracking()
				.ToListAsync();

			var monthlySummaries = new List<RecordSummaryViewModel>();
			foreach (var year in records.Select(r => r.Month.Year).Distinct().OrderBy(x => x))
			{
				var summaries = records.Where(rs => rs.Month.Year == year).OrderBy(s => s.Month).ToList();
				var months = summaries.Select(r => r.Month.Month).ToList();
				var leastMonth = months.Any() ? months.First() : 0;

				var negativeAllowance = -10;

				foreach (var month in months)
				{
					var monthlySummary = new RecordSummaryViewModel();

					var recordSummary = recordSummaries.FirstOrDefault(rs => rs.Month.Year == year && rs.Month.Month == month);
					if (recordSummary != null)
					{
						monthlySummary.Month = recordSummary.Month;
						monthlySummary.Hours = recordSummary.Hours;
						monthlySummary.RequiredHours = recordSummary.RequiredHours;
						monthlySummary.LWOPHours = recordSummary.LWOPHours;
						monthlySummary.Difference = recordSummary.Difference;
						monthlySummary.TotalHours = recordSummary.TotalHours;
						monthlySummary.ExcessHours = recordSummary.ExcessHours;
						monthlySummary.SalaryDeduction = recordSummary.SalaryDeduction;
						monthlySummary.SubmittedOn = recordSummary.SubmittedOn;
					}
					else
					{
						var excess = 0.0;
						var summary = summaries.FirstOrDefault(s => s.Month.Month == month);

						if (summary.Month.Month == leastMonth)
						{
							var excessYear = summary.Month.Year - 1;
							var excessLastYear = 0.0;
							if (year == records.Select(rs => rs.Month.Year).Min())
							{
								excessLastYear = user.ExcessHours?.FirstOrDefault(eh => eh.Year == excessYear)?.Hours ?? 0;
							}
							else
							{
								var summaryExcessYear = monthlySummaries.Where(ms => ms.Month.Year == excessYear).OrderBy(ms => ms.Month).LastOrDefault();
								excessLastYear = summaryExcessYear?.ExcessHours > 80
									? 80
									: summaryExcessYear?.ExcessHours < 0
										? (summaryExcessYear?.ExcessHours - summaryExcessYear?.SalaryDeduction) ?? 0
										: summaryExcessYear?.ExcessHours ?? 0;
							}

							excess = excessLastYear;
						}
						else
						{
							var lastMonth = months.ElementAt(months.IndexOf(month) - 1);
							var lastMonthExcessHours = recordSummaries.FirstOrDefault(rs => rs.Month.Year == year && rs.Month.Month == lastMonth)?.ExcessHours 
								?? monthlySummaries.FirstOrDefault(ms => ms.Month.Year == year && ms.Month.Month == lastMonth)?.ExcessHours;

							excess = lastMonthExcessHours ?? 0;
						}

                        // Look if there is override in Excess Hours Limit
                        var activeExcessHoursContract = user?.UserContracts?.GetActiveExcessHoursMonthLimit(new DateTime(year,month,1));
						double? monthLimit = null;
						if (activeExcessHoursContract != null)
						{
							monthLimit = activeExcessHoursContract.ExcessHoursMonthLimit > 0 ? activeExcessHoursContract.ExcessHoursMonthLimit : monthLimit;
						}

						if (!monthLimit.HasValue && user?.UserLocation?.ExcessHoursMonthLimit > 0)
						{
							monthLimit = user.UserLocation.ExcessHoursMonthLimit;
						}

						var leaveWithoutPayRequestHours = leaveWithoutPayRequests.Where(r => r.StartDate.Year == year && r.StartDate.Month == month).Select(r => r.Days * r.StartDate.RequiredHoursToday(user)).Sum();
						var totalHours = (excess > negativeAllowance) ? summary.Hours + excess : summary.Hours;
						var diff = summary.Hours - (summary.RequiredHours - leaveWithoutPayRequestHours);
						var excessHours = excess < negativeAllowance ? (diff + negativeAllowance) : (diff + excess);
						var salaryDeduction = (excessHours < negativeAllowance ? excessHours - negativeAllowance : 0) - leaveWithoutPayRequestHours;

						monthlySummary.Month = summary.Month;
						monthlySummary.Hours = summary.Hours;
						monthlySummary.RequiredHours = summary.RequiredHours;
						monthlySummary.LWOPHours = leaveWithoutPayRequestHours;
						monthlySummary.Difference = diff;
						monthlySummary.TotalHours = totalHours;
						monthlySummary.ExcessHours = !monthLimit.HasValue || 12 == month || year < 2022 || excessHours < monthLimit.Value ? excessHours : monthLimit.Value;
						monthlySummary.SalaryDeduction = salaryDeduction;
					}

					monthlySummaries.Add(monthlySummary);
				}
			}

			return monthlySummaries;
		}

		public dynamic GetMonthDetails(User user, DateTime month)
		{
			var firstDay = month.FirstDayOfMonth();
			var lastDay = month.LastDayOfMonth();

			var results = (from record in this.context.Records
					where record.Owner == user.Email && record.Date >= firstDay && record.Date <= lastDay
					select new
					{
						record.Id,
						Date = record.Date.ToUniversalTime(),
						PID = record.SubProcess.Process.Pid,
						URITag = record.SubProcess.Process.TaskUriPrefix,
						SPID = record.SubProcess.SPid,
						ProcessName = record.SubProcess.Process.Name,
						SubProcessName = record.SubProcess.Name,
						Location = record.Location ?? "",
						Task = record.Task ?? "",
						record.SubTask,
						record.Hours,
						record.Description
					}).AsNoTracking()
				.ToList();

			var emptyResults = new List<object>();
			var days = Enumerable.Range(1, DateTime.DaysInMonth(firstDay.Year, firstDay.Month)) // Days: 1, 2 ... 31 etc.
				.Select(day => new DateTime(firstDay.Year, firstDay.Month, day)) // Map each day to a date
				.ToList();

			foreach (var date in days)
			{
				var exists = results.Any(x => x.Date == date);
				if (!exists)
					emptyResults.Add(
						new
						{
							Date = date.ToUniversalTime(),
							PID = "",
							SPID = "",
							ProcessName = "",
							SubProcessName = "",
							Task = "",
							SubTask = "",
							Hours = 0,
							Description = ""
						});
			}

			var filledResults = results.Concat(emptyResults).ToList();

			return filledResults;
		}

		public async Task<IEnumerable<dynamic>> GetYearDetails(User user, int year)
		{
			var results = await (from record in this.context.Records
					where record.Owner == user.Email && record.Date.Year == year
					select new
					{
						record.Date,
						PID = record.SubProcess.Process.Pid,
						SPID = record.SubProcess.SPid,
						ProcessName = record.SubProcess.Process.Name,
						SubProcessName = record.SubProcess.Name,
						Location = record.Location ?? "",
						Task = record.Task ?? "",
						record.Hours
					}).AsNoTracking()
				.ToListAsync();

			return results;
		}

		public async Task<IEnumerable<Record>> GetLeaveYearDetails(int year, IEnumerable<int> leaveProcessIds = null)
		{
			leaveProcessIds = leaveProcessIds ?? new List<int>()
			{
				Constants.ProcessId.SICK,
				Constants.ProcessId.VACATION,
				Constants.ProcessId.TIMEOFF,
				Constants.ProcessId.BIRTHDAYLEAVE
			};

			return await this.context.Records
				.Where(r => r.Date.Year == year && (leaveProcessIds == null || !leaveProcessIds.Any() || leaveProcessIds.Contains(r.SubProcess.Process.Id)))
				.Include(r => r.SubProcess)
					.ThenInclude(sp => sp.Process)
				.ToListAsync();
		}

		public double GetUserVL(User user, DateTime month, double vlUsed)
		{
			var allowedVLDaysContract = user?.UserContracts?.GetActiveAllowedVLDaysContract(month);
			var dailyHoursRequiredContract = user?.UserContracts?.GetActiveDailyHoursRequiredContract(month);

			var excessVL = (user.ExcessVL.FirstOrDefault(u => u.Year == month.Year - 1)?.Hours ?? 0.0) - (user.ExcessVL.FirstOrDefault(u => u.Year == month.Year)?.Hours ?? 0.0);
			var vlHours = dailyHoursRequiredContract.DailyHoursRequired > 0 ? dailyHoursRequiredContract.DailyHoursRequired : user.UserLocation.DailyHours;
			var earnedRegDay = user.RegularizationDate != null && user.RegularizationDate?.Year == month.Year && user.RegularizationDate?.Day <= 15
				? 1
				: .5;
			var vlAllowedDays = allowedVLDaysContract.AllowedVLDays > 0
				? allowedVLDaysContract.AllowedVLDays
				: user.UserLocation.AllowedVLDays > 0
					? user.UserLocation.AllowedVLDays
					: 1.25 * (((user.UserType.Status == Constants.UserType.RESIGNED || user.UserType.Status == Constants.UserType.TERMINATED) && user.TerminationDate.HasValue
						? user.TerminationDate?.Month - (user.TerminationDate != null && user.TerminationDate?.Year == month.Year && user.TerminationDate?.Day <= 15 ? 0.5 : 0)
						: month.Month) + (user.RegularizationDate != null && user.RegularizationDate?.Year == month.Year ? (user.RegularizationDate?.Month * -1) + earnedRegDay : 0));
			
			var vlEarned = vlAllowedDays * vlHours;

			return Math.Round(((double)vlEarned - vlUsed) + excessVL, 2);
		}

		public double GetUserSL(User user, DateTime month, double slUsed)
		{
			var allowedSLDaysContract = user?.UserContracts?.GetActiveAllowedSLDaysContract(month);
			var dailyHoursRequiredContract = user?.UserContracts?.GetActiveDailyHoursRequiredContract(month);

			var earnedRegDay = user.RegularizationDate != null && user.RegularizationDate?.Year == month.Year && user.RegularizationDate?.Day <= 15
				? 1
				: .5;
			var slAllowedDays = allowedSLDaysContract.AllowedSLDays > 0
				? allowedSLDaysContract.AllowedSLDays
				: 1.25 * (user.RegularizationDate != null && user.RegularizationDate?.Year == month.Year
					? 12 - user.RegularizationDate?.Month + earnedRegDay
					: 12);

			var slAllowance = (dailyHoursRequiredContract.DailyHoursRequired > 0 ? dailyHoursRequiredContract.DailyHoursRequired : user.UserLocation.DailyHours) * slAllowedDays;

			return Math.Round((double)slAllowance - slUsed, 2);
		}

		public double GetUserTO(User user, DateTime month, double toUsed)
		{
			var allowedToDaysContract = user?.UserContracts?.GetActiveAllowedTODaysContract(month);
			var toAllowedDays = allowedToDaysContract.AllowedTODays > 0
				? allowedToDaysContract.AllowedTODays
				: user.UserLocation.AllowedTODays;

			return Math.Round(toAllowedDays - toUsed, 2);
		}

		public double GetUserBL(User user, double blUsed)
		{
			var hoursRequired = DateTime.Now.RequiredHoursToday(user);

			return Math.Round((user.UserLocation?.Code == "co-ph" && (user.UserType?.Status == Constants.UserType.FULLTIME || user.UserType?.Status == Constants.UserType.PARTTIME) ? hoursRequired : 0) - blUsed, 2);
		}
	}
}