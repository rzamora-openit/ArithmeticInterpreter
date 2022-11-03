namespace OpeniT.Timesheet.Web.Models
{
	using Hangfire.Server;
	using Helpers;
	using Microsoft.AspNetCore.Http;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using OpeniT.Timesheet.Web.Constants;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public class DataRepository: IDataRepository
    {
		private readonly DataContext context;

		private readonly ILogger<DataRepository> logger;

		private readonly IHttpContextAccessor httpContextAccessor;

		public DataRepository(
			DataContext context, 
			ILogger<DataRepository> logger,
			IHttpContextAccessor httpContextAccessor)
		{
			this.context = context;
			this.logger = logger;
			this.httpContextAccessor = httpContextAccessor;
		}

		public async Task<IEnumerable<Process>> GetAllProcesses(bool showDeleted)
		{
			this.logger.LogInformation("Getting all processes from the database");

			return await this.context.Processes.Where(p => !p.IsDeleted || p.IsDeleted == showDeleted)
				.Select(
					p => new Process
					{
						Id = p.Id,
						Name = p.Name,
						Pid = p.Pid,
						Type = p.Type,
						TaskUri = p.TaskUri,
						TaskUriPrefix = p.TaskUriPrefix,
						IsDeleted = p.IsDeleted,
						Owner = p.Owner,
						DeputyOwner = p.DeputyOwner,
						SubProcesses =
							p.SubProcesses.Where(s => !s.IsDeleted || s.IsDeleted == showDeleted)
								.Select(s => new SubProcess
								{
									Id = s.Id,
									Name = s.Name,
									SPid = s.SPid,
									IsDeleted = s.IsDeleted,
									Owner = s.Owner,
									DeputyOwner = s.DeputyOwner
								})
								.OrderBy(s => s.SPid)
								.ToList(),
						TaskGroups = p.TaskGroups.OrderBy(o => o.Name)
							.ToList()
					})
				.OrderBy(p => p.Pid)
				.ToListAsync();
		}

		public async Task<IEnumerable<Process>> GetOwnedProcesses(bool showDeleted)
		{
			this.logger.LogInformation("Getting all processes with owners from the database");

			return await this.context.Processes.Where(p => !p.IsDeleted || showDeleted)
				.Where(p => p.Owner != null)
				.Include(p => p.Owner)
				.OrderBy(p => p.Pid)
				.ToListAsync();
		}

		public async Task<IEnumerable<dynamic>> GetProcessUris()
		{
			this.logger.LogInformation("Getting all process URI from the database");

			return await this.context.Processes
				.Where(p => p.TaskUri != null && p.TaskUri != string.Empty)
				.Select(p => new { Prefix = p.TaskUriPrefix, Uri = p.TaskUri })
				.Distinct()
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<Record>> GetRecordsByOwner(string owner, DateTime date)
		{
			this.logger.LogInformation($"Getting all records for {owner} for {date.Date}");

			return await this.context.Records
				.Where(r => r.Owner == owner && r.Date.Date == date.Date)
				.Include(r => r.SubProcess)
				.ThenInclude(s => s.Process)
				.OrderBy(x => x.Id)
				.ToListAsync();
		}

		public async Task<IEnumerable<Record>> GetRecordsByHangfire(int hangfireJobId)
		{
			return await this.context.Records.Where(r => r.HangfireJobId == hangfireJobId)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<Record>> GetRecordsByOwner(string owner, bool? isFromExcel)
		{
			return await this.context.Records
				.Where(r => r.Owner == owner && r.IsFromExcel == isFromExcel && r.IsLocked != true)
				.ToListAsync();
		}

		public async Task<RecordSummary> GetRecordSummaryByUserMonth(User user, DateTime month)
		{
			this.logger.LogInformation($"Getting all record summaries for {user.Email} & {month.FirstDayOfMonth()}");

			return await this.context.RecordSummaries
				.Where(r => r.User == user && r.Month.Year == month.Year && r.Month.Month == month.Month)
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<RecordSummary>> GetRecordSummaryByUser(User user)
		{
			this.logger.LogInformation($"Getting all record summaries for {user.Email}");

			return await this.context.RecordSummaries.Where(r => r.User == user)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<RecordSummary>> GetRecordSummaryByMonth(DateTime month)
		{
			this.logger.LogInformation($"Getting all record summary for {month.FirstDayOfMonth()}");

			return await this.context.RecordSummaries
				.Where(r => r.Month.Year == month.Year && r.Month.Month == month.Month)
				.Include(r => r.User)
				.ThenInclude(u => u.LineManager)
				.Include(r => r.User)
				.ThenInclude(u => u.UserLocation)
				.OrderBy(r => r.User.UserLocation.Code)
				.ThenBy(r => r.User.DisplayName)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<RecordSummary>> GetRecordSummaryByDate(User user, DateTime date, bool includeComingYears = false)
		{
			return await this.context.RecordSummaries
				.Where(rs => rs.User == user
					&& rs.Month >= date 
					&& (includeComingYears ? rs.Month.Year >= date.Year : rs.Month.Year == date.Year))
				.Include(r => r.User)
				.ThenInclude(u => u.LineManager)
				.Include(r => r.User)
				.ThenInclude(u => u.UserLocation)
				.OrderBy(r => r.User.UserLocation.Code)
				.ThenBy(r => r.User.DisplayName)
				.ToListAsync();
		}

		public async Task<IEnumerable<Process>> GetProcessByType(string type)
		{
			return await this.context.Processes
				.Where(p => p.Type == type)
				.ToListAsync();
		}

		public async Task<IEnumerable<Process>> GetProcessByTerm(string term)
		{
			this.logger.LogInformation($"Getting process: {term}");

			var pid = 0;
			var result = new List<Process>();

			try
			{
				int.TryParse(term, out pid);
				result = await this.context.Processes.Where(p => p.Pid == pid || p.Name.Contains(term))
					.Select(
						p => new Process
						{
							Id = p.Id,
							Name = p.Name,
							Pid = p.Pid,
							SubProcesses = p.SubProcesses.OrderBy(o => o.SPid)
								.ToList()
						})
					.AsNoTracking()
					.ToListAsync();
			}
			catch (Exception ex)
			{
				this.logger.LogError(ex.StackTrace);
			}

			return result;
		}

		public async Task<Process> GetProcessById(int id)
		{
			return await this.context.Processes.Where(p => p.Id == id)
				.Include(p => p.Owner)
				.Include(p => p.DeputyOwner)
				.FirstOrDefaultAsync();
		}

		public async Task<SubProcess> GetSubProcessById(int id)
		{
			return await this.context.SubProcesses.Where(s => s.Id == id)
				.Include(s => s.Process)
				.Include(s => s.Owner)
				.Include(s => s.DeputyOwner)
				.FirstOrDefaultAsync();
		}

		public async Task<TaskGroup> GetTaskGroupById(int id)
		{
			return await this.context.TaskGroups.FirstOrDefaultAsync(t => t.Id == id);
		}

		public async Task<TaskGroup> GetTaskGroupByName(int processId, string name)
		{
			return await this.context.TaskGroups.FirstOrDefaultAsync(t => t.Process.Id == processId && t.Name == name);
		}

		public async Task<IEnumerable<SubProcess>> GetAllSubProcesses()
		{
			return await this.context.SubProcesses.Include(s => s.Process)
				.ToListAsync();
		}

		public async Task<IEnumerable<EmploymentCode>> GetAllEmploymentCodes()
		{
			return await this.context.EmploymentCodes.ToListAsync();
		}

		public async Task<IEnumerable<UserLocation>> GetAllUserLocations(bool showDeleted)
		{
			return await this.context.UserLocations.Where(l => !l.IsDeleted || l.IsDeleted == showDeleted)
				.OrderBy(l => l.Name)
				.ToListAsync();
		}

		public async Task<IEnumerable<UserType>> GetAllUserTypes()
		{
			return await this.context.UserTypes.ToListAsync();
		}

		public async Task<IEnumerable<int>> GetDistinctYears()
		{
			return await this.context.Records
				.Select(s => s.Date.Year)
				.Distinct()
				.ToListAsync();
		}

		public async Task<IEnumerable<int>> GetDistinctYears(string owner)
		{
			return await this.context.Records.Where(r => r.Owner == owner)
				.Select(s => s.Date.Year)
				.Distinct()
				.ToListAsync();
		}
		public async Task<Record> GetRecordById(int id)
		{
			return await this.context.Records.Where(r => r.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<Record>> GetRecordsByOwner(string owner)
		{
			return await this.context.Records
				.Where(r => r.Owner == owner)
				.ToListAsync();
		}

		public async Task<Record> GetRecordByDescription(string owner, string description)
		{
			return await this.context.Records.Where(r => r.Owner == owner && r.Description == description)
				.Include(r => r.SubProcess)
				.ThenInclude(s => s.Process)
				.AsNoTracking()
				.FirstOrDefaultAsync();
		}

		public async Task<Record> GetRecordByProcessId(string owner, int processId, DateTime date)
		{
			return await this.context.Records
				.Where(r => r.Owner == owner && r.SubProcess.Process.Id == processId && r.Date.Date == date)
				.FirstOrDefaultAsync();
		}

		public async Task<RecordSummary> GetRecordSummaryById(int id)
		{
			return await this.context.RecordSummaries
				.Include(s => s.User)
				.SingleOrDefaultAsync(s => s.Id == id);
		}

		public async Task<UserType> GetUserTypeById(int id)
		{
			return await this.context.UserTypes.Where(t => t.Id == id)
				.FirstOrDefaultAsync();
		}

		public User GetUserByEmail(string email)
		{
			return this.context.Users.Where(u => u.Email == email)
				.Include(u => u.UserLocation)
				.Include(u => u.EmploymentCode)
				.Include(u => u.UserType)
				.Include(u => u.LineManager)
				.Include(u => u.ExcessHours)
				.Include(u => u.ExcessVL)
				.Include(u => u.UserContracts)
				.SingleOrDefault();
		}

		public async Task<User> GetUserByEmailWithoutDetails(string email)
		{
			return await this.context.Users.Where(u => u.Email == email).SingleOrDefaultAsync();
		}

		public User GetUserById(int id)
		{
			return this.context.Users.Where(u => u.Id == id)
				.Include(u => u.UserLocation)
				.Include(u => u.UserType)
				.SingleOrDefault();
		}

		public async Task<IEnumerable<User>> GetUserByLM(User lineManager)
		{
			return await this.context.Users.Where(u => u.LineManager == lineManager)
							.ToListAsync();
		}

		public async Task<IEnumerable<User>> GetUserByLMEmail(string Email)
		{
			var lineManager = await this.context.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();
			return await this.context.Users.Where(u => u.LineManager == lineManager)
							.ToListAsync();
		}

		public User GetUserWithThumbnailByEmail(string email)
		{
			return this.context.Users.Where(u => u.Email == email)
				.Include(u => u.UserLocation)
				.Include(u => u.EmploymentCode)
				.Include(u => u.UserType)
				.Include(u => u.LineManager)
				.Include(u => u.ExcessHours)
				.Include(u => u.ExcessVL)
				.Include(u => u.UserContracts)
				.Include(u => u.Thumbnail)
				.SingleOrDefault();
		}

		public void AddOffice(UserLocation office)
		{
			this.context.UserLocations.Add(office);
		}

		public void AddProcess(Process process)
		{
			this.context.Processes.Add(process);
		}

		public void AddSubProcess(int id, SubProcess subProcess)
		{
			var process = this.GetProcessById(id)
				.Result;

			if (process == null) return;

			subProcess.Process = process;
			this.context.SubProcesses.Add(subProcess);
		}

		public void StopActiveTimer(string owner, double timeStop)
		{
			var results = this.context.Records.Where(r => r.Owner == owner && r.EpochStart != null)
				.ToList();
			foreach (var result in results)
			{
				var duration = (timeStop - result.EpochStart) / 60 / 60;
				result.Hours += duration ?? 0;
				result.Hours = Math.Round(result.Hours, 2);
				result.EpochStart = null;

				this.UpdateRecord(result);
			}
		}

		public async Task RevertInsertedHolidays(int hangfireJobId)
		{
			var records = await this.context.Records.Where(r => r.HangfireJobId == hangfireJobId)
				.AsNoTracking()
				.ToListAsync();
			this.context.Records.RemoveRange(records);
		}

		public async Task<bool> SaveChangesAsync(string owner)
		{
			if (owner != null)
			{
				var user = this.GetUserByEmail(owner);
				user.LastUpdate = DateTime.Now;
				this.UpdateUser(user);
			}

			var userEmail = owner 
				?? (httpContextAccessor != null && httpContextAccessor.HttpContext != null 
					? httpContextAccessor.HttpContext.User?.Identity?.Name 
					: "Server");

			var entries = this.context.ChangeTracker.Entries();
			foreach (var entry in entries.Where(e => 
				e.Entity.GetType().BaseType == typeof(BaseMonitor)
				&& e.State != EntityState.Detached 
				&& e.State != EntityState.Unchanged))
			{
				switch (entry.State)
				{
					case EntityState.Added:
						((BaseMonitor)entry.Entity).AddedDate = DateTime.UtcNow;
						((BaseMonitor)entry.Entity).AddedBy = userEmail;
						break;
					case EntityState.Modified:
						((BaseMonitor)entry.Entity).LastUpdatedDate = DateTime.UtcNow;
						((BaseMonitor)entry.Entity).LastUpdatedBy = userEmail;
						break;
				}
			}

			var results = await this.context.SaveChangesAsync();

			return results > 0;
		}

		public void AddRecord(int id, Record record)
		{
			var subProcess = this.GetSubProcessById(id)
				.Result;

			if (subProcess == null) return;

			record.SubProcess = subProcess;
			this.context.Records.Add(record);
		}

		public void AddRecordSummary(string owner, RecordSummary summary)
		{
			this.context.RecordSummaries.Add(summary);

			this.logger.LogInformation("Update all records for the submitted month");
			var records = this.context.Records.Where(
					r => r.Owner == owner && r.Date.Year == summary.Month.Year && r.Date.Month == summary.Month.Month)
				.ToList();
			records.ForEach(r => r.IsLocked = true);
		}

		public void UpdateRecordSummary(RecordSummary summary)
		{
			this.context.RecordSummaries.Update(summary);
		}

		public async Task SubmitRecordSummary(string owner, RecordSummary summary)
		{
			var user = this.GetUserByEmail(owner);

			summary.User = user;
			summary.SubmittedOn = DateTime.Now;


			#region deadcode
			/*
			 *This might be useful when we dont rely to the summary object that is supplied [FromBody] is statistics controller post [HttpPost("{owner?}")]
			 *Currently we dont check is the object is valid
			 */
			// Look if there is override in Excess Hours Limit
			//var excessHoursContract = user.UserContracts.GetActiveExcessHoursMonthLimit(DateTime.UtcNow.ToLocalTime());
			//double? limit = 0.0;
			//if (excessHoursContract != null)
			//{
			//	limit = excessHoursContract.ExcessHoursMonthLimit >= 0 ? excessHoursContract.ExcessHoursMonthLimit : limit;
			//}
			//if (!limit.HasValue &&  user?.UserLocation?.ExcessHoursMonthLimit > 0)
			//{
			//	limit = user.UserLocation.ExcessHoursMonthLimit;
			//}
			#endregion deadcode
			this.AddRecordSummary(owner, summary);

			// Add excess hours computation when submitting december timesheet
			if (summary.Month.Month == 12)
			{
				double lastYearExcess;
				if (summary.ExcessHours > 80) lastYearExcess = 80;
				else if (summary.ExcessHours < 0) lastYearExcess = summary.ExcessHours - summary.SalaryDeduction;
				else lastYearExcess = summary.ExcessHours;

				var excessHours = new UserExcessHours { UserId = user.Id, Year = summary.Month.Year, Hours = lastYearExcess };
				user.ExcessHours.Add(excessHours);
				this.AddUserExcessHours(excessHours);
			}

			var pendingRequests = await this.GetRequestsWithinMonth(user, summary.Month, new string[] { Constants.RequestType.VACATIONLEAVE, Constants.RequestType.SICKLEAVE, Constants.RequestType.TIMEOFF, Constants.RequestType.BIRTHDAYLEAVE, Constants.RequestType.LEAVEWITHOUTPAY }, new string[] { Constants.RequestStatus.PENDING });
			if (pendingRequests.Any())
			{
				this.logger.LogInformation($"Updating pending leave requests to lapsed");
				foreach (var pendingRequest in pendingRequests)
				{
					pendingRequest.Status = Constants.RequestStatus.LAPSED;

					this.UpdateRequest(pendingRequest);
				}
			}
		}

		public void UnlockRecordSummary(string owner, RecordSummary summary)
		{
			this.logger.LogInformation($"Deleting and unlocking record summary for {summary.Month}");
			this.DeleteRecordSymmary(owner, summary);
			this.logger.LogInformation($"Record summary for \'{summary.Month}\' has been deleted");

			this.logger.LogInformation($"NSTS entry for \'{summary.Month}\' has been deleted");
		}

		public void AddRequest(Request request)
		{
			this.context.Requests.Add(request);
		}

		public void UpdateRequest(Request request)
		{
			this.context.Requests.Update(request);
		}

		public void RemoveRequest(Request request)
		{
			var req = this.context.Requests.Where(r => r.Id == request.Id).Include(r => r.Record).FirstOrDefault();
			if (req.Record != null)
			{
				this.context.Records.Remove(req.Record);
			}
			this.context.Requests.Remove(request);
		}

		public void AddUserExcessHours(UserExcessHours excessHours)
		{
			this.context.UserExcessHours.Add(excessHours);
		}

		public void UpdateUserExcessHours(UserExcessHours excessHours)
		{
			this.context.UserExcessHours.Update(excessHours);
		}

		public void RemoveUserExcessHours(UserExcessHours excessHours)
		{
			this.context.UserExcessHours.Remove(excessHours);
		}

		public void AddUserExcessVL(UserExcessVL excessVL)
		{
			this.context.UserExcessVL.Add(excessVL);
		}
		public async Task<IEnumerable<User>> GetUsersByLocation(UserLocation location)
		{
			return await this.context.Users.Where(u => u.UserLocation == location)
				.Include(u => u.UserContracts)
				.Include(u => u.UserLocation)
				.Include(u => u.UserType)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<User>> GetUsersByBirthdayMonth(DateTime dateTime)
		{
			return await this.context.Users
				.Where(u => u.UserType.Id < 4)//active user
				.Where(u => u.BirthDate != null)
				.Where(u => u.BirthDate.Value.Month == dateTime.Month)
				.Include(u => u.LineManager)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<User>> GetUsersByBirthdayDay(DateTime dateTime)
		{
			return await this.context.Users
				.Where(u => u.UserType.Id < 4)//active user
				.Where(u => u.BirthDate != null)
				.Where(u => u.BirthDate.Value.Month == dateTime.Month && u.BirthDate.Value.Day == dateTime.Day)
				.Include(u => u.LineManager)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<User>> GetUsersByAnniversaryMonth(DateTime dateTime)
		{
			return await this.context.Users
				.Where(u => u.UserType.Id < 4)//active user
				.Where(u => u.StartDate != null && (DateTime.Now.Year - u.StartDate.Value.Year) > 0)
				.Where(u => u.StartDate.Value.Month == dateTime.Month)
				.Include(u => u.LineManager)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<User>> GetUsersByAnniversaryDay(DateTime dateTime)
		{
			return await this.context.Users
				.Where(u => u.UserType.Id < 4)//active user
				.Where(u => u.StartDate != null && (DateTime.Now.Year - u.StartDate.Value.Year) > 0)
				.Where(u => u.StartDate.Value.Month == dateTime.Month && u.StartDate.Value.Day == dateTime.Day)
				.Include(u => u.LineManager)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<User>> GetUsersByEmails(IEnumerable<string> emails)
		{
			return await this.context.Users
				.Where(u => emails.Contains(u.Email))
				.Include(u => u.LineManager)
				.AsNoTracking()
				.ToListAsync();
		}

		public void AddUser(User user)
		{
			this.context.Users.Add(user);
		}

		public void AddTaskGroup(int id, TaskGroup taskGroup)
		{
			var process = this.GetProcessById(id)
				.Result;

			if (process == null) return;

			taskGroup.Process = process;
			this.context.TaskGroups.Add(taskGroup);
		}

		public void UpdateUser(User user)
		{
			this.context.Users.Update(user);
		}

		public void UpdateRecord(int id, Record record)
		{
			var subProcess = this.GetSubProcessById(id)
				.Result;

			if (subProcess == null) return;

			record.SubProcess = subProcess;
			this.context.Records.Update(record);
		}

		public void UpdateRecord(Record record)
		{
			this.context.Records.Update(record);
		}

		public void ToggleImport(int id)
		{
			var user = this.GetUserById(id);
			user.IsImportAllowed = !user.IsImportAllowed;

			this.context.Users.Update(user);
		}

		public void ToggleExcessVL(User user, int year)
		{
			var month = new DateTime(year, 12, 1);
			var vl = this.GetUserVL(user, month)
				.Result;

			if (!user.ExcessVL.Select(u => u.UserId == user.Id && u.Year == year)
				.Any())
			{
				var excessVL = new UserExcessVL { Year = year, Hours = vl, UserId = user.Id };
				this.context.UserExcessVL.Add(excessVL);
			}
			else
			{
				var excess = user.ExcessVL.FirstOrDefault(u => u.UserId == user.Id && u.Year == year);
				if (excess != null) this.context.UserExcessVL.Remove(excess);
			}
		}

		public void DeleteCarryoverVLRequest(User requestor, int year)
		{
			var request = this.context.Requests.FirstOrDefault(r => r.Type == Constants.RequestType.COVL && r.Requestor == requestor && r.StartDate.Year == year);

			this.DeleteRequest(request);
		}

		public void DeleteRecords(Record[] records)
		{
			foreach (var record in records)
			{
				var request = this.context.Requests.Where(r => r.Record == record).FirstOrDefaultAsync().Result;
				if (request != null)
				{
					this.context.Requests.Remove(request);
				}
			}

			this.context.Records.RemoveRange(records);
		}

		public void DeleteRecord(Record record)
		{
			var request = this.context.Requests.Where(r => r.Record == record).FirstOrDefaultAsync().Result;
			if (request != null)
			{
				this.context.Requests.Remove(request);
			}

			this.context.Records.Remove(record);
		}

		public void DeleteRecordOnly(Record record)
		{
			this.context.Records.Remove(record);
		}

		public void DeleteRequest(Request request)
		{
			this.context.Requests.Remove(request);
		}

		public void DeleteTaskGroup(TaskGroup taskGroup)
		{
			this.context.TaskGroups.Remove(taskGroup);
		}

		public async Task<double> GetMonthTotalByOwner(string owner, DateTime month)
		{
			return await this.context.Records
				.Where(r => r.Owner == owner && r.Date.Month == month.Month && r.Date.Year == month.Year)
				.SumAsync(r => r.Hours);
		}

		public async Task<double> GetYearTotalByOwner(string owner, int year)
		{
			return await this.context.Records.Where(r => r.Owner == owner && r.Date.Year == year)
				.SumAsync(r => r.Hours);
		}

		public void AddRecord(Record record)
		{
			this.context.Records.Add(record);
		}

		public async Task<UserLocation> GetUserLocationById(int id)
		{
			return await this.context.UserLocations.Where(l => l.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<User> GetUserLocationByEmail(string email)
		{
			return await this.context.Users.Where(u => u.Email == email)
				.Include(u => u.UserLocation)
				.FirstOrDefaultAsync();
		}

		public async Task<UserContract> GetUserContractById(int id)
		{
			return await this.context.UserContracts.Where(uc => uc.Id == id)
				.Include(uc => uc.User)
				.FirstOrDefaultAsync();
		}

		public async Task<EmploymentCode> GetEmployeeCodeById(int id)
		{
			return await this.context.EmploymentCodes.Where(e => e.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<double> GetActiveTimerDuration(string owner)
		{
			var record = await this.context.Records.FirstOrDefaultAsync(r => r.Owner == owner && r.EpochStart != null);
			var hours = record?.Hours * 60 * 60;
			var result = record?.EpochStart - hours;
			return result ?? 0;
		}

		public Task<List<UserExcessHours>> GetUserExcessHours(User user)
		{
			var userId = user?.Id;

			return this.context.UserExcessHours
				.Where(eh => eh.UserId == userId)
				.ToListAsync();
		}

		public async Task<double> GetUserExcessHours(User user, DateTime month)
		{
			var recordSummaries = await this.GetRecordSummaryByMonth(month);
			var lastRecordSummary = recordSummaries as IList<RecordSummary> ?? recordSummaries.ToList();
			return lastRecordSummary.Any(u => u.User.Id == user.Id)
				? lastRecordSummary.FirstOrDefault(u => u.User.Id == user.Id).ExcessHours
				: 0;
		}

		public Task<UserExcessHours> GetUserExcessHoursById(int id)
		{
			return this.context.UserExcessHours.FirstOrDefaultAsync(eh => eh.Id == id);
		}

		public async Task<double> GetCurrentExcessHours(User user, DateTime month, bool excludeComingNegativeDiffs = true)
		{
			var recordSummary = await this.context.RecordSummaries
				.Where(rs => rs.User == user && rs.Month <= month)
				.OrderBy(rs => rs.Month)
				.LastOrDefaultAsync();

			var records = await this.context.Records
				.Where(r => r.Owner == user.Email && r.Date.Year == month.Year && r.Date.Month <= month.Month)
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
			records = records.OrderBy(r => r.Month).ToList();

			var leastMonth = records.FirstOrDefault()?.Month.Month ?? 1;

			double currentExcessHours = recordSummary?.ExcessHours ?? 0;
			double negativeAllowance = -10;
			foreach (var record in records.Where(r => r.Month > recordSummary?.Month))
			{
				if (record.Month.Month == leastMonth)
				{
					currentExcessHours = user?.ExcessHours?.FirstOrDefault(eh => eh?.Year == record.Month.Year - 1)?.Hours ?? 0;
				}

				double excessHours = 0;
				double diff = record.Hours - record.RequiredHours;
				if (currentExcessHours < negativeAllowance)
				{
					excessHours = diff + negativeAllowance;
				}
				else
				{
					excessHours = diff + currentExcessHours;
				}

				if (excludeComingNegativeDiffs
					&& diff <= 0
					&& record.Month.Year >= DateTime.Now.Year
					&& record.Month.Month >= DateTime.Now.Month)
				{
					continue;
				}

				currentExcessHours = excessHours;
			}

			return Math.Round(currentExcessHours, 2);
		}

		public async Task<double> GetUserVL(User user, DateTime month)
		{
			var allowedVLDaysContract = user?.UserContracts?.GetActiveAllowedVLDaysContract(month);
			var dailyHoursRequiredContract = user?.UserContracts?.GetActiveDailyHoursRequiredContract(month);

			var excessVL = (user.ExcessVL.FirstOrDefault(u => u.Year == month.Year - 1)?.Hours ?? 0.0) - (user.ExcessVL.FirstOrDefault(u => u.Year == month.Year)?.Hours ?? 0.0);
			var vl = (await this.GetProcessByTerm("92")).FirstOrDefault();
			var vlUsed = this.context.Records
				.Where(r => r.Owner == user.Email && r.SubProcess.Process == vl && r.Date.Year == month.Year)
				.Sum(r => r.Hours);
			var vlHours = dailyHoursRequiredContract.DailyHoursRequired > 0 ? dailyHoursRequiredContract.DailyHoursRequired : user.UserLocation.DailyHours;
			var earnedRegDay = user.RegularizationDate != null && user.RegularizationDate?.Year == month.Year && user.RegularizationDate?.Day <= 15
				? 1
				: .5;
			var vlAllowedDays = allowedVLDaysContract.AllowedVLDays > 0
				? allowedVLDaysContract.AllowedVLDays
				: user.UserLocation.AllowedVLDays > 0
					? user.UserLocation.AllowedVLDays
					: 1.25 * (user.RegularizationDate != null && user.RegularizationDate?.Year == month.Year
						? month.Month - user.RegularizationDate?.Month + earnedRegDay
						: month.Month);

			var vlEarned = vlAllowedDays * vlHours;

			return Math.Round((double)(vlEarned - vlUsed) + excessVL, 2);
		}

		public async Task<double> GetUserVacationLeave(User user, DateTime month)
		{
			var allowedVLDaysContract = user?.UserContracts?.GetActiveAllowedVLDaysContract(month);
			var dailyHoursRequiredContract = user?.UserContracts?.GetActiveDailyHoursRequiredContract(month);

			var excessVL = (user.ExcessVL.FirstOrDefault(u => u.Year == month.Year - 1)?.Hours ?? 0.0) - (user.ExcessVL.FirstOrDefault(u => u.Year == month.Year)?.Hours ?? 0.0);
			var vl = (await this.GetProcessByTerm("92")).FirstOrDefault();
			var vlUsed = await this.context.Records
				.Where(r => r.Owner == user.Email && r.SubProcess.Process == vl && r.Date.Year == month.Year)
				.SumAsync(r => r.Hours);
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

			return Math.Round((double)(vlEarned - vlUsed) + excessVL, 2);
		}

		public async Task<double> GetUserTimeOff(User user, DateTime month)
		{
			var to = (await this.GetProcessByTerm("94")).FirstOrDefault();
			var toRecords = await this.context.Records
				.Where(r => r.Owner == user.Email && r.SubProcess.Process == to && r.Date.Year == month.Year)
				.ToListAsync();
			var toRequests = await this.context.Requests
				.Where(r => r.Requestor == user && r.Record != null && r.Type == Constants.RequestType.TIMEOFF && (r.Status == Constants.RequestStatus.APPROVED || r.Status == Constants.RequestStatus.ACKNOWLEDGED))
				.Include(r => r.Record)
				.ToListAsync();

			var toUsed = 0.0;
			foreach (var toRecord in toRecords)
			{
				var toRequest = toRequests.FirstOrDefault(tr => tr.Record.Id == toRecord.Id);
				toUsed += toRequest != null ? toRequest.Days : 1;
			}

			var allowedToDaysContract = user?.UserContracts?.GetActiveAllowedTODaysContract(month);
			var toAllowedDays = allowedToDaysContract.AllowedTODays > 0
				? allowedToDaysContract.AllowedTODays
				: user.UserLocation.AllowedTODays;

			return Math.Round(toAllowedDays - toUsed, 2);
		}

		public async Task<double> GetUserBirthdayLeave(User user, DateTime month)
		{
			var bl = (await this.GetProcessByTerm(Constants.ProcessTerm.BIRTHDAYLEAVE)).FirstOrDefault();
			var blRecords = await this.context.Records
				.Where(r => r.Owner == user.Email && r.SubProcess.Process == bl && r.Date.Year == month.Year)
				.ToListAsync();

			var hoursRequired = month.RequiredHoursToday(user);

			return Math.Round((user.UserLocation?.Code == "co-ph" && (user.UserType?.Status == Constants.UserType.FULLTIME || user.UserType?.Status == Constants.UserType.PARTTIME) ? hoursRequired : 0) - blRecords.Sum(blr => blr.Hours), 2);
		}

		public async Task<IEnumerable<dynamic>> GetUserVLRecords(string owner)
		{
			var vl = (await this.GetProcessByTerm("92")).FirstOrDefault();
			var user = this.GetUserByEmail(owner);

			var results = await (from record in this.context.Records
								 where record.Owner == owner && record.SubProcess.Process == vl
								 group record by record.Date.Year
					into g
								 select new { year = g.Key, hours = g.Sum(i => i.Hours), allowed = 0 }).AsNoTracking()
				.ToListAsync();

			return results;
		}

		public async Task<IEnumerable<Record>> GetUserVLRecords(string owner, int year)
		{
			var vl = (await this.GetProcessByTerm("92")).FirstOrDefault();

			return await this.context.Records
				.Where(r => r.Owner == owner && r.SubProcess.Process == vl && r.Date.Year == year)
				.OrderBy(o => o.Date)
				.ToListAsync();
		}

		public async Task<double> GetUserTO(User user, DateTime month)
		{
			var to = (await this.GetProcessByTerm("94")).FirstOrDefault();
			var toUsed = await this.context.Records
				.Where(r => r.Owner == user.Email && r.SubProcess.Process == to && r.Date.Year == month.Year)
				.CountAsync();
			var allowedToDaysContract = user?.UserContracts?.GetActiveAllowedTODaysContract(month);
			var toAllowedDays = allowedToDaysContract.AllowedTODays > 0
				? allowedToDaysContract.AllowedTODays
				: user.UserLocation.AllowedTODays;

			return toAllowedDays - toUsed;
		}

		public async Task<double> GetUserSL(User user, DateTime month)
		{
			var allowedSLDaysContract = user?.UserContracts?.GetActiveAllowedSLDaysContract(month);
			var dailyHoursRequiredContract = user?.UserContracts?.GetActiveDailyHoursRequiredContract(month);

			var sl = (await this.GetProcessByTerm("90")).FirstOrDefault();
			var slUsed = await this.context.Records
				.Where(r => r.Owner == user.Email && r.SubProcess.Process == sl && r.Date.Year == month.Year)
				.SumAsync(r => r.Hours);
			var earnedRegDay = user.RegularizationDate != null && user.RegularizationDate?.Year == month.Year && user.RegularizationDate?.Day <= 15
				? 1
				: .5;
			var slAllowedDays = allowedSLDaysContract.AllowedSLDays > 0
				? allowedSLDaysContract.AllowedSLDays
				: user.UserLocation.AllowedSLDays > 0
					? user.UserLocation.AllowedSLDays
					: 1.25 * (user.RegularizationDate != null && user.RegularizationDate?.Year == month.Year
						? 12 - user.RegularizationDate?.Month + earnedRegDay
						: 12);

			var slAllowance = (dailyHoursRequiredContract.DailyHoursRequired > 0 ? dailyHoursRequiredContract.DailyHoursRequired : user.UserLocation.DailyHours) * slAllowedDays;

			return Math.Round((double)slAllowance - slUsed, 2);
		}

		public async Task<IEnumerable<User>> GetAllUsers()
		{
			this.logger.LogInformation("Getting all users from the database");

			return await this.context.Users.Include(u => u.UserLocation)
				.Include(u => u.EmploymentCode)
				.Include(u => u.UserType)
				.Include(u => u.ExcessHours)
				.OrderBy(u => u.DisplayName)
				.ToListAsync();
		}

		public async Task<IEnumerable<User>> GetActiveUsersIncludeRole()
		{
			this.logger.LogInformation("Getting all users from the database");

			return await this.context.Users
				.Where(u => u.UserType.Id < 4)
				.Include(u => u.UserLocation)
				.Include(u => u.UserType)
				.Include(u => u.Role)
				.OrderBy(u => u.DisplayName)
				.ToListAsync();
		}

		public async Task<IEnumerable<User>> GetActiveUsers()
		{
			this.logger.LogInformation("Getting all active users from the database");

			return await this.context.Users
				.Where(u => u.UserType.Id < 4)
				.Include(u => u.EmploymentCode)
				.Include(u => u.UserType)
				.Include(u => u.ExcessHours)
				.OrderBy(u => u.DisplayName)
				.ToListAsync();
		}

		public async Task<IEnumerable<User>> GetDetailedUsers()
		{
			return await this.context.Users
				.Include(u => u.UserLocation)
				.Include(u => u.EmploymentCode)
				.Include(u => u.UserType)
				.Include(u => u.LineManager)
				.Include(u => u.ExcessHours)
				.Include(u => u.ExcessVL)
				.Include(u => u.UserContracts)
				.ToListAsync();
		}

		public async Task<User> GetDetailedUserById(int id)
		{
			return await this.context.Users
				.Where(u => u.Id == id)
				.Include(u => u.UserLocation)
				.Include(u => u.EmploymentCode)
				.Include(u => u.UserType)
				.Include(u => u.LineManager)
				.Include(u => u.ExcessHours)
				.Include(u => u.ExcessVL)
				.Include(u => u.UserContracts)
				.FirstOrDefaultAsync();
		}

		public async Task<Request> GetRequest(int id)
		{
			return await this.context.Requests.Where(r => r.Id == id)
				.Include(r => r.Requestor)
					.ThenInclude(r => r.LineManager)
				.Include(r => r.Record)
					.ThenInclude(r => r.SubProcess)
						.ThenInclude(sp => sp.Process)
				.FirstOrDefaultAsync();
		}

		public async Task<Request> GetRequest(User requestor, DateTime date, IEnumerable<string> types)
		{
			return await this.context.Requests.Where(r => r.Requestor == requestor && r.StartDate.Date == date.Date
					&& (types == null || !types.Any() || types.Contains(r.Type)))
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<Request>> GetRequests()
		{
			return await this.context.Requests.Include(r => r.Requestor)
				.Include(r => r.AuthorizedBy)
				.ToListAsync();
		}

		public async Task<IEnumerable<Request>> GetRequests(int year)
		{
			return await this.context.Requests
				.Where(r => r.StartDate.Year == year)
				.Include(r => r.Requestor)
				.Include(r => r.AuthorizedBy)
				.OrderByDescending(r => r.Date)
				.ToListAsync();
		}

		public async Task<IEnumerable<Request>> GetRequests(string type)
		{
			return await this.context.Requests.Where(r => r.Type == type)
				.ToListAsync();
		}

		public async Task<IEnumerable<Request>> GetRequestsByType(string type, DateTime year)
		{
			if (type.CompareTo("All") == 0)
			{
				return await this.context.Requests.Where(r => r.StartDate.Year == year.Year)
					.Include(r => r.Requestor).ThenInclude(r => r.LineManager)
					.Include(r => r.Requestor).ThenInclude(r => r.UserLocation)
					.Include(r => r.AuthorizedBy)
					.Include(r => r.AcknowledgedBy)
					.ToListAsync();
			}
			else
			{
				return await this.context.Requests.Where(r => r.Type == type && r.StartDate.Year == year.Year)
					.Include(r => r.Requestor).ThenInclude(r => r.LineManager)
					.Include(r => r.Requestor).ThenInclude(r => r.UserLocation)
					.Include(r => r.AuthorizedBy)
					.Include(r => r.AcknowledgedBy)
					.ToListAsync();
			}
		}

		public async Task<IEnumerable<Request>> GetRequests(string status, DateTime month)
		{
			return await this.context.Requests.Where(r => r.Status == status && r.StartDate.Year == month.Year).ToListAsync();
		}

		public async Task<IEnumerable<Request>> GetRequests(User user, string type, string status, DateTime month)
		{
			return await this.context.Requests.Where(r => r.Requestor == user && r.Type == type && r.Status == status && r.StartDate.Year == month.Year)
				.Include(r => r.Requestor)
				.ToListAsync();
		}

		public async Task<IEnumerable<Request>> GetRequests(User user, DateTime month, IEnumerable<string> types)
		{
			var result = this.context.Requests.Where(r => r.Requestor == user && r.StartDate.Year == month.Year);

			if (types != null && types.Any())
			{
				return await result.Include(r => r.Record)
					.ThenInclude(r => r.SubProcess)
						.ThenInclude(sp => sp.Process)
						.Where(r => types.Contains(r.Type)).ToListAsync();
			}
			else
			{
				return await result.Include(r => r.Record)
					.ThenInclude(r => r.SubProcess)
						.ThenInclude(sp => sp.Process)
						.ToListAsync();
			}
		}

		public async Task<IEnumerable<Request>> GetRequests(DateTime date, IEnumerable<string> types, IEnumerable<string> statuses, bool sameMonth = false)
		{
			return await this.context.Requests.Where(r => r.StartDate.Year == date.Year && (!sameMonth || r.StartDate.Month == date.Month)
				&& (types == null || !types.Any() || types.Contains(r.Type))
				&& (statuses == null || !statuses.Any() || statuses.Contains(r.Status)))
				.Include(r => r.Record)
					.ThenInclude(r => r.SubProcess)
						.ThenInclude(sp => sp.Process)
				.ToListAsync();
		}

		public async Task<IEnumerable<Request>> GetRequests(User user, DateTime date, IEnumerable<string> types, IEnumerable<string> statuses, bool sameMonth = false)
		{
			return await this.context.Requests.Where(r => r.Requestor == user 
				&& r.StartDate.Year == date.Year
				&& (!sameMonth || r.StartDate.Month == date.Month)
				&& (types == null || !types.Any() || types.Contains(r.Type))
				&& (statuses == null || !statuses.Any() || statuses.Contains(r.Status)))
				.ToListAsync();
		}

		public async Task<IEnumerable<Request>> GetRequests(User user, string type)
		{
			return await this.context.Requests.Where(r => r.Requestor == user && r.Type == type)
				.Include(r => r.Requestor).ThenInclude(r => r.UserType)
				.ToListAsync();
		}

		public async Task<IEnumerable<Request>> GetRequestsWithinMonth(User user, DateTime month, IEnumerable<string> types, IEnumerable<string> statuses)
		{
			return await this.context.Requests
				.Where(r => r.Requestor == user && r.StartDate.Month == month.Month
					&& (types == null || !types.Any() || types.Contains(r.Type))
					&& (statuses == null || !statuses.Any() || statuses.Contains(r.Status)))
				.ToListAsync();
		}

		public async Task<IEnumerable<Request>> GetRequestsLM(User lineManager)
		{
			return await this.context.Requests
				.Where(r => r.Requestor.UserType.Id < 4 && r.Requestor.LineManager == lineManager && r.Type != RequestType.COVL)
				.Include(r => r.Requestor).ThenInclude(r => r.LineManager)
				.Include(r => r.Requestor).ThenInclude(r => r.UserType)
				.Include(r => r.AuthorizedBy)
				.Include(r => r.AcknowledgedBy)
				.ToListAsync();
		}

		public async Task<IEnumerable<Request>> GetRequestsLM(User lineManager, int year)
		{
			return await this.context.Requests
				.Where(r => r.Requestor.UserType.Id < 4 && r.Requestor.LineManager == lineManager && r.Type != RequestType.COVL && r.StartDate.Year == year)
				.Include(r => r.Requestor).ThenInclude(r => r.LineManager)
				.Include(r => r.Requestor).ThenInclude(r => r.UserType)
				.Include(r => r.AuthorizedBy)
				.Include(r => r.AcknowledgedBy)
				.OrderByDescending(r => r.Date)
				.ToListAsync();
		}

		public async Task<IEnumerable<dynamic>> GetRecordsByTerm(string owner, string term)
		{
			var last60Days = DateTime.Now.AddDays(-60);
			var pattern = term.Replace(" ", "%");
			var searchDescription = $"[Description] LIKE '%{pattern}%'";
			var searchTask = $"[Task] LIKE '%{pattern}%'";
			var filterOwner = $"[Owner] = '{owner}'";
			var filterDate = $"[Date] >= '{last60Days:yyyy-MM-dd}'";
			var query = $"SELECT * FROM Records WHERE {filterOwner} AND {filterDate} AND ({searchDescription} OR {searchTask})";

			return await this.context.Records.FromSql(query)
				.AsNoTracking()
				.OrderByDescending(r => r.Date)
				.Select(r => r.Description)
				.Distinct()
				.ToListAsync();
		}

		public async Task<IEnumerable<Record>> GetRecordsByYear(User user, IEnumerable<int> processIds, int year)
		{
			return await this.context.Records
				.Where(r => r.Owner == user.Email && r.Date.Year == year && (processIds == null || !processIds.Any() || processIds.Contains(r.SubProcess.Process.Id)))
				.Include(r => r.SubProcess)
					.ThenInclude(sp => sp.Process)
				.ToListAsync();
		}

		public async Task<IEnumerable<Record>> GetRecordsByDate(User user, Process process, DateTime date)
		{
			return await this.context.Records
				.Where(r => r.Owner == user.Email && r.SubProcess.Process == process && r.Date.Year == date.Year && r.Date.Month == date.Month && r.Date.Day == date.Day)
				.Include(r => r.SubProcess)
					.ThenInclude(sp => sp.Process)
				.ToListAsync();
		}

		public void DeleteRecordSymmary(string owner, RecordSummary summary)
		{
			var user = this.GetUserByEmail(owner);
			if (summary.Month.Month == 12)
			{
				var excessHours = user.ExcessHours.Where(e => e.Year == summary.Month.Year).ToList();

				foreach (var excessHour in excessHours)
				{
					user.ExcessHours.Remove(excessHour);
					this.context.UserExcessHours.Remove(excessHour);
				}
			}

			// Unlock timesheet for the month
			var records = this.context.Records.Where(r => r.Owner == owner
					&& r.Date.Year == summary.Month.Year
					&& r.Date.Month == summary.Month.Month)
				.ToList();
			records.ForEach(r => r.IsLocked = false);

			this.context.RecordSummaries.Remove(summary);
		}

		public void ToggleDeleteProcess(Process proccess)
		{
			proccess.IsDeleted = !proccess.IsDeleted;
			this.context.Processes.Update(proccess);
		}

		public void ToggleDeleteSubProccess(SubProcess subProccess)
		{
			subProccess.IsDeleted = !subProccess.IsDeleted;
			this.context.SubProcesses.Update(subProccess);
		}

		public void ToggleDeleteUserLocation(UserLocation office)
		{
			office.IsDeleted = !office.IsDeleted;
			this.context.UserLocations.Update(office);
		}

		public async Task<bool> IsTimesheetSubmitted(User user, DateTime month)
		{
			this.logger.LogInformation("Checking if given month is already submitted");

			return await this.context.RecordSummaries
				.Where(s => s.User == user && s.Month.Year == month.Year && s.Month.Month == month.Month)
				.AnyAsync();
		}

		public async Task<IEnumerable<DateTime>> GetMidshifts(string owner, DateTime month)
		{
			this.logger.LogInformation($"Getting all midshift summary for {month.FirstDayOfMonth()}");

			var result = await this.context.Records.Where(r => r.Owner == owner
					&& r.Date.Year == month.Year
					&& r.Date.Month == month.Month
					&& (r.Description.Contains("[midshift]") || r.Description.Contains("[mid-shift]")))
				.Select(x => x.Date)
				.Distinct()
				.ToListAsync();

			//var test = await (from r in this.context.Records.Where(r => r.Date.Year == month.Year && r.Date.Month == month.Month)
			//				  join t in this.context.TimeLogs on
			//					  r.Owner equals t.Owner
			//					  into joined
			//				  where (r.Description.Contains("[midshift]") || r.Description.Contains("[mid-shift]"))
			//				  select new { r.Date }).Distinct().ToListAsync();

			//var result = await (from record in this.context.Records
			//		where record.Owner == owner && record.Date >= start && record.Date <= end &&
			//		      (record.Description.Contains("[midshift]") || record.Description.Contains("[mid-shift]"))
			//		group record by new {record.Date.Month, record.Owner}
			//		into g
			//		select new NSTS
			//		{
			//			Month = month.FirstDayOfMonth(),
			//			User = user,
			//			Midshift = g.Select(l => l.Date)
			//				.Distinct()
			//				.Count()
			//		}).AsNoTracking()
			//	.ToListAsync();

			return result;
		}

		public async Task<IEnumerable<SiteValues>> GetSiteValues()
		{
			return await this.context.SiteValues.ToListAsync();
		}

		public async Task<IEnumerable<SiteValues>> GetSiteValuesByKeys(IEnumerable<string> keys)
		{
			return await this.context.SiteValues.Where(st => keys == null || keys.Contains(st.Key)).ToListAsync();
		}

		public async Task<SiteValues> GetSiteValuesByKey(string key)
		{
			return await this.context.SiteValues.FirstOrDefaultAsync(sv => string.Equals(sv.Key, key, StringComparison.Ordinal));
		}

		public async Task<string> GetSiteValuesValueByKey(string key)
		{
			var siteValue = await this.context.SiteValues.FirstOrDefaultAsync(sv => string.Equals(sv.Key, key, StringComparison.Ordinal));
			if (siteValue != null && !string.IsNullOrWhiteSpace(siteValue.Value))
			{
				return siteValue.Value;
			}
			else
			{
				return Constants.SiteValues.DEFAULT_KEY_VALUE_PAIRS.GetValueOrDefault(key);
			}
		}

		public async Task AddSiteValues(SiteValues siteValues)
		{
			await this.context.SiteValues.AddAsync(siteValues);
		}

		public void UpdateSiteValues(SiteValues siteValues)
		{
			this.context.SiteValues.Update(siteValues);
		}

		public void DeleteSiteValues(SiteValues siteValues)
		{
			this.context.SiteValues.Remove(siteValues);
		}

		public async Task<IEnumerable<Role>> GetRoles()
		{
			return await this.context.Roles.ToListAsync();
		}

		public async Task<Role> GetRoleById(int id)
		{
			return await this.context.Roles.FirstOrDefaultAsync(r => r.Id == id);
		}

		public async Task<Role> GetUserRole(string email)
		{
			return await this.context.Users.Where(u => u.Email == email).Select(u => u.Role).FirstOrDefaultAsync();
		}

		public async Task AddRole(Role role)
		{
			await this.context.Roles.AddAsync(role);
		}

		public void UpdateRole(Role role)
		{
			this.context.Roles.Update(role);
		}

		public void DeleteRole(Role role)
		{
			this.context.Roles.Remove(role);
		}

		public void AddUserContract(UserContract userContract)
		{
			this.context.UserContracts.Add(userContract);
		}

		public void UpdateUserContract(UserContract userContract)
		{
			this.context.UserContracts.Update(userContract);
		}

		public void RemoveUserContract(UserContract userContract)
		{
			this.context.UserContracts.Remove(userContract);
		}

		public async Task<User> GetUserByAzureId(string azureId)
        {
			return await this.context.Users
				.Where(u => u.AzureId == azureId)
				.Include(u => u.UserLocation)
				.Include(u => u.EmploymentCode)
				.Include(u => u.UserType)
				.Include(u => u.LineManager)
				.Include(u => u.ExcessHours)
				.Include(u => u.ExcessVL)
				.Include(u => u.UserContracts)
				.Include(u => u.Thumbnail)
				.FirstOrDefaultAsync();
        }

		public async Task<IEnumerable<dynamic>> GetProcessAllocations(User user, DateTime? dateFrom, DateTime? dateTo)
		{
			var workProcesses = await this.GetProcessByType(ProcessType.WORK);
			var processHours = await this.context.Records
				.Where(r => r.Owner == user.Email
					&& workProcesses.Contains(r.SubProcess.Process)
					&& ((dateFrom == null || dateTo == null)
						|| (dateFrom == null && r.Date <= dateTo)
						|| (dateTo == null && r.Date >= dateFrom)
						|| (r.Date >= dateFrom && r.Date <= dateTo)))
				.Select(r => new
				{
					Process = r.SubProcess.Process,
					ProcessOwner = r.SubProcess.Process.Owner,
					Hours = r.Hours
				})
				.ToListAsync();

			return processHours
				.GroupBy(ph => ph.Process)
				.Select(g => new
				{
					Process = g.Key,
					WorkedHours = g.Select(ph => ph.Hours).Sum()
				});
		}


	}
}