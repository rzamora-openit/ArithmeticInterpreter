namespace OpeniT.Timesheet.Web.Controllers.Api
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Diagnostics;
	using System.Dynamic;
	using System.Linq;
	using System.Threading.Tasks;

	using AutoMapper;

	using Helpers;

	using Microsoft.ApplicationInsights;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

	using Models;
	using OpeniT.Timesheet.Web.Constants;
	using OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter;
	using OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Helpers;
	using ViewModels;

	[Authorize]
	[Route("api/[controller]")]
	public class StatisticsController : Controller
	{
		private readonly ILogger<StatisticsController> logger;

		private readonly IMapper mapper;

		private readonly IDataPerformance performance;

		private readonly IDataRepository repository;

		private readonly TelemetryClient telemetry;

		public StatisticsController(
			ILogger<StatisticsController> logger,
			IDataPerformance performance,
			IDataRepository repository,
			IMapper mapper,
			TelemetryClient telemetry)
		{
			this.logger = logger;
			this.performance = performance;
			this.repository = repository;
			this.mapper = mapper;
			this.telemetry = telemetry;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] string owner, DateTime? month)
		{
			owner = owner ?? this.User.Identity.Name;
			month = month == null || month > DateTime.Now ? DateTime.Now : month;

			try
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				var user = this.repository.GetUserByEmail(owner);

				var firstActiveUserContract = user.UserContracts.GetActiveValidFrom((DateTime)month);
				firstActiveUserContract.Script = "Column Difference = System.RecordedHours - System.RequiredHours; Column RunningBalance = System.RecordedHours + System.PreviousMonth.ExcessHours; Column ExcessHours = System.PreviousMonth.ExcessHours + Difference; Column Test = -5 - - + ( 10 * 10); Column Test2 = ExcessHours + Difference;";

                var vm = this.mapper.Map<StatisticsViewModel>(user);

				vm.DistinctYears = await this.repository.GetDistinctYears(owner);
				if(!vm.DistinctYears.Contains(DateTime.Now.Year))
                {
					vm.DistinctYears = vm.DistinctYears.Append(DateTime.Now.Year);
				}
				vm.RemainingVL = await this.repository.GetUserVL(user, (DateTime)month);
				vm.RemainingSL = await this.repository.GetUserSL(user, (DateTime)month);
				vm.RemainingTO = await this.repository.GetUserTimeOff(user, (DateTime)month);
				vm.MonthlySummary = await this.performance.GetMonthlySummary(user, (DateTime)month);
				var RecordSummary = await this.repository.GetRecordSummaryByUser(user);
				vm.RecordSummary = this.mapper.Map<IEnumerable<RecordSummaryViewModel>>(RecordSummary);

				var recordSummaryThisYear = RecordSummary.Where(x => x.Month.Year == month.Value.Year ).OrderBy(x => x.Month.Month).ToList();

                var contractHelper = new ContractHelper();
				vm.StatisticsColumnRecords = contractHelper.InterpretUser(firstActiveUserContract.Script, recordSummaryThisYear, vm.ExcessHours);

                stopWatch.Stop();
				vm.Elapsed = stopWatch.Elapsed.TotalSeconds;

				
				return this.Ok(vm);
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get monthly summary for {owner}: {ex}");
			}

			return this.BadRequest("ERROR: Failed to get monthly summary");
		}

		[HttpGet("leavecredits")]
		public async Task<IActionResult> GetLeaveCredits([FromQuery] string owner, DateTime? month)
		{
			owner = owner ?? this.User.Identity.Name;
			month = month == null || month > DateTime.Now ? DateTime.Now : month;

			try
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				var user = this.repository.GetUserByEmail(owner);
				var vm = this.mapper.Map<StatisticsViewModel>(user);				
				vm.RemainingVL = await this.repository.GetUserVL(user, (DateTime)month);
				vm.RemainingSL = await this.repository.GetUserSL(user, (DateTime)month);
				vm.RemainingTO = await this.repository.GetUserTimeOff(user, (DateTime)month);				
				stopWatch.Stop();
				vm.Elapsed = stopWatch.Elapsed.TotalSeconds;

				return this.Ok(vm);
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get leave credits for {owner}: {ex}");
			}

			return this.BadRequest("ERROR: Failed to get leave credits");
		}

		[HttpGet("leavecredits/VL")]
		public async Task<IActionResult> GetVLCredits([FromQuery] string owner, DateTime? month)
		{
			owner = owner ?? this.User.Identity.Name;

			try
			{				
				var user = this.repository.GetUserByEmail(owner);				
				var remainingVL = await this.repository.GetUserVL(user, (DateTime)month);
				
				return this.Ok(remainingVL);
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get VL credits for {owner}: {ex}");
			}

			return this.BadRequest("ERROR: Failed to get VL credits");
		}

		[HttpGet("leavecredits/SL")]
		public async Task<IActionResult> GetSLCredits([FromQuery] string owner, DateTime? month)
		{
			owner = owner ?? this.User.Identity.Name;

			try
			{
				var user = this.repository.GetUserByEmail(owner);
				var remainingSL = await this.repository.GetUserSL(user, (DateTime)month);

				return this.Ok(remainingSL);
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get SL credits for {owner}: {ex}");
			}

			return this.BadRequest("ERROR: Failed to get SL credits");
		}

		[HttpGet("leavecredits/TO")]
		public async Task<IActionResult> GetTOCredits([FromQuery] string owner, DateTime? month)
		{
			owner = owner ?? this.User.Identity.Name;

			try
			{
				var user = this.repository.GetUserByEmail(owner);
				var remainingTO= await this.repository.GetUserTimeOff(user, (DateTime)month);

				return this.Ok(remainingTO);
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get TO credits for {owner}: {ex}");
			}

			return this.BadRequest("ERROR: Failed to get TO credits");
		}

		[HttpGet("leavecredits/BL")]
		public async Task<IActionResult> GetBLCredits([FromQuery] string owner, DateTime? month)
		{
			owner = owner ?? this.User.Identity.Name;

			try
			{
				var user = this.repository.GetUserByEmail(owner);
				var remainingTO = await this.repository.GetUserBirthdayLeave(user, (DateTime)month);

				return this.Ok(remainingTO);
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get BL credits for {owner}: {ex}");
			}

			return this.BadRequest("ERROR: Failed to get BL credits");
		}

		[HttpGet("record/distinctyears")]
		public async Task<IActionResult> GetRecordDistinctYears()
		{
			try
			{
				return this.Ok(await this.repository.GetDistinctYears());
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get record distinct years: {ex}");
			}

			return this.BadRequest($"ERROR: Failed to get record distinct years");
		}

		[HttpGet("{owner}/{month}")]
		public IActionResult GetMonthDetails(string owner, DateTime month)
		{
			try
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				var user = this.repository.GetUserByEmail(owner);
				var vm = this.mapper.Map<StatisticsDetailViewModel>(user);

				vm.RemainingVL = this.repository.GetUserVL(user, month).Result;
				vm.RemainingSL = this.repository.GetUserSL(user, month).Result;
				vm.RecordedHours = this.repository.GetMonthTotalByOwner(owner, month).Result;
				vm.WorkingHours = month.ToWorkingHours(user);
				vm.ExcessHours = this.repository.GetCurrentExcessHours(user, month, false).Result;
				vm.MonthDetails = this.performance.GetMonthDetails(user, month);

				stopWatch.Stop();
				vm.Elapsed = stopWatch.Elapsed.TotalSeconds;

				return this.Ok(vm);
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get {month.Date} summary for {owner}: {ex}");
			}

			return this.BadRequest("ERROR: Failed to get month details");
		}

		[HttpGet("{owner}/year/{year}")]
		public async Task<IActionResult> GetYearDetails(string owner, int year)
		{
			try
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				var user = this.repository.GetUserByEmail(owner);
				var vm = this.mapper.Map<StatisticsDetailViewModel>(user);
				var lastMonthOfYear = new DateTime(year, 12, 1);

				var workingHours = 0.0;
				for (var m = 1; m <= 12; m++)
				{
					var month = new DateTime(year, m, 1);
					workingHours += month.ToWorkingHours(user);
				}

				vm.WorkingHours = workingHours;
				vm.RemainingVL = await this.repository.GetUserVL(user, lastMonthOfYear);
				vm.RemainingSL = await this.repository.GetUserSL(user, lastMonthOfYear);
				vm.RecordedHours = await this.repository.GetYearTotalByOwner(owner, year);
				vm.MonthDetails = await this.performance.GetYearDetails(user, year);
				vm.ExcessHours = await this.repository.GetCurrentExcessHours(user, lastMonthOfYear, false);

				stopWatch.Stop();
				vm.Elapsed = stopWatch.Elapsed.TotalSeconds;

				return this.Ok(vm);
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get {year} summary for {owner}: {ex}");
			}

			return this.BadRequest("ERROR: Failed to get month details");
		}

		[HttpPost("{owner?}")]
		public async Task<IActionResult> Post(string owner, [FromBody] RecordSummaryViewModel summary)
		{
			owner = owner ?? this.User.Identity.Name;
			try
			{
				if (!this.ModelState.IsValid) return this.BadRequest("ERROR: Invalid Data");

				var newSummary = this.mapper.Map<RecordSummary>(summary);

				await this.repository.SubmitRecordSummary(owner, newSummary);
				if (await this.repository.SaveChangesAsync(owner))
					return this.Created($"api/record", Mapper.Map<RecordSummaryViewModel>(summary));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to add record summary for {owner}: {ex}");
			}

			return this.BadRequest("ERROR: Failed to add record summary");
		}

		[HttpPut("recalculate/{id}")]
        public async Task<IActionResult> Recalcuate(int id)
		{
			var owner = this.User.Identity.Name;
			try
			{
				if (!this.ModelState.IsValid) return this.BadRequest("ERROR: Invalid Data");
				this.logger.LogInformation($"Looking record summary: {id}");

				var user = this.repository.GetUserByEmail(owner);
				var summary = await this.repository.GetRecordSummaryById(id);
				if (summary == null)
				{
					this.logger.LogWarning($"Record summary \'{id}\' not found");
					return this.NotFound("ERROR: Unable to locate record summary");
				} 
				
				if (summary.User.Email != owner)
                {
					return this.BadRequest("You Have no permission to do this action");
				}

				this.repository.UnlockRecordSummary(owner, summary);
				await this.repository.SaveChangesAsync(owner);

				var monthlySummary = await this.performance.GetMonthlySummary(user, summary.Month);
                var selectedMonthSummary = monthlySummary.Where(s => s.Month == summary.Month).FirstOrDefault();

				var recalculateSummary = this.mapper.Map<RecordSummary>(selectedMonthSummary);
				await this.repository.SubmitRecordSummary(owner, recalculateSummary);

				if (await this.repository.SaveChangesAsync(owner))
				{
					this.logger.LogInformation("Record Summary has been successfully recalculated");
					return this.NoContent();
				}
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to recalculate record summary for {owner}: {ex}");
			}

			return this.BadRequest("ERROR: Failed to recalculate record summary");
		}

		[HttpDelete("{id}/{owner?}")]
        [Authorize(Roles = "Administrator, HR")]
        public async Task<IActionResult> Delete(string owner, int id)
		{
			owner = owner ?? this.User.Identity.Name;
			try
			{
				if (!this.ModelState.IsValid) return this.BadRequest("ERROR: Invalid Data");
				this.logger.LogInformation($"Looking record summary: {id}");

				var summary = await this.repository.GetRecordSummaryById(id);
				if (summary == null)
				{
					this.logger.LogWarning($"Record summary \'{id}\' not found");
					return this.NotFound("ERROR: Unable to located record summary");
				}

				this.repository.UnlockRecordSummary(owner, summary);
				if (await this.repository.SaveChangesAsync(owner))
				{
					this.logger.LogInformation("Record has been successfully removed");
					return this.NoContent();
				}
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to add record summary for {owner}: {ex}");
			}

			return this.BadRequest("ERROR: Failed to add record summary");
		}

		
		[HttpGet("summary/{month}")]
        [Authorize(Roles = "Administrator, HR")]
        public async Task<IActionResult> GetSummary(DateTime month)
		{
			try
			{
				var stopWatch = new Stopwatch();

				stopWatch.Start();
				var results = await this.repository.GetRecordSummaryByMonth(month);
				stopWatch.Stop();

				dynamic vm = new ExpandoObject();
				vm.results = this.mapper.Map<IEnumerable<RecordSummaryViewModel>>(results);
				vm.elapsed = stopWatch.Elapsed.TotalSeconds;

				return this.Ok(vm);
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get record summaries for {month.FirstDayOfMonth()}: {ex}");
			}
			return this.BadRequest("ERROR: failed to get record summaries");
		}

		[HttpGet("summary/user/{owner?}")]
        [Authorize(Roles = "Administrator, HR")]
        public async Task<IActionResult> GetUserSummary(string owner)
		{
			owner = owner ?? this.User.Identity.Name;

			try
			{
				var user = this.repository.GetUserByEmail(owner);
				var recordSummaries = await this.repository.GetRecordSummaryByUser(user);

				return this.Ok(this.mapper.Map<IEnumerable<RecordSummaryViewModel>>(recordSummaries));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get record summaries for {owner}: {ex}");
			}
			return this.BadRequest("ERROR: failed to get record summaries");
		}

		[HttpPut("summary/user/{owner?}")]
        [Authorize(Roles = "Administrator, HR")]
        public async Task<IActionResult> UpdateUserSummary(string owner, [FromBody] RecordSummaryViewModel recordSummaryViewModel)
		{
			owner = owner ?? this.User.Identity.Name;

			try
			{
				var user = this.repository.GetUserByEmail(owner);
				var recordSummary = await this.repository.GetRecordSummaryById(recordSummaryViewModel?.Id ?? 0);
				if (recordSummary != null && recordSummaryViewModel != null)
				{
					recordSummary.User = user;
					recordSummary.Month = recordSummaryViewModel.Month;
					recordSummary.Hours = recordSummaryViewModel.Hours;
					recordSummary.RequiredHours = recordSummaryViewModel.RequiredHours;
					recordSummary.LWOPHours = recordSummaryViewModel.LWOPHours;
					recordSummary.Difference = recordSummaryViewModel.Difference;
					recordSummary.TotalHours = recordSummaryViewModel.TotalHours;
					recordSummary.ExcessHours = recordSummaryViewModel.ExcessHours;
					recordSummary.SalaryDeduction = recordSummaryViewModel.SalaryDeduction;

					this.repository.UpdateRecordSummary(recordSummary);

					if (recordSummary.Month.Month == 12)
					{
						var userExcessHours = user?.ExcessHours?.FirstOrDefault(eh => eh?.Year == recordSummary.Month.Year);
						var excessHours = recordSummary.ExcessHours > 80
							? 80
							: recordSummary.ExcessHours < 0
							? recordSummary.ExcessHours - recordSummary.SalaryDeduction
								: recordSummary.ExcessHours;

						if (userExcessHours != null)
						{
							userExcessHours.Hours = excessHours;

							this.repository.UpdateUserExcessHours(userExcessHours);
						}
						else
						{
							userExcessHours = new UserExcessHours()
							{
								UserId = recordSummary.User.Id,
								Year = recordSummary.Month.Year,
								Hours = excessHours
							};

							user.ExcessHours.Add(userExcessHours);
							this.repository.AddUserExcessHours(userExcessHours);
						}
					}

					if (await this.repository.SaveChangesAsync(null))
					{
						return this.Ok(this.mapper.Map<RecordSummaryViewModel>(recordSummary));
					}
				}
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to update record summary for {owner}: {ex}");
			}

			return this.BadRequest("ERROR: failed to update record summary");
		}

		[HttpPut("summary/recompute/{owner?}")]
        [Authorize(Roles = "Administrator, HR")]
        public async Task<IActionResult> RecomputeRecordSummaries(string owner, [FromQuery] bool includeComingMonths, [FromBody] RecordSummaryViewModel recordSummaryViewModel)
		{
			try
			{
				var user = this.repository.GetUserByEmail(owner);
				var month = (recordSummaryViewModel?.Month).GetValueOrDefault();

				var recordSummaries = new List<RecordSummary>();
				if (includeComingMonths)
				{
					var _recordSummaries = await this.repository.GetRecordSummaryByDate(user, month, true);
					recordSummaries = _recordSummaries.ToList();
				}
				else
				{
					var recordSummary = await this.repository.GetRecordSummaryByUserMonth(user, month);
					if (recordSummary != null) recordSummaries.Add(recordSummary);
				}

				if (recordSummaries.Any())
				{
					foreach (var recordSummary in recordSummaries)
					{
						this.repository.UnlockRecordSummary(user?.Email, recordSummary);
					}

					// saves unlocked record summaries
					await this.repository.SaveChangesAsync(null);
				}

				var recordSummariesMonth = recordSummaries.Select(rs => rs.Month);
				var monthlySummaries = await this.performance.GetMonthlySummary(user, month, true);
				foreach (var recordSummaryVM in monthlySummaries.Where(ms => recordSummariesMonth.Any(rsm => rsm.Year == ms.Month.Year && rsm.Month == ms.Month.Month)))
				{
					var recordSummary = this.mapper.Map<RecordSummary>(recordSummaryVM);
					await this.repository.SubmitRecordSummary(user?.Email, recordSummary);
				}

				// saves submitted record summaries
				if (await this.repository.SaveChangesAsync(null))
				{
					return this.Ok(monthlySummaries);
				}
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to recompute record summaries for {owner}: {ex}");
			}

			return this.BadRequest("ERROR: failed to recompute record summaries");
			
		}

		[HttpGet("summary/excesshours/{owner?}")]
        [Authorize(Roles = "Administrator, HR")]
        public async Task<IActionResult> GetUserExcessHours(string owner)
		{
			owner = owner ?? this.User.Identity.Name;

			try
			{
				var user = this.repository.GetUserByEmail(owner);
				var excessHours = await this.repository.GetUserExcessHours(user);

				return this.Ok(this.mapper.Map<IEnumerable<UserExcessHoursViewModel>>(excessHours));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get excess hours for {owner}: {ex}");
			}
			return this.BadRequest("ERROR: failed to get excess hours");
		}

		[HttpPost("summary/excesshours")]
        [Authorize(Roles = "Administrator, HR")]
        public async Task<IActionResult> AddUserExcessHours([FromBody] UserExcessHoursViewModel vm)
		{
			try
			{
				var excessHours = this.mapper.Map<UserExcessHours>(vm);
				this.repository.AddUserExcessHours(excessHours);

				if (await this.repository.SaveChangesAsync(null))
				{
					return this.Ok(this.mapper.Map<UserExcessHoursViewModel>(excessHours));
				}
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to add excess hours: {ex}");
			}
			return this.BadRequest("ERROR: failed to add excess hours");
		}

		[HttpPut("summary/excesshours")]
        [Authorize(Roles = "Administrator, HR")]
        public async Task<IActionResult> UpdateUserExcessHours([FromBody] UserExcessHoursViewModel vm)
		{
			try
			{
				var excessHours = await this.repository.GetUserExcessHoursById(vm?.Id ?? 0);
				if (excessHours != null)
				{
					excessHours.Hours = vm.Hours;
					excessHours.UserId = vm.UserId;
					excessHours.Year = vm.Year;
					this.repository.UpdateUserExcessHours(excessHours);

					if (await this.repository.SaveChangesAsync(null))
					{
						return this.Ok(this.mapper.Map<UserExcessHoursViewModel>(excessHours));
					}
				}
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to update excess hours: {ex}");
			}
			return this.BadRequest("ERROR: failed to update excess hours");
		}

		[HttpDelete("summary/excesshours/{id}")]
        [Authorize(Roles = "Administrator, HR")]
        public async Task<IActionResult> RemoveUserExcessHours(int id)
		{
			try
			{
				var excessHours = await this.repository.GetUserExcessHoursById(id);
				if (excessHours != null)
				{
					this.repository.RemoveUserExcessHours(excessHours);

					if (await this.repository.SaveChangesAsync(null))
					{
						return this.Ok(this.mapper.Map<UserExcessHoursViewModel>(excessHours));
					}
				}
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to remove excess hours: {ex}");
			}
			return this.BadRequest("ERROR: failed to remove excess hours");
		}
	}
}