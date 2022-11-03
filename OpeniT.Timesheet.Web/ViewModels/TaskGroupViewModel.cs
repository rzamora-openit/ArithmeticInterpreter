namespace OpeniT.Timesheet.Web.ViewModels
{
	using Models;

	public class TaskGroupViewModel
	{
		public int Id { get; set; }

		public int ProcessId { get; set; }

		public string Name { get; set; }

		public bool IsDefault { get; set; }

		public string CreatedBy { get; set; }
	}
}