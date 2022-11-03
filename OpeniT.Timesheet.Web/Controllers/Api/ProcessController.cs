namespace OpeniT.Timesheet.Web.Controllers.Api
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using AutoMapper;

	using Microsoft.ApplicationInsights;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

	using Models;

	using ViewModels;

	[Authorize]
	[Route("api/[controller]")]
	public class ProcessController : Controller
	{
		private readonly ILogger<ProcessController> logger;

		private readonly IMapper mapper;

		private readonly IDataRepository repository;

		private readonly TelemetryClient telemetry;

		public ProcessController(
			IDataRepository repository,
			ILogger<ProcessController> logger,
			IMapper mapper,
			TelemetryClient telemetry)
		{
			this.repository = repository;
			this.logger = logger;
			this.mapper = mapper;
			this.telemetry = telemetry;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] bool showDeleted = false)
		{
			try
			{
				var results = await this.repository.GetAllProcesses(showDeleted);

				return this.Ok(this.mapper.Map<IEnumerable<ProcessViewModel>>(results));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get all processes: {ex}");
			}

			return this.BadRequest("ERROR: Failed to get all proccess");
		}

        [HttpGet("owned")]
        public async Task<IActionResult> GetOwned([FromQuery] bool showDeleted = false)
        {
            try
            {
                var results = await this.repository.GetOwnedProcesses(showDeleted);

                return this.Ok(this.mapper.Map<IEnumerable<ProcessViewModel>>(results));
            }
            catch (Exception ex)
            {
                this.telemetry.TrackException(ex);
                this.logger.LogError($"Failed to get all processes: {ex}");
            }

            return this.BadRequest("ERROR: Failed to get all proccess");
        }


        [HttpPost]
		public async Task<IActionResult> Post([FromBody] ProcessViewModel vm)
		{
			try
			{
				if (!this.ModelState.IsValid) return this.BadRequest("Invalid Data");

				this.logger.LogInformation($"Adding new Process {vm.Name}");
				var process = this.mapper.Map<Process>(vm);
                process.Owner = this.repository.GetUserByEmail(vm.OwnerEmail);
                process.DeputyOwner = this.repository.GetUserByEmail(vm.DeputyOwnerEmail);
                if (process.Owner == null) process.DeputyOwner = null;

                this.repository.AddProcess(process);
				if (await this.repository.SaveChangesAsync(null)) return this.Ok(this.mapper.Map<ProcessViewModel>(process));                
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Error: Failed to add new Process: {ex}");
			}

			return this.BadRequest("ERROR: Failed to add new Process");
		}

		[HttpPost("{pid}/subprocess")]
		public async Task<IActionResult> Post(int pid, [FromBody] SubProcessViewModel vm)
		{
			try
			{
				if (!this.ModelState.IsValid) return this.BadRequest("Invalid Data");

				this.logger.LogInformation($"Adding new SubProcess \"{vm.Name}\" to {pid}");
				var subProcess = this.mapper.Map<SubProcess>(vm);
                subProcess.Owner = this.repository.GetUserByEmail(vm.OwnerEmail);
                subProcess.DeputyOwner = this.repository.GetUserByEmail(vm.DeputyOwnerEmail);
                if (subProcess.Owner == null) subProcess.DeputyOwner = null;

                this.repository.AddSubProcess(pid, subProcess);
				if (await this.repository.SaveChangesAsync(null)) return this.Ok(this.mapper.Map<SubProcessViewModel>(subProcess));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Error: Failed to update SubProcess: {ex}");
			}

			return this.BadRequest("ERROR: Failed to add new SubProcess");
		}

		[HttpPut("{pid}")]
		public async Task<IActionResult> Put(int pid, [FromBody] ProcessViewModel vm)
		{
			try
			{
				if (!this.ModelState.IsValid) return this.BadRequest("Invalid Data");

				var process = await this.repository.GetProcessById(pid);

				this.logger.LogInformation($"Updating Process {pid}");
				this.mapper.Map(vm, process);
                process.Owner = this.repository.GetUserByEmail(vm.OwnerEmail);
                process.DeputyOwner = this.repository.GetUserByEmail(vm.DeputyOwnerEmail);
                if (process.Owner == null) process.DeputyOwner = null;

                if (await this.repository.SaveChangesAsync(null)) return this.Ok(this.mapper.Map<ProcessViewModel>(process));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Error: Failed to update Process: {ex}");
			}

			return this.BadRequest("ERROR: Failed to update Process");
		}

		[HttpPut("{pid}/subprocess/{spid}")]
		public async Task<IActionResult> Put(int pid, int spid, [FromBody] SubProcessViewModel vm)
		{
			try
			{
				if (!this.ModelState.IsValid) return this.BadRequest("Invalid Data");

				var process = await this.repository.GetProcessById(pid);
				var subProcess = await this.repository.GetSubProcessById(spid);

				this.logger.LogInformation($"Updating SubProcess {spid}");

				vm.Process = process;
				this.mapper.Map(vm, subProcess);
                subProcess.Owner = this.repository.GetUserByEmail(vm.OwnerEmail);
                subProcess.DeputyOwner = this.repository.GetUserByEmail(vm.DeputyOwnerEmail);
                if (subProcess.Owner == null) subProcess.DeputyOwner = null;

                if (await this.repository.SaveChangesAsync(null)) return this.Ok(this.mapper.Map<SubProcessViewModel>(subProcess));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Error: Failed to update SubProcess: {ex}");
			}

			return this.BadRequest("ERROR: Failed to update SubProcess");
		}

		[HttpGet("search/{term}")]
		public async Task<IActionResult> Get(string term)
		{
			try
			{
				var results = await this.repository.GetProcessByTerm(term);

				return this.Ok(this.mapper.Map<IEnumerable<ProcessViewModel>>(results));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get Process(es): {ex}");
			}

			return this.BadRequest($"ERROR: Failed to get Process(es) with: {term}");
		}

		[HttpGet("uris")]
		public IActionResult GetProcessUris()
		{
			try
			{
				var results = this.repository.GetProcessUris().Result;
				return this.Ok(results);
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to get Process URIs: {ex}");
			}

			return this.BadRequest("ERROR: Failed to get Process URIs");
		}

		////////////////////
		/// Depraciated ///
		////////////////////
		[HttpPost("taskgroup")]
		public async Task<IActionResult> AddNewTaskGroup([FromBody] TaskGroupViewModel vm)
		{
			var owner = this.User.Identity.Name;
			try
			{
				if (!this.ModelState.IsValid) return this.BadRequest("Invalid Data");

				var taskGroup = await this.repository.GetTaskGroupByName(vm.ProcessId, vm.Name);
				if (taskGroup != null) return this.BadRequest("Task Group already exist");

				var newTaskGroup = this.mapper.Map<TaskGroup>(vm);
				newTaskGroup.CreatedBy = owner;
				newTaskGroup.CreatedAt = DateTime.Now;

				this.repository.AddTaskGroup(vm.ProcessId, newTaskGroup);
				if (await this.repository.SaveChangesAsync(owner))
					return this.Created("api/process/taskgroup", this.mapper.Map<TaskGroup>(newTaskGroup));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed add new Task Group: {ex}");
			}

			return this.BadRequest("ERROR: Failed to add new Task Group");
		}

		[HttpPost("{pid}/taskgroup")]
		public async Task<IActionResult> AddNewTaskGroup(int pid, [FromBody] TaskGroupViewModel vm)
		{
			var owner = this.User.Identity.Name;
			try
			{
				if (!this.ModelState.IsValid) return this.BadRequest("Invalid Data");

				var taskGroup = await this.repository.GetTaskGroupByName(pid, vm.Name);
				if (taskGroup != null) return this.BadRequest("Task Group already exist");

				var newTaskGroup = this.mapper.Map<TaskGroup>(vm);
				newTaskGroup.CreatedBy = owner;
				newTaskGroup.CreatedAt = DateTime.Now;

				this.repository.AddTaskGroup(pid, newTaskGroup);
				if (await this.repository.SaveChangesAsync(owner))
					return this.Created("api/process/taskgroup", this.mapper.Map<TaskGroup>(newTaskGroup));
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Failed to new Task Group: {ex}");
			}

			return this.BadRequest("ERROR: Failed to add new Task Group");
		}

		[HttpPut("{pid}/taskgroup/{tgid}")]
		public async Task<IActionResult> UpdateTaskGroup(int pid, int tgid, [FromBody] TaskGroupViewModel vm)
		{
			try
			{
				if (!this.ModelState.IsValid) return this.BadRequest("Invalid Data");

				var taskGroup = await this.repository.GetTaskGroupById(tgid);
				this.logger.LogInformation($"Updating Task Group {tgid}");

				vm.ProcessId = pid;
				this.mapper.Map(vm, taskGroup);

				if (await this.repository.SaveChangesAsync(null)) return this.Ok(taskGroup);
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Error: Failed to update Task Group: {ex}");
			}

			return this.BadRequest("ERROR: Failed to update Task Group");
		}

		[HttpDelete("{pid}")]
		public async Task<IActionResult> ToggleDeleteProccess(int pid)
		{
			try
			{
				var process = await this.repository.GetProcessById(pid);
				if (process == null)
				{
					this.logger.LogError($"Error: Process {pid} not found");
					return this.NotFound("Process not found");
				}
				this.logger.LogInformation($"Toggling deletion of process {pid}");

				this.repository.ToggleDeleteProcess(process);
				if (await this.repository.SaveChangesAsync(null)) return this.NoContent();
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Error: Failed to toggle deletion of Process: {ex}");
			}

			return this.BadRequest("ERROR: Failed to toggle deletion of Proccess");
		}

		[HttpDelete("{pid}/subprocess/{spid}")]
		public async Task<IActionResult> ToggleDeleteSubProccess(int spid)
		{
			try
			{
				var subProcess = await this.repository.GetSubProcessById(spid);
				if (subProcess == null)
				{
					this.logger.LogError($"Error: SubProcess {spid} not found");
					return this.NotFound("SubProcess not found");
				}
				this.logger.LogInformation($"Toggling deletion of SubProcess {spid}");

				this.repository.ToggleDeleteSubProccess(subProcess);
				if (await this.repository.SaveChangesAsync(null)) return this.NoContent();
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Error: Failed to toggle deletion of SubProcess: {ex}");
			}

			return this.BadRequest("ERROR: Failed to toggle deletion of SubProcess");
		}

		[HttpDelete("{pid}/taskgroup/{tgid}")]
		public async Task<IActionResult> DeleteTaskGroup(int tgid)
		{
			try
			{
				if (!this.ModelState.IsValid) return this.BadRequest("Invalid Data");

				var taskGroup = await this.repository.GetTaskGroupById(tgid);
				if (taskGroup == null)
				{
					this.logger.LogError($"Error: SubProcess {tgid} not found");
					return this.NotFound("Task Group not found");
				}
				this.logger.LogInformation($"Deleting Task Group {tgid}");

				this.repository.DeleteTaskGroup(taskGroup);
				if (await this.repository.SaveChangesAsync(null)) return this.NoContent();
			}
			catch (Exception ex)
			{
				this.telemetry.TrackException(ex);
				this.logger.LogError($"Error: Failed to delete Task Group: {ex}");
			}

			return this.BadRequest("ERROR: Failed to delete Task Group");
		}
	}
}