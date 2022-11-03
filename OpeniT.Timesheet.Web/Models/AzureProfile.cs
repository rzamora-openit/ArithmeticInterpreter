namespace OpeniT.Timesheet.Web.Models
{
	public class AzureProfile
	{
        public string Id { get; set; }

        public string CompanyName { get; set; }

		public string DisplayName { get; set; }

		public string Department { get; set; }

		public string GivenName { get; set; }

		public string JobTitle { get; set; }


		public string PhysicalDeliveryOfficeName { get; set; }

		public string Surname { get; set; }

		public string UserPrincipalName { get; set; }

		public bool AccountEnabled { get; set; }

		public UserThumbnail Thumbnail { get; set; }

		public AzureProfile Manager { get; set; }
	}
}