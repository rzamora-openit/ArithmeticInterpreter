namespace OpeniT.Timesheet.Web.Models
{
	using System;

	public class Request
	{
		public int Id { get; set; }

		public string Type { get; set; }

		public string Status { get; set; }

		public DateTimeOffset Date { get; set; }

		public User Requestor { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public double Days { get; set; }

		public User AuthorizedBy { get; set; }

		public DateTimeOffset? AuthorizationDate { get; set; }

		public User AcknowledgedBy { get; set; }

		public DateTimeOffset? AcknowledgementDate { get; set; }

		public DateTime? FollowUpDate { get; set; }

		public string Reason { get; set; }

		public string ApprovalMessage { get; set; }

		public Record Record { get; set; }

		public bool? IsEL { get; set; }

		public string CcRecipients { get; set; }
	}
}