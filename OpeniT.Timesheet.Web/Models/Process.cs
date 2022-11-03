namespace OpeniT.Timesheet.Web.Models
{
	using System.Collections.Generic;

	public class Process
	{
		public int Id { get; set; }

		public int Pid { get; set; }

		public string Name { get; set; }

		public string TaskUri { get; set; }

		public string TaskUriPrefix { get; set; }

		public string Type { get; set; }

		public bool IsDeleted { get; set; }

		public User Owner { get; set; }

		public User DeputyOwner { get; set; }

		public ICollection<SubProcess> SubProcesses { get; set; }

		public ICollection<TaskGroup> TaskGroups { get; set; }
	}
}