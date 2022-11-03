namespace OpeniT.Timesheet.Web.Controllers.Api
{
	using AutoMapper;
	using Microsoft.ApplicationInsights;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;
	using OpeniT.Timesheet.Web.Helpers;
	using OpeniT.Timesheet.Web.Models;
	using OpeniT.Timesheet.Web.ViewModels;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	[Route("api/[controller]")]
	public class SiteValuesController : Controller
	{
		private readonly IDataRepository repository;
		private readonly IMapper mapper;
		private readonly ILogger<SiteValuesController> logger;		
		private readonly TelemetryClient telemetry;

		public SiteValuesController(
			IDataRepository repository,
			IMapper mapper,
			ILogger<SiteValuesController> logger,			
			TelemetryClient telemetry)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;
			this.telemetry = telemetry;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var siteValues = await this.repository.GetSiteValues();

				return this.Ok(this.mapper.Map<IEnumerable<SiteValuesViewModel>>(siteValues));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get site values: {ex.Message}");
			}

			return this.BadRequest("ERROR: Failed to get site values");
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] SiteValuesViewModel vm)
		{
			if (await this.IsUserRoleAllowed())
			{
				try
				{
					var siteValues = await this.repository.GetSiteValuesByKey(vm.Key);
					siteValues.Value = vm.Value;

					this.repository.UpdateSiteValues(siteValues);
					if (await this.repository.SaveChangesAsync(null))
					{
						return this.Ok(this.mapper.Map<SiteValuesViewModel>(siteValues));
					}
				}
				catch (Exception ex)
				{
					this.telemetry.TrackException(ex);
					this.logger.LogError($"Failed to update site values: {ex.Message}");
				}
			}

			return this.BadRequest("ERROR: Failed to update site values");
		}

		[HttpGet("multiple")]
		public async Task<IActionResult> GetMultiple([FromQuery] string[] keys)
		{
			if (await this.IsUserRoleAllowed())
			{
				try
				{
					var siteValues = new List<SiteValues>();
					foreach (var key in keys ?? Enumerable.Empty<string>())
					{
						var siteValue = await this.repository.GetSiteValuesByKey(key);
						if (siteValue == null && Constants.SiteValues.DEFAULT_KEY_VALUE_PAIRS.TryGetValue(key, out var value))
						{
							siteValue = new SiteValues();
							siteValue.Key = key;
							siteValue.Value = value;
							await this.repository.AddSiteValues(siteValue);
							await this.repository.SaveChangesAsync(null);
						}

						siteValues.Add(siteValue);
					}

					return this.Ok(this.mapper.Map<IEnumerable<SiteValuesViewModel>>(siteValues));
				}
				catch (Exception ex)
				{
					this.telemetry.TrackException(ex);
					this.logger.LogError($"Failed to get site values: {ex.Message}");
				}
			}

			return this.BadRequest("ERROR: Failed to get site values");
		}

		[HttpPut("multiple")]
		public async Task<IActionResult> UpdateMultiple([FromBody] IEnumerable<SiteValuesViewModel> vms)
		{
			if (await this.IsUserRoleAllowed())
			{
				try
				{
					var siteValues = new List<SiteValues>();
					foreach (var vm in vms ?? Enumerable.Empty<SiteValuesViewModel>())
					{
						var siteValue = await this.repository.GetSiteValuesByKey(vm.Key);
						if (siteValue != null)
						{
							siteValue.Value = vm.Value;
							this.repository.UpdateSiteValues(siteValue);
						}
						else
						{
							siteValue = new SiteValues();
							siteValue.Key = vm.Key;
							siteValue.Value = vm.Value;
							await this.repository.AddSiteValues(siteValue);
						}

						siteValues.Add(siteValue);
					}

					if (await this.repository.SaveChangesAsync(null))
					{
						return this.Ok(this.mapper.Map<IEnumerable<SiteValuesViewModel>>(siteValues));
					}
				}
				catch (Exception ex)
				{
					this.telemetry.TrackException(ex);
					this.logger.LogError($"Failed to update site values: {ex.Message}");
				}
			}

			return this.BadRequest("ERROR: Failed to update site values");
		}

		[HttpGet("roles")]
		public async Task<IActionResult> GetRoles()
		{
			try
			{
				var roles = await this.repository.GetRoles();

				return this.Ok(this.mapper.Map<IEnumerable<RoleViewModel>>(roles));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get roles: {ex.Message}");
			}

			return this.BadRequest("ERROR: Failed to get roles");
		}

		[HttpGet("users")]
		public async Task<IActionResult> GetUsers()
		{
			if (await this.IsUserRoleAllowed())
			{
				try
				{
					var users = await this.repository.GetActiveUsersIncludeRole();
					var results = this.mapper.Map<IEnumerable<UserRoleViewModel>>(users);

					return this.Ok(results);
				}
				catch (Exception ex)
				{
					this.telemetry.TrackException(ex);
					this.logger.LogError($"Failed to get all users: {ex.Message}");
				}
			}

			return this.BadRequest("Failed to get all users");
		}

		[HttpPut("users/{id}")]
		public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UserRoleViewModel vm)
		{
			if (await this.IsUserRoleAllowed())
			{
				try
				{
					var user = this.repository.GetUserById(id);
					user.Role = await this.repository.GetRoleById(vm?.RoleId ?? 0);

					this.repository.UpdateUser(user);
					if (await this.repository.SaveChangesAsync(null))
					{
						return this.Ok(this.mapper.Map<UserRoleViewModel>(user));
					}
				}
				catch (Exception ex)
				{
					this.telemetry.TrackException(ex);
					this.logger.LogError($"Failed to update user role: {ex.Message}");
				}
			}

			return this.BadRequest("Failed to update user role");
		}

		[HttpPost("roles")]
		public async Task<IActionResult> AddRole([FromBody] RoleViewModel vm)
		{
			if (await this.IsUserRoleAllowed())
			{
				try
				{
					var role = this.mapper.Map<Role>(vm);

					await this.repository.AddRole(role);
					if (await this.repository.SaveChangesAsync(null))
					{
						return this.Ok(this.mapper.Map<RoleViewModel>(role));
					}
				}
				catch (Exception ex)
				{
					this.telemetry.TrackException(ex);
					this.logger.LogError($"Failed to update role: {ex.Message}");
				}
			}

			return this.BadRequest("ERROR: Failed to update role");
		}

		[HttpPut("roles/{id}")]
		public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleViewModel vm)
		{
			if (await this.IsUserRoleAllowed())
			{
				try
				{
					var role = await this.repository.GetRoleById(id);
					role.Value = vm?.Value;

					this.repository.UpdateRole(role);
					if (await this.repository.SaveChangesAsync(null))
					{
						return this.Ok(this.mapper.Map<RoleViewModel>(role));
					}
				}
				catch (Exception ex)
				{
					this.telemetry.TrackException(ex);
					this.logger.LogError($"Failed to update role: {ex.Message}");
				}
			}

			return this.BadRequest("ERROR: Failed to update role");
		}


		[HttpDelete("roles/{id}")]
		public async Task<IActionResult> DeleteRole(int id)
		{
			if (await this.IsUserRoleAllowed())
			{
				try
				{
					var role = await this.repository.GetRoleById(id);

					this.repository.DeleteRole(role);
					if (await this.repository.SaveChangesAsync(null))
					{
						return this.Ok(this.mapper.Map<RoleViewModel>(role));
					}
				}
				catch (Exception ex)
				{
					this.telemetry.TrackException(ex);
					this.logger.LogError($"Failed to delete role: {ex.Message}");
				}
			}

			return this.BadRequest("ERROR: Failed to delete role");
		}

		private async Task<bool> IsUserRoleAllowed()
		{
			var owner = this.User.Identity.Name;
			try
			{
				var role = await this.repository.GetUserRole(owner);

				if (Constants.Role.SITE_VALUES_ALLOWED_ROLES.Contains(role?.Value))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get user role: {ex.Message}");
			}

			return false;
		}
	}
}
