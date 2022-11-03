namespace OpeniT.Timesheet.Web.Helpers
{
	using System.Security.Claims;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Authorization;
	using Microsoft.Extensions.Logging;

	using Models;

	public class UserAuthorizationHandler : AuthorizationHandler<UserAuthorizationRequirement>
	{
		private readonly ILogger<DataRepository> logger;

		private readonly IDataRepository repositroy;

		public UserAuthorizationHandler(ILogger<DataRepository> logger, IDataRepository repository)
		{
			this.logger = logger;
			this.repositroy = repository;			
		}

		protected override Task HandleRequirementAsync(
			AuthorizationHandlerContext context,
			UserAuthorizationRequirement requirement)
		{
			var identity = context.User.Identity as ClaimsIdentity;
			var email = identity?.Name;
			var user = this.repositroy.GetUserByEmail(email);

			this.logger.LogInformation("Checking is user exists on database");
			if (user != null && user.UserLocation != null)
			{
				// Add custom claim to user scope
				var titleClaim = new Claim("title", user.JobTitle ?? "");
				var titleClaimExists = identity?.HasClaim(titleClaim.Type, titleClaim.Value);
				if (titleClaimExists != null && !(bool)titleClaimExists)
				{
					this.logger.LogInformation($"Adding Job Title of {user.Email} to identity claims");
					identity.AddClaim(titleClaim);
				}

				this.logger.LogInformation($"User {email} exists, requirement passed");
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}