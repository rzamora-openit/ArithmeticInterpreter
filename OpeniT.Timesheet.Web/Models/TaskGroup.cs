namespace OpeniT.Timesheet.Web.Models
{
	using System;

	public class TaskGroup
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public bool IsDefault { get; set; }

		public string CreatedBy { get; set; }

		public DateTimeOffset CreatedAt { get; set; }

		public Process Process { get; set; }
	}
}