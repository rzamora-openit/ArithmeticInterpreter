namespace OpeniT.Timesheet.Web.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Models;

	public class PeopleViewModel
	{
		public int Id { get; set; }

		public string Email { get; set; }

		public string DisplayName { get; set; }

		public string Department { get; set; }

		public string JobTitle { get; set; }

		public int UserLocationId { get; set; }

		public int UserTypeId { get; set; }

		public int EmploymentCodeId { get; set; }

		public bool IsImportAllowed { get; set; }

		public DateTimeOffset CreatedAt { get; set; }

		public DateTimeOffset? UpdatedAt { get; set; }

		public DateTimeOffset? LastLogin { get; set; }

		public DateTimeOffset? LastUpdate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? RegularizationDate { get; set; }

		public DateTime? TerminationDate { get; set; }

		public DateTime? BirthDate { get; set; }

        public ICollection<UserExcessHours> ExcessHours { get; set; }
	}
}