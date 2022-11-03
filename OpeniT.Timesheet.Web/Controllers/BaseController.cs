namespace OpeniT.Timesheet.Web.Controllers
{
	using System;
	using System.Linq;
	using System.Security.Claims;
	using System.Threading.Tasks;

	using AutoMapper;

	using Helpers;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;
	using Microsoft.Extensions.Logging;

	using Models;

	using ViewModels;

	[Authorize]
	public abstract class BaseController : Controller
	{
		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			await base.OnActionExecutionAsync(context, next);

			var repository = (DataRepository)context.HttpContext.RequestServices.GetService(typeof(IDataRepository));
			var logger = (Logger<BaseController>)context.HttpContext.RequestServices.GetService(typeof(ILogger<BaseController>));
			var azure = (AzureHelper)context.HttpContext.RequestServices.GetService(typeof(AzureHelper));
			var mapper = (IMapper)context.HttpContext.RequestServices.GetService(typeof(IMapper));

			var identity = context.HttpContext.User.Identity as ClaimsIdentity;
			var email = identity?.Name;
			var user = repository.GetUserWithThumbnailByEmail(email);

			logger.LogInformation("Checking is user exists on database");
			var updated = false;
			AzureProfile azureProfile = null;

			if (user == null)
			{
				azureProfile = azure.GetAsync(email, "").Result;
                user = await repository.GetUserByAzureId(azureProfile.Id);

				if (user != null)
				{
					updated = true;

					var records = await repository.GetRecordsByOwner(user.Email);
                    foreach (var record in records)
                    {
						record.Owner = email;
						repository.UpdateRecord(record);
                    }

					user.Email = email;
				}
            }

			if (user != null)
			{
				var utc = DateTime.Now.ToUniversalTime();
				var lastUpdate = user.UpdatedAt?.AddDays(1).ToUniversalTime();
				if (user.UpdatedAt == null || lastUpdate < utc || updated)
				{
					if (azureProfile == null) azureProfile = azure.GetAsync(email, "").Result;

					azureProfile.Manager = azure.GetAsync(email, "/manager").Result;

					User currentLM = null;
					if (azureProfile.Manager != null)
                    {
						currentLM = repository.GetUserByEmail(azureProfile.Manager?.UserPrincipalName);

						if (currentLM == null)
						{
							currentLM = await repository.GetUserByAzureId(azureProfile.Manager?.Id);
						}

						if (currentLM == null)
						{
							var lineManagerProfile = azure.GetAsync(azureProfile.Manager?.UserPrincipalName, "").Result;
							lineManagerProfile.Manager = azure.GetAsync(lineManagerProfile?.UserPrincipalName, "/manager").Result;

							currentLM = new User
							{
								AzureId = lineManagerProfile?.Id,
								Email = lineManagerProfile?.UserPrincipalName,
								DisplayName = lineManagerProfile?.DisplayName,
								UserLocation = null, // Assignment coded on HomeController
								EmploymentCode = await repository.GetEmployeeCodeById(1),
								UserType = await repository.GetUserTypeById(1),
								LineManager = repository.GetUserByEmail(lineManagerProfile.Manager?.UserPrincipalName),
								Department = lineManagerProfile?.Department,
								JobTitle = lineManagerProfile?.JobTitle,
								CreatedAt = DateTime.Now,
								UpdatedAt = DateTime.Now,
								IsImportAllowed = true
							};

							repository.AddUser(currentLM);
							await repository.SaveChangesAsync(null);
						}
					}

					if (user.LineManager != currentLM)
					{
						logger.LogInformation($"Updating Line Manager of {user.Email} to {currentLM?.Email}");
						user.LineManager = currentLM;
						updated = true;
					}

					if (user.AzureId == null || user.AzureId != azureProfile.Id)
					{
						logger.LogInformation($"Updating Azure ID of {user.Email} to {azureProfile.Id}");
						user.AzureId = azureProfile.Id;
						updated = true;
					}

					if (user.Department != azureProfile.Department)
					{
						logger.LogInformation($"Updating Department of {user.Email} to {azureProfile.Department}");
						user.Department = azureProfile.Department;
						updated = true;
					}

					if (user.JobTitle != azureProfile.JobTitle)
					{
						logger.LogInformation($"Updating Job Title of {user.Email} to {azureProfile.JobTitle}");
						user.JobTitle = azureProfile.JobTitle;
						updated = true;
					}

					if (user.DisplayName != azureProfile.DisplayName)
					{
						logger.LogInformation($"Updating Display Name of {user.Email} to {azureProfile.DisplayName}");
						user.DisplayName = azureProfile.DisplayName;
						updated = true;
					}

					var userContent = user.Thumbnail?.Content;
					var azureContent = azureProfile.Thumbnail?.Content;
					if (userContent == null && azureContent != null)
					{
						logger.LogInformation($"Updating User Thumbnail of {user.Email}");
						user.Thumbnail = azureProfile.Thumbnail;
						updated = true;
					}

					if (updated)
					{
						logger.LogInformation($"Saving changes of {user.Email} to database");
						user.UpdatedAt = DateTime.Now;
						repository.UpdateUser(user);
					}
				}

				user.LastLogin = DateTime.Now;
				repository.UpdateUser(user);
				await repository.SaveChangesAsync(null);

				this.ViewData["IsTimesheetSubmitted"] = await repository.IsTimesheetSubmitted(user, DateTime.Now.AddMonths(-1).Date);
				this.ViewData["UserProfile"] = mapper.Map<UserViewModel>(user);
				var users = await repository.GetUserByLM(user);				
				this.ViewData["IsLM"] = users.Any();				
				this.ViewData["IsLMDelegated"] = false;
			}
		}
	}
}