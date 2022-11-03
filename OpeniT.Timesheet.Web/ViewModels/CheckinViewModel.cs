using Newtonsoft.Json;

namespace OpeniT.Timesheet.Web.ViewModels
{
	using System;
	using System.Runtime.Serialization;

	using Helpers;

	using Models;

	public class CheckinViewModel
	{
		public int Id { get; set; }

		public string CheckinType { get; set; }

		public DateTime CheckinIssueDate { get; set; }

		[JsonIgnore]
		public UserViewModel Owner { get; set; }

		public int? OwnerId => Owner?.Id;

		public string OwnerDisplayName => Owner?.DisplayName;

		public string OwnerEmail => Owner?.Email;

		public string OwnerComment { get; set; }

		public bool OwnerCommentLocked { get; set; }

		public DateTime OwnerCheckinDate { get; set; }

		[JsonIgnore]
		public UserViewModel LineManager { get; set; }

		public int? LineManagerId => LineManager?.Id;

		public string LineManagerDisplayName => LineManager?.DisplayName;

		public string LineManagerEmail => LineManager?.Email;

		public string LmComment { get; set; }

		public bool LmCommentLocked { get; set; }

		public DateTime LmCheckinDate { get; set; }

		public double CheckinQuantity { get; set; }

		public double CheckinPercentage { get; set; }

        public string KeyResultProgressType { get; set; }

        public double KeyResultTargetQuantity { get; set; }

        public double KeyResultTargetPercentage { get; set; }

        public double CheckinProgress =>    KeyResultProgressType == "Quantity" ? CheckinQuantity> KeyResultTargetQuantity ? 100 :(CheckinQuantity / KeyResultTargetQuantity) *100 :
                                            KeyResultProgressType == "Percentage" ? CheckinPercentage > KeyResultTargetPercentage ? 100 : (CheckinPercentage / KeyResultTargetPercentage) *100 : CheckinPercentage;

		public string Status { get; set; }

		public bool StatusLocked { get; set; }

		public bool IsDeleted { get; set; }
	}
}