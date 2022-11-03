namespace OpeniT.Timesheet.Web.ViewModels
{
	using System;
	using System.Runtime.Serialization;

	using Helpers;

	using Models;

	public class UserViewModel
	{
        [IgnoreDataMember]
        public int Id { get; set; }

		public string Email { get; set; }

		public string DisplayName { get; set; }

		public string Department { get; set; }

		public string JobTitle { get; set; }

		public DateTime? BirthDate { get; set; }

		public DateTime? RegularizationDate { get; set; }

		public DateTimeOffset? LastLogin { get; set; }

		public DateTimeOffset? LastUpdate { get; set; }

		[IgnoreDataMember]
		public User LineManager { get; set; }

		[IgnoreDataMember]
		public UserLocation UserLocation { get; set; }

		[IgnoreDataMember]
		public UserType UserType { get; set; }

		[IgnoreDataMember]
		public EmploymentCode EmploymentCode { get; set; }

		//[IgnoreDataMember]
		//public UserThumbnail Thumbnail { get; set; }

		public string Manager => this.LineManager?.DisplayName;

		public string Location => this.UserLocation?.Name;

		public string LocationCode => this.UserLocation?.Code;

		public string Status => this.UserType?.Status;

		public string Code => this.EmploymentCode?.Code;

		//public string ThumbnailPhoto => $"data:{this.Thumbnail?.ContentType};base64,{this.Thumbnail?.Content.ToBase64String()}";
	}
}