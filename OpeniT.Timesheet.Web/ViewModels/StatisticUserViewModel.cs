using OpeniT.Timesheet.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.ViewModels
{
    public class StatisticUserViewModel
    {
		[IgnoreDataMember]
		public int Id { get; set; }

		public string Email { get; set; }

		public string DisplayName { get; set; }

		public string Department { get; set; }

		public string JobTitle { get; set; }

		public DateTimeOffset? LastLogin { get; set; }

		public DateTimeOffset? LastUpdate { get; set; }

		[IgnoreDataMember]
		public User LineManager { get; set; }

		[IgnoreDataMember]
		public UserLocation UserLocation { get; set; }

		[IgnoreDataMember]
		public EmploymentCode EmploymentCode { get; set; }

		public string Manager => this.LineManager?.DisplayName;

		public string Location => this.UserLocation?.Name;

		public string LocationCode => this.UserLocation?.Code;

		public string Code => this.EmploymentCode?.Code;
	}
}
