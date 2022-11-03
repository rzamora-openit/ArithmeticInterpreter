using Newtonsoft.Json;

namespace OpeniT.Timesheet.Web.ViewModels
{
	using Models;

	public class SubProcessViewModel
	{
		public int Id { get; set; }

		public int SPid { get; set; }

		public string Name { get; set; }

		public bool IsDeleted { get; set; }

		public string OwnerDisplayName { get; set; }

		public string OwnerEmail { get; set; }

		public string DeputyOwnerDisplayName { get; set; }

		public string DeputyOwnerEmail { get; set; }

		public string Display => $"{this.SPid} - {this.Name}";

		[JsonIgnore]
		public Process Process { get; set; }
	}
}