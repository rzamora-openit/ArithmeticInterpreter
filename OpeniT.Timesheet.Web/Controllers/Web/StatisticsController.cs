namespace OpeniT.Timesheet.Web.Controllers.Web
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize(Policy = "PersistedUser")]
	public class StatisticsController : BaseController
	{
		public IActionResult Index()
		{
			return this.View();
		}
	}
}