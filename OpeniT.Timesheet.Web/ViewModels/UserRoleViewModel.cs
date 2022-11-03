namespace OpeniT.Timesheet.Web.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Models;

	public class UserRoleViewModel
	{
		public int Id { get; set; }

		public string Email { get; set; }

		public string DisplayName { get; set; }

		public string Department { get; set; }

		public string JobTitle { get; set; }

		public int UserLocationId { get; set; }

		public int UserTypeId { get; set; }

		public int RoleId { get; set; }
	}
}