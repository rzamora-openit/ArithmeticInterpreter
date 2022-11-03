namespace OpeniT.Timesheet.Web.Models
{
	using System.Collections.Generic;

	public class SubProcess
	{
		public int Id { get; set; }

		public int SPid { get; set; }

		public string Name { get; set; }

		public bool IsDeleted { get; set; }

		public virtual User Owner { get; set; }

		public User DeputyOwner { get; set; }

		public Process Process { get; set; }

		public ICollection<Record> Records { get; set; }
	}
}