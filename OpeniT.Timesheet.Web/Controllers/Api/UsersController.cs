namespace OpeniT.Timesheet.Web.Controllers.Api
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Dynamic;
	using System.IO;
	using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

	using AutoMapper;
	using Hangfire;
	using Helpers;
	using Microsoft.ApplicationInsights;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

	using Models;

	using ViewModels;

	[Authorize]
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		private readonly ILogger<UsersController> logger;

		private readonly IHostingEnvironment env;

		private readonly IMapper mapper;

		private readonly IDataRepository repository;

		private readonly TelemetryClient telemetry;

		public UsersController(
			ILogger<UsersController> logger,
			IHostingEnvironment env,
			IDataRepository repository,
			IMapper mapper,			
			TelemetryClient telemetry)
		{
			this.logger = logger;
			this.env = env;
			this.repository = repository;
			this.mapper = mapper;			
			this.telemetry = telemetry;
		}

		public async Task<IActionResult> Get()
		{
			try
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				var users = await this.repository.GetAllUsers();
				var results = this.mapper.Map<IEnumerable<UserViewModel>>(users);

				stopWatch.Stop();
				var elapsed = stopWatch.Elapsed.TotalSeconds;

				return this.Ok(new { elapsed, results });
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get all users: {ex.Message}");
			}

			return this.BadRequest("Failed to get all users");
		}

		[HttpGet("all-no-status")]
		public async Task<IActionResult> GetAllNoStatus()
		{
			try
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				var users = await this.repository.GetAllUsers();
				var results = this.mapper.Map<IEnumerable<StatisticUserViewModel>>(users);

				stopWatch.Stop();
				var elapsed = stopWatch.Elapsed.TotalSeconds;

				return this.Ok(new { elapsed, results });
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get all users: {ex.Message}");
			}

			return this.BadRequest("Failed to get all users");
		}

		[HttpGet("active")]
		public async Task<IActionResult> GetActive()
		{
			try
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				var users = await this.repository.GetActiveUsers();
				var results = this.mapper.Map<IEnumerable<UserViewModel>>(users);

				stopWatch.Stop();
				var elapsed = stopWatch.Elapsed.TotalSeconds;

				return this.Ok(new { elapsed, results });
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get all users: {ex.Message}");
			}

			return this.BadRequest("Failed to get all users");
		}

		[HttpGet("active-no-status")]
		public async Task<IActionResult> GetActiveNoStatus()
		{
			try
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				var users = await this.repository.GetActiveUsers();
				var results = this.mapper.Map<IEnumerable<StatisticUserViewModel>>(users);

				stopWatch.Stop();
				var elapsed = stopWatch.Elapsed.TotalSeconds;

				return this.Ok(new { elapsed, results });
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get all users: {ex.Message}");
			}

			return this.BadRequest("Failed to get all users");
		}

		[HttpGet("me")]
		public IActionResult Me()
		{
			var owner = this.User.Identity.Name;
			try
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				var me = this.repository.GetUserByEmail(owner);
				var results = this.mapper.Map<UserViewModel>(me);

				stopWatch.Stop();
				var elapsed = stopWatch.Elapsed.TotalSeconds;

				return this.Ok(new { elapsed, results });
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get my info: {ex.Message}");
			}

			return this.BadRequest("Failed to get my info");
		}

		[HttpGet("byEmail")]
		public IActionResult ByEmail([FromQuery]string email)
		{
			var owner = this.User.Identity.Name;
			try
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				var user = this.repository.GetUserByEmail(email);
				if (user == null)
				{
					return this.BadRequest($"Email [{ email }] does not exist.");
				}
				var results = this.mapper.Map<UserViewModel>(user);

				stopWatch.Stop();
				var elapsed = stopWatch.Elapsed.TotalSeconds;

				return this.Ok(new { elapsed, results });
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get my info: {ex.Message}");
			}

			return this.BadRequest("Failed to get my info");
		}

		[HttpGet("lm")]
		public async Task<IActionResult> GetUsersAsLM()
		{
			var owner = this.User.Identity.Name;
			try
			{
				var users = new List<User>();

				var results = await this.repository.GetUserByLMEmail(owner);
				users.AddRange(results);

				return this.Ok(this.mapper.Map<IEnumerable<UserViewModel>>(users));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get users under LM {owner}: {ex.Message}");
			}

			return this.BadRequest($"Failed to get users under LM {owner}");
		}

        [Authorize(Roles = "Administrator, HR")]
        [HttpGet("manage")]
		public async Task<IActionResult> GetUsers()
		{
			try
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				var users = await this.repository.GetAllUsers();
				var results = this.mapper.Map<IEnumerable<PeopleViewModel>>(users);

				stopWatch.Stop();
				var elapsed = stopWatch.Elapsed.TotalSeconds;

				return this.Ok(new { elapsed, results });
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get all users: {ex.Message}");
			}

			return this.BadRequest("Failed to get all users");
		}

        [Authorize(Roles = "Administrator, HR")]
        [HttpPut("manage/{id}/toggle/import")]
		public async Task<IActionResult> ToggleImport(int id)
		{
			try
			{
				this.logger.LogInformation($"Toggling import for userId: {id}");
				this.repository.ToggleImport(id);

				if (await this.repository.SaveChangesAsync(null)) return this.NoContent();
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to toggle import for userid {id}: {ex.Message}");
			}

			return this.BadRequest("Failed to toggle import");
		}

        [Authorize(Roles = "Administrator, HR")]
        [HttpPut("manage/{email}/toggle/excessvl")]
		public async Task<IActionResult> ToggleExcessVL(string email, [FromQuery] int year)
		{
			try
			{
				var user = this.repository.GetUserByEmail(email);
				this.logger.LogInformation($"Toggling carry over excess VL for user: {email}");
				this.repository.ToggleExcessVL(user, year);

				if (await this.repository.SaveChangesAsync(null)) return this.NoContent();
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to toggle carry over excess VL for userid {email}: {ex.Message}");
			}

			return this.BadRequest("Failed to toggle import");
		}

		[HttpGet("/api/user/{email}")]
		public IActionResult Get(string email)
		{
			try
			{
				var result = this.repository.GetUserByEmail(email);
				if (result != null) return this.Ok(this.mapper.Map<UserViewModel>(result));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError("Failed to get user");
			}

			return this.BadRequest("ERROR: User does not exist");
		}

		[HttpGet("/api/thumbnail/{email}")]
        public IActionResult GetThumbnail(string email)
        {
            try
            {
                var result = this.repository.GetUserWithThumbnailByEmail(email);
				if (result != null && result.Thumbnail != null)
				{
					return File(result.Thumbnail.Content, result.Thumbnail.ContentType);
				}
				else if (result != null)
				{
					using (WebClient webClient = new WebClient())
					{
						byte[] data = webClient.DownloadData("https://dummyimage.com/128/0e1e7d/fff.png&text=" + result.DisplayName.Substring(0, 1));
						return File(data, "image/png");
					}
				}
				else
				{

					using (WebClient webClient = new WebClient())
					{
						byte[] data = webClient.DownloadData("https://dummyimage.com/128/0e1e7d/fff.png&text=" + (email != null ? email.Substring(0, 1) : "%20"));
						return File(data, "image/png");
					}
				}
            }
            catch (Exception ex)
            {
                this.telemetry.TrackException(ex);
                this.logger.LogError("Failed to get user thumbnail");
            }

            return this.BadRequest("ERROR: User does not exist");
        }

		[HttpGet("/api/thumbnail/me")]
		public IActionResult GetMyThumbnail()
		{
			var owner = this.User.Identity.Name;
			try
			{
                
                var result = this.repository.GetUserWithThumbnailByEmail(owner);
				if (result.Thumbnail != null)
				{
					return File(result.Thumbnail.Content, result.Thumbnail.ContentType);
				}
				else
				{
					using (WebClient webClient = new WebClient())
					{
						byte[] data = webClient.DownloadData("https://dummyimage.com/128/0e1e7d/fff.png&text=" + result.DisplayName.Substring(0, 1));
						return File(data, "image/png");
					}
				}
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError("Failed to get user thumbnail");
			}

			return this.BadRequest("ERROR: User does not exist");
		}

        [Authorize(Roles = "Administrator, HR")]
        [HttpPost("/api/user")]
		public async Task<IActionResult> Post([FromBody] PeopleViewModel user)
		{
			try
			{
				if (this.ModelState.IsValid)
				{
					var exists = this.repository.GetUserByEmail(user.Email);
					if (exists != null) return this.BadRequest("Email already exists");

					var newUser = this.mapper.Map<User>(user);
					//newUser.EmploymentCode = await this.repository.GetEmployeeCodeById(user.EmploymentCodeId);
					//newUser.UserLocation = await this.repository.GetUserLocationById(user.UserLocationId);
					//newUser.UserType = await this.repository.GetUserTypeById(user.UserTypeId);
					newUser.CreatedAt = DateTime.Now;

					this.logger.LogInformation($"Creating user {newUser.Email}");
					this.repository.AddUser(newUser);

					if (await this.repository.SaveChangesAsync(null))
						return this.Created("api/user", this.mapper.Map<PeopleViewModel>(newUser));
				}
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to create user: {ex}");
			}

			return this.BadRequest("Failed to create user");
		}

        [Authorize(Roles = "Administrator, HR")]
        [HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] PeopleViewModel vm)
		{
			try
			{
				if (!this.ModelState.IsValid) return this.BadRequest("ERROR: Invalid data");

				var user = await this.repository.GetDetailedUserById(id);
				user.EmploymentCode = await this.repository.GetEmployeeCodeById(vm.EmploymentCodeId);
				user.UserLocation = await this.repository.GetUserLocationById(vm.UserLocationId);
				user.UserType = await this.repository.GetUserTypeById(vm.UserTypeId);
				user.StartDate = vm.StartDate;
                user.RegularizationDate = vm.RegularizationDate;
				user.TerminationDate = vm.TerminationDate;
				user.BirthDate = vm.BirthDate;
				user.UpdatedAt = DateTime.Now;

				this.logger.LogInformation($"Updating azure profile of user {id}");
				await this.UpdateAzureProfile(user);

				this.logger.LogInformation($"Updating user {id}");
				this.repository.UpdateUser(user);

				if (await this.repository.SaveChangesAsync(null)) return this.Ok(Mapper.Map<PeopleViewModel>(user));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to update record: {ex}");
			}

			return this.BadRequest("ERROR: Failed to update user");
		}

		[HttpGet("codes")]
		public async Task<IActionResult> GetEmploymentCodes()
		{
			var results = await this.repository.GetAllEmploymentCodes();
			return this.Ok(results);
		}

		[HttpGet("locations")]
		public async Task<IActionResult> GetUserLocations()
		{
			var results = await this.repository.GetAllUserLocations(false);
			return this.Ok(results);
		}

		[HttpGet("types")]
		public async Task<IActionResult> GetUserTypes()
		{
			var results = await this.repository.GetAllUserTypes();
			return this.Ok(results);
		}

		[HttpGet("{owner}/carryovervl")]
		public async Task<IActionResult> CarryoverVL(string owner)
		{
			try
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				var remainingVLYears = new List<dynamic>();
				var user = this.repository.GetUserByEmail(owner);
				var vm = this.mapper.Map<CarryoverVLViewModel>(user);

				if (string.Equals(user?.UserType?.Status, Constants.UserType.FULLTIME) || string.Equals(user?.UserType?.Status, Constants.UserType.PARTTIME))
				{
					var requests = await this.repository.GetRequests(user, Constants.RequestType.COVL);
					var usedVLs = await this.repository.GetUserVLRecords(owner);

					var currentYear = DateTime.Now.Year;
					var usedVLsMinYear = usedVLs.Any() ? usedVLs.Select(d => (int)d?.year).Min() : currentYear;
					var distinctYears = user?.RegularizationDate != null
						? Enumerable.Range(user.RegularizationDate.Value.Year, currentYear - user.RegularizationDate.Value.Year + 1)
						: Enumerable.Range(usedVLsMinYear, currentYear - usedVLsMinYear + 1);

					foreach (var year in distinctYears)
					{
						var requiredHoursPerDay = new DateTime(year, 12, 1).RequiredHoursToday(user);
						var remainingHours = await this.repository.GetUserVL(user, new DateTime(year, 12, 1));
						var remainingDays = remainingHours / requiredHoursPerDay;
						var data = usedVLs?.FirstOrDefault(d => d.year == year);

						dynamic ex = new ExpandoObject();
						ex.year = year;
						ex.used = data?.hours ?? 0;
						ex.remaining = remainingDays > 10 ? 10 : remainingDays;
						ex.allowed = ex.used + ex.remaining;
						ex.status = requests.FirstOrDefault(r => r.StartDate.Year == year)?.Status;
						remainingVLYears.Add(ex);
					}
				}

				vm.VLYears = remainingVLYears;

				stopWatch.Stop();
				vm.Elapsed = stopWatch.Elapsed.TotalSeconds;

				return this.Ok(vm);
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get monthly summary for {owner}: {ex}");
			}

			return this.BadRequest("ERROR: Failed to get carryover details");
		}

		[HttpDelete("{owner}/carryovervl/{year}")]
		public async Task<IActionResult> CarryoverVL(string owner, int year)
		{
			try
			{
				if (!this.ModelState.IsValid) return this.BadRequest("ERROR: Invalid Data");

				var requerstor = this.repository.GetUserByEmail(owner);
				this.repository.DeleteCarryoverVLRequest(requerstor, year);

				if (await this.repository.SaveChangesAsync(null)) return this.NoContent();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}

			return this.BadRequest("ERROR: Failed to submit request");
		}

		[HttpGet("lmStaffs/{owner?}")]
		public async Task<IActionResult> GetLineManagerStaffs(string owner)
		{
			owner = owner ?? this.User.Identity.Name;

			try
			{
				if (!this.ModelState.IsValid) return this.BadRequest("ERROR: Invalid Data");

				var user = this.repository.GetUserByEmail(owner);
				var staffs = await this.repository.GetUserByLM(user);

				return this.Ok(this.mapper.Map<IEnumerable<PeopleViewModel>>(staffs));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}

			return this.BadRequest("ERROR: Failed to submit request");
		}

		private async Task UpdateAzureProfile(User user)
		{
			var email = user?.Email;
			if (!string.IsNullOrWhiteSpace(email))
			{

			}
		}
	}
}