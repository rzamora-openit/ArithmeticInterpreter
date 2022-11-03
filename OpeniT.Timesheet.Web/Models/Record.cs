namespace OpeniT.Timesheet.Web.Models
{
	using System;

	public class Record
	{
		public int Id { get; set; }

		public string Task { get; set; }

		public string SubTask { get; set; }

		public string Location { get; set; }

		public DateTime Date { get; set; }

		public double Hours { get; set; }

		public string Description { get; set; }

        public bool IsWFH { get; set; }

        public string Owner { get; set; }

		public SubProcess SubProcess { get; set; }

		public bool IsFromExcel { get; set; }

		public bool IsLocked { get; set; }

		public double? EpochStart { get; set; }

		public int HangfireJobId { get; set; }

		public User InsertedBy { get; set; }
	}
}