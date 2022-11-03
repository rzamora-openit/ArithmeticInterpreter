using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OpeniT.Timesheet.Web.Models;

namespace OpeniT.Timesheet.Web.ViewModels
{
	public class ProcessViewModel
	{
		public int Id { get; set; }

		[Required] public int Pid { get; set; }

		[Required] public string Name { get; set; }

		public string TaskUri { get; set; }

		public string TaskUriPrefix { get; set; }

		public string Type { get; set; }

		public bool IsDeleted { get; set; }

		public string OwnerDisplayName { get; set; }

		public string OwnerEmail { get; set; }

		public string DeputyOwnerDisplayName { get; set; }

		public string DeputyOwnerEmail { get; set; }

		public ICollection<SubProcessViewModel> SubProcesses { get; set; }

		public ICollection<TaskGroupViewModel> TaskGroups { get; set; }

		public string Display => $"{Pid} - {Name}";
	}
}