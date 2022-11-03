namespace OpeniT.Timesheet.Web.ViewModels
{
	using System.Collections.Generic;

	using Microsoft.AspNetCore.Mvc.Rendering;

	public class UserLocationViewModel
	{
		public int LocationId { get; set; }

		public List<SelectListItem> UserLocations { get; set; }
	}
}