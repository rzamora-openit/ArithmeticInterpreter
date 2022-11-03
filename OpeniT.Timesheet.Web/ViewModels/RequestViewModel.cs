namespace OpeniT.Timesheet.Web.ViewModels
{
	using System;

	public class RequestViewModel
	{
		public int Id { get; set; }

		public string Type { get; set; }

		public string Status { get; set; }

		public string FullType => $"{Constants.RequestType.Convert(Type)}{(Type == Constants.RequestType.VACATIONLEAVE && IsEL.GetValueOrDefault() ? " (EL)" : string.Empty)}";

		public DateTimeOffset Date { get; set; }

		public string DateString => Date.ToString("yyyy/MM/dd");

		public UserViewModel Requestor { get; set; }

		public DateTime StartDate { get; set; }

		public string StartDateString => StartDate.ToString("yyyy/MM/dd");

		public DateTime? EndDate { get; set; }

		public double Days { get; set; }

		public string DaysString => (Days == 1 ? "Full day" : Days == 0.5 ? "Half day" : $"{Days} Days");

		public UserViewModel AuthorizedBy { get; set; }

		public DateTimeOffset? AuthorizationDate { get; set; }

		public DateTimeOffset? AcknowledgementDate { get; set; }

		public DateTime? FollowUpDate { get; set; }

		public string Reason { get; set; }

		public string ApprovalMessage { get; set; }

		public RecordViewModel Record { get; set; }

		public bool? IsEL { get; set; }

		public string CcRecipients { get; set; }
	}
}