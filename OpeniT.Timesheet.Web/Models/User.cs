namespace OpeniT.Timesheet.Web.Models
{
	using System;
	using System.Collections.Generic;

	public class User
	{
		public int Id { get; set; }

        public string AzureId { get; set; }

        public string Email { get; set; }

		public string DisplayName { get; set; }

		public string Department { get; set; }

		public string JobTitle { get; set; }

		public UserThumbnail Thumbnail { get; set; }

		public ICollection<UserExcessHours> ExcessHours { get; set; }

		public ICollection<UserExcessVL> ExcessVL { get; set; }

		public ICollection<UserContract> UserContracts { get; set; }

		public User LineManager { get; set; }

		public UserLocation UserLocation { get; set; }

		public UserType UserType { get; set; }

		public EmploymentCode EmploymentCode { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? RegularizationDate { get; set; }

		public DateTime? TerminationDate { get; set; }

		public DateTime? BirthDate { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

		public DateTimeOffset? UpdatedAt { get; set; }

		public DateTimeOffset? LastLogin { get; set; }

		public DateTimeOffset? LastUpdate { get; set; }

		public bool IsImportAllowed { get; set; }

		public Role Role { get; set; }
	}
}