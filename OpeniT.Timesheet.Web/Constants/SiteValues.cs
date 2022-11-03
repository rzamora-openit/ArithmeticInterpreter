using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Constants
{
	public static class SiteValues
	{
		#region Reminder
		public const string REMINDER_SENDER_KEY = "ReminderSender";
		public const string REMINDER_SEND_AS_KEY = "ReminderSendAs";
		public const string REMINDER_BCC_KEY = "ReminderBCC";
		public const string DEFAULT_REMINDER_SENDER_VALUE = "mmasaganda@openit.com";
		public const string DEFAULT_REMINDER_SEND_AS_VALUE = "HR@openit.com";
		public const string DEFAULT_REMINDER_BCC_VALUE = "rzamora@openit.com";
		#endregion Reminder

		#region CarryOverVL
		public const string COVL_SUBMISSION_START_KEY = "StartOfCarryOverVLSubmission";
		public const string COVL_SUBMISSION_END_KEY = "EndOfCarryOverVLSubmission";

		public const string DEFAULT_COVL_SUBMISSION_START_VALUE = "12-26";
		public const string DEFAULT_COVL_SUBMISSION_END_VALUE = "02-16";
		#endregion CarryOverVL

		#region Leave
		public const string BIRTHDAY_LEAVE_SUBMISSION_DAYS_START_KEY = "BirthdayLeaveSubmissionStartDays";
		public const string BIRTHDAY_LEAVE_SUBMISSION_DAYS_END_KEY = "BirthdayLeaveSubmissionEndDays";

		public const string DEFAULT_BIRTHDAY_LEAVE_SUBMISSION_DAYS_START_VALUE = "7";
		public const string DEFAULT_BIRTHDAY_LEAVE_SUBMISSION_DAYS_END_VALUE = "7";
		#endregion Leave

		#region Timesheet
		public const string TIMESHEET_DECEMBER_SUBMISSION_START_KEY = "TimesheetDecemberSubmissionStart";
		public const string DEFAULT_TIMESHEET_DECEMBER_SUBMISSION_START_VALUE = "12-26";
		#endregion Timesheet

		#region OKR
		public const string OKR_GRACE_PERIOD_DAYS_KEY = "OKRGracePeriodDays";
		public const string DEFAULT_OKR_GRACE_PERIOD_DAYS_KEY_VALUE = "45";
		#endregion OKR

		public static IReadOnlyDictionary<string, string> DEFAULT_KEY_VALUE_PAIRS = new Dictionary<string, string>()
		{
			{ REMINDER_SENDER_KEY, DEFAULT_REMINDER_SENDER_VALUE },
			{ REMINDER_SEND_AS_KEY, DEFAULT_REMINDER_SEND_AS_VALUE },
			{ REMINDER_BCC_KEY, DEFAULT_REMINDER_BCC_VALUE },
			{ COVL_SUBMISSION_START_KEY, DEFAULT_COVL_SUBMISSION_START_VALUE },
			{ COVL_SUBMISSION_END_KEY, DEFAULT_COVL_SUBMISSION_END_VALUE },
			{ BIRTHDAY_LEAVE_SUBMISSION_DAYS_START_KEY, DEFAULT_BIRTHDAY_LEAVE_SUBMISSION_DAYS_START_VALUE },
			{ BIRTHDAY_LEAVE_SUBMISSION_DAYS_END_KEY, DEFAULT_BIRTHDAY_LEAVE_SUBMISSION_DAYS_END_VALUE },
			{ TIMESHEET_DECEMBER_SUBMISSION_START_KEY, DEFAULT_TIMESHEET_DECEMBER_SUBMISSION_START_VALUE },
			{ OKR_GRACE_PERIOD_DAYS_KEY, DEFAULT_OKR_GRACE_PERIOD_DAYS_KEY_VALUE }
		};
	}
}
