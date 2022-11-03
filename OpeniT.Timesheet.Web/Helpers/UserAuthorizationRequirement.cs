namespace OpeniT.Timesheet.Web.Helpers
{
	using Microsoft.AspNetCore.Authorization;

	public class UserAuthorizationRequirement : IAuthorizationRequirement
	{
		protected bool Created { get; set; }
	}
}