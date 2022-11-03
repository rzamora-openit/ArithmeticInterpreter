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

	public interface IDataRepository
	{
        Task<IEnumerable<Process>> GetAllProcesses(bool showDeleted);
		Task<IEnumerable<Process>> GetOwnedProcesses(bool showDeleted);

		Task<IEnumerable<dynamic>> GetProcessUris();

		Task<IEnumerable<Record>> GetRecordsByOwner(string owner, DateTime date);

		Task<IEnumerable<Record>> GetRecordsByHangfire(int hangfireJobId);
		Task<IEnumerable<Record>> GetRecordsByOwner(string owner, bool? isFromExcel);

		Task<RecordSummary> GetRecordSummaryByUserMonth(User user, DateTime month);

		Task<IEnumerable<RecordSummary>> GetRecordSummaryByUser(User user);

        Task<IEnumerable<RecordSummary>> GetRecordSummaryByMonth(DateTime month);
        Task<IEnumerable<RecordSummary>> GetRecordSummaryByDate(User user, DateTime date, bool includeComingYears = false);
        Task<IEnumerable<Process>> GetProcessByType(string type);

        Task<IEnumerable<Process>> GetProcessByTerm(string term);

        Task<Process> GetProcessById(int id);
        Task<SubProcess> GetSubProcessById(int id);
        Task<TaskGroup> GetTaskGroupById(int id);
        Task<TaskGroup> GetTaskGroupByName(int processId, string name);
        Task<IEnumerable<SubProcess>> GetAllSubProcesses();

        Task<IEnumerable<EmploymentCode>> GetAllEmploymentCodes();

        Task<IEnumerable<UserLocation>> GetAllUserLocations(bool showDeleted);

        Task<IEnumerable<UserType>> GetAllUserTypes();

        Task<IEnumerable<int>> GetDistinctYears();
        Task<IEnumerable<int>> GetDistinctYears(string owner);
        Task<Record> GetRecordById(int id);
        Task<IEnumerable<Record>> GetRecordsByOwner(string owner);
        Task<Record> GetRecordByDescription(string owner, string description);

        Task<Record> GetRecordByProcessId(string owner, int processId, DateTime date);
        Task<RecordSummary> GetRecordSummaryById(int id);
        Task<UserType> GetUserTypeById(int id);

        User GetUserByEmail(string email);

        Task<User> GetUserByEmailWithoutDetails(string email);
        User GetUserById(int id);
        Task<IEnumerable<User>> GetUserByLM(User lineManager);
        Task<IEnumerable<User>> GetUserByLMEmail(string Email);

        User GetUserWithThumbnailByEmail(string email);

		void AddOffice(UserLocation office);

		void AddProcess(Process process);

		void AddSubProcess(int id, SubProcess subProcess);

		void StopActiveTimer(string owner, double timeStop);

		Task RevertInsertedHolidays(int hangfireJobId);
		Task<bool> SaveChangesAsync(string owner);
		void AddRecord(int id, Record record);
		void AddRecordSummary(string owner, RecordSummary summary);
		void UpdateRecordSummary(RecordSummary summary);
		Task SubmitRecordSummary(string owner, RecordSummary summary);

		void UnlockRecordSummary(string owner, RecordSummary summary);
		void AddRequest(Request request);

		void UpdateRequest(Request request);
		void RemoveRequest(Request request);

		void AddUserExcessHours(UserExcessHours excessHours);

		void UpdateUserExcessHours(UserExcessHours excessHours);

		void RemoveUserExcessHours(UserExcessHours excessHours);
		void AddUserExcessVL(UserExcessVL excessVL);
		Task<IEnumerable<User>> GetUsersByLocation(UserLocation location);
		Task<IEnumerable<User>> GetUsersByBirthdayMonth(DateTime dateTime);

		Task<IEnumerable<User>> GetUsersByBirthdayDay(DateTime dateTime);
		Task<IEnumerable<User>> GetUsersByAnniversaryMonth(DateTime dateTime);

		Task<IEnumerable<User>> GetUsersByAnniversaryDay(DateTime dateTime);

		Task<IEnumerable<User>> GetUsersByEmails(IEnumerable<string> emails);

		void AddUser(User user);
		void AddTaskGroup(int id, TaskGroup taskGroup);
		void UpdateUser(User user);
		void UpdateRecord(int id, Record record);

		void UpdateRecord(Record record);
		void ToggleImport(int id);
		void ToggleExcessVL(User user, int year);

		void DeleteCarryoverVLRequest(User requestor, int year);
		void DeleteRecords(Record[] records);

		void DeleteRecord(Record record);
		void DeleteRecordOnly(Record record);
		void DeleteRequest(Request request);
		void DeleteTaskGroup(TaskGroup taskGroup);

		Task<double> GetMonthTotalByOwner(string owner, DateTime month);

		Task<double> GetYearTotalByOwner(string owner, int year);

		void AddRecord(Record record);
		Task<UserLocation> GetUserLocationById(int id);

		Task<User> GetUserLocationByEmail(string email);
		Task<UserContract> GetUserContractById(int id);

		Task<EmploymentCode> GetEmployeeCodeById(int id);

		Task<double> GetActiveTimerDuration(string owner);

		Task<List<UserExcessHours>> GetUserExcessHours(User user);

		Task<double> GetUserExcessHours(User user, DateTime month);
		Task<UserExcessHours> GetUserExcessHoursById(int id);
		Task<double> GetCurrentExcessHours(User user, DateTime month, bool excludeComingNegativeDiffs = true);

		Task<double> GetUserVL(User user, DateTime month);
		Task<double> GetUserVacationLeave(User user, DateTime month);

		Task<double> GetUserTimeOff(User user, DateTime month);

		Task<double> GetUserBirthdayLeave(User user, DateTime month);

		Task<IEnumerable<dynamic>> GetUserVLRecords(string owner);

		Task<IEnumerable<Record>> GetUserVLRecords(string owner, int year);

		Task<double> GetUserTO(User user, DateTime month);

		Task<double> GetUserSL(User user, DateTime month);

		Task<IEnumerable<User>> GetAllUsers();
		Task<IEnumerable<User>> GetActiveUsersIncludeRole();
		Task<IEnumerable<User>> GetActiveUsers();
		Task<IEnumerable<User>> GetDetailedUsers();
		Task<User> GetDetailedUserById(int id);
		Task<Request> GetRequest(int id);
		Task<Request> GetRequest(User requestor, DateTime date, IEnumerable<string> types);
		Task<IEnumerable<Request>> GetRequests();
		Task<IEnumerable<Request>> GetRequests(int year);
		Task<IEnumerable<Request>> GetRequests(string type);
		Task<IEnumerable<Request>> GetRequestsByType(string type, DateTime year);
		Task<IEnumerable<Request>> GetRequests(string status, DateTime month);
		Task<IEnumerable<Request>> GetRequests(User user, string type, string status, DateTime month);
		Task<IEnumerable<Request>> GetRequests(User user, DateTime month, IEnumerable<string> types);
		Task<IEnumerable<Request>> GetRequests(DateTime date, IEnumerable<string> types, IEnumerable<string> statuses, bool sameMonth = false);

		Task<IEnumerable<Request>> GetRequests(User user, DateTime date, IEnumerable<string> types, IEnumerable<string> statuses, bool sameMonth = false);
		Task<IEnumerable<Request>> GetRequests(User user, string type);
		Task<IEnumerable<Request>> GetRequestsWithinMonth(User user, DateTime month, IEnumerable<string> types, IEnumerable<string> statuses);
		Task<IEnumerable<Request>> GetRequestsLM(User lineManager);
		Task<IEnumerable<Request>> GetRequestsLM(User lineManager, int year);

		Task<IEnumerable<dynamic>> GetRecordsByTerm(string owner, string term);

		Task<IEnumerable<Record>> GetRecordsByYear(User user, IEnumerable<int> processIds, int year);
		Task<IEnumerable<Record>> GetRecordsByDate(User user, Process process, DateTime date);

		void DeleteRecordSymmary(string owner, RecordSummary summary);

		void ToggleDeleteProcess(Process proccess);

		void ToggleDeleteSubProccess(SubProcess subProccess);

		void ToggleDeleteUserLocation(UserLocation office);

		Task<bool> IsTimesheetSubmitted(User user, DateTime month);

		Task<IEnumerable<DateTime>> GetMidshifts(string owner, DateTime month);

		Task<IEnumerable<SiteValues>> GetSiteValues();

		Task<IEnumerable<SiteValues>> GetSiteValuesByKeys(IEnumerable<string> keys);
		Task<SiteValues> GetSiteValuesByKey(string key);
		Task<string> GetSiteValuesValueByKey(string key);

		Task AddSiteValues(SiteValues siteValues);
		void UpdateSiteValues(SiteValues siteValues);

		void DeleteSiteValues(SiteValues siteValues);

		Task<IEnumerable<Role>> GetRoles();

		Task<Role> GetRoleById(int id);

		Task<Role> GetUserRole(string email);
		Task AddRole(Role role);

		void UpdateRole(Role role);

		void DeleteRole(Role role);

		void AddUserContract(UserContract userContract);

		void UpdateUserContract(UserContract userContract);

		void RemoveUserContract(UserContract userContract);

		Task<User> GetUserByAzureId(string azureId);
		Task<IEnumerable<dynamic>> GetProcessAllocations(User user, DateTime? dateFrom, DateTime? dateTo);
	}
}